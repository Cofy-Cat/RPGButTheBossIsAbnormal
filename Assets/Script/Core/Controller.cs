using UnityEngine;

namespace cfEngine.Core {
    public partial class AnimationName {
        //Make sure your animation name follow this
        public const string Idle = nameof(Idle);
        public const string Walk = nameof(Walk);
        public const string Dash = nameof(Dash);
        public const string Attack = nameof(Attack);
        public const string AttackEnd = nameof(AttackEnd);
        public const string Death = nameof(Death);

        public static string GetDirectional(string animationName, float horizontalDirection) {
            if (horizontalDirection >= 0) {
                return $"{animationName}Right";
            }
            else {
                return $"{animationName}Left";
            }
        }
    }

    public abstract class Controller : MonoBehaviour {
        [SerializeField] protected Collider2DComponent _hitbox;
        [SerializeField] protected Rigidbody2D _rb;
        [SerializeField] protected SpriteAnimation _anim;
        [SerializeField] protected CharacterStateMachine _sm;
        [SerializeField] protected Transform _mainCharacter;
        private Animation characterAnimation;
    
        [Header("Stat")]
        public Vector2 moveSpeed = Vector2.one;

        public Vector2 dashSpeed = Vector2.one;

        private float _lastFaceDirection = 0f;
        public float LastFaceDirection => _lastFaceDirection;

        private Vector2 _lastMoveDirection = Vector2.zero;
        public Vector2 LastMoveDirection => _lastMoveDirection;
    
        private bool _isDead = false;
        public bool isDead => _isDead;
    
        #region getter

        public SpriteAnimation Animation => _anim;
        public Rigidbody2D Rigidbody => _rb;
        public Transform MainCharacter => _mainCharacter;
    
        #endregion
    
        protected virtual void Awake() {
            _sm.Controller = this;
            characterAnimation = _hitbox.GetComponentInChildren<Animation>();
        }
    
        protected virtual void OnEnable() {
            _hitbox.triggerEnter += OnHitboxTriggerEnter;
            _hitbox.triggerExit += OnHitboxTriggerExit;
        }

        protected virtual void OnDisable() {
            _hitbox.triggerEnter -= OnHitboxTriggerEnter;
            _hitbox.triggerExit -= OnHitboxTriggerExit;
        }
    
        protected virtual void OnHitboxTriggerEnter(Collider2D other) { }
    
        protected virtual void OnHitboxTriggerExit(Collider2D other) { }
    
        public void SetVelocity(Vector2 velocity) {
            _rb.linearVelocity = velocity;

            if (velocity != Vector2.zero) {
                _lastMoveDirection = velocity.normalized;
            }

            if (!Mathf.Approximately(velocity.x, 0f)) {
                _lastFaceDirection = Mathf.Sign(velocity.x) * velocity.x / velocity.x;
            }
        
            foreach (AnimationState state in characterAnimation) {
                state.speed = velocity.magnitude / moveSpeed.magnitude;
            }
        }
    }
}
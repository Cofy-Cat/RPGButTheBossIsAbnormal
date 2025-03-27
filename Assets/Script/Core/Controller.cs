using UnityEngine;

namespace RPG.Core {
    public abstract class Controller : MonoBehaviour {
        [SerializeField] protected Collider2D _hitbox;
        [SerializeField] protected Rigidbody2D _rb;
        [SerializeField] protected CharacterStateMachine _sm;
        [SerializeField] protected Transform _mainCharacter;
    
        [Header("Stat")]
        public Vector2 moveSpeed = Vector2.one;
        public Vector2 dashSpeed = Vector2.one;
        
        #region getter
        
        public Rigidbody2D Rigidbody => _rb;
        public Transform MainCharacter => _mainCharacter;
    
        #endregion
        
        protected Vector2 _lastMoveInput = Vector2.zero;
        public Vector2 LastMoveInput => _lastMoveInput;
    
        protected virtual void Awake() {
            _sm.Controller = this;
        }
        
        public void SetVelocity(Vector2 velocity) {
            _rb.linearVelocity = velocity;
        }
    }
}
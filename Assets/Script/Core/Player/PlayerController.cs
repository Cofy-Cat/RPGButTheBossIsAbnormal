using cfEngine.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace cfEngine.Core.Player {
    public class PlayerController : Controller {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private float maxDashClickGap = 0.3f;
    
        private Vector2 _lastMoveInput = Vector2.zero;
        public Vector2 LastMoveInput => _lastMoveInput;
    
        private void Start() {
            _sm.TryGoToState(CharacterStateId.Idle);
        }
        
        protected override void OnEnable() {
            base.OnEnable();
            _input.onActionTriggered += OnActionTriggered;
        }

        protected override void OnDisable() {
            base.OnDisable();
            _input.onActionTriggered -= OnActionTriggered;
        }
    
        private void OnActionTriggered(InputAction.CallbackContext context) {
            switch (context.action.name) {
                case "Move":
                    OnMove(context);
                    break;
                case "Attack":
                    OnAttack(context);
                    break;
                case "Dash":
                    OnDash(context);
                    break;
                case "Jump":
                    OnJump(context);
                    break;
            }
        }
    
        private void OnMove(InputAction.CallbackContext context) {
            _lastMoveInput = context.ReadValue<Vector2>();
        
            if (_lastMoveInput != Vector2.zero) {
                _sm.TryGoToState(CharacterStateId.Move, new MoveState.Param() {
                    sm = (PlayerStateMachine)_sm,
                    direction = _lastMoveInput
                });
            }
            else {
                _sm.TryGoToState(CharacterStateId.Idle, new IdleState.Param() {
                    sm = (PlayerStateMachine)_sm,
                });
            }
        }
    
        private void OnAttack(InputAction.CallbackContext context) {
            if (context.performed) {
                _sm.TryGoToState(CharacterStateId.Attack);
            }
        }
    
        private void OnDash(InputAction.CallbackContext context) {
            if (context.performed) {
                _sm.TryGoToState(CharacterStateId.Dash);
            }
        }
    
        private void OnJump(InputAction.CallbackContext context) {
            if (context.performed) {
                _sm.TryGoToState(CharacterStateId.Jump);
            }
        }
    }
}
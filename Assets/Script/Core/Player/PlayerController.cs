using RPG.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Core.Player {
    public class PlayerController : Controller {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private float maxDashClickGap = 0.3f;
        
        private void Start() {
            _sm.TryGoToState(CharacterStateId.Idle, new IdleState.Param() {
                sm = (PlayerStateMachine)_sm
            });
        }
        
        protected void OnEnable() {
            _input.onActionTriggered += OnActionTriggered;
        }

        protected void OnDisable() {
            _input.onActionTriggered -= OnActionTriggered;
        }
    
        private void OnActionTriggered(InputAction.CallbackContext context) {
            switch (context.action.name) {
                case "Move":
                    OnMove(context);
                    break;
                case "Jump":
                    OnJump(context);
                    break;
            }
        }
    
        // Change the following to changing input instead of directly changing states,
        // and have the states check for input change to determine which states to go
        private void OnMove(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Canceled:
                    _lastMoveInput = new Vector2(0, _lastMoveInput.y);
                    break;
                default:
                    _lastMoveInput = new Vector2(context.ReadValue<Vector2>().x, _lastMoveInput.y);
                    break;
            }
        }

        private void OnJump(InputAction.CallbackContext context) {
            switch (context.phase) {
                case InputActionPhase.Canceled:
                    _lastMoveInput = new Vector2(_lastMoveInput.x, 0);
                    break;
                default:
                    _lastMoveInput = new Vector2(_lastMoveInput.x, 1);
                    break;
            }
        }
    }
}
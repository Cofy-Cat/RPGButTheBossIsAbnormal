using RPG.Core.State;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Core.Player {
    public class PlayerController : Controller {
        [SerializeField] private PlayerInput _input;
        [SerializeField] private float maxDashClickGap = 0.3f;
    
        private Vector2 _lastMoveInput = Vector2.zero;
        public Vector2 LastMoveInput => _lastMoveInput;
    
        private void Start() {
            _sm.TryGoToState(CharacterStateId.Idle);
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
            }
        }
    
        private void OnMove(InputAction.CallbackContext context) {
            _lastMoveInput = context.ReadValue<Vector2>();
            Debug.Log("OnMove " +  context.phase);
            switch (context.phase) {
                case InputActionPhase.Canceled:
                    _sm.TryGoToState(CharacterStateId.Idle);
                    break;
                default:
                    _sm.TryGoToState(CharacterStateId.Move, new MoveState.Param() {
                        sm = (PlayerStateMachine)_sm,
                        direction = _lastMoveInput
                    });
                    break;
            }
        }
    }
}
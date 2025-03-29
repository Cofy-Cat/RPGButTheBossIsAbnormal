using System.Collections.Generic;
using cfEngine.Util;
using RPG.Core.Player;
using UnityEngine;

namespace RPG.Core.State {
    public class MoveState : CharacterState {
        public class Param : StateParam {
            public PlayerStateMachine sm;
        }
        public override CharacterStateId Id => CharacterStateId.Move;
        private PlayerStateMachine _stateMachine;
        public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move, CharacterStateId.Jump }; // Dash
        protected override void StartContext(StateParam param) {
            if (param is Param p) {
                Debug.Log("Start MoveState");
                _stateMachine = p.sm;
                _stateMachine.Controller.Rigidbody.linearDamping = 0;
            }
        }

        public override void _Update() {
            base._Update();
            
            if (_stateMachine.Controller.LastMoveInput.y > 0) {
                _stateMachine.TryGoToState(CharacterStateId.Jump, new JumpState.Param { sm = _stateMachine });
            }
            else if (_stateMachine.Controller.LastMoveInput.Equals(Vector2.zero)) { 
                _stateMachine.TryGoToState(CharacterStateId.Idle, new IdleState.Param { sm = _stateMachine });
            }
            else {
                _stateMachine.Controller.SetVelocity(_stateMachine.Controller.LastMoveInput * _stateMachine.Controller.moveSpeed);
            }
        }
    }
}

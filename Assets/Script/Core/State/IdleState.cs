using System.Collections.Generic;
using cfEngine.Util;
using RPG.Core.Player;
using UnityEngine;

namespace RPG.Core.State {
    public class IdleState : CharacterState {
        public class Param : StateParam {
            public PlayerStateMachine sm;
        }
        public override CharacterStateId Id => CharacterStateId.Idle;
        private PlayerStateMachine _stateMachine;
        public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Move, CharacterStateId.Jump, CharacterStateId.Idle };
        
        protected override void StartContext(StateParam param) {
            Debug.Log("Start IdleState");
            if (param is Param p) {
                _stateMachine = p.sm;
                //_stateMachine.Controller.Rigidbody.linearDamping = 1000000;
                _stateMachine.Controller.Rigidbody.linearVelocity = Vector2.zero;
            }
        }

        public override void _Update() {
            base._Update();
            
            if (_stateMachine.Controller.LastMoveInput.x > 0 ||  _stateMachine.Controller.LastMoveInput.x < 0) {
                _stateMachine.Controller.Rigidbody.linearDamping = 0;
                _stateMachine.TryGoToState(CharacterStateId.Move, new MoveState.Param { sm = _stateMachine });
            }
            else if (_stateMachine.Controller.LastMoveInput.y > 0) {
                _stateMachine.Controller.Rigidbody.linearDamping = 0;
                _stateMachine.TryGoToState(CharacterStateId.Jump, new JumpState.Param { sm = _stateMachine });
            }
            else {
                _stateMachine.Controller.Rigidbody.linearVelocity = Vector2.zero;
            }
        }
    }
}
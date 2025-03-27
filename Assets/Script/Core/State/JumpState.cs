using System;
using System.Collections.Generic;
using cfEngine.Util;
using RPG.Core.Player;
using UnityEngine;
using UnityEngine.InputSystem;

namespace RPG.Core.State {
    public class JumpState : CharacterState {
        public class Param : StateParam {
            public PlayerStateMachine sm;
        }
        public override CharacterStateId Id => CharacterStateId.Jump;
        private PlayerStateMachine _stateMachine;
        public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Move, CharacterStateId.Idle };
        
        protected override void StartContext(StateParam param) {
            if (param is Param p) {
                _stateMachine = p.sm;
                _stateMachine.Controller.Rigidbody.linearDamping = 0;
                _stateMachine.Controller.Rigidbody.AddRelativeForceY(_stateMachine.Controller.moveSpeed.y);
            }
        }

        private void OnCollisionEnter2D(Collision2D other) {
            if (other.collider.tag.Contains("Floor")) {
                if (_stateMachine.Controller.LastMoveInput.x == 0) {
                    _stateMachine.TryGoToState(CharacterStateId.Idle, new IdleState.Param { sm = _stateMachine });
                }
                else {
                    _stateMachine.TryGoToState(CharacterStateId.Move, new MoveState.Param { sm = _stateMachine });
                }
            }
        }
    }
}
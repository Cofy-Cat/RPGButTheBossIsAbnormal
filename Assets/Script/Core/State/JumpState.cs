using System.Collections.Generic;
using cfEngine.Util;
using RPG.Core.Player;
using UnityEngine;

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
            }
        }
        
        public override void _Update() {
            base._Update();
            _stateMachine.Controller.Rigidbody.AddRelativeForceY(_stateMachine.Controller.moveSpeed.y);
        }
    }
}
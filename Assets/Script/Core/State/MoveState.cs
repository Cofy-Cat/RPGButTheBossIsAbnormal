using System.Collections.Generic;
using cfEngine.Util;
using RPG.Core.Player;
using UnityEngine;

namespace RPG.Core.State {
    public class MoveState : CharacterState {
        public class Param : StateParam {
            public PlayerStateMachine sm;
            public Vector2 direction;
        }
        public override CharacterStateId Id => CharacterStateId.Move;
        private PlayerStateMachine _stateMachine;
        Vector2 direction = Vector2.zero;
        public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move };
        protected override void StartContext(StateParam param) {
            if (param is Param p) {
                direction = p.direction;
                _stateMachine = p.sm;
                _stateMachine.Controller.Rigidbody.linearDamping = 0;
            }
        }

        public override void _Update() {
            base._Update();
            _stateMachine.Controller.SetVelocity(direction * _stateMachine.Controller.moveSpeed);
        }
    }
}

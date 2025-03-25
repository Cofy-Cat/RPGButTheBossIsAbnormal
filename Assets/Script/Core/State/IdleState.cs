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
        public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Move, CharacterStateId.Jump };
        
        protected override void StartContext(StateParam param) {
            if (param is Param p) {
                _stateMachine = p.sm;
                _stateMachine.Controller.Rigidbody.linearDamping = 1000000;
                _stateMachine.Controller.Rigidbody.linearVelocity = Vector2.zero;
            }
        }
    }
}
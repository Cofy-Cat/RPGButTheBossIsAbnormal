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
        public override HashSet<CharacterStateId> Whitelist { get; } = new() { CharacterStateId.Idle, CharacterStateId.Move };
        
        protected override void StartContext(StateParam param) {
            Vector2 direction = Vector2.zero;
        
            if (param is Param p) {
                direction = p.direction;
                p.sm.Controller.SetVelocity(direction * p.sm.Controller.moveSpeed);
            }
        }
    }
}

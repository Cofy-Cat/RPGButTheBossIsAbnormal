using cfEngine.Core.Player;
using cfEngine.Util;
using UnityEngine;

namespace cfEngine.Core.State {
    public class MoveState : CharacterState {
        public class Param : StateParam {
            public PlayerStateMachine sm;
            public Vector2 direction;
        }
        public override CharacterStateId Id => CharacterStateId.Move;
        private PlayerStateMachine _stateMachine;
    
        protected override void StartContext(StateParam param) {
            float faceDirection = 0f;
            Vector2 direction = Vector2.zero;
        
            if (param is Param p) {
                faceDirection = p.direction.x;
                direction = p.direction;
            
                string animationName= AnimationName.GetDirectional(AnimationName.Walk, faceDirection);
            
                p.sm.Controller.SetVelocity(direction * p.sm.Controller.moveSpeed);
                p.sm.Controller.Animation.Play(animationName, true);
            }
        }
    }
}

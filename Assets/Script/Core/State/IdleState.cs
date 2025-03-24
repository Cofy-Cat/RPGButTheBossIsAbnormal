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
        protected override void StartContext(StateParam param) {
            if (param is Param p) {
                _stateMachine = p.sm;
                var animationName = AnimationName.GetDirectional(AnimationName.Idle, _stateMachine.Controller.LastFaceDirection);
            
                _stateMachine.Controller.Animation.Play(animationName, true);
                _stateMachine.Controller.Rigidbody.linearVelocity = Vector2.zero;
            }
        }
    }
}
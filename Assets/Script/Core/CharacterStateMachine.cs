using cfEngine.Core.State;
using cfEngine.Util;
using cfUnityEngine.Util;
using UnityEngine;

namespace cfEngine.Core {
    public enum CharacterStateId { 
        Idle,
        Move,
        Dash,
        Attack,
        Jump,
        Dead 
    }

    public abstract class CharacterState : MonoState<CharacterStateId, CharacterState, CharacterStateMachine> { }
    public class CharacterStateMachine : MonoStateMachine<CharacterStateId, CharacterState, CharacterStateMachine> {
        public Controller Controller;
        [SerializeField] private IdleState idleState;
        [SerializeField] private MoveState moveState;
        
        // In MonoBehaviour, you can't use constructor, use Awake() instead
        // Also, in MonoStateMachine, state registration is automatically done in Awake(), no need to call RegisterState() manually
        public CharacterStateMachine() : base() {
            RegisterState(idleState);
            RegisterState(moveState);
            // RegisterState(new DashState());
            // RegisterState(new AttackState());
            // RegisterState(new JumpState());
            // RegisterState(new DeadState());
        }
    }
}
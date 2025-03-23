using cfUnityEngine.Util;

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
    }
}
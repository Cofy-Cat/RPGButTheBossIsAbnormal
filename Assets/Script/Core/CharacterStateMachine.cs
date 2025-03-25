using cfUnityEngine.Util;

namespace RPG.Core {
    public enum CharacterStateId { 
        Idle,
        Move,
        // Dash,
        // Attack,
        Jump,
        // Dead 
    }

    public abstract class CharacterState : MonoState<CharacterStateId, CharacterState, CharacterStateMachine> { }
    public class CharacterStateMachine : MonoStateMachine<CharacterStateId, CharacterState, CharacterStateMachine> {
        public Controller Controller;
        // In MonoBehaviour, you can't use constructor, use Awake() instead
        // Also, in MonoStateMachine, state registration is automatically done in Awake(), no need to call RegisterState() manually
    }
}
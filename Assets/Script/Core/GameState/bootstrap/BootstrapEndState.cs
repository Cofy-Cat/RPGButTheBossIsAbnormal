using cfEngine.Util;
using cfUnityEngine.UI;

namespace cfUnityEngine.GameState.Bootstrap
{
    public class BootstrapEndState: GameState
    {
        public override GameStateId Id => GameStateId.BootstrapEnd;
        protected internal override void StartContext(StateParam stateParam)
        {
            UIRoot.GetPanel<LoadingUI>().HidePanel();
        }
    }
}
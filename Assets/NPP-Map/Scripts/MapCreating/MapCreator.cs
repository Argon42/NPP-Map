using UnityEngine;

namespace NPPMap.MapCreating
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private CreatingRoomState creatingRoomState;
        [SerializeField] private MenuState menuState;

        private StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = new StateMachine(menuState);
        }

        public void OpenState(IState state)
        {
            _stateMachine.ChangeState(state);
        }

        public void OpenState(GameObject objectWithState)
        {
            if (objectWithState.TryGetComponent(out IState state))
                OpenState(state);
        }
    }
}
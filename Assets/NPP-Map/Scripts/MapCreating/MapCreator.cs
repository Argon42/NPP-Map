using UnityEngine;


namespace NPPMap.MapCreating
{
    public class MapCreator : MonoBehaviour
    {
        [SerializeField] private CreatingRoomState creatingRoomState;
        [SerializeField] private ViewState viewState;

        private StateMachine _stateMachine;

        private void Start()
        {
            _stateMachine = new StateMachine(viewState);
        }

        public void CreateRoom()
        {
            _stateMachine.ChangeState(creatingRoomState);
        }

        public void CancelCreateRoom()
        {
            _stateMachine.ChangeState(viewState);
        }
    }
}
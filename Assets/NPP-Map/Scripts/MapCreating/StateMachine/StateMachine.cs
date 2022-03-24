namespace NPPMap.MapCreating
{
    public class StateMachine
    {
        private IState _currentState;

        public StateMachine(IState startState)
        {
            _currentState = startState;
            startState.Enable();
        }

        public void ChangeState(IState newState)
        {
            _currentState.Disable();
            _currentState = newState;
            _currentState.Enable();
        }
    }
}
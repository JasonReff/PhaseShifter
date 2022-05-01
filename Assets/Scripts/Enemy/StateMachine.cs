using UnityEngine;

public abstract class StateMachine<T> : MonoBehaviour where T : State
{
    protected T _currentState;
    protected T _initialState;

    protected virtual void Start()
    {
        ChangeState(_initialState);
    }

    private void Update()
    {
        if (_currentState == null)
            return;
        _currentState.UpdateState();
    }

    public void ChangeState(T newState)
    {
        if (_currentState == newState)
            return;
        if (_currentState != null)
        {
            _currentState.EndState();
        }
        _currentState = newState;
        _currentState.PrepareState();
    }
}

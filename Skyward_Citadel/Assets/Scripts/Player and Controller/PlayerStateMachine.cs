using UnityEngine;

public class PlayerStateMachine
{
    public PlayerState CurrentPlayerState {  get; set; }

    public void Initialize(PlayerState StartingState)
    {
        CurrentPlayerState = StartingState;
        CurrentPlayerState.EnterState();

    }

    public void ChangeState(PlayerState newState)
    {

        CurrentPlayerState.ExitState();
        CurrentPlayerState = newState;
        CurrentPlayerState.EnterState();
    }
}

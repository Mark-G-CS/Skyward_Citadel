using UnityEngine;

public class PlayerResolverState : PlayerState
{
    public PlayerResolverState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered 'ResolverState'");
    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (player.controller.grounded == true)
        {
            player.StateMachine.ChangeState(player.groundState);
        }
        else
        {
            player.StateMachine.ChangeState(player.inAirState);
        }
        }
}

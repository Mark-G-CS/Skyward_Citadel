using UnityEngine;

public class PlayerGroundState : PlayerState
{
    
    public PlayerGroundState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {

        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered 'groundState'");

        player.animator.SetBool("Grounded", true);

    }

    public override void ExitState()
    {
        base.ExitState();
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();

        if (player.controller.grounded == false)
        {
            player.StateMachine.ChangeState(player.inAirState);
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.StateMachine.ChangeState(player.DashState);
        }
    }
}
    
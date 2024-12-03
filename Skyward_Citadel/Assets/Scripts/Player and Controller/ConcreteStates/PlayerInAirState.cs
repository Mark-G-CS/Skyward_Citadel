using UnityEngine;

public class PlayerInAirState : PlayerState
{
    public PlayerInAirState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered 'inAirState'");
        player.animator.SetBool("Grounded", false);  //"IM FREEEE FALLLIN");
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
        if (Input.GetKeyDown(KeyCode.E))
        {
            player.StateMachine.ChangeState(player.DashState);
        }
    }
    }

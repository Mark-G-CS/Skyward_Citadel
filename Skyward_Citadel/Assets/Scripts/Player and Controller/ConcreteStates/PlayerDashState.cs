using UnityEngine;

public class PlayerDashState : PlayerState
{

    float timer = 0;
    public PlayerDashState(Player player, PlayerStateMachine stateMachine) : base(player, stateMachine)
    {
    }

    public override void AnimationTriggerEvent(Player.AnimationTriggerType triggerType)
    {
        base.AnimationTriggerEvent(triggerType);
    }

    public override void EnterState()
    {
        base.EnterState();
        Debug.Log("Entered 'dashState'");
        
        

    }

    public override void ExitState()
    {
        base.ExitState();
        timer = 0;
    }

    public override void FrameUpdate()
    {
        base.FrameUpdate();
        timer += Time.deltaTime;
        
        if (timer> 0.5)
        {
            player.StateMachine.ChangeState(player.resolverState);
        }
    }
}

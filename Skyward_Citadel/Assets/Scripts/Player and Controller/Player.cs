using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{

    public  PlayerController controller;
    [field: SerializeField] public float MaxHealth { get; set; } = 100f;
    public float CurrentHealth { get; set; }

    #region State Machine Var
    public PlayerStateMachine StateMachine { get; set; }
    public PlayerGroundState groundState { get; set; }
    public PlayerInAirState inAirState { get; set; }
    public PlayerDashState DashState { get; set; }  
    public PlayerInteractState interactState { get; set; }
    public PlayerResolverState resolverState { get; set; }
    public float DealDamage { get; set; }
    public float DamageResist { get; set; }

    #endregion

    private void Awake()
    {
        StateMachine = new PlayerStateMachine();
        groundState = new PlayerGroundState(this, StateMachine);
        inAirState = new PlayerInAirState(this,StateMachine);
        DashState = new PlayerDashState(this,StateMachine);   
        interactState = new PlayerInteractState(this,StateMachine);
        resolverState = new PlayerResolverState(this,StateMachine);
        controller = GetComponentInParent<PlayerController>();

    }

    public enum AnimationTriggerType
    {
        PlayerDamaged,
        PlayerInAir

    }
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentPlayerState.AnimationTriggerEvent(triggerType);

    }


    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Death();
        }
        
    }

    public void Death()
    {
        
             Time.timeScale = 0.1f;
        
       
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = MaxHealth;
        StateMachine.Initialize(resolverState);
        
    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
        
            

        
    }

}
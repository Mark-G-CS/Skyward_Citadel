using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class Player : MonoBehaviour, IDamageable
{
    SpriteRenderer based;
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
        PlayerInAir,
        PlayerInDash,
        PlayerMovingGrounded,
        PlayerIdleGrounded

    }
    private void AnimationTriggerEvent(AnimationTriggerType triggerType)
    {
        StateMachine.CurrentPlayerState.AnimationTriggerEvent(triggerType);

    }

    // NOW IN HEALTH.CS 
    //>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    public void Damage(float damageAmount)
    {
        CurrentHealth -= damageAmount;
        if (CurrentHealth <= 0)
        {
            Death();
        }
        GetComponent<SpriteRenderer>().material.color = Color.red;
        Invoke("onPlayerHit", 0.3f);
        

    }

    public void onPlayerHit()
    {
        
        GetComponent<SpriteRenderer>().material.color = Color.white;
        
    }

    public void Death()
    {
        
             Time.timeScale = 0.1f;
        
       
    }
   // >>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>>
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        CurrentHealth = MaxHealth;
        StateMachine.Initialize(resolverState);
        based = GetComponent<SpriteRenderer>();

    }

    // Update is called once per frame
    void Update()
    {
        StateMachine.CurrentPlayerState.FrameUpdate();
        
            

        
    }

}

using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable // Damage stuffs need to be moved to Health class, consider making a copy?
{
    Health health { get; set; }
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public float DamageResist { get; set; }

    protected virtual void Awake()      // only EnemyBase's subclasses can access & allows override
    {
        health = GetComponent<Health>();
        health.MaxHealth = 100f;           // default max health
        health.CurrentHealth = health.MaxHealth;
        DamageResist = 0f;          // default damage resist
    }
    
    public virtual void Damage(float damageAmount)      // overrideable //Currently being Overwritten by Health Class
    {
        float actualDamage = damageAmount * (1 - DamageResist);
        CurrentHealth -= actualDamage;

        Debug.Log($"{gameObject.name} took {actualDamage} damage. Current Health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            
            Invoke("Death", 1f);
        }
    }

    public virtual void Death()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Remove the object from the game
        
    }
}
using UnityEngine;

public class EnemyBase : MonoBehaviour, IDamageable
{
    public float MaxHealth { get; set; }
    public float CurrentHealth { get; set; }
    public float DamageResist { get; set; }

    protected virtual void Awake()      // only EnemyBase's subclasses can access & allows override
    {
        MaxHealth = 100f;           // default max health
        CurrentHealth = MaxHealth;
        DamageResist = 0f;          // default damage resist
    }

    public virtual void Damage(float damageAmount)      // overrideable
    {
        float actualDamage = damageAmount * (1 - DamageResist);
        CurrentHealth -= actualDamage;

        Debug.Log($"{gameObject.name} took {actualDamage} damage. Current Health: {CurrentHealth}");

        if (CurrentHealth <= 0)
        {
            Death();
        }
    }

    public virtual void Death()
    {
        Debug.Log($"{gameObject.name} has died!");
        Destroy(gameObject); // Remove the object from the game
    }
}
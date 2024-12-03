using UnityEngine;

public class MeleeEnemy : EnemyBase, IDamageable
{
    
    protected override void Awake()
    {
        base.Awake();
        MaxHealth = 150f;           // default higher HP for melee enemy
        CurrentHealth = MaxHealth;
        DamageResist = 0.1f;        // slight resistance to damage
    }

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        // Additional melee-specific behavior
        Debug.Log($"{gameObject.name} is hurted >:(");
    }

    public override void Death()
    {
        base.Death();
        // Custom death logic, such as spawning loot or effects
        Debug.Log($"{gameObject.name} faints like a b-");
    }
}

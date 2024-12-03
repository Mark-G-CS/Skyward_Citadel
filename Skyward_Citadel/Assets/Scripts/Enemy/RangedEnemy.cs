using UnityEngine;

public class RangedEnemy : EnemyBase, IDamageable
{
    protected override void Awake()
    {
        base.Awake();
        MaxHealth = 80f;                // Weaker ranged enemy
        CurrentHealth = MaxHealth;
        DamageResist = 0.2f;            // Better resistance to damage
    }

    public override void Damage(float damageAmount)
    {
        base.Damage(damageAmount);
        // Additional ranged-specific behavior
        Debug.Log($"{gameObject.name} is hit! >:(");
    }

    public override void Death()
    {
        base.Death();
        // Custom death logic, like spawning projectiles
        Debug.Log($"{gameObject.name} has fallen and can't get up \\(-_-)/");
    }
}

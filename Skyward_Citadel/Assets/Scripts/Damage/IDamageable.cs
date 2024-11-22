using UnityEngine;

public interface IDamageable
{
    void Damage(float damageAmount);

    void Death();

    float MaxHealth { get; set; }

    float CurrentHealth { get; set; }
      
 

    float DamageResist { get; set; }
}

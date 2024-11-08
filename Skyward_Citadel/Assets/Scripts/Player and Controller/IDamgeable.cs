using UnityEngine;

public interface IDamgeable
{
    void Damage(float damageAmount);

    void Death();

    float MaxHealth { get; set; }

    float CurrentHealth { get; set; }
}

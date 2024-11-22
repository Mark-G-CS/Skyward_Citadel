using Unity.VisualScripting;
using UnityEngine;

public class Damage_type_Melee : MonoBehaviour
{
    float DamageToDeal = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(DamageToDeal);
            Debug.Log("Dealt " + DamageToDeal+ " damage!");
        }
        else
        {
            Debug.Log("CANT BE DAMAGED");
        }

    }
    /*
         As a Melee hit, this should be changed to impact the player more,
        such as more damage, and knockback or slow if we implement it
         
         
         */



}

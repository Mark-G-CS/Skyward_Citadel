using Unity.VisualScripting;
using UnityEngine;

public class Damage_type_Projectile : MonoBehaviour
{
    [SerializeField] float DamageToDeal = 5;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(DamageToDeal);
            Debug.Log("Delt " + DamageToDeal+ " damage!");
        }
        else
        {
            Debug.Log("CANT BE DAMAGED");
        }
        /*
         As a projectile, this should be changed to impact the player less,
        such as less damage, and less knockback if we implement it
         
         
         */
    }
    



}

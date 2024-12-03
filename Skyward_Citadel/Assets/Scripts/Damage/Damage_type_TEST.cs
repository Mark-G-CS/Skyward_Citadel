using Unity.VisualScripting;
using UnityEngine;

public class Damage_type_TEST : MonoBehaviour
{
    [SerializeField] float DamageToDeal = 10;

    private void OnTriggerEnter2D(Collider2D collider)
    {
        IDamageable damageable = collider.GetComponent<IDamageable>();
        if (damageable != null && !collider.CompareTag(tag))
        {
            damageable.Damage(DamageToDeal);
            Debug.Log("Delt " + DamageToDeal+ " damage!");
        }
        else
        {
            Debug.Log("CANT BE DAMAGED");
        }

    }
    



}

using Unity.VisualScripting;
using UnityEngine;

public class Damage_type_Melee : MonoBehaviour
{
    [SerializeField] float DamageToDeal = 10;

    public float LifeSpan = 0.3f;
    private float timer = 0.0f;

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
    private void Update()
    {

        timer += Time.deltaTime;
        Animator animator = GetComponent<Animator>();
        

        if (timer > LifeSpan)
        {
            if(animator != null) { 
            GetComponentInParent<Animator>().SetBool("Attack", false);
            }           
            Destroy(gameObject);
            Debug.Log("why am i not broken");
        }


    }

    /*
         As a Melee hit, this should be changed to impact the player more,
        such as more damage, and knockback or slow if we implement it
         
         
         */



}

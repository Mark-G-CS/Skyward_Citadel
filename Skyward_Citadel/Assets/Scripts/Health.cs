using UnityEngine;
using System.Collections;
using TMPro;

public class Health : MonoBehaviour, IDamageable
{
    public GameObject Floatingdamagetxt;
    public TMP_Text popUpText;
    [SerializeField] public bool amIPlayer = false;  // 12/1/2024 - so we can change certain things like animation calls 

    [Header("Health")]
    [SerializeField] private float startingHealth;
    public float currentHealth { get; private set; }
    public float MaxHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float CurrentHealth { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }
    public float DamageResist { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

    private Animator anim;
    private bool dead;

    [Header("iFrames")]
    [SerializeField] private float iFramesDuration;
    [SerializeField] private int numberOfFlashes;
    private SpriteRenderer spriteRend;

    [Header("Components")]
    [SerializeField] private Behaviour[] components;
    private bool invulnerable;

    private void Awake()
    {
        currentHealth = startingHealth;
        anim = GetComponent<Animator>();
        spriteRend = GetComponent<SpriteRenderer>();
    }
    public void Damage(float _damage)
    {
        Debug.Log("HEALTH CLASS DAMAGE TEST");
        if (invulnerable) return;
        currentHealth = Mathf.Clamp(currentHealth - _damage, 0, startingHealth);
        popUpText.text = _damage.ToString();

        Invoke("onPlayerHit", 0.3f);

        if (currentHealth > 0)
        {
            anim.SetTrigger("hurt");
            StartCoroutine(Invunerability());
            Instantiate(Floatingdamagetxt, transform.position, Quaternion.identity);
            GetComponent<SpriteRenderer>().material.color = new Color(1, 0.4f, 0.4f, 1);
        }
        else
        {
            popUpText.text = "I am dead";
            Instantiate(Floatingdamagetxt, transform.position, Quaternion.identity);
            if (!dead)
            {
                anim.SetTrigger("die");

                //Deactivate all attached component classes
                foreach (Behaviour component in components)
                    component.enabled = false;
                popUpText.text = "I am dead";
                Instantiate(Floatingdamagetxt, transform.position, Quaternion.identity);
                GetComponent<SpriteRenderer>().material.color = new Color(1, 0.4f, 0.4f, 1);

                dead = true;
                anim.SetBool("Dead", true);
                if (!amIPlayer)
                {
                    Invoke("Death", 2f);
                }

            }
        }

    }
    public virtual void Death()
    {
        Debug.Log($"{gameObject.name} has died!");
        popUpText.text = "Bye bye!";
        popUpText.fontSize = 40f;
        Instantiate(Floatingdamagetxt, transform.position, Quaternion.identity);
        Destroy(gameObject); // Remove the object from the game
        

    }
    public bool getisdead()
    {

        return dead;
    }


    public void onPlayerHit()
    {

        GetComponent<SpriteRenderer>().material.color = Color.white;

    }


    public void AddHealth(float _value)
    {
        currentHealth = Mathf.Clamp(currentHealth + _value, 0, startingHealth);
    }
    private IEnumerator Invunerability()
    {
        invulnerable = true;
        Physics2D.IgnoreLayerCollision(10, 11, true);
        for (int i = 0; i < numberOfFlashes; i++)
        {
            spriteRend.color = new Color(1, 0, 0, 0.5f);
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
            spriteRend.color = Color.white;
            yield return new WaitForSeconds(iFramesDuration / (numberOfFlashes * 2));
        }
        Physics2D.IgnoreLayerCollision(10, 11, false);
        invulnerable = false;
    }



}
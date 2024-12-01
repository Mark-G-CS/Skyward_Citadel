using Unity.VisualScripting;
using UnityEngine;

public class ProjectileSpawner : MonoBehaviour
{
    [SerializeField] public float Bulletfreq = 5f;
    public GameObject ProjectileBlueFireObject;
    private float timer = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer > Bulletfreq)
        {
            if (GetComponent<Health>().getisdead())
            {

                Debug.Log("DISABLED");
                this.enabled = false;

            }
            else
            {
                fireBullet(0);
            }
        }

    }


    public void fireBullet(float rotMod)
    {
        Quaternion temp = transform.rotation;
        temp *= Quaternion.Euler(0, 0, rotMod);
        Debug.Log("Q");
        Instantiate(ProjectileBlueFireObject, transform.position, temp);
        timer = 0;


    }

}

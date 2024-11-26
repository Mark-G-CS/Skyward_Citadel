using UnityEngine;

public class TextDestruction : MonoBehaviour
{

    public float destructTime;
    private float timer;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        timer = destructTime;
    }

    // Update is called once per frame
    void Update()
    {
        timer -= Time.deltaTime;
        
        if(timer < 0)
        {
            Destroy(gameObject);
        }
        
    }
}

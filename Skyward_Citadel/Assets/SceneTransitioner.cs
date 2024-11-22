using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public int sceneIndex = 0;
    public static SceneTransitioner transitioner;
    public float playerSetX = 0;
    public float playerSetY = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

    }

    // Update is called once per frame
    void FixedUpdate()
    {

    }

    public void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            collision.transform.position= new Vector3(playerSetX, playerSetY,0.0f);
            SceneManager.LoadScene(sceneIndex);
        }
    }
}

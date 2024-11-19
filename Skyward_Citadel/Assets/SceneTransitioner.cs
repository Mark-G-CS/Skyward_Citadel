using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransitioner : MonoBehaviour
{
    public int sceneIndex = 0;
    public static SceneTransitioner transitioner;

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
        Debug.Log("Attempting Scene Transition");
        SceneManager.LoadScene(sceneIndex);
    }
}

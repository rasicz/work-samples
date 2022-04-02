using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevelTrigger : MonoBehaviour
{
    public int level;
    private Renderer renderer;
    void Start()
    {
        renderer = gameObject.GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (renderer.isVisible)
        {
            SceneManager.LoadScene(level);
            Destroy(this);
        }
    }
}

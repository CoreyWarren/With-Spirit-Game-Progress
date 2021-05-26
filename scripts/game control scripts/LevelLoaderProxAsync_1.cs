using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoaderProxAsync_1 : MonoBehaviour
{
    [SerializeField]
    private float playerCheckRadius;
    [SerializeField]
    private LayerMask whatIsPlayer;

    private bool touchingPlayer;
    private bool touchedPlayer;
    private bool loadingLevel;

    [SerializeField]
    private int levelToLoad;
    public void LoadLevel (int sceneIndex)
    {
        StartCoroutine(LoadAsynchronously(sceneIndex));

        
    }
    
    IEnumerator LoadAsynchronously (int sceneIndex)
    {
        AsyncOperation loadOperation = SceneManager.LoadSceneAsync(sceneIndex);

        while (!loadOperation.isDone)
        {
            Debug.Log(loadOperation.progress);

            yield return null;
        }
    }
    
    
    
    
    // Start is called before the first frame update
    void Start()
    {
        loadingLevel = false;
        touchedPlayer = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (!touchedPlayer)
        {
            touchingPlayer = Physics2D.OverlapCircle(this.transform.position, playerCheckRadius, whatIsPlayer);
            if (touchingPlayer)
            {
                touchedPlayer = true;
            }
        }


        if (touchedPlayer && !loadingLevel)
        {
            LoadLevel(levelToLoad);
            Debug.Log("Level Load Async started due to proximity");
            loadingLevel = true;
        }
    }
}

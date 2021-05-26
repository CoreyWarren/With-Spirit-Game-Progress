using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class puzzleSuccess1 : MonoBehaviour
{
    [SerializeField]
    private GameObject theTriggerWatcher;
    private multiTriggerTracker theTriggerTracker;
    [SerializeField]
    private GameObject objToDestroy, objToSpawn;
    [SerializeField]
    private Transform placeToSpawn;
    [SerializeField]
    private bool taskComplete;
    
    [SerializeField]
    private AudioClip successAudio;
    [SerializeField]
    private AudioSource as1;

    // Start is called before the first frame update
    void Start()
    {
        taskComplete = false;
        theTriggerTracker = theTriggerWatcher.GetComponent<multiTriggerTracker>();
    }

    // Update is called once per frame
    void Update()
    {
        if(!taskComplete)
        {
            if (theTriggerTracker.allTriggersGood)
            {

                if (objToSpawn != null)
                {
                    Instantiate(objToSpawn, new Vector2(placeToSpawn.position.x, placeToSpawn.position.y), Quaternion.identity);
                }

                if(objToDestroy != null)
                {
                    Destroy(objToDestroy);
                }

                //Play Audio if desired
                if(as1 != null && successAudio != null)
                {
                    as1.PlayOneShot(successAudio);
                }



                taskComplete = true;
            }
            
        }
        
    }
}

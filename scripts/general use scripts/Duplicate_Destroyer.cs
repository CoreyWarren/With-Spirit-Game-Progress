using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Duplicate_Destroyer : MonoBehaviour
{

    //This script deletes objects in a way such that
    //objects that don't get deleted when a scene is loaded
    //don't end up with duplicates of themselves.


    GameObject[] sameObjects;
    private AudioClip ac1, ac2;
    private GameObject obj1, obj2;

    void Awake()
    {
        
        
    }

    void Start()
    {
        string myTag;
        myTag = this.gameObject.tag;
        sameObjects = GameObject.FindGameObjectsWithTag(myTag);

        obj1 = gameObject.transform.GetChild(0).gameObject;
        ac1 = obj1.GetComponent<AudioSource>().clip;

        int checker = 0;

        if (sameObjects.Length > 1)
        {
            for (checker = 0; checker < sameObjects.Length; checker++)
            {
                if (sameObjects[checker] != gameObject)
                {
                    obj2 = sameObjects[checker].transform.GetChild(0).gameObject;
                    ac2 = obj2.GetComponent<AudioSource>().clip;

                    if (ac1 != ac2)
                    {
                        Debug.Log("Found different Audio");
                        obj2.GetComponent<AudioSource>().clip = ac1;
                        obj2.GetComponent<music1script>().audioChanged = true;
                    }
                }
            }
            Debug.Log("Destroying myself");
            Destroy(gameObject);
        }
    }
    
    
}

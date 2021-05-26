using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class multiTriggerTracker : MonoBehaviour
{

    public List<bool> triggerList = new List<bool>();
    private bool triggersGoodSoFar;
    public bool allTriggersGood;
    
    // Start is called before the first frame update
    void Start()
    {
        triggersGoodSoFar = false;
        allTriggersGood = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!allTriggersGood)
        {
            
            int triggerCounter = 0;


            foreach (bool myTrigger in triggerList)
            {

                if (myTrigger == true)
                {
                    triggersGoodSoFar = true;
                }
                else
                {
                    triggersGoodSoFar = false;
                    break;
                }

                triggerCounter++;

            }

            if (triggersGoodSoFar)
            {
                allTriggersGood = true;
            }

        }
        

    }
}

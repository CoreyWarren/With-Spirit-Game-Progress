using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaTriggerGeneral1 : MonoBehaviour
{

    [SerializeField]
    private BoxCollider2D myBoxCheck;
    public bool conditionMet;
    public bool continuousCheck;
    [SerializeField]
    private string tagToCheck;
    public GameObject reportingTriggerTo;
    public int myTriggerNumber;


    // Start is called before the first frame update
    void Start()
    {
        conditionMet = false;

        
    }

    private void Update()
    {
        
        //If there is an actual object to report my trigger readings to,
        //and the trigger reading is not -current- to what I am reading,
        //set it to the correct reading.

        if(reportingTriggerTo != null)
            {
            if (conditionMet == true)
                {
                    if (reportingTriggerTo.GetComponent<multiTriggerTracker>().triggerList[myTriggerNumber] == false)
                    {
                    //Since condition is met, allow this cond to be true!
                    reportingTriggerTo.GetComponent<multiTriggerTracker>().triggerList[myTriggerNumber] = true;
                    }
                }
                else
                {
                    if (reportingTriggerTo.GetComponent<multiTriggerTracker>().triggerList[myTriggerNumber] == true)
                    {
                        //Since condition is met, set this cond. back to false!
                        reportingTriggerTo.GetComponent<multiTriggerTracker>().triggerList[myTriggerNumber] = false;
                    }
                }
            }
            else
            {
                Debug.Log("There is no object to report my Trigger To! (" + this.gameObject.name + " said that.)");
            }
        

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.tag == tagToCheck)
        {
            conditionMet = true;
        }
    }



    //In this case, "continuous check" refers to the fact that
    //if the trigger object LEAVES my 2d box trigger area,
    //then reset my trigger to false again.

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == tagToCheck)
        {
            if(continuousCheck)
            {
                conditionMet = false;
            }
            
        }
    }




}

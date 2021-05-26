using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class areaTriggerScriptStop : MonoBehaviour
{
    [SerializeField]
    private string scriptNameToDisable;

    [SerializeField]
    private GameObject triggerObject;
    [SerializeField]
    private  BoxCollider2D triggerArea;
    [SerializeField]
    private LayerMask triggerLayer;
    [SerializeField]
    private string triggerTag;

    [SerializeField]
    private int specifiedLayer;

    [SerializeField]
    bool detectingTriggerObject, detectingTriggerLayer, detectingTriggerTag, oneWaySwitch;


    bool scriptDisabled;
    //
    //This script has 2 modes:
    //
    //-Mode 1:
    //
    //"Detects (OBJECTS of certain LAYER) in certain AREA"
    //detectingTriggerObject = false
    //detectingTriggerLayer = true
    //detectingTriggerTag = false
    //
    //-Mode 2:
    //
    //"Detects a SPECIFIC OBJECT in certain AREA"
    //detectingTriggerObject = true
    //detectingTriggerLayer = false
    //detectingTriggerTag = false
    //
    //-Mode 3:
    //
    //"Detects a TAGGED object in certain AREA"
    //detectingTriggerObject = false
    //detectingTriggerLayer = false
    //detectingTriggerTag = true




    private void OnTriggerEnter2D(Collider2D other)
    {
        //Mode 1 (Trigger Layer of Objects)
        if(detectingTriggerLayer)
        {
            if (other.gameObject.layer == triggerLayer)
            {
                (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = false;
                Debug.Log("Script disabled thru area trigger.");
            }
        }
        

        //Mode 2 (Trigger on Specific Object)
        if(detectingTriggerObject)
        {
            if (other.gameObject == triggerObject)
            {
                (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = false;
                Debug.Log("Script disabled thru area trigger.");
            }
        }
            
      

        
        //Mode 3 (Trigger Tag)
        if(detectingTriggerTag)
        {
            if (other.gameObject.tag == triggerTag)
            {
                (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = false;
                Debug.Log("Script disabled thru area trigger.");
            }
        }
        

    }

    private void OnTriggerExit2D(Collider2D other)
    {


        if(!oneWaySwitch)
        {

            if (detectingTriggerLayer)
            {
                if (other.gameObject.layer == triggerLayer)
                {
                    (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = true;

                }
            }


            //Mode 2 (Trigger on Specific Object)
            if (detectingTriggerObject)
            {
                if (other.gameObject == triggerObject)
                {
                    (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = true;

                }
            }




            //Mode 3 (Trigger Tag)
            if (detectingTriggerTag)
            {
                if (other.gameObject.tag == triggerTag)
                {
                    (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = true;

                }
            }




        }//end oneWaySwitch IF statement
        
    }


    // Start is called before the first frame update
    void Start()
    {
        scriptDisabled = false;



    }

    // Update is called once per frame
    void Update()
    {
        /*

        //If object enters trigger area
        //Layer Mode:
        if(detectingTriggerLayer)
        {
            if(triggerArea.IsTouchingLayers(specifiedLayer))
                {
                (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = false;
                scriptDisabled = true;
                }

        }



        //Specific Object Mode:
        if (detectingTriggerObject)
        {
            if(triggerObject.GetComponent)
            (GetComponent(scriptNameToDisable) as MonoBehaviour).enabled = false;
            scriptDisabled = true;
        }



        //If object leaves area and oneWaySwitch = false;
        //Turn script back on.
        if(scriptDisabled && !oneWaySwitch)
        {
            if(!triggerArea.IsTouchingLayers(specfiedLayer))
            {
                scriptToDisable.enabled = true;
                scriptDisabled = false;
            }
        }


        */
    }
}

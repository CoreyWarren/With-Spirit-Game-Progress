using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wallHovering : MonoBehaviour
{

    //
    [SerializeField]
    private float initialPhaseOffset, amplitude, radConstantSpeed;
    [SerializeField]
    private bool vertical;
    private float fullCycleTime = 100;
    private float currentPhase;
    private Vector2 initialPos;
    

   

    // Start is called before the first frame update
    void Start()
    {
        initialPos = this.transform.position;

        currentPhase = initialPhaseOffset;
    }

    // Update is called once per frame
    void Update()
    {


        if(!pauser1.paused)
        {
            if(currentPhase > 2 * Mathf.PI)
            {
                currentPhase = 0;
            }

            currentPhase += radConstantSpeed;

            if(vertical)
            {
                //Move object along Y axis
                transform.position = new Vector3(transform.position.x, initialPos.y + Mathf.Sin(currentPhase) * amplitude, transform.position.z);
            }
            else
            {
                //Move object along X axis
                transform.position = new Vector3(initialPos.x + Mathf.Sin(currentPhase) * amplitude, transform.position.y, transform.position.z);
            }


        }


    }



    private void OnDrawGizmosSelected()
    {
        if(vertical)
        {
            Gizmos.color = Color.yellow;
            Vector3 topOfPhase = new Vector3(transform.position.x, transform.position.y + Mathf.Sin(0.5f * Mathf.PI) * amplitude, transform.position.z);
            Gizmos.DrawLine(transform.position, topOfPhase);

            Gizmos.color = Color.red;
            Vector3 bottomOfPhase = new Vector3(transform.position.x, transform.position.y - Mathf.Sin(0.5f * Mathf.PI) * amplitude, transform.position.z);
            Gizmos.DrawLine(transform.position, bottomOfPhase);

            Gizmos.color = Color.blue;
            Vector3 slightOffset = new Vector3(0.1f, 0f, 0f);
            Gizmos.DrawLine(transform.position + slightOffset, new Vector3(transform.position.x, transform.position.y + Mathf.Sin(initialPhaseOffset) * amplitude, transform.position.z) + slightOffset);
        }
        else
        {
            Gizmos.color = Color.yellow;
            Vector3 topOfPhase = new Vector3(transform.position.x + Mathf.Sin(0.5f * Mathf.PI) * amplitude, transform.position.y , transform.position.z);
            Gizmos.DrawLine(transform.position, topOfPhase);

            Gizmos.color = Color.red;
            Vector3 bottomOfPhase = new Vector3(transform.position.x - Mathf.Sin(0.5f * Mathf.PI) * amplitude, transform.position.y , transform.position.z);
            Gizmos.DrawLine(transform.position, bottomOfPhase);

            Gizmos.color = Color.blue;
            Vector3 slightOffset = new Vector3(0f, 0.1f, 0f);
            Gizmos.DrawLine(transform.position + slightOffset, new Vector3(transform.position.x + Mathf.Sin(initialPhaseOffset) * amplitude, transform.position.y , transform.position.z) + slightOffset);
        }

    }


}

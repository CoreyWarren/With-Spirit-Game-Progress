using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueNPC : MonoBehaviour
{

    GameObject player;
    public float talkDistance;
    DialogueTrigger dialogueTrigger;
    private bool startedDialogue;
    public int talkedNumber;
    public bool waitingToTalk;
    private AudioClip myVoice;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindWithTag("Player");
        dialogueTrigger = GetComponent<DialogueTrigger>();
        talkedNumber = 0; // may need to omit later for dialogue controller to set this number externally
    }

    // Update is called once per frame
    void Update()
    {
        if(!startedDialogue)
        {
            Vector2 difference = new Vector2(
                    player.transform.position.x - this.transform.position.x,
                    player.transform.position.y - this.transform.position.y
                    );

            float distanceSquared = difference.x * difference.x + difference.y * difference.y;
            float distance = Mathf.Sqrt(distanceSquared);

            /*
             * if (distance < talkDistance && talkedNumber == 0)
            {
                dialogueTrigger.TriggerDialogue();
                startedDialogue = true;
            }
            else
            */

            if (distance < talkDistance)
            {
                if(Input.GetKeyDown(KeyCode.DownArrow))
                {
                    dialogueTrigger.TriggerDialogue();
                    startedDialogue = true;
                }
            }

        }
        else 
        {
            //if my dialogue is now STARTED
            //AND if my dialogue is NOT COMPLETE
            if(Input.GetKeyDown("z"))
            {
                FindObjectOfType<DialogueManager>().DisplayNextSentence();
            }

            if(pauser1.endDialogue)
            {
                talkedNumber++;
                startedDialogue = false;
            }
        }
        

    }

    void OnDrawGizmosSelected()
    {
        // Draw a yellow sphere at the transform's position
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, talkDistance);
    }


}

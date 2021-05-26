using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class energyText : MonoBehaviour
{

    private Text myText;
    PlayerController playerController;
    Player_Stats_Storage playerStats;
    // Start is called before the first frame update
    void Start()
    {
        myText = GetComponent<Text>();
        playerController = GameObject.FindWithTag("Player").GetComponent<PlayerController>();

        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
    }

    // Update is called once per frame
    void Update()
    {
        myText.text = "Energy: " + playerStats.playerEnergy.ToString();
    }
}

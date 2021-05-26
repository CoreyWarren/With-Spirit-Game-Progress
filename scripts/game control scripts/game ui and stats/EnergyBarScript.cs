using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnergyBarScript : MonoBehaviour
{
    Player_Stats_Storage playerStats;
    private Image ImgEnergyBar;

    private float EnergyCurrentPercent;
    private float eCurrentValue;
    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindWithTag("Event System").GetComponent<Player_Stats_Storage>();
        eCurrentValue = -1;
        
        ImgEnergyBar = GetComponent<Image>();
    }
    // Update is called once per frame
    void Update()
    {
        if(eCurrentValue != playerStats.playerEnergy)
        {
            eCurrentValue = playerStats.playerEnergy;

            EnergyCurrentPercent = eCurrentValue / playerStats.playerEnergyMax;
            ImgEnergyBar.fillAmount = EnergyCurrentPercent;

        }

        
    }
}

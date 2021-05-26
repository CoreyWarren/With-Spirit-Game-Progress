using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class music1script : MonoBehaviour {
    public AudioClip ac1;
    static private bool playingMusic;
    AudioSource as1;
    private float defaultVolume, pausedVolume1;
    [SerializeField]
    private float loweredVolumePercent, volumeBlendTime;
    private float volumeBlendClock;
    private float volumeBlendIncrement;
    private bool mustBlendVolume;
    private bool wasPaused, startBlendingVolume;
    public bool audioChanged;





    // Use this for initialization
    void Start () {

        as1 = GetComponent<AudioSource>();
        if(ac1 == null && as1.clip != null)
        {
            ac1 = as1.clip;
        }
        

        defaultVolume = as1.volume;
        pausedVolume1 = as1.volume * loweredVolumePercent/100;
        volumeBlendIncrement = (defaultVolume - pausedVolume1) / volumeBlendTime;

        startBlendingVolume = false;
        wasPaused = false;
        mustBlendVolume = false;
        
    }




    
    // Update is called once per frame
    void Update () {


        if (!pauser1.paused && as1.volume != defaultVolume)
        {
            mustBlendVolume = true;
            wasPaused = true;

            startBlendingVolume = true;
            volumeBlendClock = volumeBlendTime;
        }


        if (pauser1.paused && as1.volume != pausedVolume1)
        {
            mustBlendVolume = true;
            wasPaused = false;

            startBlendingVolume = true;
            volumeBlendClock = volumeBlendTime;
        }
       

        if(audioChanged)
        {
            as1.Stop();
            as1.Play();
            audioChanged = false;
        }


        if(mustBlendVolume)
        {
            volumeBlendClock -= Time.deltaTime;


            if (wasPaused)
            {
                as1.volume += volumeBlendIncrement;
                if(as1.volume > defaultVolume)
                {
                    mustBlendVolume = false;
                    as1.volume = defaultVolume;
                }
            }else
            {
                as1.volume -= volumeBlendIncrement;
                if(as1.volume < pausedVolume1)
                {
                    mustBlendVolume = false;
                    as1.volume = pausedVolume1;
                }
            }
            
            if(volumeBlendClock <= 0)
            {
                mustBlendVolume = false;
            }

        }else
        if (!mustBlendVolume)
        {
            if (!pauser1.paused)
            {
                as1.volume = defaultVolume;
            }

            if (pauser1.paused)
            {
                as1.volume = pausedVolume1;
            }
        }
        
        









    }
}

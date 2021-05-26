using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class Dialogue
{

    public string speaker;
    [TextArea(3, 10)]
    public string[] sentences;
    public AudioClip myVoice;
    public List<RuntimeAnimatorController> myAnimator = new List<RuntimeAnimatorController>();
    
}

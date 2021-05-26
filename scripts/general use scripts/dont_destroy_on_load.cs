using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dont_destroy_on_load : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

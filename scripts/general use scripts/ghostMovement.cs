using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ghostMovement : MonoBehaviour
{

    [SerializeField]
    private float xDistance, yDistance;
    private float time;
    [SerializeField]
    private float timeScale;

    private Vector3 defaultPosition;
    // Start is called before the first frame update
    void Start()
    {
        defaultPosition = new Vector3(transform.position.x, transform.position.y, transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 floatingPosition;
        floatingPosition = new Vector3(Mathf.Cos(time) * xDistance, Mathf.Sin(2 * time) * yDistance, 0f);

        if(time > (2 * Mathf.PI))
        {
            time = 0;
        }

        time += Time.deltaTime * timeScale;

        transform.position = defaultPosition + floatingPosition;
    }
}

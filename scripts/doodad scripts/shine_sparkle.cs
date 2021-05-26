using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shine_sparkle : MonoBehaviour
{

    private Animator a1;
    [SerializeField]
    private Transform parentTrans;
    private Vector3 myOffset;

    // Start is called before the first frame update
    void Start()
    {
        a1 = GetComponent<Animator>();
        a1.speed = Random.Range(0.9f, 1.1f);
        myOffset = new Vector3(
            parentTrans.position.x - transform.position.x,
            parentTrans.position.y - transform.position.y,
            0f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(parentTrans.position.x - myOffset.x, parentTrans.position.y - myOffset.y, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAmmoQuestionMark : MonoBehaviour
{
    public float lifetime;
    private float lifetimer;
    private Transform player;
    private SpriteRenderer srr;

    [SerializeField]
    private float yOffset, yOffsetRange;

    // Use this for initialization
    void Start()
    {
        lifetimer = lifetime;
        transform.localScale = new Vector2(transform.localScale.x + Random.Range(-0.1f, 0.1f), transform.localScale.y + Random.Range(-0.1f, 0.1f));
        srr = GetComponent<SpriteRenderer>();
        player = GameObject.FindWithTag("Player").transform;
        yOffset += Random.Range(-yOffsetRange, yOffsetRange);
    }

    // Update is called once per frame
    void Update()
    {

        transform.position = player.position + new Vector3(0f, yOffset, 0f);
        srr.color = new Color(srr.color.r, srr.color.g, srr.color.b, lifetimer / lifetime);
        if (pauser1.paused == false)
        {
            lifetimer-= Time.deltaTime * 100;
            if (lifetimer <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}

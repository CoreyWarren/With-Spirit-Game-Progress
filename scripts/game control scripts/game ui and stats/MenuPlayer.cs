using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuPlayer : MonoBehaviour
{
    [SerializeField]
    private Sprite sprite1;

    private SpriteRenderer srr;

    void Start()
    {
        srr = GetComponent<SpriteRenderer>();
        srr.sprite = sprite1;
    }

    void Update()
    {
        srr.sprite = sprite1;
    }
}

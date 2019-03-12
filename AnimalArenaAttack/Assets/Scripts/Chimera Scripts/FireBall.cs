﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public int damage = 30;
    public float fireBallSpeed;

    // Use this for initialization
    void Start()
    {
        GetComponent<Rigidbody2D>().velocity = -transform.up.normalized * fireBallSpeed;
        Destroy(gameObject, 5);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle" || col.gameObject.tag == "Salamander")
        {
            col.gameObject.GetComponent<PlayerParent>().Damage(damage);
            GameObject.Destroy(gameObject);
        }
    }
}

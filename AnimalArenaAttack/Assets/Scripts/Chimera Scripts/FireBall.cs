﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public ParticleSystem fireBurst;

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
            Instantiate(fireBurst, transform.position, transform.rotation);

            col.gameObject.GetComponent<PlayerParent>().Damage(damage);
            GameObject.Destroy(gameObject);
            Invoke("KillBurst", 1);
        }
    }
    void KillSmoke()
    {
        if (fireBurst.IsAlive())
        {
            fireBurst.Stop();
            fireBurst.Clear();
            Destroy(fireBurst.gameObject);
        }
    }
}

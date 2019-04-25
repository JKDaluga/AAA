﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlameBurst : MonoBehaviour {

    GameObject GameManager;
    public int damage = 5;
    public float timer = 0;
    float speed = 13f;

    public float yOffset = 0f;

    public GameObject chimera;
    public GameObject redHead;

    // Use this for initialization
    void Start () {
        chimera = GameObject.FindGameObjectWithTag("Monster");
        redHead = GameObject.FindGameObjectWithTag("FeatherCollisionBox");
        GameManager = GameObject.Find("GameManager");
    }
	
	// Update is called once per frame
	void Update () {
        if (timer >= .4)
        {
            GameObject.Destroy(gameObject);
        }
        timer += 1.0F * Time.deltaTime;
        speed *= .95f;
        transform.position = Vector3.MoveTowards(transform.position, redHead.transform.position, speed*Time.deltaTime);
        transform.up = redHead.transform.position - transform.position;
    }
}
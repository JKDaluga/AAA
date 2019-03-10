﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerParent : MonoBehaviour {

    private Rigidbody2D rb;

    int health;

    public bool isPlayer1;

    public bool ability1;
    public bool ability2;


    // Use this for initialization
    void Start ()
    {
        health = 750;
        rb = GetComponent<Rigidbody2D>();
        //GameManager.addPlayer(this);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health > 0)
        {
            if (isPlayer1)
            {
                MovementP1();
            }
            if (!isPlayer1)
            {
                MovementP2();
            }
            if (isPlayer1)
            {
                if ((Input.GetKeyDown(KeyCode.V)) || (Input.GetKeyDown(KeyCode.Mouse0)))
                {
                    Ability1();

                }
                if ((Input.GetKeyDown(KeyCode.B)))
                {
                    Ability2();

                }
            }
            else if (!isPlayer1)
            {
                //Ability 1 Player 2
                if (((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.U))) || (Input.GetKeyDown(KeyCode.Mouse0)))
                {
                    // anim.SetBool("Ability1", true);
                    Ability1();
                }

                //Ability 2 Player 2
                if ((Input.GetKeyDown(KeyCode.I)) || (Input.GetKeyDown(KeyCode.E)))
                {
                    // anim.SetBool("Ability2", true);
                    Ability2();
                }
            }

        }
    }

    public virtual void Ability1()
    {

    }
    public virtual void Ability2()
    {

    }

    void MovementP1()
    {
        float x = Input.GetAxis("Horizontal1") * Time.deltaTime;
        float y = Input.GetAxis("Vertical1") * Time.deltaTime;

        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement;
        print(movement);    
    }

    void MovementP2()
    {
        float x = Input.GetAxis("Horizontal2") * Time.deltaTime;
        float y = Input.GetAxis("Vertical2") * Time.deltaTime;

        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement;
        print(movement);
    }
    public void Damage(int amount)
    {
        if (amount > 0)
        {
            health -= amount;
            if (health <= 0)
            {
                health = 0;
            }
        }
    }
    public int getHealth()
    {
        return health;
    }

}
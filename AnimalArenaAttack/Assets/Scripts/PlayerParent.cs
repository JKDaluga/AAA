using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParent : MonoBehaviour {

    private Rigidbody2D rb;

    int health;

    public bool isPlayer1;

    public bool ability1;
    public bool ability2;
    public int speed;
    public bool facingRight = true;
    public float horiz;

    public GameObject chimera;


    public Vector3 directionFacing;
    public float dashTime = 1f;
    public float dashDistance = 10f;
    public float dashSpeed = 1f;
    public float stopDash = .1f;

    private float currentDashTime;

    public Slider P1HealthBar;
    public Slider P2HealthBar;



    public void Awake()
    {
        horiz = 0;
    }
    // Use this for initialization
    void Start ()
    {
        health = 750;
        speed = 300;
        rb = GetComponent<Rigidbody2D>();
        //GameManager.addPlayer(this);
        currentDashTime = dashTime;
       
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (isPlayer1)
        {
            horiz = Input.GetAxis("Horizontal1");
            if (this.transform.position.x < chimera.transform.position.x && !facingRight)
            {
                Flip();
            }
           
            else if (this.transform.position.x > chimera.transform.position.x && facingRight)
            {
                Flip();
            }
        }
        else if (!isPlayer1)
        {
            horiz = Input.GetAxis("Horizontal2");
            if (this.transform.position.x < chimera.transform.position.x && !facingRight)
            {
                Flip();
            }

            else if (this.transform.position.x > chimera.transform.position.x && facingRight)
            {
                Flip();
            }
        }

        
        if (health > 0)
        {
            if (isPlayer1)
            {
                MovementP1();
                P1HealthBar.value = health;

            }
            if (!isPlayer1)
            {
                MovementP2();
                P2HealthBar.value =health;

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
        currentDashTime = 0f;

        if (currentDashTime < dashTime)
        {
            directionFacing = transform.forward * dashDistance;
            currentDashTime += stopDash;
        }
        else
        {
            directionFacing = Vector3.zero;
        }

        //After knowing the direction of a player if a player uses ability 2 push them in that specific diretion 
        //Store a varible for the last direction a player moves

    }

    void MovementP1()
    {
        float x = Input.GetAxis("Horizontal1") * Time.deltaTime;
        float y = Input.GetAxis("Vertical1") * Time.deltaTime;

        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement * speed;
    }

    void MovementP2()
    {
        float x = Input.GetAxis("Horizontal2") * Time.deltaTime;
        float y = Input.GetAxis("Vertical2") * Time.deltaTime;

        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement * speed;
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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

}

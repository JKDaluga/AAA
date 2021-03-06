﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParent : MonoBehaviour
{
    public ParticleSystem onDamage;

    protected Rigidbody2D rb;
    public Animator anim;

    public int health;
    public float maxHealth;

    public Rigidbody2D pRB2D;
    public BoxCollider2D Box;

    public bool isPlayer1;

    public bool ability1;
    public bool ability2;
    public int speed;
    public bool facingRight = true;
    public float horiz;
    public float rollTime;
    protected Vector2 rollVector;
    public int rollSpeed;
    protected bool isVulnerable = true;

    public GameObject chimera;

    protected SpriteRenderer sp;

    public SpriteRenderer eagleRend;
    public SpriteRenderer salamanderRend;

    public float fadeSpeed = 0f;

    public Vector2 savedVelocity;
    public float dashTime = 1f;
    public float dashDistance = 10f;
    public float dashSpeed = 1f;
    public float stopDash = .1f;

    protected float currentDashTime;

    public Slider P1HealthBar;
    public Slider P2HealthBar;
    public Image p1HB;
    public Image p2HB;

    public Color normal, hurt;

    public ParticleSystem smokeTrail;

    public float reviveTime;
    protected bool reviving;

    public Image SalamanderReviveFill;
    public Image SalamanderReviveBG;
    public Image EagleReviveFill;
    public Image EagleReviveBG;

    bool eagleDeath;
    bool salamanderDeath;

    public Color endColor;
    public Color currentColorEagle;
    public Color currentColorSalamander;

    public AudioSource eagleSrc;
    public AudioClip eagleDodge;


    public void Awake()
    {
        horiz = 0;
        anim = gameObject.GetComponent<Animator>();

    }
    // Use this for initialization
    void Start()
    {
       eagleDeath = false;
       salamanderDeath = false;

        pRB2D = GetComponent<Rigidbody2D>();
        maxHealth = health;
        speed = 350;
        rb = GetComponent<Rigidbody2D>();

        sp = GetComponent<SpriteRenderer>();
        sp.color = normal;

        reviveTime = 0;

        //GameManager.addPlayer(this);
        //dashTime  = startingDashTime;

        currentColorEagle = normal;
        currentColorSalamander = normal;

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        /*

        //if(Input.GetKeyDown(KeyCode.Space))
        //if (Input.GetKeyDown(KeyCode.Alpha1))
        if(eagleDeath)
        {
            if (currentColorEagle == normal)
            {
                currentColorEagle = endColor;
            }
            else
            {
                currentColorEagle = normal;
            }
            eagleRend.color = Color.Lerp(eagleRend.color, currentColorEagle, fadeSpeed);

        }
        else if (!eagleDeath)
        {
            currentColorEagle = normal;
            eagleRend.color = currentColorEagle;
        }
        //else if (Input.GetKeyDown(KeyCode.Alpha0))
        if(salamanderDeath)
        {
            if (currentColorSalamander == normal)
            {
                currentColorSalamander = endColor;
            }
            else
            {
                currentColorSalamander = normal;
            }
            salamanderRend.color = Color.Lerp(salamanderRend.color, currentColorSalamander, fadeSpeed);
        }
        else if(!salamanderDeath)
        {
            currentColorSalamander = normal;
            eagleRend.color = currentColorSalamander;
        }
       

    */
        if (((health / maxHealth * 100f) < 70))
        {
            anim.SetBool("isInjured", true);
        }

        if (((health / maxHealth * 100f) < 40))
        {
            anim.SetBool("isCritical", true);
            anim.SetBool("isInjured", false);
        }
        if (!reviving)
        {
            if (isPlayer1)
            {
                SalamanderReviveFill.gameObject.SetActive(false);
                SalamanderReviveFill.fillAmount = 0;
                reviveTime = 0;

            }
            else
            {
                EagleReviveFill.gameObject.SetActive(false);
                EagleReviveFill.fillAmount = 0;
                reviveTime = 0;
            }
        }
        if (health > 0)
        {


            if (!isPlayer1)
            {
                SalamanderReviveBG.gameObject.SetActive(false);
                salamanderDeath = false;
            }
            if (isPlayer1)
            {
                EagleReviveBG.gameObject.SetActive(false);
                eagleDeath = false;
            }
            
            
            anim.SetBool("isDead", false);
            if (rollTime > 0)
            {
                rollTime -= Time.deltaTime;
                rb.velocity = rollVector * rollSpeed * Time.deltaTime;
            }
            else
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
                if (isPlayer1)
                {
                    MovementP1();
                    P1HealthBar.value = ((health / maxHealth) * 100);
                    p1HB.fillAmount = (health / maxHealth);

                }
                if (!isPlayer1)
                {
                    MovementP2();
                    P2HealthBar.value = ((health / maxHealth) * 100);
                    p2HB.fillAmount = (health / maxHealth);


                }
                if (isPlayer1)
                {
                    if (( (Input.GetKeyDown(KeyCode.F)) || (Input.GetKeyDown(KeyCode.Mouse0))))
                    {
                        StartCoroutine(setAttack());
                        Ability1();

                    }
                    if ((Input.GetKeyDown(KeyCode.G)))
                    {
                        eagleSrc.PlayOneShot(eagleDodge);
                        anim.SetBool("isDodging", true);
                        if (Input.GetAxisRaw("Horizontal2") > 0)
                        {
                            transform.eulerAngles = new Vector3(0, 0, 270);
                        }
                        else if (Input.GetAxisRaw("Horizontal2") < 0)
                        {
                            transform.eulerAngles = new Vector3(0, 0, -270);
                        }
                        else if (Input.GetAxisRaw("Horizontal2") == 0)
                        {
                            if (facingRight == true)
                            {
                                transform.eulerAngles = new Vector3(0, 0, 270);
                            }
                            else if (facingRight == false)
                            {
                                transform.eulerAngles = new Vector3(0, 0, -270);
                            }
                        }
                        if (rb.velocity.magnitude != 0)
                        {
                            rollTime = .2f;
                            rollVector = rb.velocity.normalized;
                            isVulnerable = false;
                            gameObject.layer = 11;
                            Invoke("setVulnerability", 1f);
                            Invoke("endDodgeAnimation", .5f);
                            Instantiate(smokeTrail, transform.position, this.transform.rotation);
                            smokeTrail.Play();
                            Invoke("KillSmoke", 1);
                        }
                        else
                        {
                            if (facingRight == true)
                            {
                                rollTime = .2f;
                                rollVector = Vector2.right;
                                isVulnerable = false;
                                gameObject.layer = 11;
                                Invoke("setVulnerability", 1f);
                                Invoke("endDodgeAnimation", .5f);
                                Instantiate(smokeTrail, transform.position, this.transform.rotation);
                                smokeTrail.Play();
                                Invoke("KillSmoke", 1);
                            }
                            else if (facingRight == false)
                            {
                                rollTime = .2f;
                                rollVector = Vector2.left;
                                isVulnerable = false;
                                gameObject.layer = 11;
                                Invoke("setVulnerability", 1);
                                Invoke("endDodgeAnimation", .5f);
                                Instantiate(smokeTrail, transform.position, this.transform.rotation);
                                smokeTrail.Play();
                                Invoke("KillSmoke", 1);
                            }
                        }
                        //Ability2();
                    }
                }
                else if (!isPlayer1)
                {
                    //Ability 1 Player 2
                    if (((Input.GetKeyDown(KeyCode.N)) || (Input.GetKeyDown(KeyCode.Mouse0))))
                    {
                        StartCoroutine(setAttack());
                        Ability1(); MovementP2();
                    }

                    //Ability 2 Player 2
                    if ((Input.GetKeyDown(KeyCode.M)))
                    {
                        //Debug.Log("Am I Work?");
                        //anim.SetBool("Ability2", true);
                        Ability2();
                        if (rb.velocity.magnitude != 0)
                        {
                            rollTime = 2f;
                            rollVector = rb.velocity.normalized;
                            isVulnerable = false;
                            Invoke("setVulnerability", 2f);

                        }
                    }
                }
            }
        }
        else
        {
            if (isPlayer1)
            {
                P1HealthBar.value = 0;
                p1HB.fillAmount = 0;
                EagleReviveBG.gameObject.SetActive(true);
                eagleDeath = true;


            }
            else if (!isPlayer1)
            {
                P2HealthBar.value = 0;
                p2HB.fillAmount = 0;
                SalamanderReviveBG.gameObject.SetActive(true);
                salamanderDeath = true;
                

            }
            pRB2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            Box.enabled = false;
            anim.SetBool("isDead", true);
            anim.SetBool("isCritical", false);
            gameObject.layer = 9;
        }



       

    }

    public virtual void Ability1()
    {

    }
    public virtual void Ability2()
    {
        //Debug.Log("Am I work?");

        //savedVelocity = rb.velocity;
        //rb.transform.position = new Vector2(rb.transform.position.x * 3f, rb.transform.position.y);

        //checks to see if the player has dashed
        //if (dashTime <= 0)
        //{
        //    direction = 0;
        //    dashTime = startingDashTime;
        //    rb.velocity = Vector2.zero;
        //}


        //After knowing the direction of a player if a player uses ability 2 push them in that specific diretion
        //Store a varible for the last direction a player moves

    }

    void MovementP1()
    {
        float x = Input.GetAxisRaw("Horizontal2") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical2") * Time.deltaTime;

        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement * speed;
    }

    protected void MovementP2()
    {
        float x = Input.GetAxisRaw("Horizontal1") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical1") * Time.deltaTime;

        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement * speed;
        if (movement.x != 0 || movement.y != 0)
        {
            anim.SetBool("isRunning", true);

        }
        else
        {
            anim.SetBool("isRunning", false);
        }
    }
    public void Damage(int amount)
    {
        if (isVulnerable)
        {
            if (amount > 0)
            {
                StartCoroutine(getHurt());

                health -= amount;
                if (health <= 0)
                {
                    health = 0;
                }

            }
            isVulnerable = false;
            Invoke("setVulnerability", .125f);
        }

    }
    public int getHealth()
    {
        return health;
    }

    protected void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    protected IEnumerator getHurt()
    {
        sp.color = hurt;

        GameObject damage = Instantiate(onDamage.gameObject, transform.position,transform.rotation);
        onDamage.Play();
        Destroy(damage, 1);

        yield return new WaitForSeconds(1f);
        sp.color = normal;
    }

    protected IEnumerator setAttack()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(.125f);
        anim.SetBool("Attack", false);
    }
    void setVulnerability()
    {
        anim.SetBool("isDodging", false);
        isVulnerable = true;
        gameObject.layer = 8;
    }
    void endDodgeAnimation()
    {
        transform.eulerAngles = new Vector3(0, 0, 0);
    }
    public void Revive()
    {
        Box.enabled = true;
        print("REVIVE");
    }

    void KillSmoke()
    {
        if (smokeTrail.IsAlive())
        {
            smokeTrail.Stop();
            smokeTrail.Clear();
            Destroy(smokeTrail.gameObject);
        }
    }
    void KillOnDamage()
    {
        if (onDamage.IsAlive())
        {
            onDamage.Stop();
            onDamage.Clear();
            Destroy(onDamage.gameObject);
        }
    }
    void ResetDodgeRoll()
    {
        anim.SetBool("isDodging", false);
    }
}


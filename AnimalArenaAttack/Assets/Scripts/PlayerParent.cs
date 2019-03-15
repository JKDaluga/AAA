using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerParent : MonoBehaviour
{

    private Rigidbody2D rb;
    public Animator anim;
    public AudioManager audioManager;

    public int health;
    float maxHealth;

    Rigidbody2D pRB2D;


    public bool isPlayer1;

    public bool ability1;
    public bool ability2;
    public int speed;
    public bool facingRight = true;
    public float horiz;
    public float rollTime;
    private Vector2 rollVector;
    public int rollSpeed;
    private bool isVulnerable = true;

    public GameObject chimera;

    private SpriteRenderer sp;

    public Vector2 savedVelocity;
    public float dashTime = 1f;
    public float dashDistance = 10f;
    public float dashSpeed = 1f;
    public float stopDash = .1f;

    private float currentDashTime;

    public Slider P1HealthBar;
    public Slider P2HealthBar;

    public Color normal, hurt;

    public void Awake()
    {
        horiz = 0;
        anim = gameObject.GetComponent<Animator>();
        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene.");
        }
    }
    // Use this for initialization
    void Start()
    {
        pRB2D = GetComponent<Rigidbody2D>();

        health = 1501;
        maxHealth = 1501;
        speed = 350;
        rb = GetComponent<Rigidbody2D>();

        sp = GetComponent<SpriteRenderer>();
        sp.color = normal;


        //GameManager.addPlayer(this);
        //dashTime  = startingDashTime;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (health > 0)
        {
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

                }
                if (!isPlayer1)
                {
                    MovementP2();
                    P2HealthBar.value = ((health / maxHealth) * 100);

                }
                if (isPlayer1)
                {
                    if ((Input.GetKeyDown(KeyCode.V)) || (Input.GetKeyDown(KeyCode.Mouse0)))
                    {
                        StartCoroutine(setAttack());
                        Ability1();

                    }
                    if ((Input.GetKeyDown(KeyCode.B)))
                    {
                        if (rb.velocity.magnitude != 0)
                        {
                            rollTime = .125f;
                            rollVector = rb.velocity.normalized;
                            isVulnerable = false;
                            Invoke("setVulnerability", .125f);
                        }
                        //Ability2();
                    }
                }
                else if (!isPlayer1)
                {
                    //Ability 1 Player 2
                    if (((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.Mouse0))))
                    {
                        StartCoroutine(setAttack());
                        Ability1(); MovementP2();
                    }

                    //Ability 2 Player 2
                    if ((Input.GetKeyDown(KeyCode.I)))
                    {
                        //Debug.Log("Am I Work?");
                        //anim.SetBool("Ability2", true);
                        Ability2();
                        if (rb.velocity.magnitude != 0)
                        {
                            rollTime = .125f;
                            rollVector = rb.velocity.normalized;
                            isVulnerable = false;
                            Invoke("setVulnerability", .125f);
                        }
                    }
                }
            }
        }
        else
        {
            pRB2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY;
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

    void MovementP2()
    {
        float x = Input.GetAxisRaw("Horizontal1") * Time.deltaTime;
        float y = Input.GetAxisRaw("Vertical1") * Time.deltaTime;

        Vector2 movement = new Vector2(x, y);
        rb.velocity = movement * speed;
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

    private void Flip()
    {
        facingRight = !facingRight;
        Vector3 theScale = transform.localScale;
        theScale.x *= -1;
        transform.localScale = theScale;
    }

    private IEnumerator getHurt()
    {
        sp.color = hurt;
        yield return new WaitForSeconds(.5f);
        sp.color = normal;
    }

    IEnumerator setAttack()
    {
        anim.SetBool("Attack", true);
        yield return new WaitForSeconds(.6f);
        anim.SetBool("Attack", false);
    }
    void setVulnerability()
    {
        isVulnerable = true;
    }
}


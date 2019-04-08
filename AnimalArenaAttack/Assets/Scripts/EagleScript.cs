using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : PlayerParent {

    public Animator eagleAnim;
    public GameObject feather;

    public float baseAttackDuration = 1f;

    public float featherSpeed = 2f;

    public float featherUsed;

    public AudioClip featherAttk;

    public AudioSource source;

    public override void Ability1()
    {

        if (Time.time > featherUsed + .15)
        {
            source.PlayOneShot(featherAttk);
            Instantiate(feather, transform.position, transform.rotation);
            featherUsed = Time.time;
        }
    }

    public override void Ability2() 
    {
	

	}

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Salamander")
        {
            PlayerParent salamander = GetComponent<PlayerParent>();
            if (salamander.P2HealthBar.value <= 0)
            {
                reviving = true;
                Invoke("ReviveOther", 2f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Salamander")
        {
            PlayerParent salamander = GetComponent<PlayerParent>();
            if (salamander.P2HealthBar.value <= 0)
            {
                reviveTime = 0;
                reviving = false;
            }
        }
    }

    void ReviveOther()
    {
        if (reviving)
        {
            SalamanderScript salamander = FindObjectOfType<SalamanderScript>();
            salamander.health = ((int)salamander.maxHealth)/2;
            salamander.pRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            salamander.Box.enabled = true;
            salamander.gameObject.layer = 8;
        }
    }

    void FixedUpdate()
    {
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
            }
            if (isPlayer1)
            {
                EagleReviveBG.gameObject.SetActive(false);
            }
            if (reviving)
            {
                reviveTime += Time.deltaTime;
                if (isPlayer1)
                {
                    SalamanderReviveBG.gameObject.SetActive(true);
                    SalamanderReviveFill.gameObject.SetActive(true);
                    SalamanderReviveFill.fillAmount = (reviveTime / 2);
                    if (reviveTime >= 2)
                    {
                        reviveTime = 0;
                        reviving = false;
                    }
                }
                else
                {
                    EagleReviveBG.gameObject.SetActive(true);
                    EagleReviveFill.gameObject.SetActive(true);
                    EagleReviveFill.fillAmount = (reviveTime / 2);
                    if (reviveTime >= 2)
                    {
                        reviveTime = 0;
                        reviving = false;
                    }
                }
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
                            Instantiate(smokeTrail, transform.position, this.transform.rotation);
                            smokeTrail.Play();
                            Invoke("KillSmoke", 1);


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
                            Instantiate(smokeTrail, transform.position, this.transform.rotation);
                            smokeTrail.Play();
                            Invoke("KillSmoke", 1);
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
                EagleReviveBG.gameObject.SetActive(true);

            }
            else if (!isPlayer1)
            {
                P2HealthBar.value = 0;
                SalamanderReviveBG.gameObject.SetActive(true);

            }
            pRB2D.constraints = RigidbodyConstraints2D.FreezePositionX | RigidbodyConstraints2D.FreezePositionY | RigidbodyConstraints2D.FreezeRotation;
            Box.enabled = false;
            anim.SetBool("isDead", true);
            gameObject.layer = 9;
        }
    }
}

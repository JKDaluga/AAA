﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SalamanderScript : PlayerParent {
    public GameObject slash;
    public float slashUsed;

    public float baseAttackDuration = 1f;
    public Quaternion turn;

    public AudioClip slashAttk;
    public float invulTime;
    public AudioSource source;

    public string SlashAttackName;



    public override void Ability1()
    {
        if (Time.time > slashUsed + .15)
        {
            source.PlayOneShot(slashAttk);
            Instantiate(slash, (transform.position * .8f), transform.rotation);
            slashUsed = Time.time;
        }
	}

	public override void Ability2 ()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle")
        {
            print("eagle");
            PlayerParent eagle = GetComponent<PlayerParent>();
            if(eagle.P1HealthBar.value <= 0)
            {
                print("revive");
                reviving = true;
                Invoke("ReviveOther", 2f);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle")
        {
            PlayerParent eagle = GetComponent<PlayerParent>();
            if (eagle.P1HealthBar.value <= 0)
            {
                reviveTime = 0;
                reviving = false;
            }
        }
    }

    void ReviveOther()
    {
        if(reviving)
        {
            EagleScript eagle = FindObjectOfType<EagleScript>();
            eagle.health = ((int)eagle.maxHealth)/2;
            eagle.pRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            eagle.Box.enabled = true;
            eagle.gameObject.layer = 8;
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
            }
            else
            {
                SpriteRenderer s = GetComponent<SpriteRenderer>();
                Color color = new Color(255f, 255f, 255f);
                color.a = 1f;
                s.color = color;
                gameObject.layer = 8;
                //Ability 1 Player 2
                if (((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.Mouse0))))
                {
                    StartCoroutine(setAttack());
                    Ability1(); MovementP2();
                }
            }
            horiz = Input.GetAxis("Horizontal2");
            if (this.transform.position.x < chimera.transform.position.x && !facingRight)
            {
                Flip();
            }

            else if (this.transform.position.x > chimera.transform.position.x && facingRight)
            {
                Flip();
            }
            MovementP2();
            P2HealthBar.value = ((health / maxHealth) * 100);
            

            //Ability 2 Player 2
            if ((Input.GetKeyDown(KeyCode.I)))
            {
                //Debug.Log("Am I Work?");
                //anim.SetBool("Ability2", true);
                Ability2();
                rollTime = invulTime;
                SpriteRenderer s = GetComponent<SpriteRenderer>();
                Color color = new Color(102f, 144f, 192f);
                color.a = .5f;
                s.color = color;
                rollVector = rb.velocity.normalized;
                isVulnerable = false;
                Invoke("setVulnerability", invulTime);
                Instantiate(smokeTrail, transform.position, this.transform.rotation);
                smokeTrail.Play();
                Invoke("KillSmoke", 1);
                gameObject.layer = 9;
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

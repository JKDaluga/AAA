using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SalamanderScript : PlayerParent
{
    public GameObject slash;
    public float slashUsed;
    public GameObject smoke;

    public float baseAttackDuration = 1f;
    public Quaternion turn;

    public float invulTime;

    public AudioClip slashAttk;
    public AudioClip attack;
    public AudioClip dodge;
    public AudioClip cry;
    public AudioClip revive;

    public AudioSource source;

    public string SlashAttackName;



    public override void Ability1()
    {
        if (Time.time > slashUsed + .15)
        {
            source.PlayOneShot(attack, .125f);
            Instantiate(slash, (transform.position * .8f), transform.rotation);
            slashUsed = Time.time;
        }
        //source.Play();
    }

    public override void Ability2()
    {

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle")
        {
            print("eagle");
            PlayerParent eagle = GetComponent<PlayerParent>();
            if (eagle.P1HealthBar.value <= 0)
            {
                print("revive");
                reviving = true;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag != "Eagle") return;
        if (reviving)
        {
            reviveTime += Time.deltaTime;
            if (isPlayer1)
            {
                SalamanderReviveBG.gameObject.SetActive(true);
                SalamanderReviveFill.gameObject.SetActive(true);
                SalamanderReviveFill.fillAmount = (reviveTime / 1.5f);
                if (reviveTime >= 1.5f)
                {
                    print("THING");
                    source.PlayOneShot(revive);
                    reviveTime = 0;
                    reviving = false;
                    ReviveOther();
                }
            }
            else
            {
                EagleReviveBG.gameObject.SetActive(true);
                EagleReviveFill.gameObject.SetActive(true);
                EagleReviveFill.fillAmount = (reviveTime / 1.5f);
                if (reviveTime >= 1.5f)
                {
                    print("THING");
                    reviveTime = 0;
                    reviving = false;
                    ReviveOther();
                }
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
        print("eagle");
        EagleScript eagle = FindObjectOfType<EagleScript>();
        eagle.health = ((int)eagle.maxHealth)*3 / 4;
        eagle.pRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        eagle.Box.enabled = true;
        eagle.gameObject.layer = 8;
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
        if (((health / maxHealth * 100f) <= 70) && ((health / maxHealth * 100f) > 40))
        {
            anim.SetBool("isInjured", true);
            Debug.Log("fuck");
        }

        if (((health / maxHealth * 100f) <= 40))
        {
            anim.SetBool("isCritical", true);
            anim.SetBool("isInjured", false);
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
                    if (reviveTime >= 1.5)
                    {
                        source.PlayOneShot(revive);
                        reviveTime = 0;
                        reviving = false;
                        ReviveOther();
                    }
                }
                else
                {
                    EagleReviveBG.gameObject.SetActive(true);
                    EagleReviveFill.gameObject.SetActive(true);
                    EagleReviveFill.fillAmount = (reviveTime / 2);
                    if (reviveTime >= 1.5)
                    {
                        source.PlayOneShot(revive);
                        reviveTime = 0;
                        reviving = false;
                        ReviveOther();
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
                gameObject.layer = 8;
                //Ability 1 Player 2
                if (((Input.GetKeyDown(KeyCode.W)) || (Input.GetKeyDown(KeyCode.Mouse0))))
                {
                    StartCoroutine(setAttack());
                    Ability1(); 
                    MovementP2();
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
            p2HB.fillAmount = (health / maxHealth);

            //Ability 2 Player 2
            if ((Input.GetKeyDown(KeyCode.I)) && isVulnerable)
            {
                //Debug.Log("Am I Work?");
                //anim.SetBool("Ability2", true)
                Instantiate(smoke, this.transform.position, transform.rotation);
                invulTime = 2f;
                SpriteRenderer s = GetComponent<SpriteRenderer>();
                Color color = new Color(102f, 144f, 192f);
                color.a = .5f;
                s.color = color;
                rollVector = rb.velocity.normalized;
                isVulnerable = false;
                Invoke("setVulnerability", invulTime);
                Invoke("ColorChange", invulTime);
                gameObject.layer = 11;


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
    public void ColorChange()
    {
        SpriteRenderer s = GetComponent<SpriteRenderer>();
        Color color = new Color(255f, 255f, 255f);
        color.a = 1f;
        s.color = color;
    }
}

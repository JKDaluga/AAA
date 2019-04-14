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
            }
        }
    }

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Salamander")
        {
            PlayerParent salamander = GetComponent<PlayerParent>();
            if (salamander.P2HealthBar.value <= 0)
            {
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
                            reviveTime = 0;
                            reviving = false;
                            ReviveOther();
                        }
                    }
                }
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
        SalamanderScript salamander = FindObjectOfType<SalamanderScript>();
        salamander.health = ((int)salamander.maxHealth)/2;
        salamander.pRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
        salamander.Box.enabled = true;
        salamander.gameObject.layer = 8;
    }
}

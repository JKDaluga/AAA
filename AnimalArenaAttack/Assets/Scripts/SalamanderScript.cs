using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SalamanderScript : PlayerParent {
    public GameObject slash;
    public float slashUsed;

    public float baseAttackDuration = 1f;
    public Quaternion turn;

    public AudioClip slashAttk;

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
            eagle.health = (int)eagle.maxHealth;
            eagle.pRB2D.constraints = RigidbodyConstraints2D.FreezeRotation;
            eagle.Box.enabled = true;
            eagle.gameObject.layer = 8;
        }
    }

}

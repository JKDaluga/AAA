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
        source.PlayOneShot(slashAttk);

        if (Time.time > slashUsed + .15)
        {
            Instantiate(slash, (transform.position * .8f), transform.rotation);
            slashUsed = Time.time;
        }
	}

	public override void Ability2 ()
    {

    }

}

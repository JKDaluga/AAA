using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderScript : PlayerParent {

    public Animator salamanderAnim;

    public GameObject slash;
    public float slashUsed;

    public float baseAttackDuration = 1f;
    public Quaternion turn;

    public override void Ability1()
    {
        if(Time.time > slashUsed + .25)
        {
            Instantiate(slash, (transform.position * .8f), transform.rotation);
            slashUsed = Time.time;
        }
	}

	public override void Ability2 ()
    {

    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : PlayerParent {

    public Animator eagleAnim;
    public GameObject feather;

    public float baseAttackDuration = 1f;

    public float featherSpeed = 2f;

    public float featherUsed;

	public override void Ability1()
    {
        if (Time.time > featherUsed + .25)
        {
            Instantiate(feather, transform.position, transform.rotation);
            featherUsed = Time.time;
        }
    }

    public override void Ability2() 
    {
	

	}
}

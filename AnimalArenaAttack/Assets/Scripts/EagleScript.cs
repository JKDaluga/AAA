using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleScript : PlayerParent {

    public Animator eagleAnim;
    public GameObject feather;

    public Rigidbody2D featherCloneRB;
    public float baseAttackDuration = 1f;

    public float featherSpeed = 2f;

	public override void Ability1()
    {
        GameObject featherClone = (GameObject)Instantiate(feather, transform.position, transform.rotation);

        Vector2 direction = transform.up;
        featherClone.GetComponent<Rigidbody2D>().velocity = direction * featherSpeed;

        GameObject.Destroy(featherClone, baseAttackDuration);

    }

    public override void Ability2() 
    {
	

	}
}

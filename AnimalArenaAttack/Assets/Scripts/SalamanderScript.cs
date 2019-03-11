using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderScript : PlayerParent {


    public GameObject slash;
    public float baseAttackDuration = 0f;

    private Rigidbody2D rb;


    public override void Ability1  () 
    {
        Quaternion rotation = gameObject.transform.rotation;
        Vector3 hitPos = new Vector3((gameObject.transform.position.x), (gameObject.transform.position.y), gameObject.transform.position.z);


        var slashClone = Instantiate(slash, hitPos, rotation);

        Destroy(slashClone, baseAttackDuration);
	}
	
	public override void Ability2 ()
    {
       
    }

}

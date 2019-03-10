using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderScript : MonoBehaviour {


    public GameObject salamanderFist;
    public float baseAttackDuration = 0f;

    public override void Ability1  () 
    {

        Quaternion rotation = gameObject.transform.rotation;
        Vector3 hitPos = new Vector3((gameObject.transform.position.x), (gameObject.transform.position.y), gameObject.transform.position.z);


        var currentFist = Instantiate(salamanderFist, hitPos, rotation);

        Destroy(currentFist, baseAttackDuration);
	}
	
	public override void Abililty2 ()
    {
		
	}

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SmokeCloud : MonoBehaviour {

    GameObject smoke;

	void Start () {
        smoke = this.gameObject;
        Destroy(smoke, .3f);
    }
	
	
}

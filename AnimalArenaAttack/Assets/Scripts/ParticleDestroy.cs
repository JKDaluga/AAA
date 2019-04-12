using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleDestroy : MonoBehaviour {

	// Use this for initialization
	void Start ()
    {
        Invoke("Kill", 1);
    }

    // Update is called once per frame
    void Update () {
		
	}
    /*
    void Kill()
    {
        if (ParticleSystem.IsAlive())
        {
            smokeTrail.Stop();
            smokeTrail.Clear();
            Destroy(smokeTrail.gameObject);
        }
    }
    */
}

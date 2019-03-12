using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CleaveBehaviour : MonoBehaviour {

    public float warningTime;
    public float activeTime;

	void Start ()
    {
        Invoke("Attack", warningTime);
	}
	
    private void Attack()
    {
        GetComponent<PolygonCollider2D>().enabled = true;
        GetComponent<SpriteRenderer>().color = Color.white;
        Invoke("Stop", activeTime);
    }

    private void Stop()
    {
        Destroy(gameObject);
    }
}

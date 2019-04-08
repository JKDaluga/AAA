using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireGround : MonoBehaviour {

    public ParticleSystem burntSpot;

    public float timer;
    public int damage;

	// Use this for initialization
	void Start () {
        Instantiate(burntSpot, transform.position, Quaternion.Euler(new Vector3(-90, transform.rotation.y, transform.rotation.z)));

    }

    // Update is called once per frame
    void Update () {
        timer -= Time.deltaTime;
		
        if(timer <= 0)
        {
            Destroy(this.gameObject);

        }
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle" || col.gameObject.tag == "Salamander")
        {
            col.gameObject.GetComponent<PlayerParent>().Damage(damage);
        }
    }


    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle" || col.gameObject.tag == "Salamander")
        {
            col.gameObject.GetComponent<PlayerParent>().Damage(damage);
        }
    }
}

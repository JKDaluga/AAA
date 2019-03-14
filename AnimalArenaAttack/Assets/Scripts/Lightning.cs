using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {
    public float timer;
    public GameObject lighting;
    public GameObject FireGround;
    public int damage;
    bool spawned = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if (timer <= 1.5f)
        {
            lighting.SetActive(true);
        }
        if (timer <= 1)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            if(!spawned)
            {
                Instantiate(FireGround, transform.position, transform.rotation);
                spawned = true;
            }
            
        }
        if (timer <= .9f)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (timer <= 0)
        {
            DestroyMe();
        }
	}

    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle" || col.gameObject.tag == "Salamander")
        {
            col.gameObject.GetComponent<PlayerParent>().Damage(damage);
        }
    }
}

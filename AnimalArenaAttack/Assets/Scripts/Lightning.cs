using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lightning : MonoBehaviour {
    public float timer;
    public GameObject lighting;
    public GameObject FireGround;
    public int damage;
    bool spawned = false;
    public bool trackP1;
    public Transform tracker;

	// Use this for initialization
	void Start () {
        if (trackP1)
        {
            tracker = GameObject.FindGameObjectWithTag("Eagle").GetComponent<Transform>();
        }
        else
        {
            tracker = GameObject.FindGameObjectWithTag("Salamander").GetComponent<Transform>();
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer >= 2)
        {
            transform.position = tracker.position;
        }
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

﻿using System.Collections;
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
    public AudioSource source;
    public AudioClip lightning;
    bool played = false;

    //public float shakeDuration;
    //public float shakeMagnitude;

    private IEnumerator coroutine;

	// Use this for initialization
	void Start () {
        source = GameObject.Find("Chimera").GetComponent<AudioSource>();
        if (trackP1)
        {
            if (GameObject.FindGameObjectWithTag("Eagle").GetComponent<EagleScript>().health != 0)
            {
                tracker = GameObject.FindGameObjectWithTag("Eagle").GetComponent<Transform>();
            }
            else
            {
                tracker = GameObject.FindGameObjectWithTag("Salamander").GetComponent<Transform>();
            }
        }
        else 
        {
            if (GameObject.FindGameObjectWithTag("Salamander").GetComponent<SalamanderScript>().health != 0)
            {
                tracker = GameObject.FindGameObjectWithTag("Salamander").GetComponent<Transform>();
            }
            else
            {
                tracker = GameObject.FindGameObjectWithTag("Eagle").GetComponent<Transform>();
            }
        }
    }
	
	// Update is called once per frame
	void Update () {
        timer -= Time.deltaTime;
        if(timer >= 2)
        {
            transform.position = tracker.position;
        }
        if (timer <= 2)
        {
            SpriteRenderer s = this.transform.Find("Telegraph").GetComponent<SpriteRenderer>();
            Color color = new Color(255f, 0, 0);
            color.a = 255f;
            s.color = color;
        }
        
        if (timer <= 1.5f)
        {
            if (played == false)
            {
                source.PlayOneShot(lightning, .3f);
                played = true;
            }
            lighting.SetActive(true);

            //coroutine = Shake((shakeDuration / 100), (shakeMagnitude / 100));
            //StartCoroutine(coroutine);
        }
        if (timer <= 1)
        {
            GetComponent<BoxCollider2D>().enabled = true;
            if(!spawned)
            {
                Instantiate(FireGround,  new Vector3 (transform.position.x, transform.position.y-.65f, transform.position.z), transform.rotation);
                spawned = true;
            }
            
        }
        if (timer <= .95f)
        {
            GetComponent<BoxCollider2D>().enabled = false;
        }
        if (timer <= .9f)
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

    public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = Camera.main.transform.position;
        float timeElapsed = 0f;

        if (timeElapsed <= duration)
        {
            float xRange = Random.Range(-1f, 1f) * magnitude;
            float yRange = Random.Range(-1f, 1f) * magnitude;

            Camera.main.transform.position = new Vector3(xRange, yRange, originalPosition.z);
            timeElapsed += Time.deltaTime;

            yield return null;

        }

        Camera.main.transform.position = originalPosition;
    }
}

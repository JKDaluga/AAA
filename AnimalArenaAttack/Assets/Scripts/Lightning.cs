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

    //public float shakeDuration;
    //public float shakeMagnitude;

    private IEnumerator coroutine;

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

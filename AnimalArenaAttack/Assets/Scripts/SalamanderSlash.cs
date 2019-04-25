using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SalamanderSlash : MonoBehaviour {

    public int damage = 15;
    public float timer = 0;
    float speed = 0f;
    GameObject GameManager;
    public AudioSource source;
    public AudioClip slashDagger;


    public GameObject chimera;

    // Use this for initialization
    void Start()
    {
        source = GameObject.FindGameObjectWithTag("Salamander").GetComponent<AudioSource>();
        chimera = GameObject.FindGameObjectWithTag("Monster");
        speed *= Time.deltaTime;
        GameManager = GameObject.Find("GameManager");

    }

    private void FixedUpdate()
    {
        if (timer >= .05f)
        {
            GameObject.Destroy(gameObject);
        }
        timer += 1.0F * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, chimera.transform.position, speed);
        //transform.up = chimera.transform.position - transform.position;

    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            //source.PlayOneShot(slashDagger);
            col.gameObject.GetComponent<ChimeraController>().Damage(damage);
            //GameManager.GetComponent<AnalyticManager>().salamander = 15;
        }
    }
}

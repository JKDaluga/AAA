using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFeather : MonoBehaviour {

    public ParticleSystem featherBurst;
    GameObject GameManager;
    public int damage = 5;
    public float timer = 0;
    float speed = 10f;

    public GameObject chimera;
    public GameObject RedHead;

    // Use this for initialization
    void Start ()
    {
        chimera = GameObject.FindGameObjectWithTag("Monster");
        speed *= Time.deltaTime;
        GameManager = GameObject.Find("GameManager");
    }

    private void FixedUpdate()
    {
        if (timer >= .8)
        {
            GameObject.Destroy(gameObject);
        }
        timer += 1.0F * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, RedHead.transform.position, speed);
        transform.up = RedHead.transform.position - transform.position;


    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            GameManager.GetComponent<AnalyticManager>().eagle += damage;
            col.gameObject.GetComponent<ChimeraController>().Damage(damage);

            Vector3 featherBurstPos = new Vector3(transform.position.x, transform.position.y + 0.75f, transform.position.z);

            GameObject fBurst = Instantiate(featherBurst.gameObject, featherBurstPos, transform.rotation);
            featherBurst.Play();
            Destroy(fBurst, 1);
            GameObject.Destroy(gameObject);
        }
    }
}

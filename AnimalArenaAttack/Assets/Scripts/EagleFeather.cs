using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFeather : MonoBehaviour {

    public int damage = 5;
    public float timer = 0;
    float speed = 10f;

    public GameObject chimera;

    // Use this for initialization
    void Start ()
    {
        chimera = GameObject.FindGameObjectWithTag("Monster");
        speed *= Time.deltaTime;
    }

    private void FixedUpdate()
    {
        if (timer >= .8)
        {
            GameObject.Destroy(gameObject);
        }
        timer += 1.0F * Time.deltaTime;
        
        transform.position = Vector3.MoveTowards(transform.position, chimera.transform.position, speed);
        transform.right = chimera.transform.position - transform.position;


    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Monster")
        {
            col.gameObject.GetComponent<ChimeraController>().Damage(damage);
            GameObject.Destroy(gameObject);
        }
    }
}

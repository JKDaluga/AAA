using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EagleFeather : MonoBehaviour {

    public int damage = 5;
    public float timer = 0;
    public float speed = 2f;

    public GameObject chimera;

    // Use this for initialization
    void Start ()
    {
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
        
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Chimera")
        {
            col.gameObject.GetComponent<ChimeraController>().Damage(damage);
        }
    }
}

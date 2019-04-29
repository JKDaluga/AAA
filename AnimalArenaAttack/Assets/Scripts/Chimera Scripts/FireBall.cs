using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    public AudioClip expl;
   //public AudioClip explosion;

    public GameplayManager gpm;
    public ParticleSystem fireBurst;

    public int damage = 30;
    public float fireBallSpeed;

    public AudioSource src;

    // Use this for initialization
    void Start()
    {
        gpm = FindObjectOfType<GameplayManager>();
        GetComponent<Rigidbody2D>().velocity = -transform.up.normalized * fireBallSpeed;
        src = gpm.GetComponent< AudioSource > ();
        Destroy(gameObject, 5);
        
    }

    // Update is called once per frame
    void Update()
    {

    }
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle" || col.gameObject.tag == "Salamander")
        {
            GameObject fire = Instantiate(fireBurst.gameObject, transform.position, transform.rotation);
            src.PlayOneShot(expl,.01f);
            fireBurst.Play();

            Destroy(fire, .3f);

            col.gameObject.GetComponent<PlayerParent>().Damage(damage);
            GameObject.Destroy(gameObject);
            Invoke("KillBurst", 1);
        }
    }
    void KillSmoke()
    {
        if (fireBurst.IsAlive())
        {
            fireBurst.Stop();
            fireBurst.Clear();
            Destroy(fireBurst.gameObject);
        }
    }
}

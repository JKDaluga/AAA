using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterChild : MonoBehaviour {

    public int damage = 5;

    void OnTriggerStay2D(Collider2D col)
    {
        if (col.gameObject.tag == "Eagle" || col.gameObject.tag == "Salamander")
        {
            col.gameObject.GetComponent<PlayerParent>().Damage(damage);
        }
    }
}

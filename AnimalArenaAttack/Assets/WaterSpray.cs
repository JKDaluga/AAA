using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpray : MonoBehaviour {

    public float minAngle = -80f;
    public float maxAngle = 80f;
    public float progress = 0;
    public float timeToRotate ;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        progress += Time.deltaTime / timeToRotate;
        float angle = Mathf.LerpAngle(maxAngle, minAngle, progress);
        transform.eulerAngles = new Vector3(0, 0,angle);
        if(progress >= 1)
        {
            Destroy(this.gameObject);
        }
    }
}

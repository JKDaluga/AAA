using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpray : MonoBehaviour {

    public float minAngle = -80f;
    public float maxAngle = 80f;
    public float progress = 0f;
    public float timeToRotate;
    public bool startLeft;

    // Use this for initialization
    void Start () {
        if(startLeft) transform.rotation = Quaternion.Euler(0, 0, -80);
        else transform.rotation = Quaternion.Euler(0, 0, 80);
    }
	
	// Update is called once per frame
	void Update () {
        progress += Time.deltaTime / timeToRotate;
        if (progress > 0f)
        {
            float angle = Mathf.LerpAngle(maxAngle, minAngle, progress);
            transform.eulerAngles = new Vector3(0, 0, angle);
        }
        if(progress >= 1)
        {
            Destroy(this.gameObject);
        }
    }
    
}

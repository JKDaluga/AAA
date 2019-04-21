using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaterSpray : MonoBehaviour {

    public ParticleSystem waterSpray;

    public float minAngle = -80f;
    public float maxAngle = 80f;
    public float progress = 0f;
    public float timeToRotate;
    public bool startLeft;

    public float xOffset = 0f;
    public float yOffset = 0f;
    
    private GameObject waterSprayObject;

    // Use this for initialization
    void Start () {
        if(startLeft) transform.rotation = Quaternion.Euler(0, 0, -80);
        else transform.rotation = Quaternion.Euler(0, 0, 80);

        Vector3 waterSprayPos = new Vector3(gameObject.transform.position.x + xOffset, gameObject.transform.position.y + yOffset, 0);
        waterSprayObject = Instantiate(waterSpray.gameObject, waterSprayPos, transform.rotation);
        waterSpray.Play();
    }
	
	// Update is called once per frame
	void Update () {
        progress += Time.deltaTime / timeToRotate;
        if (progress > 0f)
        {
            float angle = Mathf.LerpAngle(maxAngle, minAngle, progress);
            transform.eulerAngles = new Vector3(0, 0, angle);

            
        }
        if (progress >= 1)
        {
            Destroy(this.gameObject);
            Destroy(waterSprayObject);
        }
    }
    
}

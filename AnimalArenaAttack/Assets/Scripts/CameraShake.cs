using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour {

	public IEnumerator Shake(float duration, float magnitude)
    {
        Vector3 originalPosition = transform.localPosition;
        float timeElapsed = 0f;

        while(timeElapsed > 0)
        {
            float xRange = Random.Range(-1f, 1f) * magnitude;
            float yRange = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(xRange, yRange, originalPosition.z);
            timeElapsed += Time.deltaTime;

            yield return null;

        }

        transform.localPosition = originalPosition;
    }
}

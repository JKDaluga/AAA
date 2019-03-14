using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterSelectManager : MonoBehaviour
{
    bool p1Sel = false;
    bool p2Sel = false;

    // Use this for initialization
    void Update()
    {
       
        if (Input.GetKey(KeyCode.W))
        {
            p1Sel = true;
        }
        if (Input.GetKey(KeyCode.V))
        {
            p2Sel = true;
        }
        if (((Input.GetKey(KeyCode.Alpha1)) && (Input.GetKey(KeyCode.Alpha2))))
        {
            SceneManager.LoadScene(0);
        }
        if (p2Sel == true && p1Sel == true)
        {
            StartCoroutine(WaitForSceneSwitch());
        }
    }

    private IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class InstructionsScreen : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {
        StartCoroutine(WaitForSceneSwitch());
    }


    private IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(10f);
        SceneManager.LoadScene(3);
    }
}
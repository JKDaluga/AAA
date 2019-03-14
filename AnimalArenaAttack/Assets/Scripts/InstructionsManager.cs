using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    public Text timer;
    int timeLeft = 5;
    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        StartCoroutine(WaitForSceneSwitch());
    }

    private void Update()
    {
        if (((Input.GetKey(KeyCode.Alpha1)) && (Input.GetKey(KeyCode.Alpha2))))
        {
            SceneManager.LoadScene(0);
        }
        timer.text = ("" + timeLeft);
    }
    private IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(timeLeft);
        SceneManager.LoadScene(3);
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }

}
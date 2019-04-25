using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InstructionsManager : MonoBehaviour
{
    public GameObject instructions1;
    public Text timer;
    public int timeLeft = 5;
    public AudioClip music;
    public AudioSource src;

    public Animator transAnim;

    // Use this for initialization
    void Start()
    {
        src = this.gameObject.GetComponent<AudioSource>();
        src.PlayOneShot(music, .5f);
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        StartCoroutine(WaitForSceneSwitch());
        StartCoroutine(waitToTransition());
    }

    private void Update()
    {
        if (((Input.GetKey(KeyCode.Alpha1)) && (Input.GetKey(KeyCode.Alpha2))))
        {
            SceneManager.LoadScene(0);
        }
        timer.text = ("" + timeLeft);
        if (timeLeft==7)
        {
            instructions1.SetActive(false);
        }
    }
    private IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(timeLeft);
     
        SceneManager.LoadScene(2);
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    private IEnumerator waitToTransition()
    {
        yield return new WaitForSeconds(14.5f);
        transAnim.SetBool("gameEnd", true);
    }

}
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip battleMusic;
    public AudioClip timerWarning;
    public AudioClip roar;
   // public AudioClip expl;

    public GameObject p1;
    public GameObject p2;
    public GameObject chimera;

    public Animator transAnim;


    int p1h;
    int p2h;
    int ch;

    bool canCount = true;
    bool canRoar = true;

    public Text timer;
    public int timeLeft = 90;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("MusicStart");
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
        int p1h = p1.GetComponent<PlayerParent>().health;
        int p2h = p2.GetComponent<PlayerParent>().health;
        int ch = chimera.GetComponent<ChimeraController>().Health;

    }

    // Update is called once per frame
    void Update()
    {
        p1h = p1.GetComponent<PlayerParent>().health;
        p2h = p2.GetComponent<PlayerParent>().health;
        ch = chimera.GetComponent<ChimeraController>().Health;
        if (ch <= 0)
        {
            EndDestruction();
            StartCoroutine(WaitForWin());
        }
        if (p1h <= 0 && p2h <= 0)
        {
            EndDestruction();
            StopCoroutine("LoseTime");
            StartCoroutine(WaitForLose());
        }
        if (((Input.GetKey(KeyCode.Alpha1)) && (Input.GetKey(KeyCode.Alpha2))))
        {
            SceneManager.LoadScene(0);
        }
        timer.text = ("" + timeLeft);
        if (timeLeft == 0)
        {
            StopCoroutine("LoseTime");
            StartCoroutine(WaitForLose());
        }
        if (timeLeft == 30 || timeLeft==60)
        {
            StartCoroutine("TimeNotice");
        }
        if (timeLeft <= 15)
        {
            timer.color = Color.red;
        }
        if (timeLeft == 10)
        {
            countdownTimer();
        }
    }
  
    private IEnumerator TimeNotice()
    {
        timer.color = Color.red;
        //timer.transform.localScale *= 1.1f;
        yield return new WaitForSeconds(1f);
        timer.color = Color.white;
    }

    private void countdownTimer()
    {
        if (timeLeft == 10 && canCount == true)
        {
            audioSource.PlayOneShot(timerWarning,.9f);
            canCount = false;
        }
    }

    private IEnumerator WaitForWin()
    {
        if ( canRoar == true)
        {
            audioSource.PlayOneShot(roar);
            canRoar = false;
        }

        transAnim.SetBool("gameEnd", true);
        yield return new WaitForSeconds(.75f);
        SceneManager.LoadScene(3);
    }

    IEnumerator MusicStart()
    {
        yield return new WaitForSeconds(1.5f);
        audioSource.PlayOneShot(battleMusic);
    }

    IEnumerator WaitForLose()
    {
        transAnim.SetBool("gameEnd", true);
        yield return new WaitForSeconds(.5f);
        SceneManager.LoadScene(4);
    }
    IEnumerator LoseTime()
    {
        while (true)
        {
            yield return new WaitForSeconds(1);
            timeLeft--;
        }
    }
    private void EndDestruction()
    {
        Destroy(GameObject.FindWithTag("ChimeraWater"));
        Destroy(GameObject.FindWithTag("ChimeraFire"));
        Destroy(GameObject.FindWithTag("PlayerAttack"));

    }
}
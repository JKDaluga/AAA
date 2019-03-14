﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameplayManager : MonoBehaviour
{
    private AudioManager audioManager;

    public string FightMusicName;

    public GameObject p1;
    public GameObject p2;
    public GameObject chimera;

    public int p1h;
    public int p2h;
    public int ch;

    public Text timer;
    // Use this for initialization
    void Start()
    {
        int p1h = p1.GetComponent<PlayerParent>().health;
        int p2h = p2.GetComponent<PlayerParent>().health;
        int ch = chimera.GetComponent<ChimeraController>().Health;

        audioManager = AudioManager.instance;
        if (audioManager == null)
        {
            Debug.LogError("No AudioManager found in the scene.");
        }
        audioManager.PlaySound(FightMusicName);
    }

    // Update is called once per frame
    void Update()
    {
        p1h = p1.GetComponent<PlayerParent>().health;
        p2h = p2.GetComponent<PlayerParent>().health;
        ch = chimera.GetComponent<ChimeraController>().Health;
        if (ch <= 0)
        {
            StartCoroutine(WaitForWin());                
        }
        if(p1h <= 0 && p2h <= 0)
        {
            StartCoroutine(WaitForLose());
        }
        if (((Input.GetKey(KeyCode.Alpha1)) && (Input.GetKey(KeyCode.Alpha2))))
        {
            SceneManager.LoadScene(0);
        }

    }
    private IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(4);
    }

    IEnumerator WaitForLose()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(5);
    }

}
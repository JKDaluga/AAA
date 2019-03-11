﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckIfDead : MonoBehaviour
{

    public GameObject p1;
    public GameObject p2;
    public GameObject chimera;

    public int p1h;
    public int p2h;
    public int ch;
    // Use this for initialization
    void Start()
    {
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
        if ((p1h == 0 && p2h == 0) || ch == 0)
        {
            SceneManager.LoadScene(3);

        }
    }
}
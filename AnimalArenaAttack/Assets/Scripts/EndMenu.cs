﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndMenu : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey)
        {
            StartCoroutine(WaitForSceneSwitch());

        }
    }
    private IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(0);
    }
}
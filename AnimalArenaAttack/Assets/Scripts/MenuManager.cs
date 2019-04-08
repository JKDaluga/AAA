using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public AudioClip MenuMusic;

    public AudioSource source;
    // Use this for initialization
    void Start()
    {
        source.PlayOneShot(MenuMusic);
    }

    // Update is called once per frame
    void Update()
    {
        if (((Input.GetKey(KeyCode.Alpha1)) && (Input.GetKey(KeyCode.Alpha2))))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A) ||
                  Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.S) ||
                  Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Q) ||
                  Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.W) ||
                  Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.I) ||
              Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.K))
        {
            SceneManager.LoadScene(1);
        }
    }
}
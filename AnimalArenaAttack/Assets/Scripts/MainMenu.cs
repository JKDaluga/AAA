using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
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
        if (Input.anyKeyDown)
        {
            SceneManager.LoadScene(2);
        }
    }

}
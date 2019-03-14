using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLoseManager : MonoBehaviour {

    public AudioClip PlayerLoseMusic;

    public AudioSource source;

    private bool canSwitch = false;
    // Use this for initialization
    void Start()
    {
        source.PlayOneShot(PlayerLoseMusic);
        StartCoroutine(WaitForSceneSwitch());

    }

    // Update is called once per frame
    void Update()
    {
        if (canSwitch==true && Input.anyKey)
        {
            SceneManager.LoadScene(0);
        }
    }
    private IEnumerator WaitForSceneSwitch()
    {
        yield return new WaitForSeconds(5f);
        canSwitch = true;
    }
}

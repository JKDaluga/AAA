using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EndLoseMenu : MonoBehaviour {

    public AudioClip PlayerLoseMusic;

    public AudioSource source;
    // Use this for initialization
    void Start()
    {
        source.PlayOneShot(PlayerLoseMusic);
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

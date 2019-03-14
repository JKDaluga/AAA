using System.Collections;
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
    public int timeLeft = 90;

    // Use this for initialization
    void Start()
    {
        StartCoroutine("LoseTime");
        Time.timeScale = 1;
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
            EndDestruction();
            StartCoroutine(WaitForWin());
        }
        if (p1h <= 0 && p2h <= 0)
        {
            EndDestruction();
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
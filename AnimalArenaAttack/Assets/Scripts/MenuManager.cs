using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public Animator transAnim;

    // Update is called once per frame
    void Update()
    {
        if (((Input.GetKey(KeyCode.Alpha1)) && (Input.GetKey(KeyCode.Alpha2))))
        {
            SceneManager.LoadScene(0);
        }
        else if (Input.anyKey)
        {
            StartCoroutine(switchScene());
        }
    }
    private IEnumerator switchScene()
    {
        transAnim.SetBool("gameEnd", true);
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(1);
    }
}
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
        else if (Input.GetKey(KeyCode.Z) || Input.GetKey(KeyCode.A) ||
                  Input.GetKey(KeyCode.X) || Input.GetKey(KeyCode.S) ||
                  Input.GetKey(KeyCode.C) || Input.GetKey(KeyCode.Q) ||
                  Input.GetKey(KeyCode.V) || Input.GetKey(KeyCode.W) ||
                  Input.GetKey(KeyCode.B) || Input.GetKey(KeyCode.I) ||
              Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.K)||
              Input.GetKey(KeyCode.Alpha6) || Input.GetKey(KeyCode.Alpha5) ||
              Input.GetKey(KeyCode.RightBracket) || Input.GetKey(KeyCode.C)
                  )
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
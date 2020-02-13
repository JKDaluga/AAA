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
        else if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.A) ||
                  Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) ||
                  Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) ||
                  Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) ||
                  Input.GetKey(KeyCode.Return) || Input.GetKey(KeyCode.J) ||
              Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.G)||
              Input.GetKey(KeyCode.F) || Input.GetKey(KeyCode.M) ||
              Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.N)
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
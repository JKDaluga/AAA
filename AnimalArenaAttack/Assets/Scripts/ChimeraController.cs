using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraController : MonoBehaviour {

    private int Health = 1000;

	// Use this for initialization
	void Start () {
        Invoke("combat", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void combat()
    {
        randomAbility();
        Invoke("combat", 5f);
    }

    void randomAbility()
    {
        float rand = Random.Range(0f, 4f);
        if(rand < 1)
        {
            Ability1();
        }
        else if (rand < 2)
        {
            Ability2();
        }
        else if (rand < 3)
        {
            Ability3();
        }
        else
        {
            Ability4();
        }
    }

    void Ability1()
    {
        print("Ability 1");
    }

    void Ability2()
    {
        print("Ability 2");
    }

    void Ability3()
    {
        print("Ability 3");
    }

    void Ability4()
    {
        print("Ability 4");
    }
}

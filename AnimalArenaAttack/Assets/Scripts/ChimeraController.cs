using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChimeraController : MonoBehaviour {

    private int Health = 1000;

    public GameObject cleave;
    public GameObject Electric;
    public GameObject Water;
    public GameObject FireBreath;

	// Use this for initialization
	void Start () {
        Invoke("combat", 2f);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void combat()
    {
        //randomAbility();
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

    //Cleave
    void Ability1()
    {
        Instantiate(cleave);
        print("Ability 1");
    }

    //Electric Floor
    void Ability2()
    {
        Instantiate(Electric);
        print("Ability 2");
    }

    //Water Spray
    void Ability3()
    {
        Instantiate(Water);
        print("Ability 3");
    }

    //Fire Breath
    void Ability4()
    {
        Instantiate(FireBreath);
        print("Ability 4");
    }

    void Damage(int damage)
    {
        Health -= damage;
        if(Health < 0)
        {
            Health = 0;
        }
    }
}

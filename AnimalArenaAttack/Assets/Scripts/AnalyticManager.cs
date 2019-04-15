using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AnalyticManager : MonoBehaviour {

    public Text eagleDmg;
    public Text salamanderDmg;
    public GameObject Chimera;
    public GameObject Eagle;
    public GameObject Salamander;
    public GameObject EagleFeather;
    public GameObject SalamanderSlash;

    int health = 5000;
    public int eagle;
    public int salamander;

	// Use this for initialization
	void Start ()
    {
	}
	
	// Update is called once per frame
	void Update ()
    {
        eagleDmg.text = ("Eagle Damage: " + eagle);
        salamanderDmg.text = ("Salamander Damage: " + (health-eagle));
	}
}

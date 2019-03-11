using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChimeraController : MonoBehaviour
{

    public int Health = 1000;

    //Attack Game Objects
    public GameObject cleave;
    public GameObject cleaveWarning;

    public GameObject electric;
    public GameObject waterSpray;
    public GameObject fireBall;

    public GameObject redHead;


    public float fireBallSpeed = 0f;

    public Slider HealthBar;



    public float baseAttackDuration = 0f;
    public float cleaveWarningDuration = 0f;


    public float xCleaveDisplacement = 0f;
    public float yCleaveDisplacement = 0f;

    //Use this for initialization
    void Start()
    {

        StartCoroutine(AttackPattern());


    }

    //Update is called once per frame
    void Update()
    {
        HealthBar.value = Health;
    }

    //Method determines how much damage is delt to the Chimera
    public void Damage(int damage)
    {
        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    void Electric()
    {

    }

    public void FireBall(Vector2 target)
    {

        var fireBallClone = (GameObject)Instantiate(fireBall, redHead.transform.position, transform.rotation);

        //Direction of velocity vector equals direction passed in multiplied by a float
        Vector2 direction = target;
        fireBallClone.GetComponent<Rigidbody2D>().velocity = direction * fireBallSpeed;
    }

    void FlameBreath()
    {

        Vector2 direction1 = new Vector2(-5, 0);
        Vector2 direction2 = new Vector2(-4.5f, 0);
        Vector2 direction3 = new Vector2(-4, 0);
        Vector2 direction4 = new Vector2(-3.5f, 0);
        Vector2 direction5 = new Vector2(-3, 0);

        FireBall(direction1);
        FireBall(direction2);
        FireBall(direction3);
        FireBall(direction4);
        FireBall(direction5);

        Vector2 direction6 = new Vector2(5, 0);
        Vector2 direction7 = new Vector2(4.5f, 0);
        Vector2 direction8 = new Vector2(4, 0);
        Vector2 direction9 = new Vector2(3.5f, 0);
        Vector2 direction10 = new Vector2(3, 0);

        FireBall(direction6);
        FireBall(direction7);
        FireBall(direction8);
        FireBall(direction9);
        FireBall(direction10);

        Vector2 direction11 = new Vector2(0, -5);
        Vector2 direction12 = new Vector2(0, -4.5f);
        Vector2 direction13 = new Vector2(0, -4);
        Vector2 direction14 = new Vector2(0, -3.5f);
        Vector2 direction15 = new Vector2(0, -3);

        FireBall(direction11);
        FireBall(direction12);
        FireBall(direction13);
        FireBall(direction14);
        FireBall(direction15);



    }

    void WaterSprayAttack()
    {
        var waterSprayClone = (GameObject)Instantiate(waterSpray, redHead.transform.position, transform.rotation);
    }

    void CleaveAttack()
    {
        Quaternion rotation = gameObject.transform.rotation;
        Vector3 startPos = new Vector3(gameObject.transform.position.x + xCleaveDisplacement, gameObject.transform.position.y + yCleaveDisplacement, gameObject.transform.position.z);

        var cleaveWarningClone = Instantiate(cleaveWarning, startPos, rotation);

        Destroy(cleaveWarningClone, cleaveWarningDuration);


        if (!(gameObject.activeInHierarchy))
        {
            var cleaveClone = Instantiate(cleave, startPos, rotation);
            Destroy(cleaveClone, baseAttackDuration);
        }


    }

    //Chimera attack pattern
    IEnumerator AttackPattern()
    {
        int randomInt = Random.Range(0, 3);
        if (randomInt == 0)
        {
            FlameBreath();
            yield return new WaitForSeconds(1.3f);
        }
        if (randomInt == 1)
        {
            WaterSprayAttack();
            yield return new WaitForSeconds(4f);
        }
        if (randomInt == 2)
        {
            CleaveAttack();
            yield return new WaitForSeconds(3f);
        }
        if (randomInt == 3)
        {
            yield return new WaitForSeconds(1f);

        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(AttackPattern());
    }

}


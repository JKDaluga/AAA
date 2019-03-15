using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChimeraController : MonoBehaviour
{

    public int Health;
    float maxHealth;
    public GameObject salamander;
    public GameObject eagle;

    private SpriteRenderer sp;
    //Attack Game Objects
    public GameObject cleave;
    public GameObject cleaveWarning;

    public GameObject electric;
    public GameObject waterSpray;
    public GameObject fireBall;
    public GameObject fireBall2;

    public GameObject redHead;

    public Slider HealthBar;



    public float baseAttackDuration = 0f;
    public float cleaveWarningDuration = 0f;


    public float xCleaveDisplacement = 0f;
    public float yCleaveDisplacement = 0f;

    public Color normal, hurt;

    //Use this for initialization
    void Start()
    {

        StartCoroutine(AttackPattern());
        sp = GetComponent<SpriteRenderer>();
        sp.color = normal;
        Health = 2000;
        maxHealth = Health;


    }

    //Update is called once per frame
    void Update()
    {
        HealthBar.value = ((Health / maxHealth) * 100);
    }

    //Method determines how much damage is delt to the Chimera
    public void Damage(int damage)
    {
        StartCoroutine(getHurt());

        Health -= damage;
        if (Health < 0)
        {
            Health = 0;
        }
    }

    void Electric()
    {
        StartCoroutine("lightningStrikes");
    }

    IEnumerator lightningStrikes()
    {
        for (int i = 0; i < 2; i++)
        {
            float time = 0;
            var lightningClone1 = (GameObject)Instantiate(electric, salamander.transform.position, transform.rotation);
            while (time < 1f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            lightningClone1 = (GameObject)Instantiate(electric, eagle.transform.position, transform.rotation);
            while (time < 1f)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
    }


    IEnumerator FireBall()
    {
        for(int i = 0; i < 2; i++)
        {
            float time = 0;
            var fireBallClone1 = (GameObject)Instantiate(fireBall, redHead.transform.position, transform.rotation);
            while (time < 1.5f)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            fireBallClone1 = (GameObject)Instantiate(fireBall2, redHead.transform.position, transform.rotation);
            while (time < 1.5f)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
    }

    void FlameBreath()
    {
        StartCoroutine("FireBall");
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
            yield return new WaitForSeconds(6f);
        }
        if (randomInt == 1)
        {
            WaterSprayAttack();
            yield return new WaitForSeconds(4f);
        }
        if (randomInt == 2)
        {
            Electric();
            yield return new WaitForSeconds(1f);

        }

        yield return new WaitForSeconds(1f);
        StartCoroutine(AttackPattern());
    }

    private IEnumerator getHurt()
    {
        sp.color = hurt;
        yield return new WaitForSeconds(.5f);
        sp.color = normal;
    }


}


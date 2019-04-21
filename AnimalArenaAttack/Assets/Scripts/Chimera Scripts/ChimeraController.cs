using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ChimeraController : MonoBehaviour
{
    public GameObject darkMask;
    public int Health;
    float maxHealth;
    public GameObject salamander;
    public GameObject eagle;

    public Animator anim;

    public int attackRepeatAmount;

    private SpriteRenderer sp;
    //Attack Game Objects
    public GameObject cleave;
    public GameObject cleaveWarning;

    public GameObject electric;
    public GameObject waterSpray;
    public GameObject waterSpray2;
    public GameObject fireBall;
    public GameObject fireBall2;
    public GameObject fireBallBig;
    public GameObject fireBallBig2;
    public SpriteRenderer Gem;

    //varbiles for no repeating attacks
    public int  fireBallCounter = 0;
    public int  waterCounter = 0;
    public int electricCounter = 0;

    bool fireBallUpgrade = false;
    bool waterUpgrade = false;
    bool lightningUpgrade = false;

    public GameObject redHead;

    public Slider HealthBar;

    public float baseAttackDuration = 0f;
    public float cleaveWarningDuration = 0f;


    public float xCleaveDisplacement = 0f;
    public float yCleaveDisplacement = 0f;

    public Color normal, hurt;

    public AudioSource source;

    public AudioClip thunder;
    public AudioClip fireball;
    public AudioClip water;
    float waterVol = .25f;
    public AudioClip roar;
    public AudioClip salamanderSlash;

    //Use this for initialization
    void Start()
    {
        sp = GetComponent<SpriteRenderer>();
        sp.color = normal;
        maxHealth = Health;

        fireBallCounter = 0;
        waterCounter = 0;
        electricCounter = 0;

        fireBallUpgrade = false;
        waterUpgrade = false;
        lightningUpgrade = false;

        StartCoroutine(playRoar());
        //Invoke();
        Invoke("FirstAttack", 1.4f);
    }

    public void FirstAttack()
    {
        StartCoroutine(AttackPattern());
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
        source.PlayOneShot(thunder,.3f);
        StartCoroutine("summonLightning");
        float delay = 1.5f;
        for (int i = 0; i < 2; i++)
        {
            float time = 0;

            //darkMask.SetActive(true);

            var lightningClone1 = (GameObject)Instantiate(electric, salamander.transform.position, transform.rotation);
            lightningClone1.GetComponent<Lightning>().trackP1 = false;
            while (time < delay)
            {
                time += Time.deltaTime;
                yield return null;
            }
            time = 0;
            lightningClone1 = (GameObject)Instantiate(electric, eagle.transform.position, transform.rotation);
            lightningClone1.GetComponent<Lightning>().trackP1 = true;
            while (time < delay)
            {
                time += Time.deltaTime;
                yield return null;
            }
        }
        //Invoke("DarkMaskWait", 2f);        
    }

    void DarkMaskWait()
    {
        darkMask.SetActive(false);
    }


    IEnumerator FireBall()
    {

            if (Health / maxHealth >= .75f)
            {
                for (int i = 0; i < 2; i++)
                {
                    StartCoroutine(playFire());
                    source.PlayOneShot(fireball);
                    float delay = 1.5f;
                    float time = 0;
                    yield return new WaitForSeconds(0.5f);
                    var fireBallClone1 = (GameObject)Instantiate(fireBallBig, redHead.transform.position, transform.rotation);
                    while (time < delay)
                    {
                        time += Time.deltaTime;
                        yield return null;
                    }
                    time = 0;
                    StartCoroutine(playFire());
                    source.PlayOneShot(fireball);
                    yield return new WaitForSeconds(0.5f);
                    fireBallClone1 = (GameObject)Instantiate(fireBallBig2, redHead.transform.position, transform.rotation);
                    while (time < delay)
                    {
                        time += Time.deltaTime;
                        yield return null;
                    }
                }
            }

        else
        {
            float delay = 1.5f;
            for (int i = 0; i < 2; i++)
            {
                StartCoroutine(playFire());
                float time = 0;
                source.PlayOneShot(fireball);
                yield return new WaitForSeconds(0.5f);
                var fireBallClone1 = (GameObject)Instantiate(fireBall, redHead.transform.position, transform.rotation);
                while (time < delay)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
                time = 0;
                StartCoroutine(playFire());
                source.PlayOneShot(fireball);
                yield return new WaitForSeconds(0.5f);
                fireBallClone1 = (GameObject)Instantiate(fireBall2, redHead.transform.position, transform.rotation);
                while (time < delay)
                {
                    time += Time.deltaTime;
                    yield return null;
                }
            }
        }

    }

    void FlameBreath()
    {
        StartCoroutine("FireBall");
    }

    void WaterSprayAttack()
    {
        if(Health / maxHealth >= .5f)
        {
            if (Random.Range(0, 2) == 1)
            {
                source.PlayOneShot(water, waterVol);
                var waterSprayClone = (GameObject)Instantiate(waterSpray, (redHead.transform.position + new Vector3(1.8f, -.5f, 0)), transform.rotation);
            }
            else
            {
                source.PlayOneShot(water, waterVol);
                var waterSprayClone = (GameObject)Instantiate(waterSpray2, (redHead.transform.position + new Vector3(-2.3f, -.5f, 0)), transform.rotation);
            }
        }
        else
        {
            source.PlayOneShot(water, waterVol);
            var waterSprayClone = (GameObject)Instantiate(waterSpray, (redHead.transform.position + new Vector3(1.8f, -.5f, 0)), transform.rotation);
            var waterSprayClone2 = (GameObject)Instantiate(waterSpray2, (redHead.transform.position + new Vector3(-2.3f, -.5f, 0)), transform.rotation);
        }
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

        //Forces upgraded fireball attack one time when health is 75% of original health
        if ((Health / maxHealth <= 0.75f) && (fireBallUpgrade == false))
        {
            randomInt = 0;
            fireBallUpgrade = true;
            fireBallCounter = 0;

            //Enable roar animation here
            playRoar();
            yield return new WaitForSeconds(1f);
        }
        //Forces upgraded Water Beam attack one time when health is 50% of original health
        if ((Health / maxHealth <= 0.50f) && (waterUpgrade == false))
        {
            randomInt = 1;
            waterUpgrade = true;
            waterCounter = 0;

            //Enable roar animation here
            playRoar();
            yield return new WaitForSeconds(1.4f);

        }
        //Forces upgraded Lightning attack one time when health is 25% of original health
        if ((Health / maxHealth <= 0.25f) && (lightningUpgrade == false))
        {
            randomInt = 2;
            lightningUpgrade = true;
            electricCounter = 0;

            //Enable roar animation here
            playRoar();
            yield return new WaitForSeconds(1.4f);

        }

        if (randomInt == 0)
        {
            fireBallCounter++;
            if (fireBallCounter > attackRepeatAmount)
            {
                StartCoroutine(AttackPattern());
                
            }
            else
            {
                waterCounter = 0;
                electricCounter = 0;
                Gem.color = new Color(255f, 0f, 0f);
                yield return new WaitForSeconds(1f);
                FlameBreath();
                if (Health / maxHealth >= .75f)
                {
                    yield return new WaitForSeconds(6f);
                }
                else
                {
                    yield return new WaitForSeconds(5f);
                }

                yield return new WaitForSeconds(1f);
                StartCoroutine(AttackPattern());

            }
        }
        if (randomInt == 1)
        {
            waterCounter++;
            if (waterCounter > attackRepeatAmount)
            {
                StartCoroutine(AttackPattern());
            }
            else
            {
                fireBallCounter = 0;
                electricCounter = 0;
                Gem.color = new Color(0f, 0f, 255f);
                yield return new WaitForSeconds(1f);
                WaterSprayAttack();
                yield return new WaitForSeconds(3f);
                yield return new WaitForSeconds(1f);

                StartCoroutine(AttackPattern());

            }
        }
        if (randomInt == 2)
        {
            electricCounter++;
            if (electricCounter > attackRepeatAmount)
            {
                StartCoroutine(AttackPattern());
            }
            else
            {
                fireBallCounter = 0;
                waterCounter = 0;
                Gem.color = new Color(255f, 218f, 0f);
                yield return new WaitForSeconds(1f);
                Electric();
                if (Health / maxHealth >= .25f)
                {
                    yield return new WaitForSeconds(5f);
                }
                else
                {
                    yield return new WaitForSeconds(2f);
                }
                yield return new WaitForSeconds(1f);
                StartCoroutine(AttackPattern());

            }
        }
    }
    private IEnumerator summonLightning()
    {
        anim.SetBool("lightning", true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("lightning", false);
    }
    private IEnumerator getHurt()
    {
        sp.color = hurt;
        yield return new WaitForSeconds(.5f);
        sp.color = normal;
    }

    private IEnumerator playRoar()
    {
        yield return new WaitForSeconds(0.15f);
        anim.SetBool("roar",true);
        source.PlayOneShot(roar);
        yield return new WaitForSeconds(2f);
        anim.SetBool("roar", false);
    }
     private IEnumerator playFire()
    {
        anim.SetBool("fireBreath", true);
        yield return new WaitForSeconds(1.2f);
        anim.SetBool("fireBreath", false);
    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "SalamanderSlash")
        {
            source.PlayOneShot(salamanderSlash, .25f);
        }
    }

}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;
using Cinemachine;
using UnityEngine.Rendering;
using UnityEngine.UI;

public class BossController : MonoBehaviour
{
    [SerializeField] private Transform target;
    [SerializeField] private float wakeupDistance;

    [SerializeField] private Animator AnimatorComponent;
    [SerializeField] private SpriteRenderer SpriteRendererComponent;
    [SerializeField] private BoxCollider2D BoxCollider2DComponent;
    [SerializeField] private BossHPBar BossHPBarScripts;

    [SerializeField] private float flyupHeight = 5f;
    [SerializeField] private float flyupSpeed = 2f;
    [SerializeField] private float MoveSpeed = 3f;

    [SerializeField] private bool isWakeup;
    [SerializeField] private bool isFlying;

    [SerializeField] private Vector3 startPosition;
    [SerializeField] private Transform wayPoint1;
    [SerializeField] private Transform wayPoint2;

    [SerializeField] private int maxHealth = 100; //MaxHealth Enemy
    [SerializeField] private int currentHealth; //CurrentHealth Enemy

    [SerializeField] private bool isSpecialForm;
    [SerializeField] private int BossDamageNormal; //Damage Boss in Normal Form
    [SerializeField] private int BossDamageSpecial; //Damage Boss in Normal Form

    [SerializeField] private GameObject HPBarBoss;

    [SerializeField] private GameObject EndPoint;

    [SerializeField] private GameObject DialogVictory;


    //Skill Cycle Variable
    [SerializeField] private float skillInterval = 3f; //Time Between each skill
    private int currentSkillPharse = 0; //Current Skill Pharse
    private int totalSkillPharse = 4; //Total Skill is 4
    [SerializeField] private bool isCastingSkill; //Boss is casting a skill
    [SerializeField] private bool isSkillCycleRunning = false;

    //********************************************************************
    //********************************************************************
    //***************Skillfisrt of boss in NORMAL FORM********************
    public GameObject SpikeBossPrefabs;

    [SerializeField] private Transform spawnSpike;

    private int spikeWaves = 3;
    [SerializeField] private float TimeBetweenSpikeWaves = 1.5f;
    [SerializeField] private float spikeTimeLife = 1f;

    //****************SkillSecond of Boss in NORMAL FORM*****************
    [SerializeField] protected private float BulletSpeed = 2f;
    [SerializeField] public GameObject Bullet;
    [SerializeField] protected Transform firePoint;
    [SerializeField] private BulletBoss BulletNormalScripts;

    private int BulletNormalWaves = 8;
    [SerializeField] private float TimeBetweenBulletNormal = 1.5f;

    //***************SkillThird of Boss in NORMAL FORM*******************
    [SerializeField] private GameObject EnemySummonPrefabs;
    [SerializeField] private int numberEnemy = 5;
    [SerializeField] private float TimeBetweenEnemy = 1.5f;

    //***************Skillfourth of Boss in NORMAL FORM*******************
    [SerializeField] private float FastSpeedNormal = 15;

    [SerializeField] private int AttackWavesNormal = 4;
    [SerializeField] private float TimeBetweenAttackNormal = 1.5f;

    //*******************************************************************
    //*******************************************************************
    //*******************************************************************


    //********************************************************************
    //********************************************************************
    //***************Skillfirst of boss in SPECIAL FORM********************
    [SerializeField] private GameObject firePrefabs;

    [SerializeField] private Transform spawnFire; //Position create Fire

    [SerializeField] private int fireCount = 20; //Amount of fire created
    [SerializeField] private float spacing = 1.5f; //Distance between fires
    [SerializeField] private int fireWaves = 3;
    [SerializeField] private float TimeBetweenFireWaves = 1.5f;
    [SerializeField] private float FireTimeLife = 1f;

    //***************SkillSecond of boss in SPECIAL FORM********************
    [SerializeField] private GameObject BulletSpecial;
    [SerializeField] private int BulletSpecialWaves = 15;
    [SerializeField] private float TimeBetweenBulletSpecial = 1f;

    //***************SkillThird of boss in SPECIAL FORM********************
    [SerializeField] private float FastSpeedSpecial = 15;

    [SerializeField] private int AttackWavesSpecial = 4;
    [SerializeField] private float TimeBetweenAttackSpecial = 1.5f;

    //***************Skillfourth of Boss in SPECIAL FORM*******************
    [SerializeField] private Transform spawnEnergyBall;
    [SerializeField] private GameObject EnergyBall;

    [SerializeField] private Transform spawnVortex;
    [SerializeField] private GameObject Vortex;

    //[SerializeField] private GameObject WindLeft;
    [SerializeField] private GameObject WindLeft;
    [SerializeField] private GameObject WindRight;
    [SerializeField] private Transform spawnWindLeft;
    [SerializeField] private Transform spawnWindRight;
    //*******************************************************************
    //*******************************************************************
    //*******************************************************************


    //*******************Manage Boss's Trail effect**********************
    [SerializeField] private TrailRenderer ShadowNormal;
    [SerializeField] private TrailRenderer ShadowSpecial;

    public enum BossState
    {
        Normal,
        Special
    }

    public BossState currentState;
    void SwtichTrail(BossState state)
    {
        currentState = state;

        ShadowNormal.gameObject.SetActive(state == BossState.Normal);
        ShadowSpecial.gameObject.SetActive(state == BossState.Special);
    }

    //*******************************************************************
    private void Awake()
    {
        startPosition = transform.position;
        currentHealth = maxHealth;
        StartCoroutine(TurnOffEndPoint());
        HPBarBoss.SetActive(false);

        SwtichTrail(BossState.Normal);
    }

    private void Start()
    {
        StartCoroutine(WaitForPlayer());
    }

    void SwitchToSpecialForm()
    {
        currentSkillPharse = 0;
        isSpecialForm = true;
        AnimatorComponent.SetTrigger("Special");
        SwtichTrail(BossState.Special);
    }

    private void Update()
    {
        TurnDirection();

        if (Vector2.Distance(transform.position, target.position) <= wakeupDistance && !isWakeup)
        {
            WakeUp();
            
        }

        if (isFlying)
        {
            FlyUp();
        }

        if (isWakeup && !isCastingSkill && !isFlying && !isSkillCycleRunning)
        {
            StartCoroutine(SkillCycles());
        }

        if (!isSpecialForm && currentHealth <= maxHealth * 0.4f)
        {
            SwitchToSpecialForm();
        }
    }

    IEnumerator SkillCycles()
    {
        isSkillCycleRunning = true;
        isCastingSkill = true;
        while (currentSkillPharse < totalSkillPharse)
        {
            if (currentHealth <= 0)
            {
                yield break; //Stop if boss died
            }

            if (currentSkillPharse % 2 == 0) //Check current skill pharse. Neu current skill pharse chia het cho 2 (phan du = 0) --> So chan
            {
                yield return StartCoroutine(MoveToWayPoint(wayPoint1.position));
            }
            else
            {
                yield return StartCoroutine(MoveToWayPoint(wayPoint2.position));
            }

            if (isSpecialForm)
            {
                ExecuteSpecialSkill(currentSkillPharse);
            }
            else
            {
                ExecuteNormalSkill(currentSkillPharse);
            }

            yield return new WaitForSeconds(skillInterval);
            currentSkillPharse++;
            yield return null;
        }

        currentSkillPharse = 0;

        isCastingSkill = false;
        isSkillCycleRunning = false;
    }

    void WakeUp()
    {
        isWakeup = true;
        AnimatorComponent.SetTrigger("Idle 2");
        BoxCollider2DComponent.enabled = true;
        HPBarBoss.SetActive(true);
        isFlying = true;
    }

    void FlyUp()
    {
        Vector3 FlyPointPosition = startPosition + new Vector3(0, flyupHeight, 0);

        transform.position = Vector2.MoveTowards(transform.position, FlyPointPosition, flyupSpeed * Time.deltaTime);

        if (Vector2.Distance(transform.position, FlyPointPosition) < 0.1f)
        {
            isFlying = false;
        }

    }

    //********************************************************************
    //********************************************************************
    //************************NORMAL SKILL********************************
    void ExecuteNormalSkill(int pharse)
    {
        switch(pharse)
        {          
            
            case 0:
                StartCoroutine(NormalSkill1());
                break;
            case 1:
                StartCoroutine(NormalSkill2());
                break;
            case 2:
                StartCoroutine(NormalSkill3());
                break;
            case 3:
                StartCoroutine(NormalSkill4());
                break;
             
        }
    }
    
    //************************NORMAL SKILL 1******************************
    IEnumerator NormalSkill1()
    {

        AnimatorComponent.SetTrigger("Idle 2");

        yield return new WaitForSeconds(2f);
        AnimatorComponent.SetBool("Wait", true);

        for (int i = 0; i < spikeWaves; i++)
        {
            SpawnSpike();
            yield return new WaitForSeconds(spikeTimeLife);

            if (i < spikeWaves - 1)
            {
                yield return new WaitForSeconds(TimeBetweenSpikeWaves);
            }
        }

        yield return new WaitForSeconds(0.1f);    
        AnimatorComponent.SetBool("Wait", false);

    }

    void SpawnSpike()
    {
        GameObject Spike = Instantiate(SpikeBossPrefabs, spawnSpike.position, Quaternion.identity, null);
        Destroy(Spike, spikeTimeLife);
    }
    
    //************************NORMAL SKILL 2******************************
    IEnumerator NormalSkill2()
    {
        AnimatorComponent.SetTrigger("Idle 2");

        yield return new WaitForSeconds(3f);
        AnimatorComponent.SetBool("Wait", true);


        for (int i = 0; i < BulletNormalWaves; i++)
        {
            CreateBulletNormal();

            if (i < BulletNormalWaves - 1)
            {
                yield return new WaitForSeconds(TimeBetweenBulletNormal);
            }
        }


        yield return new WaitForSeconds(0.1f);
        AnimatorComponent.SetBool("Wait", false);
    }

    void CreateBulletNormal()
    {
        Vector2 directionToPlayer = (target.position - firePoint.position).normalized;
        var NewBullet = Instantiate(Bullet, firePoint.position, Quaternion.identity, null); 
        var NewBulletRigibody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigibody.velocity = directionToPlayer * BulletSpeed;
        NewBulletRigibody.AddTorque(-20f);
    }

    //*************************NORMAL SKILL 3*****************************

    public GameObject SummonEffect;
    private float summonDelay = 0.8f;

    IEnumerator NormalSkill3()
    {

        AnimatorComponent.SetTrigger("Idle 2");

        yield return new WaitForSeconds(3f);
        AnimatorComponent.SetBool("Wait", true);

        for (int i = 0; i < numberEnemy; i++)
        {
            StartCoroutine(Summoning());

            if (i < numberEnemy - 1)
            {
                yield return new WaitForSeconds(TimeBetweenEnemy);
            }
        }

        yield return new WaitForSeconds(1f);
        AnimatorComponent.SetBool("Wait", false);
    }


    IEnumerator Summoning()
    {
        float rangeX = Random.Range(-4, 15);
        float rangeY = Random.Range(1, 4);
        Vector3 spawnPosition = new Vector3(rangeX, rangeY, 0f);

        GameObject summonEffect = Instantiate(SummonEffect, spawnPosition, Quaternion.identity, null);

        yield return new WaitForSeconds(summonDelay);

        Instantiate(EnemySummonPrefabs, summonEffect.transform.position, Quaternion.identity, null);

        Destroy(summonEffect);
    }

    //*************************NORMAL SKILL 4*****************************
    IEnumerator NormalSkill4()
    {
        AnimatorComponent.SetTrigger("Idle 2");

        yield return new WaitForSeconds(3f);
        AnimatorComponent.SetBool("Wait", true);

        ShadowNormal.emitting = true;

        for (int i = 0; i < AttackWavesNormal; i++) 
        {

            yield return StartCoroutine(AttackNormalThePlayer());

            if (i < AttackWavesNormal - 1)
            {
                yield return new WaitForSeconds(TimeBetweenAttackNormal);
            }
        }

        ShadowNormal.emitting = false;  

        yield return new WaitForSeconds(1f);
        AnimatorComponent.SetBool("Wait", false);
    }

    IEnumerator AttackNormalThePlayer()
    {
        Vector2 targetPosition = target.position;
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, FastSpeedNormal * 2f * Time.deltaTime);
            Debug.Log("Boss is heading towards the player");
            yield return null;
        }
    }

    //********************************************************************
    //********************************************************************
    //********************************************************************

    //********************************************************************
    //********************************************************************
    //************************SPECIAL SKILL********************************
    void ExecuteSpecialSkill(int pharse)
    {
        switch (pharse)
        {
            case 0:
                StartCoroutine(SpecialSkill1());
                break;
            case 1:
                StartCoroutine(SpecialSkill2());
                break;
            case 2:
                StartCoroutine(SpecialSkill3());
                break;
            case 3:
                StartCoroutine(SpecialSkill4());
                break;
        }
    }

    //*************************SPECIAL SKILL 1*****************************
    IEnumerator SpecialSkill1()
    {
        AnimatorComponent.SetTrigger("Idle 1");

        yield return new WaitForSeconds(2f);
        AnimatorComponent.SetBool("Wait 2", true);

        StartCoroutine(SpawnFire());


        yield return new WaitForSeconds(0.1f);
        AnimatorComponent.SetBool("Wait 2", false); 
    }

    IEnumerator SpawnFire()
    {
        for (int i = 0; i < fireWaves; i ++)
        {
            for (int i1 = 0; i1 < fireCount; i1++)
            {
                float offset = (i1 - (fireCount - 1) / 2.0f) * spacing;
                Vector3 spawnPosition = spawnFire.position + new Vector3(offset, 0, 0);

                GameObject Fire = Instantiate(firePrefabs, spawnPosition, Quaternion.identity, null);
                Destroy(Fire, FireTimeLife);
            }

            if (i < fireWaves - 1)
            {
                yield return new WaitForSeconds(TimeBetweenFireWaves);
            }
        }
        
    }

    //*************************SPECIAL SKILL 2*****************************
    IEnumerator SpecialSkill2()
    {
        AnimatorComponent.SetTrigger("Idle 1");

        yield return new WaitForSeconds(2f);
        AnimatorComponent.SetBool("Wait 2", true);

        for (int i = 0; i < BulletSpecialWaves; i++)
        {
            CreateBulletSpecial();

            if (i < BulletSpecialWaves - 1)
            {
                yield return new WaitForSeconds(TimeBetweenBulletSpecial);
            }
        }

        yield return new WaitForSeconds(0.1f);
        AnimatorComponent.SetBool("Wait 2", false);
    }

    void CreateBulletSpecial()
    {
        float rangeX = Random.Range(-7, 19);
        float rangeY = Random.Range(10,10);
        Vector3 spawnPosition = new Vector3(rangeX, rangeY, 0f);
        Instantiate(BulletSpecial, spawnPosition, Quaternion.identity, null);
    }

    //*************************SPECIAL SKILL 3*****************************
    IEnumerator SpecialSkill3()
    {
        AnimatorComponent.SetTrigger("Idle 1");

        yield return new WaitForSeconds(2f);
        AnimatorComponent.SetBool("Wait 2", true);

        ShadowSpecial.emitting = true;

        for (int i = 0; i < AttackWavesSpecial; i++)
        {
            yield return StartCoroutine(AttackSpecialThePlayer());

            if (i < AttackWavesSpecial - 1)
            {
                yield return new WaitForSeconds(TimeBetweenAttackSpecial);
            }
        }

        ShadowSpecial.emitting = false;


        yield return new WaitForSeconds(0.1f);
        AnimatorComponent.SetBool("Wait 2", false);
    }
    IEnumerator AttackSpecialThePlayer()
    {
        Vector2 targetPosition = target.position;
        while (Vector2.Distance(transform.position, targetPosition) > 0.01f)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, FastSpeedSpecial * 2f * Time.deltaTime);
            yield return null;
        }
    }

    //*************************SPECIAL SKILL 4*****************************
    IEnumerator SpecialSkill4()
    {
        AnimatorComponent.SetTrigger("Idle 1");

        yield return new WaitForSeconds(2f);
        AnimatorComponent.SetBool("Wait 2", true);

        StartCoroutine(UltimateSkill());

        yield return new WaitForSeconds(0.1f);
        AnimatorComponent.SetBool("Wait 2", false);
    }

    IEnumerator UltimateSkill()
    {
        SpawnVortex();

        yield return new WaitForSeconds(1f);

        SpawnWindEfect();

        yield return new WaitForSeconds(2.5f);

        SpawnEnergyBall();
    }

    void SpawnVortex()
    {
        GameObject vortex = Instantiate(Vortex, spawnVortex.position, Quaternion.identity, null);

        VortexController vortexScript = vortex.GetComponent<VortexController>();

        vortexScript.vortexCenter = spawnVortex; //Pass the spawnVortex variable to the vortexCenter in the VortexController script

        Destroy(vortex,9f);
    }

    void SpawnWindEfect()
    {
        GameObject winright = Instantiate(WindRight, spawnWindRight.position, Quaternion.identity, null);
        GameObject winleft = Instantiate(WindLeft, spawnWindLeft.position, Quaternion.identity, null);

        Destroy(winright, 9f);
        Destroy(winleft, 9f);
    }

    void SpawnEnergyBall()
    {
        Instantiate(EnergyBall, spawnEnergyBall.position, Quaternion.identity, null);
    }
    //********************************************************************
    //********************************************************************
    //********************************************************************
    void TurnDirection()
    {
        if (transform.position.x < target.position.x)
        {
            SpriteRendererComponent.flipX = false;
            ShadowNormal.transform.localPosition = new Vector3(-0.1f, 0.1f, 0.1f);
            ShadowSpecial.transform.localPosition = new Vector3(-0.2f, 0.25f, 0.2f);
        }
        else
        {
            SpriteRendererComponent.flipX = true;
            ShadowNormal.transform.localPosition = new Vector3(0.1f, 0.1f, 0.1f);
            ShadowSpecial.transform.localPosition = new Vector3(0.2f, 0.25f, 0.2f);
        }
    }

    IEnumerator MoveToWayPoint(Vector3 WayPointposition)
    {
        while (Vector2.Distance(transform.position, WayPointposition) > 0.1f)
        {
            transform.position = Vector2.MoveTowards(transform.position, WayPointposition, MoveSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator TakeDamage(int damageAmount)
    {
        currentHealth = Mathf.Max(currentHealth - damageAmount, 0);
        BossHPBarScripts.UpdateBossHPBar(currentHealth);
        AnimatorComponent.SetTrigger("Hit");
        yield return new WaitForSeconds(0.2f);

        if (currentState == BossState.Normal)
        {
            AnimatorComponent.SetTrigger("Idle 2");
            Debug.Log("1");
        }
        else if (currentState == BossState.Special)
        {
            AnimatorComponent.SetTrigger("Idle 1");
        }

        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        AnimatorComponent.SetTrigger("Die");
        Destroy(gameObject);
        HPBarBoss.SetActive(false);
        EndPoint.SetActive(true);
    }

    void TakeDamageToPlayer()
    {
        if(currentState == BossState.Normal)
        {
            PlayerControllers.Instance.PlayerHealthPointUpdate(BossDamageNormal);

        }
        else
        {
            PlayerControllers.Instance.PlayerHealthPointUpdate(BossDamageSpecial);

        }

    }

    void AttackPlayer()
    {
        TakeDamageToPlayer();
    }


    protected virtual void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.transform.gameObject.CompareTag("Player"))
        {
            AttackPlayer();
        }
    }

    private IEnumerator WaitForPlayer()
    {
        GameObject player = null;
        while (player == null)
        {
            player = GameObject.FindWithTag("Player");
            yield return new WaitForSeconds(0.5f); 
        }
        target = player.transform;
    }

    IEnumerator TurnOffEndPoint()
    {
        while (DialogVictory != null && DialogVictory.activeSelf)
        {
            yield return null;
        }
        EndPoint.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        //Draw a circle in Scene View to see the blast radius
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, wakeupDistance);
    }
    
}

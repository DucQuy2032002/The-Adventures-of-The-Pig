using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using static UnityEngine.AudioSettings;


public class PlayerControllers : MonoBehaviour
{
    public ParticleSystem SmokingRunning;
    public bool isMoving = false; //Check player move

    public GameObject Laser;
    public GameObject Bomb;
    public GameObject Heal;
    public GameObject Meteoroid;
    [SerializeField] private GameObject Shield;

    public bool isShielded; //Can Shielded

    //[SerializeField] private float spawnHeight = 7f; //Meteoroid spawn Height
    [SerializeField] private float MeteoroidSpeed = 5f; //Speed of Meteoroid


    public bool canUseSkillFirst = true; //Can USE SKILL 1
    public bool canUseSkillSecond = true; //Can USE SKILL 2
    public bool canUseSkillThird = true; //Can USE SKILL 3
    public bool canShoot = true;


    public PlayerHPBar PlayerHPBarScripts;
    public BulletManager BulletManagerScripts;

    public Transform Right;
    public Transform Left;

    public bool isNonLoopAnimation = false;


    public SkillCooldown SkillCooldownButtonU;
    public SkillCooldown SkillCooldownButtonI;
    public SkillCooldown SkillCooldownButtonO;

    public SkillCooldown ButtonBomb;
    public SkillCooldown ButtonShield;
    public SkillCooldown ButtonMeteoroid;

    private Vector2 VectortoRight = new Vector2(1, 0);
    private Vector2 VectortoLeft = new Vector2(-1, 0);
    public string CurrentAnimation = "";

    public float MoveSpeed = 3.0f;
    public float WalkSpeed = 3.0f;
    public float RunSpeed = 5.0f;

    public Rigidbody2D PlayerRigidbody2D;
    public SpriteRenderer PlayerSpriteRender;
    public Animator PlayerAnimator;

    public float jumpStrength = 10f;        //Lực nhảy
    public bool isGrounded;         //Kiểm tra người chơi ở mặt đất
    public int JumpCount;
    public int MaxJumpCount = 2;

    public bool isPressedButtonRight;
    public bool isPressedButtonLeft;

    public int HealthPoint;
    public int maxHealPoint;

    public bool isCanTakeDamage = true;

    public Color ColorShield;
    public Color ColorNormal;

    public GameObject ButtonsMobile;
    public GameObject KeyboardPC;

    public bool doubleJump;

    public string currentDirection = "";

    //*****Apply Singleton to PlayerControllers*****
    public static PlayerControllers Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;    
        }
        else
        {
            Destroy(gameObject);
        }
        //************************************************//
        Physics2D.IgnoreLayerCollision(8, 6);
        Physics2D.IgnoreLayerCollision(8, 9);
        Physics2D.IgnoreLayerCollision(6, 9);
        //************************************************//

        PlayerHPBarScripts = GameObject.Find("HPColor").GetComponent<PlayerHPBar>();
        SkillCooldownButtonU = GameObject.Find("SkillCooldownManager 1").GetComponent<SkillCooldown>();
        SkillCooldownButtonI = GameObject.Find("SkillCooldownManager 2").GetComponent<SkillCooldown>();
        SkillCooldownButtonO = GameObject.Find("SkillCooldownManager 3").GetComponent<SkillCooldown>();
        ButtonBomb = GameObject.Find("Button Bomb").GetComponent<SkillCooldown>();
        ButtonShield = GameObject.Find("Button Shield").GetComponent<SkillCooldown>();
        ButtonMeteoroid = GameObject.Find("Button Meteoroid").GetComponent<SkillCooldown>();
        ButtonsMobile = GameObject.Find("ButtonCanvas");
        KeyboardPC = GameObject.Find("KeyboardPC");

        //CheckPlatform and ChangeUI
        #if UNITY_STANDALONE || UNITY_WEBGL
           ButtonsMobile.SetActive(false);
        #elif UNITY_IOS || UNITY_ANDROID
           KeyboardPC.SetActive(false); // Hiện UI trên Mobile
        #endif

    }
      
    void Update()
    {
        #if UNITY_STANDALONE || UNITY_WEBGL
            KeyBoardController(); // keyboard control on PC
        #elif UNITY_IOS || UNITY_ANDROID
            TouchController(); // Button control on Mobile  
        #endif
    }

    void TouchController()
    {
        if (!isDashing)
        {
            if (currentDirection == "Right")
            {
                PlayerMoveRight();
            }
            else if (currentDirection == "Left")
            {
                PlayerMoveLeft();
            }
            else
            {
                PlayerStopMovement();
            }
        }
    }
    //***********************************************************
    //********************FOR MOBILE*****************************
    public void OnMobileRightButtonPressed()
    {
        currentDirection = "Right";
        isMoving = true;
    }

    public void OnMobileLeftButtonPressed()
    {
        currentDirection = "Left";
        isMoving = true;
    }

    public void OnMobileRLButtonUpPressed()
    {
        if (currentDirection != "") 
        {
            currentDirection = ""; 
            isMoving = false;
        }
    }
    
    public void OnMobileShootButtonPressed()
    {
        PlayerAttack();
    }

    public void OnMobileJumpButtonPressed()
    {
        PlayerJump();
        PlayerRunOff();
        SmokingRunning.Stop();
        AudioManager.Instance.PlaySoundJump();
    }

    public void OnMobileDashButtonPressed()
    {
        if (canDash == true)
        {
            StartCoroutine(PlayerDash());
            AudioManager.Instance.PlaySoundDash();
        }
    }

    public void OnMobileSpeedRunButtonDownPressed()
    {
        PlayerRunOn();
        if (isMoving == true && MoveSpeed == RunSpeed) //Check player movement and running
        {
            if (SmokingRunning.isPlaying == false) //Check if the running effect is playing
            {
                SmokingRunning.Play();
            }
        }
    }

    public void OnMobileSpeedRunButtonUpPressed()
    {
        PlayerRunOff();
        SmokingRunning.Stop();
    }

    public void OnMobileBombButtonPressed()
    {
        if (!SkillCooldownButtonU.isCooldown)
        {
            PlaySkillFirst();
            ButtonBomb.StartCooldown();
        }
    }

    public void OnMobileShieldButtonPressed()
    {
        if (!SkillCooldownButtonI.isCooldown && !isShielded)
        {
            PlaySkillSecond();
            ButtonShield.StartCooldown();
        }
    }

    public void OnMobileMeteoriteButtonPressed()
    {
        if (!SkillCooldownButtonO.isCooldown)
        {
            PlaySkillThird();
            ButtonMeteoroid.StartCooldown();
        }
    }


    //**********************************************************
    //**********************************************************

    void KeyBoardController()
    {
        if ((Input.GetKeyDown("l")))
        {
            if (canDash == true)
            {
                StartCoroutine(PlayerDash());
                AudioManager.Instance.PlaySoundDash();
            }
        }
        if (Input.GetKeyDown("o"))
        {
            if (!SkillCooldownButtonO.isCooldown)
            {
                PlaySkillThird();
                SkillCooldownButtonO.StartCooldown();
            }
            //PlaySkillThird();
        }
        if (Input.GetKeyDown("i") && !isShielded) 
        {
            if (!SkillCooldownButtonI.isCooldown)
            {
                PlaySkillSecond();
                SkillCooldownButtonI.StartCooldown();
            }
        }
        if (Input.GetKeyDown("u"))
        {
            if (!SkillCooldownButtonU.isCooldown)
            {
                PlaySkillFirst();
                SkillCooldownButtonU.StartCooldown();
            }
        }
        if (Input.GetKeyDown("j"))
        {
            PlayerAttack();
        }
        if (Input.GetKey("d"))
        {
            if(isDashing == false)
            {
                PlayerMoveRight();
            }
            isMoving = true;
        }
        else if (Input.GetKey("a"))
        {
            if (isDashing == false)
            {
                PlayerMoveLeft();
            }
            isMoving = true;
        }
        else
        {
            PlayerStopMovement();
            isMoving = false;
        }
        if (Input.GetKeyDown("k") == true)
        {
            PlayerJump();
            PlayerRunOff();
            SmokingRunning.Stop();
            AudioManager.Instance.PlaySoundJump();
        }
        if (Input.GetKey("left shift") == true)
        {
            PlayerRunOn();
            if (isMoving == true && MoveSpeed == RunSpeed) //Check player movement and running
            {
                if (SmokingRunning.isPlaying == false) //Check if the running effect is playing
                {
                    SmokingRunning.Play();
                }
            }
        }
        if (Input.GetKeyUp("left shift") == true)
        {
            PlayerRunOff();
            SmokingRunning.Stop();
        }      
    }

    
    //FUNCTION DASH
    public TrailRenderer DashTrail;
    public float DashPower = 25f;  //Dash distance
    Vector2 DashDirection = new Vector2(); //Dash Direction
    bool isDashing = false;
    public bool canDash = true;
    

    public IEnumerator PlayerDash()
    {
        //Start Dash
        DashTrail.emitting = true;
        canDash = false; 
        isDashing = true; //disable button Right or Left
        if (PlayerSpriteRender.flipX == true)
            DashDirection = new Vector2(1,0); //Dashing to Right
        else
            DashDirection = new Vector2(-1,0); //Dashing to Left

        PlayerRigidbody2D.velocity = DashDirection * DashPower;
        yield return new WaitForSeconds(0.1f);
        StartCoroutine(PrepareNonLoopAnimation("Player Dash")); //Disable other Loop Animations, then play Animation Dash
        PlayerRigidbody2D.velocity = DashDirection * MoveSpeed; //Set the dash value equal to the MoveSpeed after the dash
        isDashing = false;
        //End Dash
        DashTrail.emitting = false;

        //Cooldown Dash is 1s
        yield return new WaitForSeconds(0.8f); 
        canDash = true;
    }
    public void PlayerRunOn()
    {
        if ((MoveSpeed != RunSpeed) && isGrounded == true)
        {
            StartCoroutine(ChangePlayerSpeed(RunSpeed));
        }
    }
    public void PlayerRunOff()
    {
        if (MoveSpeed != WalkSpeed)
        {
            StartCoroutine(ChangePlayerSpeed(WalkSpeed));
        }
    }
    IEnumerator ChangePlayerSpeed(float NewSpeed)
    {
        yield return new WaitUntil(()=> isGrounded == true);
        MoveSpeed = NewSpeed;
        Debug.Log("New Speed is " + MoveSpeed);
    }
    void PlayerMove(Vector2 MoveVector)
    {
        Vector2 NewMoveVector = new Vector2((MoveVector.x * MoveSpeed) + MovingPlatformVelocityX, PlayerRigidbody2D.velocity.y);
        PlayerRigidbody2D.velocity = NewMoveVector;
        AnimationWalk();
    }

    //Set the PLAYER SPEED EQUAL TO THE MOVINGPLATFORM SPEED when the player is standing on the movingplatform
    float MovingPlatformVelocityX = 0;
    private int SkillIndex;
    internal bool CanDash;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            var MovingPlatformRigibody2DComponent = collision.gameObject.GetComponent<Rigidbody2D>();
            MovingPlatformVelocityX = MovingPlatformRigibody2DComponent.velocity.x;
        }
    }
    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "MovingPlatform")
        {
            MovingPlatformVelocityX = 0;
        }
    }

    void RotatePlayer(bool bool_value)
    {
        PlayerSpriteRender.flipX = bool_value;
    }
    void AnimationWalk()
    {
        if (isGrounded == true)
        {
            if (MoveSpeed == RunSpeed)
            {
                PlayingAnimation("Player Run");
            }
            else if (MoveSpeed == WalkSpeed)
            {
                PlayingAnimation("Player Walk");
            }
        }
    }
    
    void AnimationStop()
    {
        if (isGrounded == true)
        {
            PlayingAnimation("Player Idle");
        }
    }
    IEnumerator AnimationJump()
    {
        yield return new WaitForSeconds(0.1f);
        PlayingAnimation("Player Jump");
    }
    void PlayingAnimation(string Animationname) //play loop animation
    {
        if (CurrentAnimation != Animationname && isNonLoopAnimation == false) 
        {
            CurrentAnimation = Animationname;
            PlayerAnimator.Play(CurrentAnimation);
        }
    }
    void PlayingNonLoopAnimation(string Animationname) //play non loop animation
    {
        if (CurrentAnimation != Animationname ) 
        {
            CurrentAnimation = Animationname;
            PlayerAnimator.Play(CurrentAnimation);

        }
    }

    IEnumerator PrepareNonLoopAnimation(string Animationname)
    {
        isNonLoopAnimation = true;
        PlayingNonLoopAnimation(Animationname);  //this animation will be played at the end of frame
        yield return new WaitForEndOfFrame();
        var CurrentAnimationInfo = PlayerAnimator.GetCurrentAnimatorStateInfo(0);
        if (CurrentAnimationInfo.IsName(Animationname) == true)        
        {
            var AnimationDuration = CurrentAnimationInfo.length;
            yield return new WaitForSeconds(AnimationDuration);
            isNonLoopAnimation = false;
        }
        else
        {
            yield return null;
            isNonLoopAnimation = true;
        }
    }

    public void PlayerAttack()
    {
        if (canShoot)
        {
            StartCoroutine(PrepareNonLoopAnimation("Attack"));
            StartCoroutine(CreateBullet());
            AudioManager.Instance.PlaySoundLaser();
        }
    }

    IEnumerator CreateBullet()
    {
        canShoot = false;
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();
        float LaserSpeed = 10f;
        if (PlayerSpriteRender.flipX == true)
        {
            BulletPosition = Right.position;
            BulletDirection = new Vector2(1, 0);
            BulletManagerScripts.BulletRotate(false);
        }
        else
        {
            BulletPosition = Left.position;
            BulletDirection = new Vector2(-1, 0);
            BulletManagerScripts.BulletRotate(true);
        }
        var NewBullet = Instantiate(Laser, BulletPosition, Quaternion.identity, null); //Create a new Laser
        var NewBulletRigibody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigibody.velocity = BulletDirection * LaserSpeed;
        yield return new WaitForSeconds(0.2f);
        canShoot = true;
    }

    //************************************************
    //************************************************
    //*****************FIRST SKILL********************
    public void PlaySkillFirst()
    {
        if (canUseSkillFirst)
        {
            StartCoroutine(CreateBomb());
        }  
    }

    IEnumerator CreateBomb()
    {
        canUseSkillFirst = false;
        Vector3 BulletPosition = new Vector3();
        Vector2 BulletDirection = new Vector2();
        float BombSpeed = 4f;
        if (PlayerSpriteRender.flipX == true)
        {
            BulletPosition = Right.position;
            BulletDirection = new Vector2(1, 2);  
        }
        else
        {
            BulletPosition = Left.position;
            BulletDirection = new Vector2(-1, 2);          
        }
        var NewBullet = Instantiate(Bomb, BulletPosition, Quaternion.identity, null); //Create a new Bomb
        var NewBulletRigibody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigibody.velocity = BulletDirection * BombSpeed;
        NewBulletRigibody.AddTorque(-5f); //Make a spiral boom
        yield return new WaitForSeconds(8f);
        canUseSkillFirst = true;
    }
    //************************************************
    

    //****************SECOND SKILL********************
    void PlaySkillSecond()
    {
        if (canUseSkillSecond == true)
        {
            StartCoroutine(CreateShield());
        }
    }

    IEnumerator CreateShield()
    {
        canUseSkillSecond = false;

        isShielded = true;
        Shield.SetActive(true);
        yield return new WaitForSeconds(3f);//Shield only lasts for 3s
        Shield.SetActive(false);
        isShielded = false;
        yield return new WaitForSeconds(7f);

        canUseSkillSecond = true;
    }

    void PlaySkillThird()
    {
        if (canUseSkillThird == true)
        {
            StartCoroutine(CreateMeteoroid());
        }
    }

    IEnumerator CreateMeteoroid()
    {
        canUseSkillThird = false;

        Vector3 spawnPosition = new Vector3(Camera.main.transform.position.x + 10, Camera.main.transform.position.y + 7, 0); //The meteor spawn position will be 6 units to the right of the camera center.
        Vector2 spawnDirection = new Vector2(-1f, -1f); //x=-1: go left; y=-1: go down
        var NewBullet = Instantiate(Meteoroid, spawnPosition, Quaternion.identity, null);
        var NewBulletRigibody = NewBullet.GetComponent<Rigidbody2D>();
        NewBulletRigibody.velocity = spawnDirection * MeteoroidSpeed;
        yield return new WaitForSeconds(20f);

        canUseSkillThird = true;

    }
    //*************************************************
    //*************************************************
    //*************************************************
    public void PlayerMoveRight()
    {
        PlayerMove(VectortoRight);
        RotatePlayer(true);
    }

    public void PlayerMoveLeft()
    {
        PlayerMove(VectortoLeft);
        RotatePlayer(false);
    }

    public void PlayerStopMovement()
    {
        AnimationStop();
    }

    public void PlayerJump()
    {
        
        if(isGrounded == true)
        {
            PlayerRigidbody2D.velocity = new Vector2(PlayerRigidbody2D.velocity.x, jumpStrength * 2f);
            StartCoroutine(AnimationJump());
            doubleJump = true;

        }
        else if (doubleJump == true)
        {
            PlayerRigidbody2D.velocity = new Vector2(PlayerRigidbody2D.velocity.x, jumpStrength *1.8f); 
            StartCoroutine(AnimationJump());
            doubleJump = false;  
        }
    }

    public void GameRestart()
    {
        string CurrentSceneGame = gameObject.scene.name;
        SceneManager.LoadScene(CurrentSceneGame);
    }

    public void PlayerHealthPointUpdate(int AddingValue)
    {
        if (isCanTakeDamage == true)
        {
            if (isShielded)
            {
                return;
            }

            isCanTakeDamage = false;
            HealthPoint = HealthPoint + AddingValue; //update HP
            
            //*****CHECK HEALPOINT < 0. CALL GAMEOVER!!!!*****
            if (HealthPoint < 0)
            {
                HealthPoint = 0;
                Destroy(this.gameObject);
                GameOver();
                Debug.Log("Game Over!!!!!");
            }

            PlayerHPBarScripts.UpdatePlayerHPBar(HealthPoint); //update PlayerHP to HPBar
            //PlayerPrefs.SetInt("PlayerHP", HealthPoint);
            //PlayerPrefs.Save(); //call this when game is loading or player no input
          //PlayerPrefs.DeleteKey("PlayerHP");

            StartCoroutine(GivePlayerShield());
        }
    }

    IEnumerator GivePlayerShield()
    {
        PlayerSpriteRender.color = ColorShield;
        yield return new WaitForSeconds(2); //can't take dame in 2 seconds
        PlayerSpriteRender.color = ColorNormal;
        isCanTakeDamage = true;
    }

    private void Start()
    { 
        HealthPoint = PlayerPrefs.GetInt("PlayerHP", 100); //update HP
        PlayerHPBarScripts.UpdatePlayerHPBar(HealthPoint); //update PlayerHP to HPBar       
    }       

    //*****FUNCTION RECOVERYHEALPLAYER*****
    public void RecoveryHealPlayer(int Healing)
    {
        if (HealthPoint < maxHealPoint)
        {
            HealthPoint += Healing;

            if (HealthPoint >= maxHealPoint)
            {
                HealthPoint = maxHealPoint;
            }

            PlayerHPBarScripts.UpdatePlayerHPBar(HealthPoint); //update PlayerHP to HPBar
            //PlayerPrefs.SetInt("PlayerHP", HealthPoint);
            //PlayerPrefs.Save(); //call this when game is loading
            Debug.Log("has recovered");
        }
    }

    public void GameOver()
    {
        GameOverManager.Instance.PlayerDied();
        // Switch to GameOver scene
        SceneManager.LoadScene("GameOver");
        AudioManager.Instance.PlaySoundGameOver();
    }

    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.gameObject.tag == "Item")
        {
            //Debug.Log("Đã gom");
            ItemManager.Instance.ItemCount();
            AudioManager.Instance.PlaySoundCollectItem();
            Destroy(other.gameObject);
        }
    }
}

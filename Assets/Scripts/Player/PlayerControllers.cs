using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PlayerControllers : MonoBehaviour
{
    public ParticleSystem SmokingRunning;
    bool isMoving = false; //Check player move

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


    public PlayerHPBar PlayerHPBarScripts;
    public BulletManager BulletManagerScripts;

    public Transform Right;
    public Transform Left;

    public bool isNonLoopAnimation = false;


    public SkillCooldown SkillCooldownButtonU;
    public SkillCooldown SkillCooldownButtonI;
    public SkillCooldown SkillCooldownButtonO;

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

    public ItemManager ItemManagerScripts;

    //*****Apply Singleton to PlayerControllers*****
    public static PlayerControllers Singleton { get; private set; }

    private void Awake()
    {
        if (Singleton == null)
        {
            Singleton = this;
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
    }
    void Update()
    {
        KeyBoardController();   
        //TouchController();
    }

    void TouchController()
    {
        if (isPressedButtonRight == true)
        {
            PlayerMoveRight();
        }
        else if (isPressedButtonLeft == true)
        {
            PlayerMoveLeft();
        }
        else
        {
            PlayerStopMovement();
        }
    }

    void KeyBoardController()
    {
        if ((Input.GetKeyDown("l")))
        {
            if (canDash == true)
            {
                StartCoroutine(PlayerDash());
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
            AudioManager.instance.PlaySoundLaser();
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
            AudioManager.instance.PlaySoundJump();
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
    IEnumerator PlayerDash()
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

    void PlayerAttack()
    {
        StartCoroutine(PrepareNonLoopAnimation("Attack"));
        CreateBullet();
    }

    void CreateBullet()
    {
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

    }

    //************************************************
    //************************************************
    //*****************FIRST SKILL********************
    void PlaySkillFirst()
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
        if (isGrounded == true && JumpCount <= 0)
        {
            if (JumpCount < MaxJumpCount)
            {
                JumpCount++;

                PlayerRigidbody2D.AddForce(new Vector2(0, 1) * jumpStrength *2f, ForceMode2D.Impulse);
                StartCoroutine(AnimationJump());
                StartCoroutine(CheckJumpCount());
            }
        }
        if (isGrounded == false && JumpCount > 0)
        {
            if (JumpCount < MaxJumpCount)
            {
                JumpCount++;

                PlayerRigidbody2D.AddForce(new Vector2(0, 1) * jumpStrength, ForceMode2D.Impulse);
                StartCoroutine(AnimationJump());
            }
        }
    }

    //Reset JumpCount về 0 khi người chơi chạm đất
    public void ResetJumpCount()
    {
        JumpCount = 0;
    }

    public IEnumerator CheckJumpCount()
    {
        yield return new WaitForSeconds(0.1f);
        if (isGrounded == true)
        {
            JumpCount = 0;
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
                AudioManager.instance.PlaySoundGameOver();
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
        yield return new WaitForSeconds(2); //can't take dame in 3 seconds
        //isCanTakeDamage = true;
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
            PlayerPrefs.SetInt("PlayerHP", HealthPoint);
            PlayerPrefs.Save(); //call this when game is loading
            Debug.Log("has recovered");
        }
    }

    public void GameOver()
    {
        GameOverManager.Singleton.PlayerDied();
        // Switch to GameOver scene
        SceneManager.LoadScene("GameOver");
    }

    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Item")
        {
            Debug.Log("Đã gom");
            ItemManagerScripts.itemCount++;
            Destroy(other.gameObject);
        }
    }
    

}

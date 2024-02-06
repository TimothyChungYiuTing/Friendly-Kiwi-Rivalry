using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public enum PlayerID {Kiwi1, Kiwi2}

public class KiwiPlayer : MonoBehaviour
{
    public PlayerID playerID;
    public int gameIndex;

    private float speed = 15.0f;
    private Rigidbody2D rb;
    private KeyCode up;
    private KeyCode left;
    private KeyCode down;
    private KeyCode right;
    private KeyCode interact;

    private LevelController levelController;

    
    [Header("Sprite Management")]
        private SpriteRenderer spriteRenderer;
        private int spriteIndex = 0;
        private float lastFrameTime;

    [Header("Top-Down Sprites")]
        public Sprite TD_Still;
        public List<Sprite> TD_Runs;
        public Sprite TD_Kick_L;
        public Sprite TD_Kick_R;

    [Header("Platformer Sprites")]
        public Sprite P_Back;
        public Sprite P_Kick;
        public List<Sprite> P_Runs;
        public List<Sprite> P_Spins;

    [Header("Audio Clips")]
        private AudioSource audioSource;
        public AudioClip FeetSFX;
        public AudioClip SpinSFX;
        public AudioClip KickSFX;
        public AudioClip ShootSFX;
        public AudioClip LoseSFX;
    
    [Header("Game0")]
        public bool touched = false;

    [Header("Game1")]
        public GameObject KickHitbox;
        private float lastTimeKicked = 0f;
        private float kickCD = 0.2f;
        public int hearts = 3;
        public Canvas1 canvas1;

    [Header("Game2")]
        public bool smashStarted = false;
        public bool smashEnded = false;
        public int smashCount = 0;
        public BoostedPlane boostedPlane;

    [Header("Game3")]
        public Target target;
        private Camera cam;
        private Rigidbody2D targetRB;
        private float lastTimeShot = -10f;
        private float shootCD = 1.0f;

    [Header("Game4")]
        public Canvas4 canvas4;
        public int hp = 10;
        private bool lost = false;
    
    // Start is called before the first frame update
    void Start()
    {
        gameIndex = GameManager.Instance.currentGameIndex;
        InitializeInputs();
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        audioSource = GetComponent<AudioSource>();
        levelController = FindObjectOfType<LevelController>();
        switch (gameIndex) {
            case 0:
                audioSource.clip = FeetSFX;
                break;
            case 1:
                audioSource.clip = KickSFX;
                rb.bodyType = RigidbodyType2D.Kinematic;
                break;
            case 2:
                if (GameManager.Instance.currentGameIndex == 2) {
                    Invoke("SmashStart", 3.2f);
                }   
                break;
            case 3:
                cam = FindObjectOfType<SplitScreenController>().p1Camera;
                targetRB = target.transform.GetComponent<Rigidbody2D>();
                audioSource.clip = ShootSFX;
                break;
            case 4:
                audioSource.clip = FeetSFX;
                rb.gravityScale = 0.5f;
                break;
            case 5:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameIndex) {
            case 0:
                if (!touched)
                    TopDownMovement();
                break;
            case 1:
                if (Time.time - lastTimeKicked > kickCD) {
                    KarateMovement();
                }
                break;
            case 2:
                RecordSmashes();
                break;
            case 3:
                TargetMovement();
                break;
            case 4:
                if (!lost)
                    PlatformerMovement();
                break;
            case 5:
                break;
            default:
                break;
        }
    }

    private void InitializeInputs()
    {
        if (playerID == PlayerID.Kiwi1) {
            up = KeyCode.W;
            left = KeyCode.A;
            down = KeyCode.S;
            right = KeyCode.D;
            interact = KeyCode.Space;
        }
        else {
            up = KeyCode.I;
            left = KeyCode.J;
            down = KeyCode.K;
            right = KeyCode.L;
            interact = KeyCode.Return;
        }
    }

    private void TopDownMovement()
    {        
        float horizontalInput = 0f;
        float verticalInput = 0f;
		if(Input.GetKey(up)) {
            verticalInput = 1f;
		}
		if(Input.GetKey(left)) {
            horizontalInput = -1f;
		}
		if(Input.GetKey(down)) {
            verticalInput = -1f;
		}
		if(Input.GetKey(right)) {
            horizontalInput = 1f;
		}

        //Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        if (movement == Vector2.zero) {
            rb.AddForce(-rb.velocity.normalized, ForceMode2D.Force);
        }
        rb.AddForce(movement * 3.7f, ForceMode2D.Force);
        //Clamp magnitude of speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, speed);
        //Transform.position += movement * speed * Time.deltaTime;

        //Update the player's facing direction based on movement
        if (rb.velocity.magnitude > 0.5f || movement != Vector2.zero)
        {
            float angle = Mathf.Atan2(rb.velocity.y, rb.velocity.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            //Animations
            if (Time.time - lastFrameTime > (2f / rb.velocity.magnitude)) {
                spriteIndex++;
                spriteIndex %= TD_Runs.Count;
                spriteRenderer.sprite = TD_Runs[spriteIndex];
                lastFrameTime = Time.time;

                audioSource.Play();
            }
        }
        else {
            //Animation
            spriteRenderer.sprite = TD_Still;
            lastFrameTime = -1f;
        }
    }

    private void KarateMovement()
    {        
        int dir = -1;
		if(Input.GetKeyDown(right)) {
            dir = 0;
            Instantiate(KickHitbox, transform.position + Vector3.right * 1.5f, Quaternion.Euler(0f, 0f, 90f * dir));
		}
		if(Input.GetKeyDown(up)) {
            dir = 1;
            Instantiate(KickHitbox, transform.position + Vector3.up * 1.5f, Quaternion.Euler(0f, 0f, 90f * dir));
		}
		if(Input.GetKeyDown(left)) {
            dir = 2;
            Instantiate(KickHitbox, transform.position + Vector3.left * 1.5f, Quaternion.Euler(0f, 0f, 90f * dir));
		}
		if(Input.GetKeyDown(down)) {
            dir = 3;
            Instantiate(KickHitbox, transform.position + Vector3.down * 1.5f, Quaternion.Euler(0f, 0f, 90f * dir));
		}

        if (dir != -1) {
            lastTimeKicked = Time.time;
            audioSource.clip = KickSFX;
            audioSource.Play();
            if (Random.Range(0,2) == 0)
                spriteRenderer.sprite = TD_Kick_L;
            else
                spriteRenderer.sprite = TD_Kick_R;
            Invoke("BackTo_TDStill", 0.1f);
            transform.rotation = Quaternion.Euler(0f, 0f, 90f * dir);

        }
    }

    private void BackTo_TDStill()
    {
        spriteRenderer.sprite = TD_Still;
    }

    public void Hurt()
    {
        hearts--;
        canvas1.UpdateHearts(hearts);

        if (hearts <= 0 && !levelController.concluded) {
            //Karate win (lose)
            levelController.concluded = true;
            if (playerID == PlayerID.Kiwi1)
                GameManager.Instance.PrevWinner = PlayerID.Kiwi2;
            else
                GameManager.Instance.PrevWinner = PlayerID.Kiwi1;

            levelController.TransitionSignal();
        }
    }

    private void SmashStart()
    {
        smashStarted = true;
        Invoke("EndSmash", 6f);
        audioSource.clip = SpinSFX;
    }

    private void EndSmash()
    {
        smashEnded = true;
        spriteRenderer.sprite = P_Kick;
        boostedPlane.smashCount = smashCount;
        boostedPlane.flying = true;
        boostedPlane.transform.GetComponent<Rigidbody2D>().AddForce(Vector2.right * (smashCount + 3f) * 0.4f, ForceMode2D.Impulse);
        audioSource.clip = KickSFX;
        audioSource.Play();
    }

    private void RecordSmashes()
    {
		if(smashStarted && !smashEnded && Input.GetKey(interact)) {
            smashCount++;
            //Animation
            spriteIndex++;
            spriteIndex %= P_Spins.Count;
            spriteRenderer.sprite = P_Spins[spriteIndex];
            if (smashCount < 20) {
                if (smashCount % 5 == 1) {
                    audioSource.Play();
                }
            }
            else if (smashCount < 40) {
                if (smashCount % 4 == 1) {
                    audioSource.Play();
                }
            }
            else if (smashCount < 70) {
                if (smashCount % 3 == 1) {
                    audioSource.Play();
                }
            }
            else {
                if (smashCount % 2 == 1) {
                    audioSource.Play();
                }
            }
		}
    }

    private void TargetMovement()
    {        
        float horizontalInput = 0f;
        float verticalInput = 0f;
		if(Input.GetKey(up)) {
            verticalInput = 1f;
		}
		if(Input.GetKey(left)) {
            horizontalInput = -1f;
		}
		if(Input.GetKey(down)) {
            verticalInput = -1f;
		}
		if(Input.GetKey(right)) {
            horizontalInput = 1f;
		}

        //Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, verticalInput).normalized;

        targetRB.velocity = movement * speed;
        //Prevent Targets from exiting screen (By clamping)
        target.transform.position = new Vector3(Mathf.Clamp(target.transform.position.x, -cam.orthographicSize * cam.aspect, cam.orthographicSize * cam.aspect), Mathf.Clamp(target.transform.position.y, -cam.orthographicSize, cam.orthographicSize), target.transform.position.z);
        
		if(Input.GetKeyDown(interact)) {
            if (Time.time - lastTimeShot > shootCD) {
                lastTimeShot = Time.time;
                audioSource.Play();
                target.Shoot();
            }
		}
    }

    private void PlatformerMovement()
    {        
        float horizontalInput = 0f;
		if(Input.GetKey(left)) {
            horizontalInput = -1f;
		}
		if(Input.GetKey(right)) {
            horizontalInput = 1f;
		}

        //Calculate the movement direction
        Vector2 movement = new Vector2(horizontalInput, 0f).normalized;

        rb.AddForce(movement * 20f, ForceMode2D.Force);
        //Clamp magnitude of speed
        rb.velocity = Vector2.ClampMagnitude(rb.velocity, 12f);
        //Transform.position += movement * speed * Time.deltaTime;

        //Update the player's facing direction based on movement
        if (movement != Vector2.zero)
        {
            if (horizontalInput > 0)
                spriteRenderer.flipX = false;
            else if (horizontalInput < 0)
                spriteRenderer.flipX = true;

            //Animations
            if (Time.time - lastFrameTime > (2f / rb.velocity.magnitude)) {
                spriteIndex++;
                spriteIndex %= P_Runs.Count;
                spriteRenderer.sprite = P_Runs[spriteIndex];
                lastFrameTime = Time.time;

                audioSource.Play();
            }
        }
    }

    public void Seeded()
    {
        if (hp > 0) {
            hp--;
            canvas4.HP.localScale = new Vector3(0.1f * hp, 1f, 1f);
            if (hp <= 0) {
                lost = true;
                spriteRenderer.flipY = true;

                //Dodge win (lose)
                if (!levelController.concluded) {
                    levelController.concluded = true;
                    if (playerID == PlayerID.Kiwi1)
                        GameManager.Instance.PrevWinner = PlayerID.Kiwi2;
                    else
                        GameManager.Instance.PrevWinner = PlayerID.Kiwi1;

                    levelController.TransitionSignal();
                }
            }
        }
    }

    private void ToTransition()
    {
        SceneManager.LoadScene("Transition", LoadSceneMode.Single);
    }
}

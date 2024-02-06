using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Invader : MonoBehaviour
{
    private int runSpriteIndex = 0;
    public float speed = 4f;
    public int dirIndex;
    public Transform playerTransform;
    public List<Transform> wayPoints;
    
    public GameObject Juice;

    public bool trick = false;
    public int trickIndex;
    private bool tricked = false;

    private bool dead = false;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;

    private float fadeOutStartTime;

    private AudioSource ShatterSFX;
    
    [Header("Sprites")]
    public List<Sprite> TD_Runs;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        ShatterSFX = GetComponent<AudioSource>();
        InvokeRepeating("RunAnimation", Random.Range(0f, 0.15f), 0.15f);

        Destroy(gameObject, 6f);
    }

    // Update is called once per frame
    void Update()
    {
        if (!dead) {
            if (trick && ((Vector2)playerTransform.position - (Vector2)transform.position).magnitude < 8f && !tricked) {
                if (((Vector2)wayPoints[trickIndex].position - (Vector2)transform.position).magnitude > 0.2f)
                    transform.position += (Vector3)(((Vector2)wayPoints[trickIndex].position - (Vector2)transform.position).normalized * Time.deltaTime * speed * 2);
                else
                    tricked = true;
            }
            else {
                transform.position += (Vector3)(((Vector2)playerTransform.position - (Vector2)transform.position).normalized * Time.deltaTime * speed);
            }
        }
        else {
            Color fromColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.7f);
            Color toColor = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.2f);
            spriteRenderer.color = Color.Lerp(fromColor, toColor, (Time.time-fadeOutStartTime)/7f);

            transform.localScale = Vector3.Lerp(Vector3.one, Vector3.one * 5f, (Time.time-fadeOutStartTime)/7f);
        }
    }
    
    private void RunAnimation()
    {
        runSpriteIndex++;
        runSpriteIndex %= 2;
        if (!dead)
            spriteRenderer.sprite = TD_Runs[runSpriteIndex];
        else {

        }
    }

    public void Kill()
    {
        Instantiate(Juice, transform.position, Quaternion.identity);
        //TODO: Change this
        transform.position = new Vector3(transform.position.x, transform.position.y, -0.2f);

        Vector2 kickDir = Quaternion.AngleAxis(Random.Range(-15f, 15f), Vector3.forward) * ((Vector2)transform.position - (Vector2)playerTransform.position).normalized;
        rb.AddForce((Vector3)(kickDir * 40f), ForceMode2D.Impulse);
        
        dead = true;
        fadeOutStartTime = Time.time;
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, 0.7f);
        rb.AddTorque(40f);

        Destroy(gameObject, 0.3f);
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (!dead && other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            if (other.transform.GetComponent<KiwiPlayer>().hearts > 0) {
                dead = true;
                ShatterSFX.Play();
                other.transform.GetComponent<KiwiPlayer>().Hurt();
                spriteRenderer.enabled = false;
            }
            Destroy(gameObject, 0.4f);
        }
    }
}

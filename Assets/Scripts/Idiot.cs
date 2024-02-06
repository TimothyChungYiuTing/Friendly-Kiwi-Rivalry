using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Idiot : MonoBehaviour
{
    public GameObject Juice;
    private AudioSource shotBird;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    public int dir = 1;

    private bool dead = false;

    private float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(7f, 11f) * (2f - (transform.position.y + 3f)/10f);
        shotBird = GetComponent<AudioSource>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();

        transform.localScale = Vector3.Lerp(new Vector3(0.5f, 0.5f, 1f), new Vector3(0.2f, 0.2f, 1f), (transform.position.y + 3f)/10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (dir == -1 && !spriteRenderer.flipX) {
            spriteRenderer.flipX = true;
        }

        if (!dead)
            transform.position += (Vector3)(Vector2.right * Time.deltaTime * speed * dir);

        if (transform.position.x > 35f || transform.position.x < -35f) {
            Destroy(gameObject);
        }
    }
    
    public void Shot()
    {
        shotBird.Play();
        Instantiate(Juice, new Vector3(transform.position.x, transform.position.y, 0.3f), Quaternion.identity);
        rb.gravityScale = 30f;
        Destroy(gameObject, 3f);
    }
}

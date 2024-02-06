using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class KiwiFrisbee : MonoBehaviour
{
    public Transform followingTransform;
    private bool collided = false;
    private CameraScript targetedCam = null;

    private float spawnedTime;
    private float collidedTime;

    private List<Canvas0> canvas0s = null;
    private Rigidbody2D rb;

    private bool distConcluded = false;
    private int hitID = -1;

    private LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        spawnedTime = Time.time;
        rb = GetComponent<Rigidbody2D>();
        levelController = FindObjectOfType<LevelController>();
    }

    private void OnCollisionEnter2D(Collision2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player")) {
            KiwiPlayer kiwiPlayer = other.transform.GetComponent<KiwiPlayer>();
            if (!kiwiPlayer.touched) {
                collided = true;
                collidedTime = Time.time;
                kiwiPlayer.touched = true;
                GetComponent<SpriteRenderer>().enabled = true;
                followingTransform.GetComponent<KiwiFrisbeeMaker>().Cut();

                //Change camera target
                foreach (CameraScript cam in FindObjectsOfType<CameraScript>()) {
                    if (cam.kiwiPlayerTransform.GetComponent<KiwiPlayer>()?.playerID == kiwiPlayer.playerID) {
                        targetedCam = cam;
                        Invoke("ChangeCameraTarget", 0.8f);
                    }
                }

                canvas0s = FindObjectsOfType<Canvas0>().ToList();
                canvas0s.Sort((a,b) => {return a.ID.CompareTo(b.ID);});

                if (kiwiPlayer.playerID == PlayerID.Kiwi1) {
                    hitID = 0;
                }
                else {
                    hitID = 1;
                }
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!collided && followingTransform != null) {
            transform.position = followingTransform.position;
        }

        if (!collided && Time.time - spawnedTime > 16f)
            Destroy(gameObject);

        if (collided && Time.time - collidedTime > 0.3f) {
            canvas0s[hitID].Text_Dist.text = (transform.position.x - 12.5f).ToString("0.00");
            if (rb.velocity.magnitude < 1.5f) {
                rb.velocity -= rb.velocity.normalized * Time.deltaTime * 2f;
            }
        }

        if (!distConcluded && collided && Time.time - collidedTime > 1.5f && rb.velocity.magnitude < 0.3f) {
            rb.velocity = Vector2.zero;
            distConcluded = true;
            levelController.RegisterDist(hitID, transform.position.x - 12.5f);
        }
    }

    private void ChangeCameraTarget()
    {
        targetedCam.kiwiPlayerTransform = transform;
    }
}

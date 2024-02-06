using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoostedPlane : MonoBehaviour
{
    public PlayerID playerID;
    public bool flying = false;
    public float yMid;
    public int smashCount;
    public Camera relativeCamera;
    private Vector3 cameraOriginalPos;

    public GameObject harmlessSeed;
    private float lastSeedPoopedX = 10f;
    private float randPoopDist = 50f;

    private LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        cameraOriginalPos = relativeCamera.transform.position;
        levelController = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (flying) {
            transform.position = new Vector3(transform.position.x, yMid - Mathf.Sin((transform.position.x + 10f) * 0.015f) * 7f, transform.position.z);
            if (transform.position.x > 15f) {
                //relativeCamera.transform.position = new Vector3(transform.position.x + 7f, cameraOriginalPos.y, cameraOriginalPos.z);
                if (transform.position.x < 30f) {
                    relativeCamera.orthographicSize = Mathf.Lerp(3.5f, 9f, (transform.position.x + 15f) / 15f);
                    relativeCamera.transform.position = Vector3.Lerp(new Vector3(transform.position.x + 7f, cameraOriginalPos.y, cameraOriginalPos.z), new Vector3(transform.position.x + 17f, cameraOriginalPos.y + 3f, cameraOriginalPos.z), (transform.position.x + 15f) / 15f);
                }
                else {
                    relativeCamera.transform.position = new Vector3(transform.position.x + 17f, cameraOriginalPos.y + 3f, cameraOriginalPos.z);
                }
            }

            if (transform.position.x - lastSeedPoopedX > randPoopDist) {
                randPoopDist = Random.Range(20f, 85f);
                lastSeedPoopedX = transform.position.x;
                Instantiate(harmlessSeed, new Vector3(transform.position.x + 0.5f, transform.position.y - 1f, 0.2f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
            }

            if (playerID == PlayerID.Kiwi1)
                levelController.canvas2_1.Text_Dist.text = (transform.position.x + 5.4f).ToString("0.00");
            else
                levelController.canvas2_2.Text_Dist.text = (transform.position.x + 5.4f).ToString("0.00");
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (!levelController.concluded && other.gameObject.layer == LayerMask.NameToLayer("BoostGoal")) {
            //Boost win

            levelController.concluded = true;
            GameManager.Instance.PrevWinner = playerID;
            
            levelController.TransitionSignal();
        }
    }
}

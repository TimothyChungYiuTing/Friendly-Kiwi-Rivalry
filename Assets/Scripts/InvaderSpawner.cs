using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvaderSpawner : MonoBehaviour
{
    public Transform playerTransform;
    public List<Transform> spawnPoints;
    public List<Transform> wayPoints;

    public GameObject Invader;

    private int randSpawnIndex;
    private float lastTimeSpawned = 0.5f;
    private float spawnCD = 1f;

    private float speed = 4f;

    // Start is called before the first frame update
    void Start()
    {
        randSpawnIndex = Random.Range(0, 4);
    }

    // Update is called once per frame
    void Update()
    {
        spawnCD -= Time.deltaTime * 0.008f;
        spawnCD = Mathf.Clamp(spawnCD, 0.4f, 1f);
        speed += Time.deltaTime * 0.2f;
        if (Time.time - lastTimeSpawned > spawnCD) {
            lastTimeSpawned = Time.time;

            GameObject InvaderPrefab = Invader;
            InvaderPrefab.GetComponent<Invader>().dirIndex = randSpawnIndex;
            InvaderPrefab.GetComponent<Invader>().playerTransform = playerTransform;
            InvaderPrefab.GetComponent<Invader>().speed = speed;
            InvaderPrefab.GetComponent<Invader>().wayPoints = wayPoints;
            InvaderPrefab.GetComponent<Invader>().trick = false;
            if (speed > 7f && Random.Range(0, 2) == 0) {
                InvaderPrefab.GetComponent<Invader>().trick = true;
                InvaderPrefab.GetComponent<Invader>().trickIndex = (randSpawnIndex + ((Random.Range(0, 2) == 0)?-1:1) + 4) % 4;
            }
            Instantiate(InvaderPrefab, spawnPoints[randSpawnIndex].position, Quaternion.Euler(0f, 0f, 90f * randSpawnIndex + 180f));

            randSpawnIndex = Random.Range(0, 4);
        }
    }
}

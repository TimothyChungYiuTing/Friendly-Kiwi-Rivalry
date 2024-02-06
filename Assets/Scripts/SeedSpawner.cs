using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedSpawner : MonoBehaviour
{
    public GameObject HarmfulSeed;

    private float startTime;

    private float lastTimeSpawnedSeed1 = 0.5f;
    private float lastTimeSpawnedSeed2 = 0.5f;
    private float lastTimeSpawnedSeed3 = 0.5f;

    private float spawnCD1 = 0.8f;
    private float spawnCD2 = 0.8f;
    private float spawnCD3 = 0.8f;
    private float spawnNoise1;
    private float spawnNoise2;
    private float spawnNoise3;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;

        spawnNoise1 = Random.Range(0.8f, 1.2f);
        spawnNoise2 = Random.Range(0.8f, 1.2f);
        spawnNoise3 = Random.Range(0.8f, 1.2f);
    }

    // Update is called once per frame
    void Update()
    {
        spawnCD1 = Mathf.Clamp(0.8f - (Time.time - startTime) * 0.013f, 0.1f, 0.8f);
        spawnCD2 = Mathf.Clamp(0.8f - (Time.time - startTime) * 0.013f, 0.1f, 0.8f);
        spawnCD3 = Mathf.Clamp(0.8f - (Time.time - startTime) * 0.013f, 0.1f, 0.8f);

        if (Time.time - lastTimeSpawnedSeed1 > spawnCD1 * spawnNoise1) {
            lastTimeSpawnedSeed1 = Time.time;
            spawnNoise1 = Random.Range(0.8f, 1.2f);
            SpawnSeed(-12.2f, 6.2f, 0.55f);
        }
        if (Time.time - lastTimeSpawnedSeed2 > spawnCD2 * spawnNoise2) {
            lastTimeSpawnedSeed2 = Time.time;
            spawnNoise2 = Random.Range(0.8f, 1.2f);
            SpawnSeed(0f, 6f);
        }
        if (Time.time - lastTimeSpawnedSeed3 > spawnCD3 * spawnNoise3) {
            lastTimeSpawnedSeed3 = Time.time;
            spawnNoise3 = Random.Range(0.8f, 1.2f);
            SpawnSeed(12.2f, 6.2f, 0.55f);
        }
    }

    private void SpawnSeed(float xOffset, float range, float clampVal = 0)
    {
        if (xOffset < 0)
            Instantiate(HarmfulSeed, transform.position + Vector3.right * xOffset + new Vector3(Mathf.Clamp(Random.Range(-range, range), -range+clampVal, range), 0f, 0f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        else if (xOffset > 0)
            Instantiate(HarmfulSeed, transform.position + Vector3.right * xOffset + new Vector3(Mathf.Clamp(Random.Range(-range, range), -range, range-clampVal), 0f, 0f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
        else
            Instantiate(HarmfulSeed, transform.position + Vector3.right * xOffset + new Vector3(Random.Range(-range, range), 0f, 0f), Quaternion.Euler(0f, 0f, Random.Range(0f, 360f)));
    }
}

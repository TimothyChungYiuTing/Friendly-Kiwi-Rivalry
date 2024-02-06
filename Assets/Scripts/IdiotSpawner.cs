using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdiotSpawner : MonoBehaviour
{
    public GameObject Idiot;
    public bool right;
    private float lastIdiotSpawned = 0.3f;
    private float randomSpawnInterval = 1.0f;
    // Start is called before the first frame update
    void Start()
    {
        randomSpawnInterval = Random.Range(1.6f, 2f) * (2f - (transform.position.y + 3f)/10f);
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - lastIdiotSpawned > randomSpawnInterval) {
            lastIdiotSpawned = Time.time;
            randomSpawnInterval = Random.Range(1.6f, 2f) * (2f - (transform.position.y + 3f)/10f);
            GameObject IdiotPrefab = Idiot;
            if (right)
                IdiotPrefab.GetComponent<Idiot>().dir = 1;
            else
                IdiotPrefab.GetComponent<Idiot>().dir = -1;
            Instantiate(IdiotPrefab, transform.position + Vector3.up * Random.Range(-0.7f, 0.7f), Quaternion.identity);
        }
    }
}

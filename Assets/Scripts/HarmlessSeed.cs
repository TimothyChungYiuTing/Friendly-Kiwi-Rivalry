using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HarmlessSeed : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.forward, 120f * Time.deltaTime);

        if ((transform.position.y < 9f && transform.position.y > -9f) || transform.position.y < -30f) {
            Destroy(gameObject);
        }
    }
}

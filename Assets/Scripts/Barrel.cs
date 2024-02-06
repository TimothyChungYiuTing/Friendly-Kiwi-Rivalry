using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Barrel : MonoBehaviour
{
    public int rollDir = 1;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (transform.position.y < -8f)
            rollDir = 1;
        if (transform.position.y > 8f)
            rollDir = -1;
        transform.position += Vector3.up * Time.deltaTime * 5f * rollDir;
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiSpawner : MonoBehaviour
{
    public GameObject KFM;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("SendKiwiFrisbeeMaker", 0f, 0.7f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void SendKiwiFrisbeeMaker()
    {
        Instantiate(KFM, transform.position, Quaternion.Euler(0f, 0f, -90f));
    }
}

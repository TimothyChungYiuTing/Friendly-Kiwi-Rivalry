using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KickHitbox : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Invader")) {
            other.transform.GetComponent<Invader>().Kill();
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Juice : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    private AudioSource kiwiSquishSFX;

    // Start is called before the first frame update
    void Start()
    {
        kiwiSquishSFX = GetComponent<AudioSource>();
        //kiwiSquishSFX.Play();
        spriteRenderer = GetComponent<SpriteRenderer>();
        Destroy(gameObject, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        spriteRenderer.color = new Color(spriteRenderer.color.r, spriteRenderer.color.g, spriteRenderer.color.b, spriteRenderer.color.a - Time.deltaTime*0.8f);
    }
}

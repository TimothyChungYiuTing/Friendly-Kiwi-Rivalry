using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpKiwi : MonoBehaviour
{
    public Sprite LookFront;
    public Sprite LookUp;
    public Sprite Jump;
    public Sprite Angry;
    public SpriteRenderer spriteRenderer;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartJump()
    {
        InvokeRepeating("HappyJump", 0f, 0.2f);
    }

    public void StopJump()
    {
        CancelInvoke("HappyJump");
        spriteRenderer.sprite = LookFront;
    }
    

    private void HappyJump()
    {
        if (spriteRenderer.sprite == Jump)
            spriteRenderer.sprite = LookFront;
        else
            spriteRenderer.sprite = Jump;
    }
}

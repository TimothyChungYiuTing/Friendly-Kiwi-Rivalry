using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KiwiFrisbeeMaker : MonoBehaviour
{
    private int runSpriteIndex = 0;
    private float speed = 2f;
    public GameObject Kiwi;
    public GameObject Juice;

    private bool cut = false;
    private SpriteRenderer spriteRenderer;

    [Header("Sprites")]
    public List<Sprite> TD_Runs;
    public List<Sprite> TD_Runs_Half;
    // Start is called before the first frame update
    void Start()
    {
        GameObject NewKiwi = Kiwi;
        Kiwi.GetComponent<KiwiFrisbee>().followingTransform = transform;
        Instantiate(NewKiwi, transform.position, Quaternion.Euler(0f, 0f, -90f));
        
        spriteRenderer = GetComponent<SpriteRenderer>();
        InvokeRepeating("RunAnimation", Random.Range(0f, 0.15f), 0.15f);

        Destroy(gameObject, 16f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += (Vector3)(Vector2.down * Time.deltaTime * speed);
    }

    public void Cut()
    {
        cut = true;
        Instantiate(Juice, new Vector3(transform.position.x, transform.position.y, 0.3f), Quaternion.identity);
    }
    
    private void RunAnimation()
    {
        runSpriteIndex++;
        runSpriteIndex %= 2;
        if (!cut)
            spriteRenderer.sprite = TD_Runs[runSpriteIndex];
        else
            spriteRenderer.sprite = TD_Runs_Half[runSpriteIndex];
    }
}

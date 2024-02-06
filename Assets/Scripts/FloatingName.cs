using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingName : MonoBehaviour
{
    public PlayerID playerID;
    public Transform transformToFollow;
    public float offset = 1.3f;

    // Start is called before the first frame update
    void Start()
    {
        if (playerID == PlayerID.Kiwi1)
            GetComponent<TextMesh>().text = GameManager.Instance.p1Name;
        else
            GetComponent<TextMesh>().text = GameManager.Instance.p2Name;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = new Vector3(transformToFollow.position.x, transformToFollow.position.y + offset, transform.position.z);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    public Transform kiwiPlayerTransform;
    public int gameIndex;
    // Start is called before the first frame update
    void Start()
    {
        gameIndex = GameManager.Instance.currentGameIndex;
        
        switch (gameIndex) {
            case 0:
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                break;
            case 5:
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        switch (gameIndex) {
            case 0:
                Follow(true, true, 10f, 0f);
                break;
            case 1:
                break;
            case 2:
                break;
            case 3:
                break;
            case 4:
                Follow(true, false, 0f, 0f);
                transform.position = new Vector3(Mathf.Clamp(transform.position.x, -9f, 11f), transform.position.y, transform.position.z);
                break;
            case 5:
                break;
            default:
                break;
        }
    }

    private void Follow(bool followX = true, bool followY = true, float xOffset = 0f, float yOffset = 0f)
    {
        if (kiwiPlayerTransform != null) {
            if (followX && followY)
                transform.position = new Vector3(kiwiPlayerTransform.position.x + xOffset, kiwiPlayerTransform.position.y + yOffset, transform.position.z);
            else if (!followX && followY)
                transform.position = new Vector3(transform.position.x, kiwiPlayerTransform.position.y + yOffset, transform.position.z);
            else if (followX && !followY)
                transform.position = new Vector3(kiwiPlayerTransform.position.x + xOffset, transform.position.y, transform.position.z);
        }
    }
}

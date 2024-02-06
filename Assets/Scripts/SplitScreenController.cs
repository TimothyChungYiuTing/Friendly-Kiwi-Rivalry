using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SplitType {None, Horizontal, Vertical}
public class SplitScreenController : MonoBehaviour
{
    public Camera p1Camera;
    public Camera p2Camera;
    
    public GameObject h_line_1;
    public GameObject h_line_2;
    public GameObject v_line_1;
    public GameObject v_line_2;
    public SplitType splitType;

    private float screenWidth;
    private float screenHeight;

    private float originalSize;
    // Start is called before the first frame update
    void Start()
    {        
        // Get the screen width and height
        screenWidth = Screen.width;
        screenHeight = Screen.height;

        originalSize = p1Camera.orthographicSize;

        switch (splitType) {
            case SplitType.None:
                NoneSplit();
                break;
            case SplitType.Horizontal:
                HorizontalSplit();
                break;
            case SplitType.Vertical:
                VerticalSplit();
                break;
            default:
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        /*
        if (Input.GetKeyDown(KeyCode.Alpha0)) {
            NoneSplit();
        }
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            HorizontalSplit();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            VerticalSplit();
        }
        */
    }

    void NoneSplit()
    {
        // Set the aspect ratio for each camera to achieve splitscreen
        p1Camera.aspect = screenWidth / screenHeight;
        p2Camera.aspect = screenWidth / screenHeight;

        // Set the viewport rect for each camera
        p1Camera.rect = new Rect(0f, 0f, 1f, 1f);
        p2Camera.rect = new Rect(0f, 0f, 1f, 1f);
        
        p1Camera.enabled = true;
        p2Camera.enabled = false;
        
        h_line_1.SetActive(false);
        h_line_2.SetActive(false);
        v_line_1.SetActive(false);
        v_line_2.SetActive(false);

        p1Camera.orthographicSize = originalSize;
        p2Camera.orthographicSize = originalSize;
    }

    void HorizontalSplit()
    {
        // Set the aspect ratio for each camera to achieve splitscreen
        p1Camera.aspect = screenWidth / 2 / screenHeight;
        p2Camera.aspect = screenWidth / 2 / screenHeight;

        // Set the viewport rect for each camera
        p2Camera.rect = new Rect(0.5f, 0f, 0.5f, 1f);
        p1Camera.rect = new Rect(0f, 0f, 0.5f, 1f);
        
        p1Camera.enabled = true;
        p2Camera.enabled = true;

        h_line_1.SetActive(true);
        h_line_2.SetActive(true);
        v_line_1.SetActive(false);
        v_line_2.SetActive(false);

        p1Camera.orthographicSize = originalSize;
        p2Camera.orthographicSize = originalSize;
    }

    void VerticalSplit()
    {
        // Set the aspect ratio for each camera to achieve splitscreen
        p1Camera.aspect = screenWidth * 2 / screenHeight;
        p2Camera.aspect = screenWidth * 2 / screenHeight;

        // Set the viewport rect for each camera
        p1Camera.rect = new Rect(0f, 0.5f, 1f, 0.5f);
        p2Camera.rect = new Rect(0f, 0f, 1f, 0.5f);
        
        p1Camera.enabled = true;
        p2Camera.enabled = true;
        
        h_line_1.SetActive(false);
        h_line_2.SetActive(false);
        v_line_1.SetActive(true);
        v_line_2.SetActive(true);

        p1Camera.orthographicSize = originalSize / 2f;
        p2Camera.orthographicSize = originalSize / 2f;
    }
}

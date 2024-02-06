using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CanvasH : MonoBehaviour
{
    private float startTime;
    private bool started = false;
    public Light2D globalLight2D;
    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        GameManager.Instance.RandomizeNextGame();
        AudioManager.Instance.ChangeSong(1);
        transform.GetChild(GameManager.Instance.currentGameIndex + 1).gameObject.SetActive(true);
        globalLight2D = FindObjectOfType<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time - startTime < 0.28f)
            globalLight2D.intensity = Mathf.Clamp01((Time.time - startTime) * 4f) + 0.1f;
        else
            globalLight2D.intensity = (Mathf.Sin(Time.time - startTime)*0.25f) + 0.85f;
        
        if (!started && (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))) {
            started = true;
            GameManager.Instance.LoadNextGame();
        }
    }
}

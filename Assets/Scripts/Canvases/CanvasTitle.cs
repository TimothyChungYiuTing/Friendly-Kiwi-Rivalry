using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class CanvasTitle : MonoBehaviour
{
    private bool started = false;
    private float startTime;
    public List<GameObject> Kiwis;
    private int kiwiCounter = 0;
    private AudioSource audiosource;
    private Light2D globalLight2D;

    // Start is called before the first frame update
    void Start()
    {
        startTime = Time.time;
        AudioManager.Instance.ChangeSong(0);
        audiosource = GetComponent<AudioSource>();
        globalLight2D = FindObjectOfType<Light2D>();
    }

    // Update is called once per frame
    void Update()
    {
        globalLight2D.intensity = Mathf.Clamp01((Time.time - startTime) * 4f) + 0.1f;

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            NextScene();
        }
    }

    public void NextScene()
    {
        if (!started) {
            started = true;
            InvokeRepeating("PopKiwi", 0f, 0.15f);
            Invoke("SwitchScene", 2f);
        }
    }

    private void PopKiwi()
    {
        if (kiwiCounter < Kiwis.Count) {
            Kiwis[kiwiCounter].SetActive(true);
            kiwiCounter++;
            audiosource.Play();
        }
    }

    private void SwitchScene()
    {
        SceneManager.LoadScene("Naming", LoadSceneMode.Single);
    }
}

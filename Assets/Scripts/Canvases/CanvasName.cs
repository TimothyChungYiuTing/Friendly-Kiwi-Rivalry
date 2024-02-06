using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public class CanvasName : MonoBehaviour
{
    public PlayerID playerID;
    public TextMeshProUGUI Text_Name;
    public CanvasName nextCanvas;
    private AudioSource audioSource;

    public AudioClip WrongSFX;
    public AudioClip RandomSFX;
    public AudioClip DoneSFX;
    public AudioClip TypeSFX;

    public JumpKiwi jumpKiwi;
    private Light2D globalLight2D;

    private bool globalLightFade = false;

    public List<string> NameList = new List<string>()
    {
        "Beak-a-Boo",
        "Kiwi McQuilligan",
        "Quirkly Quills",
        "Avian Assassin",
        "If you see this, you're so lucky wtf LOL, but DON'T actually use this name",
        "Featherface",
        "Squawkzilla",
        "Sir Stabbington",
        "Winged Warrior",
        "Pecky",
        "Flightless Friend",
        "Grounded Gusto",
        "Feathered Foot",
        "The Permanently Perched",
        "Juicy Kiwi",
        "Bitten Kiwi",
        "The Pulp Peeper",
        "Beaky Berry",
        "Beak of the Vine",
        "Tangy Tidbit",
        "Sir Sourbeak",
        "The Avian Avocado",
        "The Ripe Ruffler",
        "Peckish Predator",
        "Cannibalistic Fruit",
        "Furball",
        "Snugglefeathers",
        "The Feathery Frenzy",
        "Ancient Trashbird",
        "AnimalFruit",
        "AngryFood",
        "Quilliam the Killer",
        "Matthew's Demise",
        "Alex's Nemesis",
    };

    // Start is called before the first frame update
    void Start()
    {
        GameManager.Instance.p1Score = 5000;
        GameManager.Instance.p2Score = 5000;
        GameManager.Instance.currentGameIndex = 5;
        GameManager.Instance.multiplier = 1;
        audioSource = GetComponent<AudioSource>();
        AudioManager.Instance.ChangeSong(0);
        globalLight2D = GetComponent<Light2D>();
        jumpKiwi.StartJump();

        if (playerID == PlayerID.Kiwi1)
            Text_Name.text = GameManager.Instance.p1Name;
        else
            Text_Name.text = GameManager.Instance.p2Name;
    }

    // Update is called once per frame
    void Update()
    {
        if (globalLightFade) {
            globalLight2D.intensity -= Time.deltaTime * 1.5f;
        }

        if (Input.GetKeyDown(KeyCode.Backspace)) {
            if (Text_Name.text.Length > 0) {
                audioSource.clip = TypeSFX;
                audioSource.Play();
                Text_Name.text = Text_Name.text.Substring(0, Text_Name.text.Length - 1);
            }
        }
        else if (Input.GetKeyDown(KeyCode.Return)) {
            if (Text_Name.text == "If you see this, you're so lucky wtf LOL, but DON'T actually use this name") {
                SceneManager.LoadScene("Broken", LoadSceneMode.Single);
            }
            else if (Text_Name.text.Length < 1 || Text_Name.text.Length > 24 || (playerID == PlayerID.Kiwi2 && Text_Name.text == GameManager.Instance.p1Name)) {
                audioSource.clip = WrongSFX;
                audioSource.Play();
            }
            else {
                if (playerID == PlayerID.Kiwi1) {
                    GameManager.Instance.p1Name = Text_Name.text;
                    jumpKiwi.transform.GetChild(0).GetComponent<TextMesh>().text = Text_Name.text;
                    nextCanvas.gameObject.SetActive(true);
                }
                if (playerID == PlayerID.Kiwi2) {
                    GameManager.Instance.p2Name = Text_Name.text;
                    jumpKiwi.transform.GetChild(0).GetComponent<TextMesh>().text = Text_Name.text;
                    AudioManager.Instance.Fade();
                    globalLightFade = true;
                    Invoke("NextScene", 0.7f);
                }
                audioSource.clip = DoneSFX;
                audioSource.Play();
                Invoke("SetInactive", 0.3f);
                jumpKiwi.StopJump();
            }
        }
        else if (Regex.IsMatch(Input.inputString, @"^[a-zA-Z0-9\-_ ]*$")) {
            if (Input.inputString != "") {
                audioSource.clip = TypeSFX;
                audioSource.Play();
                Text_Name.text += Input.inputString;
            }
        }
    }

    private void NextScene()
    {
        SceneManager.LoadScene("HowToPlay", LoadSceneMode.Single);
    }

    public void RandomizeName()
    {
        string newName;
        do {
            newName = NameList[Random.Range(0, NameList.Count)];
        } while (Text_Name.text == newName || GameManager.Instance.p1Name == newName || GameManager.Instance.p2Name == newName);
        audioSource.clip = RandomSFX;
        audioSource.Play();
        Text_Name.text = newName;
    }

    private void SetInactive()
    {
        gameObject.SetActive(false);
    }
}

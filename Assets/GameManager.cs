using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.SceneManagement;

public enum State {
    TITLE, MENU, GAME, SETTINGS, END
}

public class GameManager : Singleton<GameManager>
{
    public int currentGameIndex = -1;
    public List<string> sceneNames;
    public List<int> prev3Games;

    public float p1Score = 5000f;
    public float p2Score = 5000f;
    private float normalWinScore = 500f;
    public float multiplier = 1.0f;

    public PlayerID PrevWinner;
    public string p1Name;
    public string p2Name;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.M)) {
            //Randomize next game
            RandomizeNextGame();
        }
        
        //Debug
        /*
        if (Input.GetKeyDown(KeyCode.R)) {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
        */
    }

    public void UpdateScore()
    {
        if (PrevWinner == PlayerID.Kiwi1) {
            p1Score += normalWinScore * multiplier;
            p2Score -= normalWinScore * multiplier;
            multiplier += 0.5f;
        }
        else {
            p1Score -= normalWinScore * multiplier;
            p2Score += normalWinScore * multiplier;
            multiplier += 0.5f;
        }
    }

    public void RandomizeNextGame()
    {
            int randomGameIndex;
            do {
                randomGameIndex = Random.Range(0, sceneNames.Count);
            } while (prev3Games.Contains(randomGameIndex));
            currentGameIndex = randomGameIndex;
            if (prev3Games.Count == 3)
                prev3Games.RemoveAt(0);
            prev3Games.Add(currentGameIndex);
    }

    public void LoadNextGame()
    {
        AudioManager.Instance.Fade();
        Invoke("SceneChangeNextGame", 0.7f);
    }
    private void SceneChangeNextGame()
    {
        SceneManager.LoadScene(sceneNames[currentGameIndex], LoadSceneMode.Single);
        AudioManager.Instance.ChangeSong(currentGameIndex + 3);
    }
}
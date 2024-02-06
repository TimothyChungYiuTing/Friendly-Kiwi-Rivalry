using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CanvasW : MonoBehaviour
{
    public TextMeshProUGUI Text_Win;
    public WinKiwi winKiwi1;
    public WinKiwi winKiwi2;

    private bool WK1_happy = false;
    private bool WK2_happy = false;
    
    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.ChangeSong(1);
        if (GameManager.Instance.p1Score > GameManager.Instance.p2Score) {
            Text_Win.text = GameManager.Instance.p1Name + " Wins!";
            WK1_happy = true;
            InvokeRepeating("HappyJump", 0f, 0.2f);
        }
        else {
            Text_Win.text = GameManager.Instance.p2Name + " Wins!";
            WK2_happy = true;
            InvokeRepeating("HappyJump", 0f, 0.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return)) {
            AudioManager.Instance.Fade();
            Invoke("SwitchScene", 0.7f);
        }
    }

    private void HappyJump()
    {
        if (WK1_happy) {
            if (winKiwi1.spriteRenderer.sprite == winKiwi1.Jump)
                winKiwi1.spriteRenderer.sprite = winKiwi1.LookFront;
            else
                winKiwi1.spriteRenderer.sprite = winKiwi1.Jump;
        }
        if (WK2_happy) {
            if (winKiwi2.spriteRenderer.sprite == winKiwi2.Jump)
                winKiwi2.spriteRenderer.sprite = winKiwi2.LookFront;
            else
                winKiwi2.spriteRenderer.sprite = winKiwi2.Jump;
        }
    }
    
    private void SwitchScene()
    {
        SceneManager.LoadScene("TitleScreen", LoadSceneMode.Single);
    }
}

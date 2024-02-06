using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    private AudioSource audioSource;
    public AudioClip lowBeepSFX;
    public AudioClip highBeepSFX;
    public AudioClip loseSFX;

    public float p1ResultDist = -999f;
    public float p2ResultDist = -999f;

    public bool concluded = false;

    public Canvas2 canvas2_1;
    public Canvas2 canvas2_2;
    private int cooldownNum = 3;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        if (GameManager.Instance.currentGameIndex == 2) {
            Invoke("LowBeep", 0.2f);
            Invoke("LowBeep", 1.2f);
            Invoke("LowBeep", 2.2f);
            Invoke("HighBeep", 3.2f);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void LowBeep()
    {
        audioSource.clip = lowBeepSFX;
        audioSource.Play();
        canvas2_1.Text_CountDown.text = cooldownNum.ToString();
        canvas2_2.Text_CountDown.text = cooldownNum.ToString();
        cooldownNum--;
    }

    private void HighBeep()
    {
        audioSource.clip = highBeepSFX;
        audioSource.Play();
        canvas2_1.Text_CountDown.text = "";
        canvas2_2.Text_CountDown.text = "";

        Invoke("BoostConclude", 13f);
    }

    public void RegisterDist(int ID, float dist) {
        if (ID == 0) {
            p1ResultDist = dist;
        }
        else {
            p2ResultDist = dist;
        }

        if (p1ResultDist != -999f && p2ResultDist != -999f) {
            //Frisbee win
            if (p1ResultDist > p2ResultDist)
                GameManager.Instance.PrevWinner = PlayerID.Kiwi1;
            else
                GameManager.Instance.PrevWinner = PlayerID.Kiwi2;
            
            TransitionSignal();
        }
    }

    private void BoostConclude()
    {
        if (!concluded && float.Parse(canvas2_1.Text_Dist.text) < 220f && float.Parse(canvas2_2.Text_Dist.text) < 220f) {
            //If too slow

            //Boost win
            concluded = true;
            GameManager.Instance.PrevWinner = (float.Parse(canvas2_1.Text_Dist.text) > float.Parse(canvas2_2.Text_Dist.text)) ? PlayerID.Kiwi1 : PlayerID.Kiwi2;
            TransitionSignal();
        }
    }

    public void TransitionSignal()
    {
        audioSource.clip = loseSFX;
        audioSource.Play();
        AudioManager.Instance.Fade();
        Invoke("ToTransition", 0.7f);
    }

    private void ToTransition()
    {
        SceneManager.LoadScene("Transition", LoadSceneMode.Single);
    }
}

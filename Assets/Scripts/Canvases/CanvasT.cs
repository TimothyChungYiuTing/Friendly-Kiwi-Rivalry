using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CanvasT : MonoBehaviour
{
    public Slider Slider_Score;
    public TextMeshProUGUI Text_P1Score;
    public TextMeshProUGUI Text_P2Score;
    public TextMeshProUGUI Text_Multiplier;
    public TextMeshProUGUI Text_P1Name;
    public TextMeshProUGUI Text_P2Name;

    private AudioSource audioSource;
    public AudioClip BarSFX;
    public AudioClip MultSFX;
    public AudioClip WinSFX;
    private float original_p1Score;
    private float original_p2Score;
    private float original_multiplier;

    private float new_p1Score;
    private float new_p2Score;
    private float new_multiplier;

    private float sliderStartTime = 0.4f;

    public TransitionKiwi TK1;
    public TransitionKiwi TK2;

    private bool TK1_happy = false;
    private bool TK2_happy = false;

    private bool transitioned = false;

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.Instance.ChangeSong(1);

        audioSource = GetComponent<AudioSource>();

        PlayBarSFX();

        InvokeRepeating("HappyJump", 0f, 0.2f);

        Invoke("LookUp", 0.3f);

        original_p1Score = GameManager.Instance.p1Score;
        original_p2Score = GameManager.Instance.p2Score;
        original_multiplier = GameManager.Instance.multiplier;
        Text_P1Score.text = original_p1Score.ToString();
        Text_P2Score.text = original_p2Score.ToString();
        Text_Multiplier.text = "x" + original_multiplier.ToString();

        Text_P1Name.text = GameManager.Instance.p1Name;
        Text_P2Name.text = GameManager.Instance.p2Name;

        GameManager.Instance.UpdateScore();

        new_p1Score = GameManager.Instance.p1Score;
        new_p2Score = GameManager.Instance.p2Score;
        new_multiplier = GameManager.Instance.multiplier;
        Invoke("Reaction", 1.5f);
        Invoke("UpdateMultiplier", 2f);
    }

    // Update is called once per frame
    void Update()
    {
        Text_P1Score.text = ((int)Mathf.Lerp(original_p1Score, new_p1Score, (Time.timeSinceLevelLoad - sliderStartTime)*2)).ToString();
        Text_P2Score.text = ((int)Mathf.Lerp(original_p2Score, new_p2Score, (Time.timeSinceLevelLoad - sliderStartTime)*2)).ToString();
        Slider_Score.value = Mathf.Lerp(original_p1Score/10000f, new_p1Score/10000f, (Time.timeSinceLevelLoad - sliderStartTime)*2);

        if (!transitioned && Time.timeSinceLevelLoad > 2.8f) {
            transitioned = true;
            if (new_p1Score <= 0f || new_p2Score <= 0f)
                SceneManager.LoadScene("Win", LoadSceneMode.Single);
            else
                SceneManager.LoadScene("HowToPlay", LoadSceneMode.Single);
        }
    }

    private void PlayBarSFX()
    {
        audioSource.clip = BarSFX;
        audioSource.Play();
    }

    private void LookUp()
    {
        TK1.spriteRenderer.sprite = TK1.LookUp;
        TK2.spriteRenderer.sprite = TK2.LookUp;
    }

    private void UpdateMultiplier()
    {
        Text_Multiplier.text = "x" + new_multiplier.ToString();
        Text_Multiplier.fontSize = 128f;

        audioSource.clip = MultSFX;
        audioSource.Play();

        Invoke("FontSize72", 0.2f);
    }

    private void FontSize72()
    {
        Text_Multiplier.fontSize = 72f;
    }

    private void Reaction()
    {
        if (GameManager.Instance.PrevWinner == PlayerID.Kiwi1) {
            TK1_happy = true;
            TK2.spriteRenderer.sprite = TK2.Angry;
        }
        else {
            TK1.spriteRenderer.sprite = TK1.Angry;
            TK2_happy = true;
        }
    }

    private void HappyJump()
    {
        if (TK1_happy) {
            if (TK1.spriteRenderer.sprite == TK1.Jump)
                TK1.spriteRenderer.sprite = TK1.LookFront;
            else
                TK1.spriteRenderer.sprite = TK1.Jump;
        }
        if (TK2_happy) {
            if (TK2.spriteRenderer.sprite == TK2.Jump)
                TK2.spriteRenderer.sprite = TK2.LookFront;
            else
                TK2.spriteRenderer.sprite = TK2.Jump;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Target : MonoBehaviour
{
    private AudioSource ShellsSFX;
    public List<Idiot> OverlappingIdiots;
    public PlayerID playerID;
    private Canvas3 canvas3;
    private int shotCount = 0;
    private LevelController levelController;

    // Start is called before the first frame update
    void Start()
    {
        ShellsSFX = GetComponent<AudioSource>();
        canvas3 = FindObjectOfType<Canvas3>();
        levelController = FindObjectOfType<LevelController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shoot()
    {
        transform.localScale = new Vector3(2f, 2f, 2f);
        Invoke("TurnOffShootSignal", 0.05f);
        Invoke("DelayedGunShells", 0.2f);

        foreach(Idiot idiot in OverlappingIdiots) {
            idiot.Shot();
            shotCount++;
            if (playerID == PlayerID.Kiwi1)
                canvas3.Text_ShotCount_1.text = shotCount.ToString();
            else
                canvas3.Text_ShotCount_2.text = shotCount.ToString();
            if (!levelController.concluded && shotCount == 10) {
                if (playerID == PlayerID.Kiwi1)
                    canvas3.Text_ShotCount_1.fontSize = 108;
                else
                    canvas3.Text_ShotCount_2.fontSize = 108;
                
                //Snipe win
                levelController.concluded = true;
                GameManager.Instance.PrevWinner = playerID;
                
                levelController.TransitionSignal();
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Idiot")) {
            OverlappingIdiots.Add(other.transform.GetComponent<Idiot>());
        }
    }

    private void OnTriggerExit2D(Collider2D other) {
        if (other.gameObject.layer == LayerMask.NameToLayer("Idiot")) {
            OverlappingIdiots.Remove(other.transform.GetComponent<Idiot>());
        }
    }

    private void TurnOffShootSignal()
    {
        transform.localScale = new Vector3(1f, 1f, 1f);
    }

    private void DelayedGunShells()
    {
        ShellsSFX.Play();
    }
}

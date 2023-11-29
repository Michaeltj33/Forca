using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelSfx : MonoBehaviour
{
    private int getValueMusic;
    public GameObject musicOn;
    public GameObject musicOff;
    public AudioSource audioSource;
    private GameController gameController;
    public bool rodarStart;

    // Start is called before the first frame update
    void Start()
    {
        if (rodarStart)
        {
            gameController = GameObject.Find("Panel").GetComponent<GameController>();
            getValueMusic = gameController.VerifySaveInt("music", 1);
            StatusMusic(getValueMusic);
        }

    }
    public void PlaySound()
    {
        audioSource.Play();
    }

    public void StatusMusic(int statusMusic)
    {
        if (statusMusic == 0)
        {
            musicOn.SetActive(false);
            musicOff.SetActive(true);
        }
        else
        {
            musicOn.SetActive(true);
            musicOff.SetActive(false);
        }
    }
}


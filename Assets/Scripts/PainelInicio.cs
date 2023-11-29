using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PainelInicio : MonoBehaviour
{
    public Sprite[] sprite;
    public int posImage;
    int getNumberCerebro;
    public Image image;
    public GameObject musicOn;
    public GameObject musicOff;
    private GameController gameController;
    private AudioSource sfxMusic;
    private Music allowAudio;
    private int getMusic;
    // Start is called before the first frame update
    void Start()
    {
        gameController = GameObject.Find("Panel").GetComponent<GameController>();
        allowAudio = GameObject.Find("Audio/Music").GetComponent<Music>();
        sfxMusic = GameObject.Find("Audio/Music").GetComponent<AudioSource>();

        getMusic = VerifySaveInt("music", 1);
        StatusMusic(getMusic);
    }

    // Update is called once per frame
    public void SetImage()
    {
        getNumberCerebro = gameController.VerifySaveInt("cerebro", 0);
        image.sprite = sprite[getNumberCerebro];
    }

    public int VerifySaveInt(string txt, int value)
    {
        int pgSaveInt;
        if (PlayerPrefs.HasKey(txt))
        {
            pgSaveInt = PlayerPrefs.GetInt(txt);
        }
        else
        {
            pgSaveInt = value;
        }

        return pgSaveInt;
    }

    public float VerifySaveFloat(string txt, float value)
    {
        float pgSaveFloat;
        if (PlayerPrefs.HasKey(txt))
        {
            pgSaveFloat = PlayerPrefs.GetFloat(txt);
        }
        else
        {
            pgSaveFloat = value;
        }

        return pgSaveFloat;
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

    public void LoadScene()
    {
        SceneManager.LoadScene(1);
    }

    /* public void OnAllowAudio()
    {
        allowAudio = true;
    }

    public void OffAllowAudio()
    {
        allowAudio = false;
    } */

    public void OffMusic()
    {

        allowAudio.OffAllowAudio();
        sfxMusic.Stop();
        PlayerPrefs.SetInt("music", 0);
        StatusMusic(0);
    }

    public void OnMusic()
    {
        allowAudio.OnAllowAudio();
        sfxMusic.Play();
        PlayerPrefs.SetInt("music", 1);
        StatusMusic(1);
    }
}

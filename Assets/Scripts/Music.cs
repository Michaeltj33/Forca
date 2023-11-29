using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music : MonoBehaviour
{

    public AudioClip[] audioClips;

    public AudioSource audioSource;
    //private bool allowAudio;
    private int music;
    private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {

        //allowAudio = true;
        audioSource.loop = false;
        gameController = GameObject.Find("Panel").GetComponent<GameController>();
        music = gameController.VerifySaveInt("music", 1);
    }

    private AudioClip GetRandomClip()
    {
        return audioClips[UnityEngine.Random.Range(0, audioClips.Length)];
    }

    public void OnAllowAudio()
    {
        music = 1;
        PlayerPrefs.SetInt("music", 1);
    }

    public void OffAllowAudio()
    {
        music = 0;
        PlayerPrefs.SetInt("music", 0);
    }

    // Update is called once per frame
    void Update()
    {

        if (!audioSource.isPlaying && music == 1)
        {
            audioSource.clip = GetRandomClip();
            audioSource.Play();
        }

    }
}

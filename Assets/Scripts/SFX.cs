using System.Collections;
using System.Collections.Generic;
using Microsoft.Unity.VisualStudio.Editor;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public AudioSource audioSource;
    private GameController gameController;
    // Start is called before the first frame update
    public void PlaySound()
    {
        audioSource.Play();
    }

}

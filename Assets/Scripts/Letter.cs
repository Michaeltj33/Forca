using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Letter : MonoBehaviour
{
    public Sprite[] letterTotal;
    public Sprite[] letterSpecial;

    public Color[] color;
    public Image Letters;


    /* private void Start()
    {
        // Obtém o componente Image do objeto
        Letters = GetComponent<Image>();
    } */
    public void IndexSprite(int idexSprite, int cor)
    {
        Letters.color = color[cor];
        if (idexSprite <= 122)
        {
            Letters.sprite = letterTotal[idexSprite - 97];
        }
        else
        {
            GetSpecial(idexSprite);
        }

    }

    public void SetColorLetter(int cor)
    {
        Letters.color = color[cor];
    }

    private void GetSpecial(int special)
    {
        int[] array = { 224, 225, 226, 227, 231, 233, 234, 237, 242, 244, 245, 243, 250 };
        Letters.sprite = letterSpecial[Array.IndexOf(array, special)];
    }

    public void ChooseColor(int cor)
    {
        Letters.color = color[cor];
    }



}

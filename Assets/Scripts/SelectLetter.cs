using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SelectLetter : MonoBehaviour
{
    // Start is called before the first frame update
    public char Letter;
    private Palavras pr;

    void Awake()
    {
        pr = GameObject.Find("Panel").GetComponent<Palavras>();
    }


    public void CharButton(string ch)
    {
        char getCh = ch[0];
        pr.GetLetter(getCh);
    }



}

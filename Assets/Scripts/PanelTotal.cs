using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;

public class PanelTotal : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject gameOver;
    public GameObject victory;
    public int numeroDesc;

    private Text txtVictory;
    private Text canvasValue;
    private Text canvasGanho;
    private int SomaTotal;
    private int numb;
    private Text ExpConhecimento;
    private int canvasValueInt;

    private SelectMemory selectMemory;
    private GameController gameController;


    public void Victory(int lucro)
    {
        victory.SetActive(true);
        numb = lucro;
        gameController = GameObject.Find("Panel").GetComponent<GameController>();
        selectMemory = GameObject.Find("Panel").GetComponent<SelectMemory>();
        txtVictory = GameObject.Find("Canvas/Vitória/TotalGanho/Value").GetComponent<Text>();
        canvasValue = GameObject.Find("Canvas/Values").GetComponent<Text>();
        canvasGanho = GameObject.Find("Canvas/Vitória/TotalGanho/Ganho").GetComponent<Text>();
        ExpConhecimento = GameObject.Find("Canvas/Vitória/TotalGanho/valueBook").GetComponent<Text>();

        canvasValueInt = int.Parse(canvasValue.text);
        ExpConhecimento.text = lucro.ToString();

        canvasValueInt += numb;
        canvasGanho.text = numb.ToString();
        canvasValue.text = canvasValueInt.ToString();
        PlayerPrefs.SetInt("coin", canvasValueInt);



        StartCoroutine(Looping(lucro, 0.2f));
        selectMemory.CalcExp(numb);
    }

    private IEnumerator Looping(float lc, float second)
    {
        yield return new WaitForSeconds(2.0f);
        for (int i = 0; i < lc / numeroDesc; i++)
        {
            yield return new WaitForSeconds(second);
            numb -= numeroDesc;
            canvasGanho.text = numb.ToString();
            ExpConhecimento.text = numb.ToString();

            SomaTotal = numeroDesc + int.Parse(txtVictory.text);
            txtVictory.text = SomaTotal.ToString();
        }


    }

    public void GameOver()
    {
        gameOver.SetActive(true);

        canvasValue = GameObject.Find("Canvas/Values").GetComponent<Text>();

        txtVictory = GameObject.Find("Canvas/Game Over/TotalGanho/Value").GetComponent<Text>();
        txtVictory.text = canvasValue.text;
    }

    public void RestarGame(int lvl)
    {
        SceneManager.LoadScene(lvl);
    }

    public void ClosePanel()
    {
        if (gameOver != null && gameOver.activeSelf)
        {
            gameOver.SetActive(false);
        }

        if (victory != null && victory.activeSelf)
        {
            victory.SetActive(false);
        }
    }
}

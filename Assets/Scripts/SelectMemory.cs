using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectMemory : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite[] sprite;
    public int posImage;
    public Image image;
    private Image barraRpg;
    private float ExperienceTotal;
    private float levelUpExperienceTotal;
    private float Experience;
    private Text scoreExp;
    private Text nivelInicial;
    private Text nivelInicialCopy;

    private float resultExperienceTotal;
    public int nivel;
    private GameController gameController;
    int getNumberCerebro;
    public int setNv;

    void Start()
    {
        gameController = GameObject.Find("Panel").GetComponent<GameController>();
        nivelInicial = GameObject.Find("Canvas/PanelMemory/nivel").GetComponent<Text>();
        nivelInicialCopy = GameObject.Find("Canvas/PanelMemory/nivel/nivel").GetComponent<Text>();
        scoreExp = GameObject.Find("Canvas/PanelMemory/scoreExp").GetComponent<Text>();
        barraRpg = GameObject.Find("Canvas/PanelMemory/Barra").GetComponent<Image>();

        levelUpExperienceTotal = 0;//adicional de experiência final                                   
        nivel = gameController.VerifySaveInt("nivel", 1);

        nivelInicial.text = nivel.ToString();
        nivelInicialCopy.text = nivel.ToString();
        ExperienceTotal = (nivel * 100);
        Experience = gameController.VerifySaveFloat("experince", 0);

        barraRpg.fillAmount = resultExperienceTotal / 100;
        scoreExp.text = Experience + "  /  " + ExperienceTotal;
        SetImage();
        CalcExp(0);

        //seta o nivel inicial
        if (setNv > 0)
        {
            LevelUp(setNv);
        }

    }//scoreExp 

    public int GetPosImage()
    {
        return posImage;
    }

    public int GetNivel()
    {
        return nivel;
    }

    public void SetPosImage(int img)
    {
        posImage = img;
        SetImage();
    }

    public int GetTotalCerebro()
    {
        return sprite.Length;
    }

    public void CalcExp(int exp)
    {
        Experience = (Experience + levelUpExperienceTotal) + (float)exp;
        resultExperienceTotal = ((Experience * 100) / ExperienceTotal);
        if (Experience >= ExperienceTotal)
        {
            LevelUp();
        }
        else
        {
            barraRpg.fillAmount = resultExperienceTotal / 100;
            scoreExp.text = Experience + "  /  " + ExperienceTotal;
        }

        PlayerPrefs.SetFloat("experince", Experience);

    }

    public void LevelUp(int lv = 0)
    {
        if (lv > 0)
        {
            nivel += lv;
        }
        else
        {
            nivel++;
            Experience -= ExperienceTotal;
        }

        PlayerPrefs.SetInt("nivel", nivel);
        ExperienceTotal = (nivel * 100);
        nivelInicial.text = nivel.ToString();
        nivelInicialCopy.text = nivel.ToString();
        resultExperienceTotal = ((Experience * 100) / ExperienceTotal);
        barraRpg.fillAmount = resultExperienceTotal / 100;
        scoreExp.text = Experience + "  /  " + ExperienceTotal;

    }



    public void SetExperience(float setExp)
    {
        Experience = setExp;
    }

    public void SetExperienceTotal(float setExpTotal)
    {
        ExperienceTotal = setExpTotal;
    }

    public void SetImage()
    {
        getNumberCerebro = gameController.VerifySaveInt("cerebro", 0);
        image.sprite = sprite[getNumberCerebro];
    }

}



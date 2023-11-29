using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public Color[] color;
    private char letter;
    private Image colorButton;

    public int colorButtonAz;
    private AudioSource sfxMusic;
    private Music allowAudio;
    public GameObject painelOption;
    private Animator animatorPainelOption;
    private Image imageOption;
    private Image panelLife;
    private Image panelCerebro;
    private String painelLife;
    private Text textPainelLife;
    private String painelCerebro;
    private Text pgValueCoin;
    private Text setValueChange;
    private Text setValueNivelChange;
    private GameObject gameObjectLife;
    private GameObject gameObjectCerebro;
    private GameObject gameObjectPainelChance;
    private Image setCountChance;
    private int getVidasDisponivel;

    private Palavras palavras;
    private PanelSfx panelSfx;
    private SelectMemory selectMemory;

    private LifeIcon lifeIcon;
    private int getLevel;
    private readonly List<int> releaseLevel = new();
    private readonly List<int> releaseCerebro = new();
    private readonly List<int> releaseChance = new();
    private readonly String nomeChance = "Canvas/painelOption/Chance";
    private GameObject GetGameObject;
    public bool liberarStart;


    public void Start()
    {
        if (liberarStart)
        {

            painelLife = "Canvas/painelOption/Life/life";
            painelCerebro = "Canvas/painelOption/Cerebro/cerebro";
            palavras = GameObject.Find("Panel").GetComponent<Palavras>();
            imageOption = GameObject.Find("Canvas/Opções").GetComponent<Image>();
            allowAudio = GameObject.Find("Audio/Music").GetComponent<Music>();
            sfxMusic = GameObject.Find("Audio/Music").GetComponent<AudioSource>();
            selectMemory = GameObject.Find("Panel").GetComponent<SelectMemory>();
            PaintButtonTotal();


            releaseLevel.Add(0);
            releaseLevel.Add(30);//gatinho
            releaseLevel.Add(40);//coelhinho
            releaseLevel.Add(20);//fada
            releaseLevel.Add(5);//brasil 1
            releaseLevel.Add(10);//brasil 2
            releaseLevel.Add(15);//coração EUA
            releaseLevel.Add(50);//Tucano
            releaseLevel.Add(60);//Fantasma
            releaseLevel.Add(70);//Panda

            releaseCerebro.Add(0);
            releaseCerebro.Add(0);
            releaseCerebro.Add(0);
            releaseCerebro.Add(0);
            releaseCerebro.Add(25);
            releaseCerebro.Add(35);
            releaseCerebro.Add(45);
            releaseCerebro.Add(55);

            releaseChance.Add(3500);
            releaseChance.Add(7000);
            releaseChance.Add(20000);
            releaseChance.Add(50000);
            releaseChance.Add(0);
        }


    }

    public void OnPainelOption()
    {
        imageOption.enabled = false;
        painelOption.SetActive(true);
        int getNumberLife = VerifySaveInt("life", 0);
        int getNumberCerebro = VerifySaveInt("cerebro", 0);
        animatorPainelOption = GameObject.Find("Canvas/painelOption/Opções").GetComponent<Animator>();
        panelLife = GameObject.Find("Canvas/painelOption/Life/life" + getNumberLife).GetComponent<Image>();
        panelCerebro = GameObject.Find("Canvas/painelOption/Cerebro/cerebro" + getNumberCerebro).GetComponent<Image>();

        lifeIcon = palavras.IconError[0].GetComponent<LifeIcon>();

        getLevel = selectMemory.GetNivel();

        //Preenche os valores a ser comprado no Lifes, e apaga os qeu ja foram comprados
        for (int i = 0; i < lifeIcon.icons.Length; i++)
        {
            gameObjectLife = GameObject.Find(painelLife + i);
            gameObjectLife.SetActive(true);
            if (getLevel >= releaseLevel[i])
            {
                if (palavras.GetPriceLife(i) > 0)
                {
                    SetMoneyPanel(i, palavras.GetPriceLife(i).ToString());
                }
                else
                {
                    if (i > 0)
                    {
                        SetPanelOff(i);
                    }

                }
            }
            else
            {
                gameObjectLife = GameObject.Find(painelLife + i);
                gameObjectLife.SetActive(false);
            }

        }


        //usado para configurar painel de chance de life
        SetChanceErro();


        for (int c = 0; c < releaseCerebro.Count; c++)
        {
            gameObjectCerebro = GameObject.Find(painelCerebro + c);
            if (getLevel >= releaseCerebro[c])
            {
                gameObjectCerebro.SetActive(true);
            }
            else
            {
                gameObjectCerebro.SetActive(false);
            }
        }
        panelLife.sprite = palavras.selectIconFinal[1];
        panelCerebro.sprite = palavras.selectIconFinal[1];
        animatorPainelOption.SetBool("Allow", true);
        palavras.RemovePainelIconLifeTotal();
    }

    public void SetChanceErro()
    {
        //Seleciona as vidas disponível
        getVidasDisponivel = VerifySaveInt("chance", 2);
        //Carrega o Sprite do Life
        setCountChance = GameObject.Find(nomeChance + "/chance").GetComponent<Image>();
        setCountChance.sprite = palavras.imageCoutError[palavras.GetCountError() - 2];

        setValueNivelChange = GameObject.Find(nomeChance + "/nivel").GetComponent<Text>();
        setValueNivelChange.text = "Libera no Nv:" + ((getVidasDisponivel - 1) * 10);
        if (selectMemory.GetNivel() >= (getVidasDisponivel - 1) * 10)
        {
            SetGameObject(nomeChance + "/painel", true);
        }
        else
        {
            SetGameObject(nomeChance + "/painel");
        }

        SetValChange();
    }

    public int GetReleaseChance(int vl)
    {
        return releaseChance[vl];
    }

    public void SetGameObject(string texto, bool ver = false)
    {
        GetGameObject = GameObject.Find(texto);
        if (ver)
        {
            GetGameObject.SetActive(true);
        }
        else
        {
            GetGameObject.SetActive(false);
        }
    }

    public void SetValChange()
    {
        //Aponta o Valor do Life
        setValueChange = GameObject.Find(nomeChance + "/painel/preco").GetComponent<Text>();
        setValueChange.text = releaseChance[palavras.GetCountError() - 2].ToString();
    }

    public void SetMoneyPanel(int tipo, String valor)
    {
        textPainelLife = GameObject.Find(painelLife + tipo + "/painel/preco").GetComponent<Text>();
        textPainelLife.text = valor;
    }

    public void SetPanelOff(int tipo)
    {
        gameObjectLife = GameObject.Find(painelLife + tipo + "/painel");
        if (gameObjectLife.activeSelf)
        {
            gameObjectLife.SetActive(false);
        }

    }
    public void SetPanelOm(int tipo)
    {
        gameObjectLife = GameObject.Find(painelLife + tipo + "/painel");
        if (gameObjectLife.activeSelf)
        {
            gameObjectLife.SetActive(true);
        }

    }

    public void OffPainelOption()
    {
        imageOption.enabled = true;
        animatorPainelOption.SetBool("Allow", false);
        painelOption.SetActive(false);
    }

    public void OffMusic()
    {
        panelSfx = GameObject.Find("Canvas/painelOption/Sound").GetComponent<PanelSfx>();
        allowAudio.OffAllowAudio();
        sfxMusic.Stop();
        PlayerPrefs.SetInt("music", 0);
        panelSfx.StatusMusic(0);
    }

    public void OnMusic()
    {
        panelSfx = GameObject.Find("Canvas/painelOption/Sound").GetComponent<PanelSfx>();
        allowAudio.OnAllowAudio();
        sfxMusic.Play();
        PlayerPrefs.SetInt("music", 1);
        panelSfx.StatusMusic(1);
    }

    public void PaintButtonTotal()
    {
        for (int i = 0; i < 26; i++)
        {
            letter = ConvertToChar(i + 97);
            colorButton = GameObject.Find("Canvas/Btns/Btn" + letter).GetComponent<Image>();

            if (colorButton.color != color[colorButtonAz])
            {
                colorButton.color = color[colorButtonAz];
            }
        }

    }

    private char ConvertToChar(int letter)
    {
        return Convert.ToChar(letter);
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

    public void GiveCoin(int vl, bool verify = false)
    {
        pgValueCoin = GameObject.Find("Canvas/Values").GetComponent<Text>();
        int pgCoin = VerifySaveInt("coin", 0);
        pgCoin += vl;
        PlayerPrefs.SetInt("coin", pgCoin);
        pgValueCoin.text = pgCoin.ToString();
        if (verify)
        {
            Debug.LogWarningFormat("***Valor de Coin Adicionado*** " + vl);
        }

    }
}



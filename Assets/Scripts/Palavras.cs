using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System;
using Unity.VisualScripting;

public class Palavras : MonoBehaviour
{
    int countLetter;
    public int countError;
    public Sprite[] imageCoutError;

    int qntAcerto;
    int contarGeral;
    public int dropCoin;
    int Esp;
    private int amountPlayed;

    public bool ExibirTamanhoLetter;

    public bool ExibirResposta;
    private Image img;
    private int pgError;
    private readonly List<char> verifyCharChoose = new();
    private RectTransform getRectTransform;
    private int getValueChance;

    private int lifeTotal;
    private readonly List<float> posYImgError = new();

    public float posXInicial;
    public float posYInicial;
    private float getPosXInicial;
    private float getPosYInicial;

    public GameObject[] wood;
    public bool verificaSpaceLetter;

    public Text texto;
    public Text ContValue;
    public GameObject Let;

    private string[] listaEspaco;

    public GameObject[] IconError;//trocar para inteiro
    public int posIcon;
    public int posIconTotal;
    public Sprite[] selectIconFinal;
    private int getVidasDisponivel;

    public float basicPosY;
    //readonly List<Image> contTotalIcon = new();//ver
    readonly List<GameObject> contTotalIcon = new();//ver



    private readonly List<char> caracter = new();
    private readonly List<char> caracterNoSpace = new();

    readonly List<GameObject> listObj = new();
    readonly List<string> lista = new();

    private PanelTotal panelTotal;
    private SelectMemory selectMemory;
    private GameController gameController;
    private LifeIcon lifeIcon;
    private SFX sfxCorreto;
    private SFX sfxErrado;
    private SFX Descontar;

    private float intervalX;
    private float intervalY;

    private Image spriteLife;
    private GameObject objectSpriteLife;

    private Image spriteLife2;
    private GameObject getIconLife;
    private Image setCountChance;
    private Text setTextAviso;

    private readonly String nomeIcon = "Canvas/painelOption/Life/life";
    private readonly String nomeCerebro = "Canvas/painelOption/Cerebro/cerebro";
    private readonly String nomeValueJogadas = "Canvas/Jogadas/value";

    private Text nomeValueJ;
    private Text valueCoin;
    private int pgCoin;
    int getNumberCerebro;
    int getValueLife;

    //private readonly List<bool> boolLife = new();
    private readonly List<int> coinLife = new();



    private Animator animator;

    void Awake()
    {
        amountPlayed = 0;
        lifeTotal = 6;
        LoadingErrorIcon(lifeTotal, 0, 180f, basicPosY + 10f);
        ReadText();//lê arquivo Palavra.CS
        lifeIcon = IconError[0].GetComponent<LifeIcon>();
    }

    void Start()
    {   //boolLife[0] = true;
        selectMemory = GameObject.Find("Panel").GetComponent<SelectMemory>();
        sfxCorreto = GameObject.Find("Audio/Correto").GetComponent<SFX>();
        sfxErrado = GameObject.Find("Audio/Errado").GetComponent<SFX>();
        Descontar = GameObject.Find("Audio/Descontar").GetComponent<SFX>();
        gameController = GameObject.Find("Panel").GetComponent<GameController>();
        animator = GameObject.Find("Canvas/Hits").GetComponent<Animator>();
        panelTotal = GameObject.Find("Panel").GetComponent<PanelTotal>();


        getNumberCerebro = gameController.VerifySaveInt("cerebro", 0);
        spriteLife2 = GameObject.Find("Canvas/PanelMemory/Memory").GetComponent<Image>();
        spriteLife2.sprite = selectMemory.sprite[getNumberCerebro];

        //Preço dos Icones Lifes
        coinLife.Add(0);
        coinLife.Add(gameController.VerifySaveInt("coin-life1", 20000));
        coinLife.Add(gameController.VerifySaveInt("coin-life2", 40000));
        coinLife.Add(gameController.VerifySaveInt("coin-life3", 35000));
        coinLife.Add(gameController.VerifySaveInt("coin-life4", 10000));
        coinLife.Add(gameController.VerifySaveInt("coin-life5", 15000));
        coinLife.Add(gameController.VerifySaveInt("coin-life6", 25000));
        coinLife.Add(gameController.VerifySaveInt("coin-life7", 45000));
        coinLife.Add(gameController.VerifySaveInt("coin-life8", 55000));
        coinLife.Add(gameController.VerifySaveInt("coin-life9", 55000));

        ContValue.text = gameController.VerifySaveInt("coin", 0).ToString();
        countError = gameController.VerifySaveInt("chance", countError);
        pgError = countError;
        qntAcerto = 0;

        ChooseQuantity(150f, 1420f);
        ChooseLetter();
        Organization();
        VerifySpace();//verifica se as palavras caberão dentro da madeira        
        posIconTotal = gameController.VerifySaveInt("life", 0);
        SelectIconError(posIconTotal);//troca life icon error


        //Serve para adicionar dinheiro.
        if (dropCoin != 0)
        {
            gameController.GiveCoin(dropCoin, true);
        }
        CountErrorIcon();
    }

    //Usando para resetar a fase
    public void ResetTotal()
    {
        amountPlayed = 0;//quantidade jogada
        countLetter = 0;
        caracterNoSpace.Clear();
        caracter.Clear();
        countError = gameController.VerifySaveInt("chance", pgError);
        CountErrorIcon(1);
        verifyCharChoose.Clear();
        qntAcerto = 0;
        panelTotal.ClosePanel();
        gameController.PaintButtonTotal();
        ChooseLetter();
        SetValueJogada();
    }

    public void LoadingErrorIcon(int qntError, int posItem, float vX, float vY)
    {
        float espaceIcon = 126f;
        for (int i = 0; i < qntError; i++)
        {
            if (posItem >= IconError.Length)
            {
                Debug.Log("Erro no Tamanho na Imagem, Posição do Img LIFE: " + 0 + ", tanho total: " + IconError.Length);
                break;
            }
            var addIcon = Instantiate(IconError[posItem], new Vector2(vX, vY), transform.rotation);
            //contTotalIcon.Add(addIcon.GetComponent<Image>());
            contTotalIcon.Add(addIcon);
            Transform parentTransform = GameObject.Find("Canvas/Life").transform; // Obtém a referência ao Transform do objeto "Btns"
            addIcon.transform.SetParent(parentTransform);
            vX += espaceIcon;
        }

    }

    public void SetCountError()
    {
        countError = gameController.VerifySaveInt("chance", countError);
        setTextAviso = GameObject.Find("Canvas/painelOption/painelAviso/aviso/text").GetComponent<Text>();
        getVidasDisponivel = gameController.VerifySaveInt("chance", 2);



        if (selectMemory.GetNivel() >= (getVidasDisponivel - 1) * 10)
        {


            if (countError < 6)
            {
                objectSpriteLife = GameObject.Find("Canvas/painelOption/painelAviso");
                objectSpriteLife.SetActive(true);


                pgCoin = gameController.VerifySaveInt("coin", 0);
                valueCoin = GameObject.Find("Canvas/Values").GetComponent<Text>();

                if (pgCoin > gameController.GetReleaseChance(GetCountError() - 2))
                {
                    getValueChance = pgCoin - gameController.GetReleaseChance(GetCountError() - 2);
                    setTextAviso.text = "o Valor Só sera adicionado na proxima rodada.";
                    countError++;
                    PlayerPrefs.SetInt("chance", countError);
                    gameController.SetChanceErro();//life
                    PlayerPrefs.SetInt("coin", getValueChance);
                    valueCoin.text = getValueChance.ToString();
                }
                else
                {
                    setTextAviso.text = "Você não possui saldo.";
                }

            }
        }
        else
        {
            objectSpriteLife = GameObject.Find("Canvas/painelOption/painelAviso");
            objectSpriteLife.SetActive(true);
            setTextAviso.text = "Nível Insuficiente.";
        }

    }

    public void SelectIcon(int sIcon)//akiii
    {
        int getCoin;
        pgCoin = gameController.VerifySaveInt("coin", 0);
        getValueLife = gameController.VerifySaveInt("life", 0);

        if ((pgCoin >= coinLife[sIcon]) && coinLife[sIcon] > 0)
        {
            Descontar.PlaySound();
            getCoin = pgCoin - coinLife[sIcon];
            valueCoin = GameObject.Find("Canvas/Values").GetComponent<Text>();
            objectSpriteLife = GameObject.Find(nomeIcon + sIcon.ToString() + "/painel");
            objectSpriteLife.SetActive(false);
            coinLife[sIcon] = 0;
            PlayerPrefs.SetInt("coin", getCoin);
            PlayerPrefs.SetInt("coin-life" + sIcon, 0);
            valueCoin.text = getCoin.ToString();
        }
        else if ((pgCoin < coinLife[sIcon]) && coinLife[sIcon] > 0)
        {
            objectSpriteLife = GameObject.Find("Canvas/painelOption/painelAviso");
            objectSpriteLife.SetActive(true);
            setTextAviso = GameObject.Find("Canvas/painelOption/painelAviso/aviso/text").GetComponent<Text>();
            setTextAviso.text = "Você não possui dinheiro para comprar esse item.";
        }


        if (getValueLife != sIcon && coinLife[sIcon] == 0)
        {
            spriteLife = GameObject.Find(nomeIcon + sIcon.ToString()).GetComponent<Image>();
            spriteLife.sprite = selectIconFinal[1];
            posIconTotal = sIcon;
            SelectIconError(sIcon);
            PlayerPrefs.SetInt("life", sIcon);
            RemovePainelIconLifeTotal();
        }

    }

    //fecha o painel de aviso, quando falta dinheiro
    public void ClosePainelAviso()
    {
        objectSpriteLife = GameObject.Find("Canvas/painelOption/painelAviso");
        objectSpriteLife.SetActive(false);
    }

    public void RemovePainelIconLifeTotal()
    {
        for (int i = 0; i < lifeIcon.icons.Length; i++)
        {
            if (i != posIconTotal)
            {
                spriteLife = GameObject.Find(nomeIcon + i.ToString()).GetComponent<Image>();
                spriteLife.sprite = selectIconFinal[0];
            }

        }
    }

    public int GetPriceLife(int vl)
    {
        return coinLife[vl];
    }

    public void SelectCerebro(int sIcon)
    {
        spriteLife = GameObject.Find(nomeCerebro + sIcon.ToString()).GetComponent<Image>();
        spriteLife.sprite = selectIconFinal[1];
        PlayerPrefs.SetInt("cerebro", sIcon);
        selectMemory.SetPosImage(sIcon);
        RemovePainelCerebroLifeTotal();
    }

    public void RemovePainelCerebroLifeTotal()
    {
        for (int i = 0; i < selectMemory.GetTotalCerebro(); i++)
        {
            if (i != selectMemory.GetPosImage())
            {
                spriteLife = GameObject.Find(nomeCerebro + i.ToString()).GetComponent<Image>();
                spriteLife.sprite = selectIconFinal[0];
            }

        }
    }

    //Captura posição em Y dos icones de Erros
    private float ReturnPosYIcon(int posY)
    {
        posYImgError.Add(basicPosY + 10f);
        posYImgError.Add(basicPosY + 20f);
        posYImgError.Add(basicPosY);
        posYImgError.Add(basicPosY + 20f);
        posYImgError.Add(basicPosY + 20f);
        posYImgError.Add(basicPosY + 40f);
        posYImgError.Add(basicPosY + 20f);
        if (posY < posYImgError.Count)
        {
            return posYImgError[posY];
        }
        else
        {
            Debug.Log("O Valor de posY da Imagem não foi Selecionada");
            return 0;
        }


    }

    public void CountErrorIcon(int verify = 0)
    {
        if (verify == 0)
        {
            for (int i = lifeTotal; i > 0; i--)
            {
                if (i > countError && !contTotalIcon[i - 1].activeSelf == false)
                {
                    contTotalIcon[i - 1].SetActive(false);
                }
            }
        }
        else
        {
            for (int i = 0; i < lifeTotal; i++)
            {
                if (i < countError && !contTotalIcon[i].activeSelf == true)
                {
                    contTotalIcon[i].SetActive(true);
                }
            }
        }
    }

    //contTotalIcon1

    public void SelectIconError(int valor)
    {
        for (int i = 0; i < lifeTotal; i++)
        {
            contTotalIcon[i].GetComponent<LifeIcon>().iconIndex = valor;
        }

    }

    //Lê o arquivo Palavra.cs
    private void ReadText()
    {
        // Caminho do arquivo CSV
        string filePath = "Assets/Resources/palavras.csv";

        // Lê o arquivo CSV
        StreamReader reader = new(filePath);

        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] values = line.Split(';');


            // Faça algo com os valores da linha (exemplo: imprimir no console)
            foreach (string value in values)
            {
                lista.Add(value);
            }
        }
        reader.Close();
        //colocar isos aki em uma função
    }
    //Escolhe a Palavra
    public void ChooseLetter()
    {
        // Loop através das linhas do arquivo      
        System.Random random = new();
        int length;

        // Fecha o leitor de arquivos      
        int nAleatorio = random.Next(0, (lista.Count));
        if (nAleatorio % 2 != 0)
        {
            nAleatorio--;
        }
        texto.text = MaxString(lista[nAleatorio]);//Exibe na tela o texto da pergunta                   
        length = lista[nAleatorio + 1].Length;//pega tamanho da palavra Chave
        listaEspaco = lista[nAleatorio + 1].Split(' ');

        //(lista[nAleatorio]); pega a pergunta
        //(lista[nAleatorio + 1]); pega a resposta        


        int contLetterTotal = 0;

        for (int x = 0; x < listaEspaco.Length; x++)
        {
            for (int y = 0; y < listaEspaco[x].Length; y++)
            {
                if (ReturnAscii(lista[nAleatorio + 1][y + contLetterTotal]) > 122)
                {
                    caracter.Add(GetLetterEspecial(MinLetter(lista[nAleatorio + 1][y + contLetterTotal])));
                }
                else
                {
                    caracter.Add(MinLetter(lista[nAleatorio + 1][y + contLetterTotal]));
                }
                caracterNoSpace.Add(caracter[^1]);
            }
            contLetterTotal += listaEspaco[x].Length + 1;
            caracter.Add(' ');
        }

        caracter.RemoveAt(caracter.Count - 1);

        if (ExibirResposta)
        {
            Debug.Log(lista[nAleatorio + 1]);
        }

        SelectLetter(lista[nAleatorio + 1]);
        Organization();

    }

    public void DropObjectDown(GameObject obj, float vX, float vY, int posIndex)
    {
        var thisLetter = Instantiate(obj, new Vector2(vX, vY), transform.rotation);
        listObj.Add(thisLetter);
        Transform parentTransform = GameObject.Find("Canvas/Btns").transform; // Obtém a referência ao Transform do objeto "Btns"
        thisLetter.transform.SetParent(parentTransform);
    }

    void ChooseQuantity(float posX, float posY)
    {
        int totalLetterResp = ChooseTotalLetters(ExibirTamanhoLetter);//verifca o tamanho de maximo de respostas
        float posIniX = posX;
        intervalX = 90f;
        intervalY = -135f;
        Esp = 10;

        //mostra o array das letras Mostrada para advinhar(vLetter) ,que são as palavras separadas por espaços       
        for (int x = 0; x < totalLetterResp; x++)
        {
            if (x % Esp == 0 && x > 0)
            {
                posY += intervalY;
                posX = posIniX;
            }
            DropObjectDown(Let, posX, posY, ConvertToChar(0));
            posX += intervalX;


        }

    }

    private char ConvertToChar(int letter)
    {
        return Convert.ToChar(letter);
    }

    public int ChooseTotalLetters(bool verify = false)
    {
        int textMax = 0;
        int countText;
        string getPosMax = "";
        for (int i = 0; i < lista.Count; i++)
        {
            if (i % 2 != 0)
            {
                countText = lista[i].Length;
                if (countText > textMax)
                {
                    textMax = countText;
                    getPosMax = lista[i];
                }
            }
        }
        if (verify)
        {
            Debug.Log("Tamanho: " + textMax + " Texto resposta: " + getPosMax);
        }

        return textMax;

    }

    public void SelectLetter(string texto)
    {
        for (int i = 0; i < texto.Length; i++)
        {
            char pgText = MinLetter(texto[i]);
            if (pgText.ToString() == " ")
            {
                listObj[i].SetActive(false);
            }
            else
            {
                if (!listObj[i].activeSelf)
                {
                    listObj[i].SetActive(true);
                }
                listObj[i].GetComponent<Letter>().IndexSprite(ReturnAscii(pgText), 0);//define Cor

            }
        }

        for (int x = texto.Length; x < listObj.Count; x++)
        {
            if (listObj[x].activeSelf)
            {
                listObj[x].SetActive(false);
            }

        }
    }


    public void Organization()
    {

        for (int w = 1; w < wood.Length; w++)
        {
            wood[w].SetActive(false);
        }

        int indexWood = 0;
        Esp = 10;
        contarGeral = 0;
        intervalX = 90f;
        intervalY = -140f;
        getPosXInicial = posXInicial;
        getPosYInicial = posYInicial;
        int contLetterTotal = 0;

        for (int x = 0; x < listaEspaco.Length; x++)
        {
            for (int y = 0; y < listaEspaco[x].Length; y++)
            {
                getRectTransform = listObj[y + contLetterTotal].GetComponent<RectTransform>();
                getRectTransform.position = new Vector2(getPosXInicial, getPosYInicial);

                //Quando o tamanho total chega ao final 
                if (contarGeral == Esp)
                {
                    indexWood++;
                    wood[indexWood].SetActive(true);
                    contarGeral = 0;
                    getPosYInicial += intervalY;
                    getPosXInicial = posXInicial;
                }
                else
                {
                    contarGeral++;
                    getPosXInicial += intervalX;
                }
            }

            //precisa calcular espaço entra palavras
            getPosXInicial += intervalX;
            contarGeral++;

            if (x + 1 < listaEspaco.Length)
            {
                if (contarGeral + listaEspaco[x + 1].Length > Esp)
                {
                    indexWood++;
                    wood[indexWood].SetActive(true);
                    contarGeral = 0;
                    getPosYInicial += intervalY;
                    getPosXInicial = posXInicial;
                }
            }

            contLetterTotal += listaEspaco[x].Length + 1;
        }
    }

    public void VerifySpace()
    {
        if (verificaSpaceLetter)
        {
            int ContTotal = 0;
            string[] verLetter;
            for (int i = 0; i < lista.Count; i += 2)
            {
                ContTotal++;
                verLetter = lista[i + 1].Split(" ");

                contarGeral = 0;
                int CountPosWood = 1;

                for (int x = 0; x < verLetter.Length; x++)
                {
                    for (int y = 0; y < verLetter[x].Length; y++)
                    {

                        if (contarGeral == Esp)
                        {
                            contarGeral = 0;
                            CountPosWood++;
                        }
                        else
                        {
                            contarGeral++;
                        }
                    }

                    if (x + 1 < verLetter.Length)
                    {
                        if (contarGeral + verLetter[x + 1].Length > Esp)
                        {
                            contarGeral = 0;
                            CountPosWood++;
                        }
                    }
                }
                if (CountPosWood > 3)
                {
                    Debug.LogError("ULTRASSOU :* " + CountPosWood + " * Pergunta: " + lista[i] + " -- Respsta: " + lista[i + 1]);
                }
            }
            Debug.Log("Palavras Analisadas: " + ContTotal);
        }

    }

    public int GetChance()
    {
        return amountPlayed;
    }

    public void SetValueJogada()
    {
        nomeValueJ = GameObject.Find(nomeValueJogadas).GetComponent<Text>();
        nomeValueJ.text = amountPlayed.ToString();
    }

    //Verifica e exibe a letra escondida.
    public void GetLetter(char letter)
    {
        amountPlayed++;
        SetValueJogada();

        //para evitar clicar 2 vezes no mesmo botão
        if (!verifyCharChoose.Contains(letter))
        {
            // 'a' não está presente na lista
            verifyCharChoose.Add(letter);
            img = GameObject.Find("Canvas/Btns/Btn" + letter).GetComponent<Image>();
            int idenfyErro = 0;
            for (int i = 0; i < caracter.Count; i++)
            {
                if (letter == caracter[i])
                {
                    //Exibe a letra selecionada
                    listObj[i].GetComponent<Letter>().ChooseColor(1);

                    //contas Letras Corretas    
                    countLetter++;
                    idenfyErro = 1;

                }

            }

            //verifica se as letras acertas é igual com quantidade de letras
            if (countLetter == caracterNoSpace.Count)
            {
                panelTotal.Victory(caracter.Count * 10);
                qntAcerto = 0;
                TotalErroFinal();
            }

            //verifica se caso erre todas as opções 
            if (idenfyErro == 0)
            {

                sfxErrado.PlaySound();
                qntAcerto = 0;
                animator.SetTrigger("Errado");
                img.color = Color.red;
                countError--;
                if (countError == 0)
                {
                    panelTotal.GameOver();
                }
            }
            else
            {
                sfxCorreto.PlaySound();
                qntAcerto++;
                img.color = Color.green;
                animator.SetTrigger("Certo");
            }
            CountErrorIcon();
        }

    }

    public int GetCountError()
    {
        return countError;
    }

    public void TotalErroFinal()
    {
        countError = 0;
        CountErrorIcon();
    }

    private void Acertos(int qntAcerto)//mudar
    {
        if (qntAcerto == 1)
        {
            animator.SetTrigger("Certo");
        }
        else if (qntAcerto == 2)
        {
            animator.SetTrigger("Great");
        }
        else if (qntAcerto == 3)
        {
            animator.SetTrigger("Incredible");
        }
        else if (qntAcerto == 4)
        {
            animator.SetTrigger("Fabulous");
        }
        else if (qntAcerto > 4)
        {
            animator.SetTrigger("NoWay");
        }
    }

    private char GetLetterEspecial(char letter)
    {
        string pgLetter = letter.ToString();
        pgLetter = pgLetter.Replace('á', 'a');
        pgLetter = pgLetter.Replace('à', 'a');
        pgLetter = pgLetter.Replace('ã', 'a');
        pgLetter = pgLetter.Replace('â', 'a');
        pgLetter = pgLetter.Replace('ç', 'c');
        pgLetter = pgLetter.Replace('é', 'e');
        pgLetter = pgLetter.Replace('ê', 'e');
        pgLetter = pgLetter.Replace('í', 'i');
        pgLetter = pgLetter.Replace('ò', 'o');
        pgLetter = pgLetter.Replace('ô', 'o');
        pgLetter = pgLetter.Replace('õ', 'o');
        pgLetter = pgLetter.Replace('ó', 'o');
        pgLetter = pgLetter.Replace('ú', 'u');

        char final = pgLetter[0];
        return final;
    }

    private int ReturnAscii(char lt)
    {
        return (int)lt;
    }

    private char MinLetter(char lt)
    {
        return char.ToLower(lt);
    }

    private string MaxString(string text)
    {
        return text.ToUpper();
    }

}

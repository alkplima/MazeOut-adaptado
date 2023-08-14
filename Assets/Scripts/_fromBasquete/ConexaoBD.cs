using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConexaoBD : MonoBehaviour {
    public ConfigBehaviours controllerRelatorio;
    bool cr_PostDataCoroutine_running;
    public GameObject telaGravandoBD, telaMenuInicial;
    public GameObject[] telasProtocolos = new GameObject[4];
    public CommomBehaviours telaDeLoading_commomBehaviours;
    // Baseado no código disponível em
    // http://wiki.unity3d.com/index.php?title=Server_Side_Highscores#C.23_-_HSController.cs

    private string secretKey = "GrupoJogosSerios"; // Edit this value and make sure it's the same as the one stored on the server
    //public string addDataURL = "https://rogarpon.com.br/projetos/support/addDataGames.php?"; //be sure to add a ? to your url
                                                                                             //public string highscoreURL = "http://localhost/unity_test/display.php";
    public string addDataPOST_URL = "https://rogarpon.com.br/projetos/support/addDataGamesPOST.php";
    // Use this for initialization
    void Start () {
        //StartCoroutine(GetScores());
        //DontDestroyOnLoad(this.gameObject);
        //VariaveisGlobais.conexaoBD = this.gameObject;
        VariaveisGlobais.RefreshValues();
        if (VariaveisGlobais.estaNaPesquisa)
            PostData();
        else
            LimparData();
    }
	
	// Update is called once per frame
	void Update () {

    }

    public void PostData()
    {
        if (controllerRelatorio && (!cr_PostDataCoroutine_running))
            //StartCoroutine(PostDataCoroutine());
            StartCoroutine(PostDataCoroutineNew());
    }

    public void LimparData()
    {
        if (VariaveisGlobais.emProcessoDeProtocolo)
        {
            if ((VariaveisGlobais.expressoProtocolo) && (VariaveisGlobais.partidaProtocoloCorrente < 13))
            {
                VariaveisGlobais.partidaProtocoloCorrente++;
                VariaveisGlobais.ProtocoloToConfigValues();
                telaDeLoading_commomBehaviours.CB_LoadProtocoloSceneWithoutPesquisa();
            }
            else
            {
                telaGravandoBD.SetActive(false);
                telasProtocolos[VariaveisGlobais.protocoloCorrente].SetActive(true);
            }
        }
        else
        {
            telaGravandoBD.SetActive(false);
            telaMenuInicial.SetActive(true);
        }
    }

    public IEnumerator PostDataCoroutineNew()
    {
        cr_PostDataCoroutine_running = true;

        string NomeDoJogo = "Jogo_1_Basquete";
        #if UNITY_EDITOR
            NomeDoJogo += "_TEST";
        #endif

        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.

        string[] listaItens = controllerRelatorio.GerarRelatorio(false, false, "EUA").Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);

        string[][] listao = new string[listaItens.Length][];
        string[] newLista = new string[0];


        for (int i = 0; i < listaItens.Length; i++)
        {
            string[] linhaAtual = listaItens[i].Split(new char[] { ';' }, StringSplitOptions.None);
            if (linhaAtual.Length > 1)
            {
                for (int j = 0; j < linhaAtual.Length; j++)
                {
                    if (linhaAtual[j] == "" || linhaAtual[j] == " ")
                        linhaAtual[j] = "*****";
                }
            }
            Array.Resize(ref newLista, newLista.Length + 1);
            newLista[i] = string.Join(";", linhaAtual);
        }

        string conteudoPost = string.Join("§", newLista);
        var conteudoBytes = System.Text.Encoding.UTF8.GetBytes(conteudoPost);
        
        var form = new WWWForm();
        form.AddField("nomeJogo", NomeDoJogo);
        form.AddField("hash", Md5Sum(NomeDoJogo + secretKey));
        form.AddField("conteudo", conteudoPost);

        //form.AddBinaryData("Conteudo", conteudoBytes, "text/csv");

        var w = new WWW(addDataPOST_URL, form);
        yield return w;
        if (w.error != null)
            Debug.Log("Houve um erro na conexão com o banco de dados: " + w.error);
        else
            VariaveisGlobais.LimparListaItensRelatorio();

        
            

        if (VariaveisGlobais.emProcessoDeProtocolo)
        {
            if ((VariaveisGlobais.expressoProtocolo) && (VariaveisGlobais.partidaProtocoloCorrente < 13))
            {
                VariaveisGlobais.partidaProtocoloCorrente++;
                VariaveisGlobais.ProtocoloToConfigValues();
                telaDeLoading_commomBehaviours.CB_LoadProtocoloScene(null);
            }
            else
            {
                telaGravandoBD.SetActive(false);
                telasProtocolos[VariaveisGlobais.protocoloCorrente].SetActive(true);
            }
        }
        else
        {
            telaGravandoBD.SetActive(false);
            telaMenuInicial.SetActive(true);
        }



        cr_PostDataCoroutine_running = false;
    }

    public string Md5Sum(string strToEncrypt)
    {
        System.Text.UTF8Encoding ue = new System.Text.UTF8Encoding();
        byte[] bytes = ue.GetBytes(strToEncrypt);

        // encrypt bytes
        System.Security.Cryptography.MD5CryptoServiceProvider md5 = new System.Security.Cryptography.MD5CryptoServiceProvider();
        byte[] hashBytes = md5.ComputeHash(bytes);

        // Convert the encrypted bytes back to a string (base 16)
        string hashString = "";

        for (int i = 0; i < hashBytes.Length; i++)
        {
            hashString += System.Convert.ToString(hashBytes[i], 16).PadLeft(2, '0');
        }

        return hashString.PadLeft(32, '0');
    }
}
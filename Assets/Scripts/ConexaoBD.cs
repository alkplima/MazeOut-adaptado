using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;

public class ConexaoBD : MonoBehaviour {
    [HideInInspector]
    public bool cr_PostDataCoroutine_running;
    public GameObject[] telasProtocolos = new GameObject[4];
    // Baseado no código disponível em
    // http://wiki.unity3d.com/index.php?title=Server_Side_Highscores#C.23_-_HSController.cs

    private string secretKey = "GrupoJogosSerios"; // Edit this value and make sure it's the same as the one stored on the server
    //public string addDataURL = "https://rogarpon.com.br/projetos/support/addDataGames.php?"; //be sure to add a ? to your url
                                                                                             //public string highscoreURL = "http://localhost/unity_test/display.php";
    public string addDataPOST_URL = "https://rogarpon.com.br/projetos/support/addDataGamesPOST.php";
    // Use this for initialization
    void Start () {
        if (VariaveisGlobais.conexaoBD == null)
        {
            VariaveisGlobais.conexaoBD = this;
            VariaveisGlobais.RefreshValues();
        }
        else Destroy(this);
    }

    public void PostData()
    {
        if (!cr_PostDataCoroutine_running)
            //StartCoroutine(PostDataCoroutine());
            StartCoroutine(PostDataCoroutineNew());
    }

    public string GerarRelatorio(string formatoData)
    {
        string str = "";
        string dataAtual;

        if (formatoData == "International")
            dataAtual = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
        else
            dataAtual = System.DateTime.Now.ToString("dd'-'MM'-'yyyy' 'HH'h 'mm'm 'ss's'");

        for (int i = 0; i < VariaveisGlobais.itensRelatorio.Length; i++)
        {
            if (!string.IsNullOrWhiteSpace(VariaveisGlobais.itensRelatorio[i].NomePaciente)) {
                str = str +
                    VariaveisGlobais.itensRelatorio[i].DateTimeInicioPartida + ";" + // Horário do início da partida
                    VariaveisGlobais.itensRelatorio[i].NomePaciente + ";" + // Nome do paciente
                    VariaveisGlobais.itensRelatorio[i].TipoPartida + ";" + // Tipo de partida
                    VariaveisGlobais.itensRelatorio[i].TipoAdaptacao + ";" + // Tipo de adaptacao
                    VariaveisGlobais.itensRelatorio[i].NumReta + ";" +
                    VariaveisGlobais.itensRelatorio[i].DirecaoReta + ";" + // Tempo total configurado para a partida
                    // VariaveisGlobais.itensRelatorio[i].TempoTotalPartida + ";" + // Tempo total configurado (planejado) para a partida
                    VariaveisGlobais.itensRelatorio[i].TempoTotalGasto.ToString("00.00", CultureInfo.InvariantCulture).Replace(",", ".") + ";" + // NEW!
                    VariaveisGlobais.itensRelatorio[i].TempoTotalReta.ToString("00.00", CultureInfo.InvariantCulture).Replace(",", ".") + ";" + // Tempo total na reta
                    VariaveisGlobais.itensRelatorio[i].TotalMoedasColetadas + ";" +    //Total de moedas coletadas na partida (até o momento)
                    VariaveisGlobais.itensRelatorio[i].TotalMoedasColetadasReta + ";" + // Total de moedas coletadas na reta em específico
                    VariaveisGlobais.itensRelatorio[i].UsouAjudaNaReta + ";" + // Usou ajuda na reta?
                    VariaveisGlobais.itensRelatorio[i].EscalaMaxDaAjuda.ToString("00.00", CultureInfo.InvariantCulture).Replace(",", ".") + ";" + // NEW!
                    VariaveisGlobais.itensRelatorio[i].CoordenadaX_InicioReta + ";" +
                    VariaveisGlobais.itensRelatorio[i].CoordenadaY_InicioReta + ";" +
                    VariaveisGlobais.itensRelatorio[i].CoordenadaX_FimReta + ";" +
                    VariaveisGlobais.itensRelatorio[i].CoordenadaY_FimReta + ";" +
                    VariaveisGlobais.itensRelatorio[i].CoordenadaX_Maxima + ";" +
                    VariaveisGlobais.itensRelatorio[i].CoordenadaY_Maxima + ";" +
                    VariaveisGlobais.itensRelatorio[i].CoordenadaX_Minima + ";" +
                    VariaveisGlobais.itensRelatorio[i].CoordenadaY_Minima + ";" +
                    VariaveisGlobais.itensRelatorio[i].FrequenciaCardiacaMediaReta + ";" +
                    VariaveisGlobais.itensRelatorio[i].FrequenciaCardiacaMinimaPartida + ";" +
                    VariaveisGlobais.itensRelatorio[i].FrequenciaCardiacaMediaPartida + ";" +
                    VariaveisGlobais.itensRelatorio[i].FrequenciaCardiacaMaximaPartida + ";" +
                    VariaveisGlobais.itensRelatorio[i].ScoreFinal + ";" +
                    VariaveisGlobais.NomeDoJogo + System.Environment.NewLine;
            }
        }

        return str;
    }


    public IEnumerator PostDataCoroutineNew()
    {
        cr_PostDataCoroutine_running = true;

        string NomeDoJogo = "Jogo_4_MazeOut";
        #if UNITY_EDITOR
            NomeDoJogo += "_TEST";
        #endif

        //This connects to a server side php script that will add the name and score to a MySQL DB.
        // Supply it with a string representing the players name and the players score.

        string[] listaItens = GerarRelatorio("International").Split(new string[] { System.Environment.NewLine }, StringSplitOptions.None);

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
        form.AddField("hash", PaterlandGlobal.SomaMd5(NomeDoJogo + secretKey));
        form.AddField("conteudo", conteudoPost);

        var w = new WWW(addDataPOST_URL, form);
        yield return w;
        if (w.error != null)
            Debug.Log("Houve um erro na conexão com o banco de dados: " + w.error);
        else if (w.text.StartsWith("Falha na query"))
            Debug.Log("Houve um erro na conexão com o banco de dados: " + w.text);
        else
            VariaveisGlobais.LimparListaItensRelatorio();

        w.Dispose();

        cr_PostDataCoroutine_running = false;
    }
}
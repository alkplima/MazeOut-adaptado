using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectionController : MonoBehaviour
{
    public bool firstCoin;
    public int coinCountLeftToRight;
    public int coinCountRightToLeft;
    public int coinCountTopToBottom;
    public int coinCountBottomToTop;
    public float timeLeftToRight;
    public float timeRightToLeft;
    public float timeTopToBottom;
    public float timeBottomToTop;
    public float timerTimeFromLastCoin;
    public GameObject grid;
    private Vector3[] cantosCelula = new Vector3[4];
    public UI_DisplayTimer displayTimer;
    ScoreHUD _uiManager;

    // Start is called before the first frame update
    void OnEnable()
    {
        _uiManager = GameObject.Find("GameScreenManager").GetComponent<ScoreHUD>();
        displayTimer = GameObject.FindObjectOfType<UI_DisplayTimer>();
        firstCoin = true;
        coinCountTopToBottom = 0;
        coinCountLeftToRight = 0;
        coinCountBottomToTop = 0;
        coinCountRightToLeft = 0;
        timeLeftToRight = 0.0f;
        timeRightToLeft = 0.0f;
        timeTopToBottom = 0.0f;
        timeBottomToTop = 0.0f;

        // Marcar na variável global tamanhoBufferBD o tamanho corrente da pilha de dados.
        VariaveisGlobais.tamanhoBufferBD = VariaveisGlobais.itensRelatorio.Length;
    }

    public void CountTimePerCoin(char lado) 
    {
        float timeWentBy = 0.0f;

        // if (firstCoin) {
        //     firstCoin = false;
        //     // if (displayTimer != null) timeWentBy = PlayerPrefs.GetInt("Timer") - displayTimer.timer;
        //     if (displayTimer != null) timeWentBy = 0;
        // }
        // else {
            if (displayTimer != null) timeWentBy = displayTimer.timer - timerTimeFromLastCoin;
        // }
        CalcularScore(timeWentBy);

        if (displayTimer != null) timerTimeFromLastCoin = displayTimer.timer;

        switch (lado)
        {
            case 'C':
                coinCountTopToBottom++;
                timeTopToBottom += timeWentBy;
                break;
            case 'E':
                coinCountLeftToRight++;
                timeLeftToRight += timeWentBy;
                break;
            case 'B':
                coinCountBottomToTop++;
                timeBottomToTop += timeWentBy;
                break;
            case 'D':
                coinCountRightToLeft++;
                timeRightToLeft += timeWentBy;
                break;
            default:
                break;
        }
    }

    void CalcularScore(double tempo)
    {
        if (tempo == 0)
        {
            tempo = 0.1;
        }

        _uiManager.Score += (int) (10 / tempo);

        // double limiteRapido = 0.6;
        // double limiteModerado = 1.0;

        // if (tempo < limiteRapido)
        // {
        //     // Score rápido
        //     return (int)(100 / tempo);
        // }
        // else if (tempo < limiteModerado)
        // {
        //     // Score moderado
        //     return (int)(50 / tempo);
        // }
        // else
        // {
            // Score lento
            // return (int)(10 / tempo);
        // }
    }

    void OnDisable() 
    {
        VariaveisGlobais.timePerCoinTopToBottom = timeTopToBottom / coinCountTopToBottom;
        VariaveisGlobais.timePerCoinBottomToTop = timeBottomToTop / coinCountBottomToTop;
        VariaveisGlobais.timePerCoinLeftToRight = timeLeftToRight / coinCountLeftToRight;
        VariaveisGlobais.timePerCoinRightToLeft = timeRightToLeft / coinCountRightToLeft;
    }

    public void AcrescentarEntradaRelatorio()
    {
        ItemEventoDB itemNovo = new ItemEventoDB
        {
            TipoPartida = VariaveisGlobais.tipoPartida,
            TipoAdaptacao = VariaveisGlobais.dataProcessingMode,
            NumReta = VariaveisGlobais.numReta,
            DirecaoReta = VariaveisGlobais.direcaoReta,
            DateTimeInicioPartida = VariaveisGlobais.dateTimeInicioPartida,
            NomePaciente = VariaveisGlobais.nomePaciente,
            TotalMoedasColetadas = VariaveisGlobais.totalMoedasColetadas,
            TotalMoedasColetadasReta = VariaveisGlobais.totalMoedasColetadasReta,
            // TempoTotalPartida = PlayerPrefs.GetInt("Timer"),
            TempoTotalReta = VariaveisGlobais.tempoTotalReta,
            UsouAjudaNaReta = VariaveisGlobais.usouAjudaNaReta,
            EscalaMaxDaAjuda = VariaveisGlobais.escalaMaxDaAjuda,
            CoordenadaX_InicioReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_InicioReta, true),
            CoordenadaY_InicioReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_InicioReta, false),
            CoordenadaX_FimReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_FimReta, true),
            CoordenadaY_FimReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_FimReta, false),
            CoordenadaX_Maxima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_Maxima, true),
            CoordenadaY_Maxima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_Maxima, false),
            CoordenadaX_Minima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_Minima, true),
            CoordenadaY_Minima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_Minima, false),
            TempoTotalGasto = VariaveisGlobais.tempoTotalGasto,
            FrequenciaCardiacaMediaReta = VariaveisGlobais.avgHRRetaAtual,
            FrequenciaCardiacaMinimaPartida = VariaveisGlobais.minHRPartidaAtual,
            FrequenciaCardiacaMediaPartida = VariaveisGlobais.avgHRPartidaAtual,
            FrequenciaCardiacaMaximaPartida = VariaveisGlobais.maxHRPartidaAtual
        };

        Array.Resize(ref VariaveisGlobais.itensRelatorio, VariaveisGlobais.itensRelatorio.Length + 1);

        VariaveisGlobais.itensRelatorio[VariaveisGlobais.itensRelatorio.Length - 1] = itemNovo;

        // APAGAR DEPOIS DE TESTAR E VALIDAR COM RODRIGO E PROF CARLOS - NÃO VAI REGISTRAR A ESCALA NA PRÓXIMA RETA SE FOR RESQUÍCIO DA RETA ANTERIOR...
        // VariaveisGlobais.usouAjudaNaReta = 'N';
        // VariaveisGlobais.escalaMaxDaAjuda = 1;

        // Debug.Log(VariaveisGlobais.itensRelatorio.Length + " itens adicionados ao relatório.");
        // Debug.Log("Dados gravados:");
        // Debug.Log("Tipo da partida: "+itemNovo.TipoPartida);
        // Debug.Log("NumReta: "+itemNovo.NumReta);
        // Debug.Log("DirecaoReta: "+itemNovo.DirecaoReta);
        // Debug.Log("DateTimeInicioPartida: "+itemNovo.DateTimeInicioPartida);
        // Debug.Log("NomePaciente: "+itemNovo.NomePaciente);
        // Debug.Log("TotalMoedasColetadas: "+itemNovo.TotalMoedasColetadas);
        // Debug.Log("TotalMoedasColetadasReta: "+itemNovo.TotalMoedasColetadasReta);
        // Debug.Log("TempoTotalReta: "+itemNovo.TempoTotalReta);
        // Debug.Log("UsouAjudaNaReta: "+itemNovo.UsouAjudaNaReta);
        // Debug.Log("EscalaMaxDaAjuda: "+itemNovo.EscalaMaxDaAjuda);

    }

    public int GetCoordinateIndexInGrid(double coordinate, bool isXAxis)
    {
        int maxRows = grid.transform.GetChild(0).childCount;
        int maxColumns = grid.transform.childCount;
        int index = -1;
        if (isXAxis)
        {
            for (int col = 0; col < maxColumns; col++)
            {
                Transform column = grid.transform.GetChild(col);
                column.GetComponent<RectTransform>().GetWorldCorners(cantosCelula);
                float cellWidth = Mathf.Abs(cantosCelula[1].x - cantosCelula[2].x); // largura da célula

                if (coordinate >= column.transform.position.x - cellWidth / 2 && coordinate <= column.transform.position.x + cellWidth / 2)
                {
                    index = col;
                    break;
                }
            }
        }
        else
        {
            int reverseIndexForCells = 0;
            for (int cel = maxRows - 1; cel >= 0; cel--)
            {
                Transform cell = grid.transform.GetChild(0).GetChild(cel);
                cell.GetComponent<RectTransform>().GetWorldCorners(cantosCelula);
                float cellHeight = Mathf.Abs(cantosCelula[0].y - cantosCelula[1].y); // altura da célula

                if (coordinate >= cell.transform.position.y - cellHeight / 2 && coordinate <= cell.transform.position.y + cellHeight / 2)
                {
                    index = reverseIndexForCells;
                    break;
                }
                reverseIndexForCells++;
            }
        }
        return index;
    }
}
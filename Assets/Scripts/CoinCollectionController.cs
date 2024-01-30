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

    // Start is called before the first frame update
    void OnEnable()
    {
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

        if (firstCoin) {
            firstCoin = false;
            // if (displayTimer != null) timeWentBy = PlayerPrefs.GetInt("Timer") - displayTimer.timer;
            if (displayTimer != null) timeWentBy = 0;
        }
        else {
            if (displayTimer != null) timeWentBy = timerTimeFromLastCoin - displayTimer.timer;
        }

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

    void OnDisable() 
    {
        VariaveisGlobais.timePerCoinTopToBottom = timeTopToBottom / coinCountTopToBottom;
        VariaveisGlobais.timePerCoinBottomToTop = timeBottomToTop / coinCountBottomToTop;
        VariaveisGlobais.timePerCoinLeftToRight = timeLeftToRight / coinCountLeftToRight;
        VariaveisGlobais.timePerCoinRightToLeft = timeRightToLeft / coinCountRightToLeft;

        Debug.Log("Tempo médio por moeda (cima-baixo): " + VariaveisGlobais.timePerCoinTopToBottom);
        Debug.Log("Tempo médio por moeda (baixo-cima): " + VariaveisGlobais.timePerCoinBottomToTop);
        Debug.Log("Tempo médio por moeda (esq-dir): " + VariaveisGlobais.timePerCoinLeftToRight);
        Debug.Log("Tempo médio por moeda (dir-esq): " + VariaveisGlobais.timePerCoinRightToLeft);

    }

    public void AcrescentarEntradaRelatorio()
    {
        ItemEventoDB itemNovo = new ItemEventoDB
        {
            NumReta = VariaveisGlobais.numReta,
            DirecaoReta = VariaveisGlobais.direcaoReta,
            DateTimeInicioPartida = VariaveisGlobais.dateTimeInicioPartida,
            NomePaciente = VariaveisGlobais.nomePaciente,
            TotalMoedasColetadas = VariaveisGlobais.totalMoedasColetadas,
            TotalMoedasColetadasReta = VariaveisGlobais.totalMoedasColetadasReta,
            TempoTotalPartida = PlayerPrefs.GetInt("Timer"),
            TempoTotalReta = VariaveisGlobais.tempoTotalReta,
            CoordenadaX_InicioReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_InicioReta, true),
            CoordenadaY_InicioReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_InicioReta, false),
            CoordenadaX_FimReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_FimReta, true),
            CoordenadaY_FimReta = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_FimReta, false),
            CoordenadaX_Maxima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_Maxima, true),
            CoordenadaY_Maxima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_Maxima, false),
            CoordenadaX_Minima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaX_Minima, true),
            CoordenadaY_Minima = GetCoordinateIndexInGrid(VariaveisGlobais.coordenadaY_Minima, false),
            TempoTotalGasto = VariaveisGlobais.tempoTotalGasto
        };

        Array.Resize(ref VariaveisGlobais.itensRelatorio, VariaveisGlobais.itensRelatorio.Length + 1);

        VariaveisGlobais.itensRelatorio[VariaveisGlobais.itensRelatorio.Length - 1] = itemNovo;
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

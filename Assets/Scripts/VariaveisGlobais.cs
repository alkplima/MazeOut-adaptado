using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public static class VariaveisGlobais
{
    public static ControllerLabirinto atualControllerLabirinto;
    public static int partidaCorrente = 0;
    public static char tipoPartida = ' '; // P - Personalizado, T - Calibração, A - Automático, D - Desafio
    public static int contagemPartidasAuto = 0;
    public static float timePerCoinTopToBottom, timePerCoinBottomToTop, timePerCoinLeftToRight, timePerCoinRightToLeft;
    public static float maxX, maxY, minX, minY;
    public static string NomeDoJogo = "MazeOut";
    public static string Idioma = "EN";
    public static int idWebcam;
    public static bool Webcam_espelhar_H, Webcam_espelhar_V;
    public static string estiloJogoCorrente;

    // Grupo de variáveis a serem preenchidas nos atos
    public static string DateTime_InicioPartida, DateTime_Full, IdentificadorNome;
    public static int DuracaoJogo;
    public static double tempoRestanteEmDouble;
    public static bool ehPrimeiraMoedaDoJogo;
    public static int totalMoedasNaPartida;

    public static ConexaoBD conexaoBD = null;
    public static ItemEventoDB[] itensRelatorio = new ItemEventoDB[0];

    // variáveis auxiliares para cada entrada no relatório
    public static int numReta;
    public static char direcaoReta;
    public static string dateTimeInicioPartida;
    public static string nomePaciente = "";
    public static int totalMoedasColetadas;
    public static int totalMoedasColetadasReta;
    public static double tempoTotalReta;
    public static double coordenadaX_InicioReta;
    public static double coordenadaY_InicioReta;
    public static double coordenadaX_FimReta;
    public static double coordenadaY_FimReta;
    public static double coordenadaX_Maxima;
    public static double coordenadaY_Maxima;
    public static double coordenadaX_Minima;
    public static double coordenadaY_Minima;
    public static double tempoTotalGasto;

    public static float tempoInicioReta;
    public static float tempoInicioRetaAux;
    public static char lastCollectedCoinDirection;
    public static char currentCollectedCoinDirection; // 0, 1, 2, 3 (cima, dir, baixo, esq)
    
    public static int tamanhoBufferBD = 0; // Armazena temporariamente o tamanho do vetor de buffer a ser enviado pro BD


    public static void AtualizarAtributosBuffer(int startIndex)
    {
        int corX_Max = -1;
        int corX_Min = 1000;
        int corY_Max = -1;
        int corY_Min = 1000;

        for (int i = startIndex; i < itensRelatorio.Length; i++)
        {
            corX_Max = Mathf.Max(corX_Max, itensRelatorio[i].CoordenadaX_Maxima);
            corY_Max = Mathf.Max(corY_Max, itensRelatorio[i].CoordenadaY_Maxima);
            corX_Min = Mathf.Min(corX_Min, itensRelatorio[i].CoordenadaX_Minima);
            corY_Min = Mathf.Min(corY_Min, itensRelatorio[i].CoordenadaY_Minima);
        }
        
        for (int i = startIndex; i < itensRelatorio.Length; i++)
        {
            itensRelatorio[i].TotalMoedasColetadas = totalMoedasColetadas;
            itensRelatorio[i].TempoTotalGasto = tempoTotalGasto;
            itensRelatorio[i].CoordenadaX_Maxima = corX_Max;
            itensRelatorio[i].CoordenadaY_Maxima = corY_Max;
            itensRelatorio[i].CoordenadaX_Minima = corX_Min;
            itensRelatorio[i].CoordenadaY_Minima = corY_Min;    
        }
    }

    public static void LimparListaItensRelatorio()
    {
        itensRelatorio = new ItemEventoDB[0];
    }

    public static float CurrentFPS()
    {
        return ((1 / (Time.deltaTime * 60)) * 60);
    }

    public static void RefreshValues()
    {
        // Idioma = PlayerPrefs.GetString("Idioma");
        idWebcam = PlayerPrefs.GetInt("ID_Webcam");
        Webcam_espelhar_H = System.Convert.ToBoolean(PlayerPrefs.GetInt("Webcam_espelhar_H"));
        Webcam_espelhar_V = System.Convert.ToBoolean(PlayerPrefs.GetInt("Webcam_espelhar_V"));
    }
}

public class ItemEventoDB
{
    public int TipoPartida { get; set; }
    public int NumReta { get; set; }
    public char DirecaoReta { get; set; }
    public string DateTimeInicioPartida { get; set; }
    public string NomePaciente { get; set; }
    public int TotalMoedasColetadas { get; set; }
    public int TotalMoedasColetadasReta { get; set; }
    public int TempoTotalPartida { get; set; }
    public double TempoTotalReta { get; set; }
    public int CoordenadaX_InicioReta { get; set; }
    public int CoordenadaY_InicioReta { get; set; }
    public int CoordenadaX_FimReta { get; set; }
    public int CoordenadaY_FimReta { get; set; }
    public int CoordenadaX_Maxima { get; set; }
    public int CoordenadaY_Maxima { get; set; }
    public int CoordenadaX_Minima { get; set; }
    public int CoordenadaY_Minima { get; set; }
    public double TempoTotalGasto { get; set; }

}


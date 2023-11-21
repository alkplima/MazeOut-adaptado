using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Collections;
using UnityEngine;

public static class VariaveisGlobais
{
    public static ControllerLabirinto atualControllerLabirinto;

    public static int partidaCorrente = 0;
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

    public static float tempoInicioReta;
    public static float tempoInicioRetaAux;
    public static char lastCollectedCoinDirection;
    public static char currentCollectedCoinDirection; // C, E, B, D (cima, esq, baixo, dir)


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
    public int NumReta { get; set; }
    public char DirecaoReta { get; set; }
    public string DateTimeInicioPartida { get; set; }
    public string NomePaciente { get; set; }
    public int TotalMoedasColetadas { get; set; }
    public int TotalMoedasColetadasReta { get; set; }
    public int TempoTotalPartida { get; set; }
    public double TempoTotalReta { get; set; }
    public double CoordenadaX_InicioReta { get; set; }
    public double CoordenadaY_InicioReta { get; set; }
    public double CoordenadaX_FimReta { get; set; }
    public double CoordenadaY_FimReta { get; set; }
    public double CoordenadaX_Maxima { get; set; }
    public double CoordenadaY_Maxima { get; set; }
    public double CoordenadaX_Minima { get; set; }
    public double CoordenadaY_Minima { get; set; }

}


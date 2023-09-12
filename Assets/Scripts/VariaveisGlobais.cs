using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VariaveisGlobais
{
    public static ControllerLabirinto atualControllerLabirinto;

    public static bool passedThroughtStart = false;
    public static string NomeDoJogo = "Basquete";
    //public static string Idioma = "BR";
    public static string Idioma = "EN";
    public static int QuantidadeDefensores, VelocidadeDefensores, idWebcam;

    // Grupo de variáveis a serem preenchidas nos atos
    public static string Device_ID, OperationalSystem, DateTime_Full, TempoRestante,
        TipoInterface, SequenciaProgramada, IdentificadorNome, SimboloDiv,
        TipoMarcacao, TipoInteracao, Senha_Pesquisa;
    public static int Enemy1_Pos, Enemy2_Pos, Enemy3_Pos, ScoreBasquito,
        ScoreAdversarios, ScreenSize_X, ScreenSize_Y, Touch_X, Touch_Y, PosicaoCorrenteSequencia,
        DuracaoJogo, ColetaDados;
    public static bool HitTarget, FirstShot;
    //public static string PosicaoJogada_Zona
    public static string PosicaoJogada, PosicaoJogadaASerRegistrada;
    public static double tempoRestanteEmDouble;
    public static float tempoParaVoltarALancarAposErrar;
    public static bool emProcessoDeProtocolo, expressoProtocolo = false;

    public static bool estouNoXbox;

    public static ItemEventoDB[] itensRelatorio = new ItemEventoDB[0];

    public static PartidaProtocolo[][] protocolos = new PartidaProtocolo[4][];
    public static int protocoloCorrente, partidaProtocoloCorrente;
    public static bool estaNaPesquisa;

public static bool Webcam_espelhar_H, Webcam_espelhar_V;
    public static string DateTime_InicioPartida;

    public static int tempoParaPenalizacao = 5;

    public static void ConfigProtocolosInicial()
    {
        for (int i = 0; i < 4; i++)
        {
            protocolos[i] = new PartidaProtocolo[14];
            for (int j = 0; j < 14; j++)
                protocolos[i][j] = new PartidaProtocolo();
        }
            

        // Configurações protocolo 01
        foreach (PartidaProtocolo partidaProtocolo in protocolos[0])
        {
            //Debug.Log(partidaProtocolo);
            partidaProtocolo.estiloJogo = "Zona";
            partidaProtocolo.tempo = 45;
        }

        for (int i = 0; i < 7; i++)
            protocolos[0][i].velocidade = 1;
        for (int i = 7; i < 11; i++)
            protocolos[0][i].velocidade = 2;
        for (int i = 11; i < 14; i++)
            protocolos[0][i].velocidade = 3;

        protocolos[0][0].sequencia = "C";
        protocolos[0][1].sequencia = "ED";
        protocolos[0][2].sequencia = "ECD";
        protocolos[0][3].sequencia = "DCE";
        protocolos[0][4].sequencia = "CED";
        protocolos[0][5].sequencia = "CDE";
        protocolos[0][6].sequencia = "ECDDCECEDCDE";
        protocolos[0][7].sequencia = "ECD";
        protocolos[0][8].sequencia = "DCE";
        protocolos[0][9].sequencia = "CED";
        protocolos[0][10].sequencia = "ECDDCECEDCDE";
        protocolos[0][11].sequencia = "ECDDCE";
        protocolos[0][12].sequencia = "CEDCDE";
        protocolos[0][13].sequencia = "ECDDCECEDCDE";

        // Configurações protocolo 02
        foreach (PartidaProtocolo partidaProtocolo in protocolos[1])
        {
            partidaProtocolo.estiloJogo = "Zona";
            partidaProtocolo.tempo = 45;
        }

        for (int i = 0; i < 7; i++)
            protocolos[1][i].velocidade = 2;
        for (int i = 7; i < 14; i++)
            protocolos[1][i].velocidade = 3;

        protocolos[1][0].sequencia = "C";
        protocolos[1][1].sequencia = "ED";
        protocolos[1][2].sequencia = "ECD";
        protocolos[1][3].sequencia = "DCE";
        protocolos[1][4].sequencia = "CED";
        protocolos[1][5].sequencia = "CDE";
        protocolos[1][6].sequencia = "ECDDCECEDCDE";
        protocolos[1][7].sequencia = "C";
        protocolos[1][8].sequencia = "ED";
        protocolos[1][9].sequencia = "ECD";
        protocolos[1][10].sequencia = "DCE";
        protocolos[1][11].sequencia = "CED";
        protocolos[1][12].sequencia = "CDE";
        protocolos[1][13].sequencia = "ECDDCECEDCDE";

        // Configurações protocolo 03
        foreach (PartidaProtocolo partidaProtocolo in protocolos[2])
        {
            partidaProtocolo.estiloJogo = "Circular";
            partidaProtocolo.tempo = 45;
        }

        for (int i = 0; i < 7; i++)
            protocolos[2][i].velocidade = 1;
        for (int i = 7; i < 11; i++)
            protocolos[2][i].velocidade = 2;
        for (int i = 11; i < 14; i++)
            protocolos[2][i].velocidade = 3;

        protocolos[2][0].sequencia = "C";
        protocolos[2][1].sequencia = "ED";
        protocolos[2][2].sequencia = "ECD";
        protocolos[2][3].sequencia = "DCE";
        protocolos[2][4].sequencia = "CED";
        protocolos[2][5].sequencia = "CDE";
        protocolos[2][6].sequencia = "ECDDCECEDCDE";
        protocolos[2][7].sequencia = "C";
        protocolos[2][8].sequencia = "ED";
        protocolos[2][9].sequencia = "ECD";
        protocolos[2][10].sequencia = "DCE";
        protocolos[2][11].sequencia = "CED";
        protocolos[2][12].sequencia = "CDE";
        protocolos[2][13].sequencia = "ECDDCECEDCDE";


        // Configurações protocolo 04
        for (int i = 0; i < 14; i++)
        {
            if (i % 2 == 0)
                protocolos[3][i].estiloJogo = "Zona";
            else protocolos[3][i].estiloJogo = "Circular";

            protocolos[3][i].tempo = 45;
        }

        for (int i = 0; i < 6; i++)
            protocolos[3][i].velocidade = 2;
        for (int i = 6; i < 14; i++)
            protocolos[3][i].velocidade = 3;

        protocolos[3][0].sequencia = "ECD";
        protocolos[3][1].sequencia = "DCE";
        protocolos[3][2].sequencia = "CED";
        protocolos[3][3].sequencia = "CDE";
        protocolos[3][4].sequencia = "ECDDCECEDCDE";
        protocolos[3][5].sequencia = "DCEECDCEDDEC";
        protocolos[3][6].sequencia = "ECD";
        protocolos[3][7].sequencia = "DCE";
        protocolos[3][8].sequencia = "CED";
        protocolos[3][9].sequencia = "CDE";
        protocolos[3][10].sequencia = "ECDDCECEDCDE";
        protocolos[3][11].sequencia = "DCEECDCEDDEC";
        protocolos[3][12].sequencia = "ECDDCECEDCDE";
        protocolos[3][13].sequencia = "DCEECDCEDDEC";
    }
    public static void ProtocoloToConfigValues()
    {
        if (protocoloCorrente>=0 && partidaProtocoloCorrente >=0)
        {
            PlayerPrefs.SetInt("QTD_Defensores", 3);
            PlayerPrefs.SetInt("VLC_Defensores", protocolos[protocoloCorrente][partidaProtocoloCorrente].velocidade);
            PlayerPrefs.SetInt("DUR_Jogo", protocolos[protocoloCorrente][partidaProtocoloCorrente].tempo);
            PlayerPrefs.SetString("TIPO_Interface","Webcam1");
            PlayerPrefs.SetString("TIPO_Marcacao", protocolos[protocoloCorrente][partidaProtocoloCorrente].estiloJogo);
            PlayerPrefs.SetString("Sequencia_Programada",protocolos[protocoloCorrente][partidaProtocoloCorrente].sequencia);

            RefreshValues();
        }
    }

    public static void LimparListaItensRelatorio()
    {
        itensRelatorio = new ItemEventoDB[0];
    }

    public static void RefreshValues()
    {
        Idioma = PlayerPrefs.GetString("Idioma");
        QuantidadeDefensores = PlayerPrefs.GetInt("QTD_Defensores");
        VelocidadeDefensores = PlayerPrefs.GetInt("VLC_Defensores");
        DuracaoJogo = PlayerPrefs.GetInt("DUR_Jogo");
        TipoInterface = PlayerPrefs.GetString("TIPO_Interface");
        TipoMarcacao = PlayerPrefs.GetString("TIPO_Marcacao");
        SequenciaProgramada = PlayerPrefs.GetString("Sequencia_Programada");
        IdentificadorNome = PlayerPrefs.GetString("Identificador_Nome");
        ColetaDados = PlayerPrefs.GetInt("COLETA_Dados");
        idWebcam = PlayerPrefs.GetInt("ID_Webcam");
        SimboloDiv = PlayerPrefs.GetString("DIV_Simbolo");
        Senha_Pesquisa = PlayerPrefs.GetString("Senha_Pesquisa");
        Webcam_espelhar_H = System.Convert.ToBoolean(PlayerPrefs.GetInt("Webcam_espelhar_H"));
        Webcam_espelhar_V = System.Convert.ToBoolean(PlayerPrefs.GetInt("Webcam_espelhar_V"));
    }

    public static float CurrentFPS()
    {
        return ((1 / (Time.deltaTime * 60)) * 60);
    }
}

public class ItemEventoDB
{
    public int NumJogada { get; set; }
    public string TipoLance { get; set; }
    public string DateTimeInicioPartida { get; set; }
    public string DateTime { get; set; }
    public string Device_ID { get; set; }
    public string Player_ID { get; set; }
    public string OperationalSystem { get; set; }
    public int Enemies { get; set; }
    public int Enemy1_Pos { get; set; }
    public int Enemy2_Pos { get; set; }
    public int Enemy3_Pos { get; set; }
    public int ScreenSize_X { get; set; }
    public int ScreenSize_Y { get; set; }
    public int Touch_X { get; set; }
    public int Touch_Y { get; set; }
    public int Velocity { get; set; }
    public bool HitTarget { get; set; }
    public int ScoreBasquito { get; set; }
    public int ScoreAdversarios { get; set; }
    public int TotalTimeOfTheMatch { get; set; }
    public string RemainingTime { get; set; }
    public double RemainingTimeAtDouble { get; set; }
    public string Position_E_C_D { get; set; }
    public string InterfaceOfMatch { get; set; }
    public string MarkingOfMatch { get; set; }
    public string BasquetebolKid_Version { get; set; }
    public bool FirstShot { get; set; }
    public int IDProtocolo { get; set; }
    public int IDPartidaProtocolo { get; set; }
}

public class PartidaProtocolo
{
    public int tempo { get; set; }
    public string estiloJogo { get; set; }
    public int velocidade { get; set; }
    public string sequencia { get; set; }
}


using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ScriptConfiguracoesIniciais : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("Idioma") == false)
        {
            PlayerPrefs.SetString("Idioma", "BR");
        }

        if (PlayerPrefs.HasKey("QTD_Defensores") == false)
        {
            PlayerPrefs.SetInt("QTD_Defensores", 3);
        }

        if (PlayerPrefs.HasKey("VLC_Defensores") == false)
        {
            PlayerPrefs.SetInt("VLC_Defensores", 2);
        }

        if (PlayerPrefs.HasKey("DUR_Jogo") == false)
        {
            PlayerPrefs.SetInt("DUR_Jogo", 60);
        }

        if (PlayerPrefs.HasKey("TIPO_Marcacao") == false)
        {
            PlayerPrefs.SetString("TIPO_Marcacao", "Circular");
        }

        if (PlayerPrefs.HasKey("Senha_Pesquisa") == false)
        {
            PlayerPrefs.SetString("Senha_Pesquisa", "");
        }

        // Tipo de interface
        // "C" para "interface de toque/mouse, marcação circular"
        // "Z" para "interface de toque/mouse, marcação por zona"
        // "A" para "interface via webcam, arremesso em alvo"
        // "M" para "interface via webcam, movimento à bola"

        if (PlayerPrefs.HasKey("TIPO_Interface") == false)
        {
            PlayerPrefs.SetString("TIPO_Interface", "Mouse");
        }

        if (PlayerPrefs.HasKey("TIPO_Marcacao") == false)
        {
            PlayerPrefs.SetString("TIPO_Marcacao", "Circular");
        }

        if (PlayerPrefs.HasKey("COLETA_Dados") == false)
        {
            PlayerPrefs.SetInt("COLETA_Dados", 1);
        }

        if (PlayerPrefs.HasKey("Sequencia_Programada") == false)
        {
            PlayerPrefs.SetString("Sequencia_Programada", "ECD");
        }

        if (PlayerPrefs.HasKey("Identificador_Nome") == false)
        {
            PlayerPrefs.SetString("Identificador_Nome", "");
        }

        if (PlayerPrefs.HasKey("ID_Webcam") == false)
        {
            PlayerPrefs.SetInt("ID_Webcam", 0);
        }

        if (PlayerPrefs.HasKey("Webcam_espelhar_H") == false)
        {
            PlayerPrefs.SetInt("Webcam_espelhar_H", 0);
        }

        if (PlayerPrefs.HasKey("Webcam_espelhar_V") == false)
        {
            PlayerPrefs.SetInt("Webcam_espelhar_V", 0);
        }

        //if (PlayerPrefs.HasKey("DIV_Simbolo") == false)
        //{
        PlayerPrefs.SetString("DIV_Simbolo", ".");
        //}

        VariaveisGlobais.RefreshValues();
        VariaveisGlobais.estouNoXbox = (Application.platform == RuntimePlatform.WSAPlayerX64 && SystemInfo.deviceType == DeviceType.Console);

        VariaveisGlobais.ConfigProtocolosInicial();
    }
}

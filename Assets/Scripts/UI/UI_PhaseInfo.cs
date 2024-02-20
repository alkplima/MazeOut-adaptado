using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_PhaseInfo : MonoBehaviour
{
    public Text text;

    private void OnEnable()
    {
        if (VariaveisGlobais.partidaCorrente > 0) // Trilha ou automática
        {
            if (VariaveisGlobais.partidaCorrente == 2001) // automática
            {
                ++VariaveisGlobais.contagemPartidasAuto;
                text.text = "Auto maze " + VariaveisGlobais.contagemPartidasAuto.ToString();
            }
            else {
                text.text = "Challenge " + VariaveisGlobais.partidaCorrente.ToString() + "/14";
            }
        }
        else if (VariaveisGlobais.partidaCorrente < 0) // Calibração
        {
            if (VariaveisGlobais.partidaCorrente == -8)
            {
                text.text = "Fast calibration";
            }
            else
            {
                text.text = "Tutorial&Calibration " + (-VariaveisGlobais.partidaCorrente).ToString() + "/7";
            }
        }
        else // Partida avulsa
        {
            text.text = "Custom match";
        }

        UpdateCurrentMatchType();
    }

    private void UpdateCurrentMatchType()
    {
        if (VariaveisGlobais.partidaCorrente == 0) // Personalizar
        {
            VariaveisGlobais.tipoPartida = 'P';
        }
        else if (VariaveisGlobais.partidaCorrente < 0 && VariaveisGlobais.partidaCorrente != -8) // Calibração (tutorial)
        {
            VariaveisGlobais.tipoPartida = 'T';
        }
        else if (VariaveisGlobais.partidaCorrente == -8 || VariaveisGlobais.partidaCorrente == 2001) // Automático / Adaptativo
        {
            VariaveisGlobais.tipoPartida = 'A';
        }
        else // Desafio
        {
            VariaveisGlobais.tipoPartida = 'D';
        }
    }
        
}

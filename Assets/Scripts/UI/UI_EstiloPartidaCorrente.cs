using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_EstiloPartidaCorrente : MonoBehaviour
{
    public GameObject menuOrigemMain, menuOrigemTrilha;

    public void Setar (string estilo)
    {
        VariaveisGlobais.estiloJogoCorrente = estilo;
    }

    public void Voltar()
    {
        if (VariaveisGlobais.estiloJogoCorrente == "PartidaAvulsa")
        {
            menuOrigemMain.SetActive(true);
        }
        else if (VariaveisGlobais.estiloJogoCorrente == "Trilha")
        {
            menuOrigemTrilha.SetActive(true);
        }
        else if (VariaveisGlobais.estiloJogoCorrente == "Calibracao")
        {
            //Continuar daqui        
        }
        else
        {
            //Continuar daqui        
        }

    }
}

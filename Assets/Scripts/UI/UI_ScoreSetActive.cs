using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ScoreSetActive : MonoBehaviour
{
    [SerializeField] private GameObject scoreWrapper;

    public void OnEnable()
    {
        if (VariaveisGlobais.estiloJogoCorrente != "PartidaAvulsa")
        {
            scoreWrapper.SetActive(true);
        }
        else
        {
            scoreWrapper.SetActive(false);
        }
    }
}

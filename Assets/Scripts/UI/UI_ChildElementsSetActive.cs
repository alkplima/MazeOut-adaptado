using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_ChildElementsSetActive : MonoBehaviour
{
    [SerializeField] private GameObject scoreWrapper;
    [SerializeField] private GameObject hrWrapper;

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

        if (PlayerPrefs.GetInt("DataProcessingMode") == 1)
        {
            hrWrapper.SetActive(true);
        }
        else
        {
            hrWrapper.SetActive(false);
        }
    }
}

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

        if (PlayerPrefs.GetInt("ShowHeartRate") == 0)
        {
            hrWrapper.SetActive(false);
        }
        else
        {
            hrWrapper.SetActive(true);
        }
    }
}

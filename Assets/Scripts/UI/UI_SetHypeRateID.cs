using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetHypeRateID : MonoBehaviour
{
    public InputField IDInputField;

    public Text heartRateText;

    public void OnEnable()
    {
        if (PlayerPrefs.HasKey("HypeRateID"))
        {
            AtualizarValorInputField();
        }
    }
    public void ChangeHypeRateID(string id)
    {
        PlayerPrefs.SetString("HypeRateID", id);
        RecarregarIndicadorHR();
    }

    private void AtualizarValorInputField()
    {
        if (IDInputField != null)
        {
            IDInputField.text = PlayerPrefs.GetString("HypeRateID");
        }
    }

    private void RecarregarIndicadorHR()
    {
        heartRateText.gameObject.SetActive(false);

        Invoke("ReativarTexto", 1f);
    }

    void ReativarTexto()
    {
        heartRateText.gameObject.SetActive(true);
    }
}

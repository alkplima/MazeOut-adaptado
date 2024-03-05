using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_SetHypeRateID : MonoBehaviour
{
    public InputField IDInputField;

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
    }

    private void AtualizarValorInputField()
    {
        if (IDInputField != null)
        {
            IDInputField.text = PlayerPrefs.GetString("HypeRateID");
        }
    }
}

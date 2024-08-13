using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_ChangeName : MonoBehaviour
{
    public InputField nomeInputField;

    public void OnEnable()
    {
        if (PlayerPrefs.HasKey("NomePaciente"))
        {
            VariaveisGlobais.nomePaciente = PlayerPrefs.GetString("NomePaciente");
            AtualizarValorInputField();
        }
    }
    public void ChangeName(string name)
    {
        VariaveisGlobais.nomePaciente = name;
        PlayerPrefs.SetString("NomePaciente", name);
        ZerarBestScore();
    }

    private void AtualizarValorInputField()
    {
        if (nomeInputField != null)
        {
            nomeInputField.text = VariaveisGlobais.nomePaciente;
        }
    }

    private void ZerarBestScore()
    {
        PlayerPrefs.SetInt("ScoreRecorde", 0);
        VariaveisGlobais.scoreRecorde = 0;
    }
}

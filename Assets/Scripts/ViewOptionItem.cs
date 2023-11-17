using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewOptionItem : MonoBehaviour
{
    public Text text;
    private string[] dataProcessingModes = new string[] { "OptionWeightedAverage", "OptionPerformanceFromPreviousMatchOnly", "OptionPerformanceFromCalibrationOnly" }; 

    private void OnEnable()
    {
        if (!PlayerPrefs.HasKey("DataProcessingMode"))
            PlayerPrefs.SetInt("DataProcessingMode", 1);

        UpdateValues(gameObject.name);
    }

    public void UpdateValues(string itemName)
    {
        switch (itemName)
        {
            case "TextoTimerContagem":
                if (VariaveisGlobais.Idioma=="BR")
                    text.text = "Contagem regressiva: " + PlayerPrefs.GetInt("Timer").ToString() + " segundos (padrão = 60)";
                else if (VariaveisGlobais.Idioma == "EN")
                    text.text = "Timer countdown: " + PlayerPrefs.GetInt("Timer").ToString() + " seconds (default = 60)";
                else
                    text.text = "Contagem regressiva: " + PlayerPrefs.GetInt("Timer").ToString() + " segundos (padrão = 60)";
                break;
            case "TextoVelocidadeMov":
                if (VariaveisGlobais.Idioma=="BR")
                    text.text = "Velocidade de movimentação: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (padrão = 75)";
                else if (VariaveisGlobais.Idioma == "EN")
                    text.text = "Movement speed: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (default = 75)";
                else
                    text.text = "Velocidade de movimentação: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (padrão = 75)";
                break;
            case "TextoArraste":
                if (VariaveisGlobais.Idioma == "BR") 
                    text.text = "Efeito de atrito (\"arraste\"): " + PlayerPrefs.GetInt("Arraste").ToString() + " (padrão = 05)";
                else if (VariaveisGlobais.Idioma == "EN")
                    text.text = "\"Friction\" effect: " + PlayerPrefs.GetInt("Arraste").ToString() + " (default = 05)";
                else
                    text.text = "Efeito de atrito (\"arraste\"): " + PlayerPrefs.GetInt("Arraste").ToString() + " (padrão = 05)";
                break;
            case "TextoModoProcessamentoDeDados":
                if (VariaveisGlobais.Idioma == "BR") 
                    text.text = "Modo de processamento de dados:";
                else if (VariaveisGlobais.Idioma == "EN")
                    text.text = "Data processing mode:";
                else
                    text.text = "Modo de processamento de dados:";
                break;
            case "SliderTimerContagem":
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Timer");
                UpdateValues("TextoTimerContagem");
                break;
            case "SliderVelocidadeMov":
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Velocidade");
                UpdateValues("TextoVelocidadeMov");
                break;
            case "SliderArraste":
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Arraste");
                UpdateValues("TextoArraste");
                break;
            case "OptionWeightedAverage":
                if (PlayerPrefs.GetInt("DataProcessingMode") == 1) 
                {
                    GameObject.Find("OptionWeightedAverage").GetComponent<Image>().color = new Color(0.85f, 0.93f, 0.74f, 1f);
                }
                else
                {
                    GameObject.Find("OptionWeightedAverage").GetComponent<Image>().color = Color.white;
                }
                UpdateValues("DataProcessingMode");
                break;
            case "OptionPerformanceFromPreviousMatchOnly":
                if (PlayerPrefs.GetInt("DataProcessingMode") == 2) 
                {
                    GameObject.Find("OptionPerformanceFromPreviousMatchOnly").GetComponent<Image>().color = new Color(0.85f, 0.93f, 0.74f, 1f);
                }
                else
                {
                    GameObject.Find("OptionPerformanceFromPreviousMatchOnly").GetComponent<Image>().color = Color.white;
                }
                UpdateValues("DataProcessingMode");
                break;
            case "OptionPerformanceFromCalibrationOnly":
                if (PlayerPrefs.GetInt("DataProcessingMode") == 3) 
                {
                    GameObject.Find("OptionPerformanceFromCalibrationOnly").GetComponent<Image>().color = new Color(0.85f, 0.93f, 0.74f, 1f);
                }
                else
                {
                    GameObject.Find("OptionPerformanceFromCalibrationOnly").GetComponent<Image>().color = Color.white;
                }
                UpdateValues("DataProcessingMode");
                break;
        }
    }

    public void SetValueTimer(float value)
    {
        SetValueTimer(Mathf.FloorToInt(value));
        UpdateValues("TextoTimerContagem");
    }

    public void SetValueVelocidade(float value)
    {
        SetValueVelocidade(Mathf.FloorToInt(value));
        UpdateValues("TextoVelocidadeMov");
    }

    public void SetValueArraste(float value)
    {
        SetValueArraste(Mathf.FloorToInt(value));
        UpdateValues("TextoArraste");
    }

    public void SetValueTimer(int value)
    {
        PlayerPrefs.SetInt("Timer", value);
#if UNITY_WEBGL
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
        Debug.Log("Sincronia disco - navegador realizada.");
#endif
    }

    public void SetValueVelocidade(int value)
    {
        PlayerPrefs.SetInt("Velocidade", value);
#if UNITY_WEBGL
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
        Debug.Log("Sincronia disco - navegador realizada.");
#endif
    }
    public void SetValueArraste(int value)
    {
        PlayerPrefs.SetInt("Arraste", value);
#if UNITY_WEBGL
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
        Debug.Log("Sincronia disco - navegador realizada.");
#endif
    }

    public void SetDataProcessingMode(int value)
    {
        PlayerPrefs.SetInt("DataProcessingMode", value);
        UpdateValues("OptionWeightedAverage");
        UpdateValues("OptionPerformanceFromPreviousMatchOnly");
        UpdateValues("OptionPerformanceFromCalibrationOnly");
#if UNITY_WEBGL
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
        Debug.Log("Sincronia disco - navegador realizada.");
#endif
    }
}

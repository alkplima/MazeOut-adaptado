using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_PhaseInfo : MonoBehaviour
{
    public Text text;

    private void OnEnable()
    {
        if (VariaveisGlobais.partidaCorrente > 0) // Trilha ou automática
        {
            if (VariaveisGlobais.partidaCorrente == 2001) // automática
            {
                ++VariaviesGlocais.contagemPartidasAuto;
                // TODO: somar 1 internamente usando a variavel global e implementar a lógica pro texto da fase no jogo
            }
        }
            PlayerPrefs.SetInt("DataProcessingMode", 2);

        UpdateValues(gameObject.name);
    }

    public void UpdateValues(string itemName)
    {
        // switch (itemName)
        // {
        //     case "TextoTimerContagem":
        //         if (VariaveisGlobais.Idioma=="BR")
        //             text.text = "Contagem regressiva: " + PlayerPrefs.GetInt("Timer").ToString() + " segundos (padrão = 60)";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text.text = "Timer countdown: " + PlayerPrefs.GetInt("Timer").ToString() + " seconds (default = 60)";
        //         else
        //             text.text = "Contagem regressiva: " + PlayerPrefs.GetInt("Timer").ToString() + " segundos (padrão = 60)";
        //         break;
        //     case "TextoVelocidadeMov":
        //         if (VariaveisGlobais.Idioma=="BR")
        //             text.text = "Velocidade de movimentação: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (padrão = 75)";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text.text = "Movement speed: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (default = 75)";
        //         else
        //             text.text = "Velocidade de movimentação: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (padrão = 75)";
        //         break;
        //     case "TextoArraste":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text.text = "Efeito de atrito (\"arraste\"): " + PlayerPrefs.GetInt("Arraste").ToString() + " (padrão = 05)";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text.text = "\"Friction\" effect: " + PlayerPrefs.GetInt("Arraste").ToString() + " (default = 05)";
        //         else
        //             text.text = "Efeito de atrito (\"arraste\"): " + PlayerPrefs.GetInt("Arraste").ToString() + " (padrão = 05)";
        //         break;
        //     case "TextoModoProcessamentoDeDados":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text.text = "Modo de processamento de dados:";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text.text = "Data processing mode:";
        //         else
        //             text.text = "Modo de processamento de dados:";
        //         break;
        //     case "SliderTimerContagem":
        //         gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Timer");
        //         UpdateValues("TextoTimerContagem");
        //         break;
        //     case "SliderVelocidadeMov":
        //         gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Velocidade");
        //         UpdateValues("TextoVelocidadeMov");
        //         break;
        //     case "SliderArraste":
        //         gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Arraste");
        //         UpdateValues("TextoArraste");
        //         break;
        //     case "TextWeighted":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text2.text = "Média ponderada de todas partidas";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text2.text = "Weighted average of all matches";
        //         else
        //             text2.text = "Média ponderada de todas partidas";
        //         break;
        //     case "TextPreviousMatch":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text2.text = "Desempenho da partida anterior";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text2.text = "Performance from previous match";
        //         else
        //             text2.text = "Desempenho da partida anterior";
        //         break;
        //     case "TextCalibration":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text2.text = "Desempenho apenas da calibração";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text2.text = "Performance from calibration only";
        //         else
        //             text2.text = "Desempenho apenas da calibração";
        //         break;
        //     case "OptionWeightedAverage":
        //         if (PlayerPrefs.GetInt("DataProcessingMode") == 1) 
        //         {
        //             GameObject.Find("OptionWeightedAverage").GetComponent<Image>().color = new Color(0.85f, 0.93f, 0.74f, 1f);
        //         }
        //         else
        //         {
        //             GameObject.Find("OptionWeightedAverage").GetComponent<Image>().color = Color.white;
        //         }
        //         UpdateValues("TextoModoProcessamentoDeDados");
        //         UpdateValues("TextWeighted");
        //         break;
        //     case "OptionPerformanceFromPreviousMatchOnly":
        //         if (PlayerPrefs.GetInt("DataProcessingMode") == 2) 
        //         {
        //             GameObject.Find("OptionPerformanceFromPreviousMatchOnly").GetComponent<Image>().color = new Color(0.85f, 0.93f, 0.74f, 1f);
        //         }
        //         else
        //         {
        //             GameObject.Find("OptionPerformanceFromPreviousMatchOnly").GetComponent<Image>().color = Color.white;
        //         }
        //         UpdateValues("TextoModoProcessamentoDeDados");
        //         UpdateValues("TextPreviousMatch");
        //         break;
        //     case "OptionPerformanceFromCalibrationOnly":
        //         if (PlayerPrefs.GetInt("DataProcessingMode") == 3) 
        //         {
        //             GameObject.Find("OptionPerformanceFromCalibrationOnly").GetComponent<Image>().color = new Color(0.85f, 0.93f, 0.74f, 1f);
        //         }
        //         else
        //         {
        //             GameObject.Find("OptionPerformanceFromCalibrationOnly").GetComponent<Image>().color = Color.white;
        //         }
        //         UpdateValues("TextoModoProcessamentoDeDados");
        //         UpdateValues("TextCalibration");
        //         break;
        //     case "TextoAjustesWebcam":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text.text = "Imagem da webcam";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text.text = "Webcam image";
        //         else
        //             text.text = "Imagem da webcam";
        //         break;
        //     case "BTNWebcamEspelharHorizontalmente":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text2.text = "Espelhar horizontalmente";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text2.text = "Mirror horizontally";
        //         else
        //             text2.text = "Espelhar horizontalmente";
        //         UpdateValues("TextoAjustesWebcam");
        //         break;
        //     case "BTNWebcamEspelharVerticalmente":
        //         if (VariaveisGlobais.Idioma == "BR") 
        //             text2.text = "Espelhar verticalmente";
        //         else if (VariaveisGlobais.Idioma == "EN")
        //             text2.text = "Mirror vertically";
        //         else
        //             text2.text = "Espelhar verticalmente";
        //         UpdateValues("TextoAjustesWebcam");
        //         break;
        // }
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
}

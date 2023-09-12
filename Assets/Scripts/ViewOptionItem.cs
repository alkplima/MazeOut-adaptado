using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewOptionItem : MonoBehaviour
{
    public Text text;

    private void OnEnable()
    {
        UpdateValues(gameObject.name);
    }

    public void UpdateValues(string itemName)
    {
        switch (itemName)
        {
            case "TextoVelocidadeMov":
                if (VariaveisGlobais.Idioma=="BR")
                    text.text = "Velocidade de movimentação: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (padrão '75')";
                else if (VariaveisGlobais.Idioma == "EN")
                    text.text = "Movement speed:" + PlayerPrefs.GetInt("Velocidade").ToString() + " (default '75')";
                else
                    text.text = "Velocidade de movimentação: " + PlayerPrefs.GetInt("Velocidade").ToString() + " (padrão '75')";
                break;
            case "TextoArraste":
                if (VariaveisGlobais.Idioma == "BR") 
                    text.text = "Efeito de atrito (\"arraste\"): " + PlayerPrefs.GetInt("Arraste").ToString() + " (padrão '05')";
                else if (VariaveisGlobais.Idioma == "EN")
                    text.text = "\"Friction\" effect: " + PlayerPrefs.GetInt("Arraste").ToString() + " (default '05')";
                else
                    text.text = "Efeito de atrito (\"arraste\"): " + PlayerPrefs.GetInt("Arraste").ToString() + " (padrão '05')";
                break;
            case "SliderVelocidadeMov":
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Velocidade");
                UpdateValues("TextoVelocidadeMov");
                break;
            case "SliderArraste":
                gameObject.GetComponent<Slider>().value = PlayerPrefs.GetInt("Arraste");
                UpdateValues("TextoArraste");
                break;
        }
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
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_HR_Message : MonoBehaviour
{
    Text textBox;
  
    public void OnEnable()
    {
        textBox = GetComponent<Text>();
        textBox.text = "Min HR: " + VariaveisGlobais.minHRPartidaAtual.ToString() + "\n" +
                       "Avg HR: " + VariaveisGlobais.avgHRPartidaAtual.ToString() + "\n" +
                       "Max HR: " + VariaveisGlobais.maxHRPartidaAtual.ToString();
        Debug.Log("Min HR: " + VariaveisGlobais.minHRPartidaAtual.ToString());
        Debug.Log("Avg HR: " + VariaveisGlobais.avgHRPartidaAtual.ToString());
        Debug.Log("Max HR: " + VariaveisGlobais.maxHRPartidaAtual.ToString());
    }
}

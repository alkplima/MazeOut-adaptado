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
        textBox.text = "Max HR: " + VariaveisGlobais.maxHRPartidaAtual.ToString();
        Debug.Log("Max HR: " + VariaveisGlobais.maxHRPartidaAtual.ToString());
        Debug.Log(VariaveisGlobais.maxHRPartidaAtual);
    }
}

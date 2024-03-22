using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Timer_Message : MonoBehaviour
{
    TMP_Text textBox;
  
    public void OnEnable()
    {
        textBox = GetComponent<TMP_Text>();
        float tempoTotalEmSegundos = (float) VariaveisGlobais.tempoTotalGasto;
        
        int minutes = Mathf.FloorToInt(tempoTotalEmSegundos / 60);
        int seconds = Mathf.FloorToInt(tempoTotalEmSegundos - minutes * 60);
        textBox.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }
}

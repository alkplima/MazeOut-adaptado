using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Score_Message : MonoBehaviour
{
    Text textBox;
  
    public void OnEnable()
    {
        textBox = GetComponent<Text>();
        textBox.text = "Score: " + VariaveisGlobais.scoreFinal.ToString() + "\n" +
                       "Your best: " + VariaveisGlobais.scoreRecorde.ToString();
    }
}

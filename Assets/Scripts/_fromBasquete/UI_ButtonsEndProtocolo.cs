using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UI_ButtonsEndProtocolo : MonoBehaviour {
	public bool isClose, isRetry, isNext;

	public void ClickButton()
    {
        if (isClose)
            VariaveisGlobais.expressoProtocolo = false;
        else if (isRetry)
        {
            VariaveisGlobais.expressoProtocolo = true;
            VariaveisGlobais.partidaProtocoloCorrente--;
        }
        else if (isNext)
            VariaveisGlobais.expressoProtocolo = true;

        SceneManager.LoadScene("MenuScene");
    }

}

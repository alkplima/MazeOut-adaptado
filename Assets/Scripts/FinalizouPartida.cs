using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalizouPartida : MonoBehaviour
{
    GameObject gameScreenManager;
    void OnEnable()
    {
        gameScreenManager = GameObject.Find("GameScreenManager");
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        NovaDeteccao(other);
    }
    
    private void NovaDeteccao(Collider2D other)
    {
        if (other.tag.StartsWith("Bola"))
        {
            if (VariaveisGlobais.estiloJogoCorrente != "PartidaAvulsa")
            {               
                VariaveisGlobais.conexaoBD.PostData();
            }
            gameScreenManager.GetComponent<UI_MessageScreen>().ShowSuccessModal();
        }
    }
}
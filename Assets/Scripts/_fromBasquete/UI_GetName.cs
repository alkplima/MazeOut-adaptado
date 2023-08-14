using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_GetName : MonoBehaviour {

    private void OnEnable()
    {
        if (VariaveisGlobais.estaNaPesquisa)
            gameObject.GetComponent<Text>().text = VariaveisGlobais.IdentificadorNome;
        else
            gameObject.GetComponent<Text>().text = "";
    }
}

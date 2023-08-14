using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class PegarImagemQuebracabecaCorrente : MonoBehaviour
{
    private void OnEnable()
    {
        if (gameObject.GetComponent<Image>())
            if (PaterlandGlobal.quebracabeca_da_vez != null)
                gameObject.GetComponent<Image>().sprite = PaterlandGlobal.quebracabeca_da_vez;
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ECD_Webcam : MonoBehaviour {
    public string ECD_tipo;
	public GameObject botaoLancar;
	public ScriptLancar colliderClicks;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void verificarJogada()
    {
		//if (ECD_tipo == VariaveisGlobais.PosicaoJogada_Zona)
		if (ECD_tipo == VariaveisGlobais.PosicaoJogada)
			if (botaoLancar)
                if (botaoLancar.activeSelf == true)
					colliderClicks.Lancamento();
    }
}

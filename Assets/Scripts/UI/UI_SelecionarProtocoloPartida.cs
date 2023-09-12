using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelecionarProtocoloPartida : MonoBehaviour {
	public int protocolo, partida;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void SelPartida()
    {

		VariaveisGlobais.protocoloCorrente = protocolo;
		VariaveisGlobais.partidaProtocoloCorrente = partida;
		VariaveisGlobais.ProtocoloToConfigValues();
	}


}

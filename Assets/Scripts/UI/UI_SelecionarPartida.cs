using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_SelecionarPartida : MonoBehaviour {
	public int partida;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	public void SelPartida()
  {
		VariaveisGlobais.partidaCorrente = partida;
		// VariaveisGlobais.ProtocoloToConfigValues();
	}
}

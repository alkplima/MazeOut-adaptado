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

	public void SelecionarPartidaDinamicamente()
	{
		// if (VariaveisGlobais.estiloJogoCorrente == "PartidaAvulsa")
		// {
		// 	VariaveisGlobais.partidaCorrente = partida;
		// }
		// else 
		if (VariaveisGlobais.estiloJogoCorrente == "Trilha")
		{
			VariaveisGlobais.partidaCorrente += 1;
		}
		else if (VariaveisGlobais.estiloJogoCorrente == "Calibracao")
		{
			if (VariaveisGlobais.partidaCorrente == -7 || VariaveisGlobais.partidaCorrente == 2001) { // TODO: Mudar pra -7
				VariaveisGlobais.partidaCorrente = 2001; // Partida de labirinto automático
			} else {
				VariaveisGlobais.partidaCorrente -= 1;
			}
		}
		else
		{
			//Continuar daqui        
		}
	}
}

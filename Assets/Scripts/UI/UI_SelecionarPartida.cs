﻿using System.Collections;
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
		Debug.Log("Estilo Jogo Corrente: " + VariaveisGlobais.estiloJogoCorrente + " Partida Corrente: " + VariaveisGlobais.partidaCorrente + " Partida: " + partida);
		if (VariaveisGlobais.estiloJogoCorrente == "Trilha") // Trilha
		{
			VariaveisGlobais.partidaCorrente += 1;
		}
		else if (VariaveisGlobais.estiloJogoCorrente == "Calibracao")
		{
			if (VariaveisGlobais.partidaCorrente >= 21 && VariaveisGlobais.partidaCorrente != 2001)
			{
				VariaveisGlobais.partidaCorrente += 1; // Partidas não-adaptativas
			}
			else if (VariaveisGlobais.partidaCorrente == -8 || VariaveisGlobais.partidaCorrente == 2001) 
			{ 
				VariaveisGlobais.partidaCorrente = 2001; // Partida de labirinto automático
			} 
			else
			{
				VariaveisGlobais.partidaCorrente -= 1; // Calibração
			}
		}
		else
		{
			//Continuar daqui        
		}
	}
}

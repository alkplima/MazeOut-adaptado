using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyBotaoSensivelAIdentificador : MonoBehaviour
{
	public Text botaoToBeSensibilizado;

	// Use this for initialization
	private void OnEnable()
	{
		VariaveisGlobais.RefreshValues();

		if (VariaveisGlobais.IdentificadorNome.Trim().Length > 0)
        {
			botaoToBeSensibilizado.text = "Olá, " + VariaveisGlobais.IdentificadorNome + "!"
				+ System.Environment.NewLine + "Não é você? Clique aqui.";

			botaoToBeSensibilizado.gameObject.SetActive(true);
		}

		else
			botaoToBeSensibilizado.gameObject.SetActive(false);
	}
}
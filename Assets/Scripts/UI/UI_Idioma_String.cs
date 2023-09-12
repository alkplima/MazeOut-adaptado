using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Idioma_String : MonoBehaviour {
	public string[] idiomas= new string[2];
	[Multiline]
	public string[] textoAdequado = new string[2];

	public void OnEnable()
    {
		int indexIdioma = -1;

		for (int i = 0; i < idiomas.Length; i++)
			if (idiomas[i] == VariaveisGlobais.Idioma)
				indexIdioma = i;

		if (indexIdioma >= 0)
			gameObject.GetComponent<Text>().text = textoAdequado[indexIdioma];
    }
	
}

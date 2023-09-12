using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Idioma_SetConfig : MonoBehaviour {
	public GameObject[] groupToRefresh;
	public string idiomaCorrespondente;
	public AudioSource audioEffect;
	public void SelectIdioma()
    {
		PlayerPrefs.SetString("Idioma", idiomaCorrespondente);
		VariaveisGlobais.RefreshValues();

		StartCoroutine(groupRefresh());

		if (audioEffect!=null)
			audioEffect.Play();
}

	IEnumerator groupRefresh()
    {
		foreach (GameObject go in groupToRefresh)
			go.SetActive(false);
		
		yield return new WaitForEndOfFrame();

		foreach (GameObject go in groupToRefresh)
			go.SetActive(true);
	}


}

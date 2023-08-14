using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class REF_imagem : MonoBehaviour
{	
	public GameObject ImageAguardandoAtualizacao;

	void Awake()
	{
		if (PaterlandGlobal.REF_image_instance == null)
		{
            PaterlandGlobal.REF_image_instance = this;
			DontDestroyOnLoad(this.gameObject);
		}
		else
			Destroy(this.gameObject);
		
	}

	// Update is called once per frame
	void Update () {
		
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DummySomeComOmenino : MonoBehaviour {
	
	public GameObject menino;

	// Use this for initialization
	void Start () {
		menino.SetActive(false);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	private void OnEnable()
	{
			menino.SetActive(false);
	}
}

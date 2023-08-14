using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DummyNewColor : MonoBehaviour {

	public Color newColor;
	public Image imageToBeColored; 
	// Use this for initialization
	void Start () {
		imageToBeColored.color = newColor;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}

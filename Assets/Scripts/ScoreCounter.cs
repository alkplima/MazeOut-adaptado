
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreCounter : MonoBehaviour {
    [SerializeField] ScoreHUD scoreHUD;

    private void Start() {
        StartCoroutine (CountPoints());
    }

    private IEnumerator CountPoints() {
        while (true) {
            scoreHUD.Score += 100;

            yield return new WaitForSeconds (1);
        }
    }
}
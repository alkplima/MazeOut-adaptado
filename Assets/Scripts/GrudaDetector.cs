using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrudaDetector : MonoBehaviour
{
    public char lado;
    void Update()
    {
        Transform bola = GameObject.FindGameObjectsWithTag("Bola")[0].transform;

        switch (lado.ToString().ToUpper())
        {
            case "L":
                transform.position = new Vector2(bola.position.x - (bola.GetComponent<RectTransform>().rect.width * 0.5f), bola.position.y);
                break;
            case "R":
                transform.position = new Vector2(bola.position.x + (bola.GetComponent<RectTransform>().rect.width * 0.5f), bola.position.y);
                break;
            case "T":
                transform.position = new Vector2(bola.position.x, bola.position.y + (bola.GetComponent<RectTransform>().rect.height * 0.5f));
                break;
            case "B":
                transform.position = new Vector2(bola.position.x, bola.position.y - (bola.GetComponent<RectTransform>().rect.height * 0.5f));
                break;
        }
    }
}

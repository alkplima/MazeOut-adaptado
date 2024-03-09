using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrudaDetector : MonoBehaviour
{
    public char lado;
    public Transform bola;

    void Update()
    {
        switch (lado.ToString().ToUpper())
        {
            case "L":
                Modificar("L");
                break;
            case "R":
                Modificar("R");
                break;
            case "T":
                Modificar("T");
                break;
            case "B":
                Modificar("B");
                break;
            case "!":
                Modificar("!");
                break;
        }
    }

    private void Modificar(string ladoStr)
    {
        Vector3[] cantosBola = new Vector3[4];
        Vector3[] cantosBotaoSensor = new Vector3[4];

        bola.GetComponent<RectTransform>().GetWorldCorners(cantosBola);
        GetComponent<RectTransform>().GetWorldCorners(cantosBotaoSensor);

        float alturaBola = Mathf.Abs(cantosBola[0].y - cantosBola[1].y);
        float larguraBola = Mathf.Abs(cantosBola[1].x - cantosBola[2].x);

        float alturaBotaoSensor = Mathf.Abs(cantosBotaoSensor[0].y - cantosBotaoSensor[1].y);
        float larguraBotaoSensor = Mathf.Abs(cantosBotaoSensor[1].x - cantosBotaoSensor[2].x);

        switch (ladoStr)
        {
            case "L":
                transform.position = new Vector3(bola.position.x - (larguraBola * 0.5f) - (larguraBotaoSensor / 2), bola.position.y + (alturaBotaoSensor / 2), bola.position.z);
                transform.localScale = new Vector3(transform.localScale.x, alturaBola, transform.localScale.z);
                break;
            case "R":
                transform.position = new Vector3(bola.position.x + (larguraBola * 0.5f) - (larguraBotaoSensor / 2), bola.position.y + (alturaBotaoSensor / 2), bola.position.z);
                transform.localScale = new Vector3(transform.localScale.x, alturaBola, transform.localScale.z);
                break;
            case "T":
                transform.position = new Vector3(bola.position.x - (larguraBotaoSensor / 2), bola.position.y + (alturaBola * 0.5f) + (alturaBotaoSensor / 2), bola.position.z);
                transform.localScale = new Vector3(larguraBola, transform.localScale.y, transform.localScale.z);
                break;
            case "B":
                transform.position = new Vector3(bola.position.x - (larguraBotaoSensor / 2), bola.position.y - (alturaBola * 0.5f) + (alturaBotaoSensor / 2), bola.position.z);
                transform.localScale = new Vector3(larguraBola, transform.localScale.y, transform.localScale.z);
                break;
            case "!":
                transform.position = bola.position;
                break;
        }
    }
}

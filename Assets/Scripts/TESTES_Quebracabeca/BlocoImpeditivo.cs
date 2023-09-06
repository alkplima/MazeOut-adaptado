using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoImpeditivo : MonoBehaviour
{
    // public Controller_Ativ01_QuebraCabeca controller;
    // public RectTransform posicaoAtualNoGrid;
    // public GameObject ponto, ponto2;
    // public bool ChegouNoAlvo = false;

    // Start is called before the first frame update
    void Start()
    {
        // if (posicaoAtualNoGrid)
        //     transform.position = posicaoAtualNoGrid.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void OnTriggerEnter2D(Collider2D other) 
    {
        float alturaParede = gameObject.GetComponent<RectTransform>().rect.height;
        float larguraParede = gameObject.GetComponent<RectTransform>().rect.width;
        float alturaHandGear = other.transform.GetChild(0).GetComponent<RectTransform>().rect.height;
        float larguraHandGear = other.transform.GetChild(0).GetComponent<RectTransform>().rect.width;

        // Em cima da parede
        if (other.transform.position.y >= (this.transform.position.y+(alturaParede/2+0.4*alturaHandGear)) && 
            !(other.transform.position.x < (this.transform.position.x-(larguraParede/2 + larguraHandGear/2)) && 
            other.transform.position.x > (this.transform.position.x+(larguraParede/2 + larguraHandGear/2))))
        {
            Debug.Log("EM CIMA");
            other.transform.position = new Vector3(other.transform.position.x, this.transform.position.y + ((alturaParede/2) + (alturaHandGear/2)), other.transform.position.z);
        }
        // Embaixo da parede
        else if (other.transform.position.y <= (this.transform.position.y-(alturaParede/2+0.4*alturaHandGear)) && 
            !(other.transform.position.x < (this.transform.position.x-(larguraParede/2 + larguraHandGear/2)) && 
            other.transform.position.x > (this.transform.position.x+(larguraParede/2 + larguraHandGear/2)))) 
        {
            Debug.Log("EMBAIXO");
            other.transform.position = new Vector3(other.transform.position.x, this.transform.position.y - ((alturaParede/2) + (alturaHandGear/2)), other.transform.position.z);
        }
        // À direita da parede
        else if (other.transform.position.x >= (this.transform.position.x+(larguraParede/2+0.4*larguraHandGear)) && 
            !(other.transform.position.y < (this.transform.position.y-(alturaParede/2 + alturaHandGear/2)) && 
            other.transform.position.y > (this.transform.position.y+(alturaParede/2 + alturaHandGear/2)))) 
        {
            Debug.Log("DIREITA");
            other.transform.position = new Vector3(this.transform.position.x + ((larguraParede/2) + (larguraHandGear/2)), other.transform.position.y, other.transform.position.z);
        }
        // À esquerda da parede
        else if (other.transform.position.x <= (this.transform.position.x-(larguraParede/2+0.4*larguraHandGear)) && 
            !(other.transform.position.y < (this.transform.position.y-(alturaParede/2 + alturaHandGear/2)) && 
            other.transform.position.y > (this.transform.position.y+(alturaParede/2 + alturaHandGear/2)))) 
        {
            Debug.Log("ESQUERDA");
            other.transform.position = new Vector3(this.transform.position.x - ((larguraParede/2) + (larguraHandGear/2)), other.transform.position.y, other.transform.position.z);
        }
    }
}

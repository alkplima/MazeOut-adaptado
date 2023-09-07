using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoImpeditivo : MonoBehaviour
{
    private void OnTriggerStay2D(Collider2D other) 
    {
        if (other.tag.StartsWith("Bola"))
        {
            float alturaParede = gameObject.GetComponent<RectTransform>().rect.height * gameObject.GetComponent<RectTransform>().lossyScale.y;
            float larguraParede = gameObject.GetComponent<RectTransform>().rect.width * gameObject.GetComponent<RectTransform>().lossyScale.x;
            float alturaHandGear = other.transform.GetComponent<RectTransform>().rect.height * other.transform.GetComponent<RectTransform>().lossyScale.y;
            float larguraHandGear = other.transform.GetComponent<RectTransform>().rect.width * other.transform.GetComponent<RectTransform>().lossyScale.x;

            // Em cima da parede
            if (other.transform.position.y >= (this.transform.position.y + (alturaParede / 2 + 0.4 * alturaHandGear)) &&
                !(other.transform.position.x < (this.transform.position.x - (larguraParede / 2 + larguraHandGear / 2)) &&
                other.transform.position.x > (this.transform.position.x + (larguraParede / 2 + larguraHandGear / 2))))
            {
                Debug.Log("EM CIMA");
                other.GetComponent<PieceController>().MovimentarPeca('C', 3, 20);
            }
            // Embaixo da parede
            else if (other.transform.position.y <= (this.transform.position.y - (alturaParede / 2 + 0.4 * alturaHandGear)) &&
                !(other.transform.position.x < (this.transform.position.x - (larguraParede / 2 + larguraHandGear / 2)) &&
                other.transform.position.x > (this.transform.position.x + (larguraParede / 2 + larguraHandGear / 2))))
            {
                Debug.Log("EMBAIXO");
                other.GetComponent<PieceController>().MovimentarPeca('B', 3, 20);
            }
            // À direita da parede
            else if (other.transform.position.x >= (this.transform.position.x + (larguraParede / 2 + 0.4 * larguraHandGear)) &&
                !(other.transform.position.y < (this.transform.position.y - (alturaParede / 2 + alturaHandGear / 2)) &&
                other.transform.position.y > (this.transform.position.y + (alturaParede / 2 + alturaHandGear / 2))))
            {
                Debug.Log("DIREITA");
                other.GetComponent<PieceController>().MovimentarPeca('D', 3, 20);
            }
            // À esquerda da parede
            else if (other.transform.position.x <= (this.transform.position.x - (larguraParede / 2 + 0.4 * larguraHandGear)) &&
            !(other.transform.position.y < (this.transform.position.y - (alturaParede / 2 + alturaHandGear / 2)) &&
            other.transform.position.y > (this.transform.position.y + (alturaParede / 2 + alturaHandGear / 2))))
            {
                Debug.Log("ESQUERDA");
                other.GetComponent<PieceController>().MovimentarPeca('E', 3, 20);
            }
        }        
    }
}

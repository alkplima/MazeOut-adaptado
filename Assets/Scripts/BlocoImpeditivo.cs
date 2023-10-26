using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoImpeditivo : MonoBehaviour
{
    private Vector3[] cantosBloco = new Vector3[4];
    private Vector3[] cantosOtherSombra = new Vector3[4];

    private void OnTriggerStay2D(Collider2D other)
    {
        //VelhaDeteccao(other);
        NovaDeteccao(other);
    }
    
    private void NovaDeteccao(Collider2D other)
    {
        if (other.tag.StartsWith("Bola"))
        {
            gameObject.GetComponent<RectTransform>().GetWorldCorners(cantosBloco);
            other.GetComponent<PieceController>().sombra.GetWorldCorners(cantosOtherSombra);

            float alturaBloco = Mathf.Abs(cantosBloco[0].y - cantosBloco[1].y);
            float larguraBloco = Mathf.Abs(cantosBloco[1].x - cantosBloco[2].x);

            float alturaOther = Mathf.Abs(cantosOtherSombra[0].y - cantosOtherSombra[1].y);
            float larguraOther = Mathf.Abs(cantosOtherSombra[1].x - cantosOtherSombra[2].x);

            char sentido = ' ';

            if (other.GetComponent<PieceController>().enabled)
                if (Vector2.Distance(other.GetComponent<RectTransform>().position, GetComponent<RectTransform>().position) <= (0.8f * larguraOther / 2 + larguraBloco / 2))
                {
                    float alturaParede = alturaBloco;
                    float larguraParede = larguraBloco;
                    float alturaHandGear = 0.7f * alturaOther;
                    float larguraHandGear = 0.7f * larguraOther;

                    // Verificar se o movimento vem de baixo, de cima, do lado esquerdo ou do lado direito

                    float distanciaParaCima = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x, other.GetComponent<RectTransform>().position.y - (alturaOther / 2), other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                    float distanciaParaBaixo = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x, other.GetComponent<RectTransform>().position.y + (alturaOther / 2), other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                    float distanciaParaDireita = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x + (larguraOther / 2), other.GetComponent<RectTransform>().position.y, other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                    float distanciaParaEsquerda = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x - (larguraOther / 2), other.GetComponent<RectTransform>().position.y, other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);

                    if ((distanciaParaCima <= distanciaParaBaixo) && (distanciaParaCima <= distanciaParaEsquerda) && (distanciaParaCima <= distanciaParaDireita))
                    {
                        // sentido = 'N';
                        //Debug.Log("Bateu em N");
                        other.transform.position = new Vector3(other.transform.position.x, this.transform.position.y + ((alturaParede/2) + (alturaHandGear/2)), other.transform.position.z);                    }
                    else if ((distanciaParaEsquerda <= distanciaParaCima) && (distanciaParaEsquerda <= distanciaParaBaixo) && (distanciaParaEsquerda <= distanciaParaDireita))
                    {
                        // sentido = 'L';
                        //Debug.Log("Bateu em L");
                        other.transform.position = new Vector3(this.transform.position.x + ((larguraParede/2) + (larguraHandGear/2)), other.transform.position.y, other.transform.position.z);
                    }
                    else if ((distanciaParaBaixo <= distanciaParaCima) && (distanciaParaBaixo <= distanciaParaEsquerda) && (distanciaParaBaixo <= distanciaParaDireita))
                    {
                        // sentido = 'S';
                        //Debug.Log("Bateu em S");
                        other.transform.position = new Vector3(other.transform.position.x, this.transform.position.y - ((alturaParede/2) + (alturaHandGear/2)), other.transform.position.z);
                    }
                    else if ((distanciaParaDireita <= distanciaParaCima) && (distanciaParaDireita <= distanciaParaEsquerda) && (distanciaParaDireita <= distanciaParaBaixo))
                    {
                        // sentido = 'O';
                        //Debug.Log("Bateu em O");
                        other.transform.position = new Vector3(this.transform.position.x - ((larguraParede/2) + (larguraHandGear/2)), other.transform.position.y, other.transform.position.z);
                    }


                    // switch (sentido)
                    // {
                    //     case 'N':
                    //         other.GetComponent<PieceController>().MovimentarPeca('C', 3, 999);
                    //         break;
                    //     case 'S':
                    //         other.GetComponent<PieceController>().MovimentarPeca('B', 3, 999);
                    //         break;
                    //     case 'L':
                    //         other.GetComponent<PieceController>().MovimentarPeca('D', 3, 999);
                    //         break;
                    //     case 'O':
                    //         other.GetComponent<PieceController>().MovimentarPeca('E', 3, 999);
                    //         break;
                    // }
                }
        }
    }

    private void VelhaDeteccao(Collider2D other)
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
                other.GetComponent<PieceController>().MovimentarPeca('C', 3, 20);
            }
            // Embaixo da parede
            else if (other.transform.position.y <= (this.transform.position.y - (alturaParede / 2 + 0.4 * alturaHandGear)) &&
                !(other.transform.position.x < (this.transform.position.x - (larguraParede / 2 + larguraHandGear / 2)) &&
                other.transform.position.x > (this.transform.position.x + (larguraParede / 2 + larguraHandGear / 2))))
            {
                other.GetComponent<PieceController>().MovimentarPeca('B', 3, 20);
            }
            // À direita da parede
            else if (other.transform.position.x >= (this.transform.position.x + (larguraParede / 2 + 0.4 * larguraHandGear)) &&
                !(other.transform.position.y < (this.transform.position.y - (alturaParede / 2 + alturaHandGear / 2)) &&
                other.transform.position.y > (this.transform.position.y + (alturaParede / 2 + alturaHandGear / 2))))
            {
                other.GetComponent<PieceController>().MovimentarPeca('D', 3, 20);
            }
            // À esquerda da parede
            else if (other.transform.position.x <= (this.transform.position.x - (larguraParede / 2 + 0.4 * larguraHandGear)) &&
            !(other.transform.position.y < (this.transform.position.y - (alturaParede / 2 + alturaHandGear / 2)) &&
            other.transform.position.y > (this.transform.position.y + (alturaParede / 2 + alturaHandGear / 2))))
            {
                other.GetComponent<PieceController>().MovimentarPeca('E', 3, 20);
            }
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminhoWebcamItemController : MonoBehaviour
{
    public char sentido;
    public Controller_Ativ01_QuebraCabeca controller;
    public bool areaDeTolerancia = false;

    private void OnEnable()
    {

    }

    void Update()
    {
        if (areaDeTolerancia)
            Verificar(0.5f);
    }

    public void Verificar()
    {
        Verificar(1);
    }

    public void Verificar(float intensidade)
    {
        for (int i = 0; i < controller.pecas.Length; i++)
        {
            if (controller.pecas[i].GetComponent<PieceController>().enabled)
                if (Vector2.Distance(controller.pecas[i].position, transform.position) < (controller.pecas[i].rect.width / 2))//gameObject.GetComponent<RectTransform>().rect.width / 2))
                {
                    // Verificar se o movimento vem de baixo, de cima, do lado esquerdo ou do lado direito

                    float distanciaParaCima = Vector3.Distance(new Vector3(controller.pecas[i].position.x, controller.pecas[i].position.y - (controller.pecas[i].rect.height / 2), controller.pecas[i].position.z), transform.position);
                    float distanciaParaBaixo = Vector3.Distance(new Vector3(controller.pecas[i].position.x, controller.pecas[i].position.y + (controller.pecas[i].rect.height / 2), controller.pecas[i].position.z), transform.position);
                    float distanciaParaDireita = Vector3.Distance(new Vector3(controller.pecas[i].position.x + (controller.pecas[i].rect.width / 2), controller.pecas[i].position.y, controller.pecas[i].position.z), transform.position);
                    float distanciaParaEsquerda = Vector3.Distance(new Vector3(controller.pecas[i].position.x - (controller.pecas[i].rect.width / 2), controller.pecas[i].position.y, controller.pecas[i].position.z), transform.position);

                    if ((distanciaParaCima <= distanciaParaBaixo) && (distanciaParaCima <= distanciaParaEsquerda) && (distanciaParaCima <= distanciaParaDireita))
                        sentido = 'N';
                    else if ((distanciaParaBaixo <= distanciaParaCima) && (distanciaParaBaixo <= distanciaParaEsquerda) && (distanciaParaBaixo <= distanciaParaDireita))
                        sentido = 'S';
                    else if ((distanciaParaEsquerda <= distanciaParaCima) && (distanciaParaEsquerda <= distanciaParaBaixo) && (distanciaParaEsquerda <= distanciaParaDireita))
                        sentido = 'L';
                    else if ((distanciaParaDireita <= distanciaParaCima) && (distanciaParaDireita <= distanciaParaEsquerda) && (distanciaParaDireita <= distanciaParaBaixo))
                        sentido = 'O';


                    switch (sentido)
                    {
                        case 'N':
                            controller.pecas[i].GetComponent<PieceController>().MovimentarPeca('C', intensidade);
                            break;
                        case 'S':
                            controller.pecas[i].GetComponent<PieceController>().MovimentarPeca('B', intensidade);
                            break;
                        case 'L':
                            controller.pecas[i].GetComponent<PieceController>().MovimentarPeca('D', intensidade);
                            break;
                        case 'O':
                            controller.pecas[i].GetComponent<PieceController>().MovimentarPeca('E', intensidade);
                            break;
                    }
                }
        }        
    }
}

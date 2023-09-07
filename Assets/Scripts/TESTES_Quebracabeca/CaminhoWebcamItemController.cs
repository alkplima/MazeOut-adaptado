using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminhoWebcamItemController : MonoBehaviour
{
    public char sentido;
    public bool areaDeTolerancia = false;

    void Update()
    {
        if (areaDeTolerancia)
            //Verificar(0.5f);
            Verificar(1);
    }

    public void Verificar()
    {
        Verificar(2);
    }

    public void Verificar(float intensidade)
    {
        for (int i = 0; i < VariaveisGlobais.atualControllerLabirinto.pecas.Length; i++)
        {
            if (VariaveisGlobais.atualControllerLabirinto.pecas[i].GetComponent<PieceController>().enabled)
                if (Vector2.Distance(VariaveisGlobais.atualControllerLabirinto.pecas[i].position, transform.position) < (VariaveisGlobais.atualControllerLabirinto.pecas[i].rect.width / 2))//gameObject.GetComponent<RectTransform>().rect.width / 2))
                {
                    // Verificar se o movimento vem de baixo, de cima, do lado esquerdo ou do lado direito

                    float distanciaParaCima = Vector3.Distance(new Vector3(VariaveisGlobais.atualControllerLabirinto.pecas[i].position.x, VariaveisGlobais.atualControllerLabirinto.pecas[i].position.y - (VariaveisGlobais.atualControllerLabirinto.pecas[i].rect.height / 2), VariaveisGlobais.atualControllerLabirinto.pecas[i].position.z), transform.position);
                    float distanciaParaBaixo = Vector3.Distance(new Vector3(VariaveisGlobais.atualControllerLabirinto.pecas[i].position.x, VariaveisGlobais.atualControllerLabirinto.pecas[i].position.y + (VariaveisGlobais.atualControllerLabirinto.pecas[i].rect.height / 2), VariaveisGlobais.atualControllerLabirinto.pecas[i].position.z), transform.position);
                    float distanciaParaDireita = Vector3.Distance(new Vector3(VariaveisGlobais.atualControllerLabirinto.pecas[i].position.x + (VariaveisGlobais.atualControllerLabirinto.pecas[i].rect.width / 2), VariaveisGlobais.atualControllerLabirinto.pecas[i].position.y, VariaveisGlobais.atualControllerLabirinto.pecas[i].position.z), transform.position);
                    float distanciaParaEsquerda = Vector3.Distance(new Vector3(VariaveisGlobais.atualControllerLabirinto.pecas[i].position.x - (VariaveisGlobais.atualControllerLabirinto.pecas[i].rect.width / 2), VariaveisGlobais.atualControllerLabirinto.pecas[i].position.y, VariaveisGlobais.atualControllerLabirinto.pecas[i].position.z), transform.position);

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
                            VariaveisGlobais.atualControllerLabirinto.pecas[i].GetComponent<PieceController>().MovimentarPeca('C', intensidade);
                            break;
                        case 'S':
                            VariaveisGlobais.atualControllerLabirinto.pecas[i].GetComponent<PieceController>().MovimentarPeca('B', intensidade);
                            break;
                        case 'L':
                            VariaveisGlobais.atualControllerLabirinto.pecas[i].GetComponent<PieceController>().MovimentarPeca('D', intensidade);
                            break;
                        case 'O':
                            VariaveisGlobais.atualControllerLabirinto.pecas[i].GetComponent<PieceController>().MovimentarPeca('E', intensidade);
                            break;
                    }
                }
        }        
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CaminhoWebcamItemController : MonoBehaviour
{
    internal char sentido;
    public bool areaDeTolerancia = false;
    public bool newDetection = false;

    void Update()
    {
        if (areaDeTolerancia)
            Verificar(0.5f);
    }

    public void Verificar()
    {
        if (newDetection)
            Mover(gameObject.GetComponent<GrudaDetector>().lado, 1);
        else
            Verificar(1);
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

                    if ((distanciaParaCima <= distanciaParaBaixo) && (distanciaParaCima <= distanciaParaEsquerda) && (distanciaParaCima <= distanciaParaDireita)) {
                        sentido = 'N';
                        //Debug.Log("Bateu em N");
                    }
                    else if ((distanciaParaEsquerda <= distanciaParaCima) && (distanciaParaEsquerda <= distanciaParaBaixo) && (distanciaParaEsquerda <= distanciaParaDireita)) {
                        sentido = 'L';
                        //Debug.Log("Bateu em L");
                    }
                    else if ((distanciaParaBaixo <= distanciaParaCima) && (distanciaParaBaixo <= distanciaParaEsquerda) && (distanciaParaBaixo <= distanciaParaDireita)) {
                        sentido = 'S';
                        //Debug.Log("Bateu em S");
                    }
                    else if ((distanciaParaDireita <= distanciaParaCima) && (distanciaParaDireita <= distanciaParaEsquerda) && (distanciaParaDireita <= distanciaParaBaixo)) {
                        sentido = 'O';
                        //Debug.Log("Bateu em O");
                    }


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

    public void Mover(char lado, float intensidade)
    {
        switch (lado)
        {
            case 'L': // Veio de left, mover pra right
                VariaveisGlobais.atualControllerLabirinto.pecas[0].GetComponent<PieceController>().MovimentarPeca('D', intensidade);
                break;
            case 'R':// Veio de right, mover pra left
            VariaveisGlobais.atualControllerLabirinto.pecas[0].GetComponent<PieceController>().MovimentarPeca('E', intensidade);
                break;
            case 'T':// Veio de top, mover pra bottom
            VariaveisGlobais.atualControllerLabirinto.pecas[0].GetComponent<PieceController>().MovimentarPeca('B', intensidade);
                break;
            case 'B':// Veio de bottom, mover pra top
            VariaveisGlobais.atualControllerLabirinto.pecas[0].GetComponent<PieceController>().MovimentarPeca('C', intensidade);
                break;
        }
    }
}

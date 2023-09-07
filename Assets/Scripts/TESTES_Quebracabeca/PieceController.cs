using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    public RectTransform posicaoAtualNoGrid;
    public GameObject ponto, ponto2;
    public bool ChegouNoAlvo = false;

    // Start is called before the first frame update
    void Start()
    {
        if (posicaoAtualNoGrid)
            transform.position = posicaoAtualNoGrid.position;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void MovimentarPeca(char direcao)
    {
        MovimentarPeca(direcao, 2);
    }

    public void MovimentarPeca(char direcao, float intensidade)
    {
        MovimentarPeca(direcao,  intensidade, 1);
    }

    public void MovimentarPeca(char direcao, float intensidade, float multArraste)
    {
        //bool bateu = false;
        switch (direcao)
        {
            case 'C':
                // Ver se bate em cima
                if (!(transform.position.y + 0.5f*(transform.GetComponent<RectTransform>().rect.height / 2) > VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.y))
                    StartCoroutine(moverPeca('y', PlayerPrefs.GetInt("Velocidade") * -0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
            case 'B':
                // Ver se bate em baixo
                if (!(transform.position.y - 0.5f*(transform.GetComponent<RectTransform>().rect.height / 2) < VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.y))
                    StartCoroutine(moverPeca('y', PlayerPrefs.GetInt("Velocidade") * 0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
            case 'E':
                // Ver se bate na esquerda
                if (!(transform.position.x - 0.5f*(transform.GetComponent<RectTransform>().rect.width / 2) < VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.x))
                    StartCoroutine(moverPeca('x', PlayerPrefs.GetInt("Velocidade") * -0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
            case 'D':
                // Ver se bate na direita
                if (!(transform.position.x + 0.5f*(transform.GetComponent<RectTransform>().rect.width / 2) > VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.x))
                    StartCoroutine(moverPeca('x', PlayerPrefs.GetInt("Velocidade")*0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
        }
        //controller.VerificarPecaIndividual(this);
    }

    IEnumerator moverPeca(char eixo, float valorInicial, float decrescimo)
    {
        yield return new WaitForEndOfFrame();

        float valorDecrescido = valorInicial;

        switch (eixo.ToString().ToUpper())
        {
            case "X":
                if (valorInicial > 0)
                {
                    while (valorDecrescido > 0)
                    {
                        if (transform.position.x + 0.5f * (transform.GetComponent<RectTransform>().rect.width / 2) > VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.x)
                            valorDecrescido = 0;
                        else
                        {
                            transform.position = new Vector3(transform.position.x + ((VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.x - VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.x) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z); ;
                            valorDecrescido = valorDecrescido - decrescimo;
                            //controller.VerificarPecaIndividual(this);
                            yield return new WaitForEndOfFrame();
                        }                                
                    }
                }
                else
                {
                    while (valorDecrescido < 0)
                    {
                        if (transform.position.x - 0.5f * (transform.GetComponent<RectTransform>().rect.width / 2) < VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.x)
                            valorDecrescido = 0;
                        else
                        {
                            transform.position = new Vector3(transform.position.x + ((VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.x - VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.x) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z); ;
                            valorDecrescido = valorDecrescido + decrescimo;
                            //controller.VerificarPecaIndividual(this);
                            yield return new WaitForEndOfFrame();
                        }
                    }
                }

                break;
            case "Y":
                if (valorInicial > 0)
                {                
                    while (valorDecrescido > 0)
                    {
                        if (transform.position.y - 0.5f * (transform.GetComponent<RectTransform>().rect.height / 2) < VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.y)
                            valorDecrescido = 0;
                        else
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y - ((VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.x - VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.x) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
                            valorDecrescido = valorDecrescido - decrescimo;
                            //controller.VerificarPecaIndividual(this);
                            yield return new WaitForEndOfFrame();
                        }                            
                    }
                }
                else
                {
                    while (valorDecrescido < 0)
                    {
                        if (transform.position.y + 0.5f * (transform.GetComponent<RectTransform>().rect.height / 2) > VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.y)
                            valorDecrescido = 0;
                        else
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y - ((VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.position.x - VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.x) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
                            valorDecrescido = valorDecrescido + decrescimo;
                            //controller.VerificarPecaIndividual(this);
                            yield return new WaitForEndOfFrame();                            
                        }                            
                    }
                }
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlocoImpeditivo : MonoBehaviour
{
    // public Controller_Ativ01_QuebraCabeca controller;
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

    private void OnTriggerEnter2D(Collider2D other) 
    {
        bool colidiu = false;
        // À direita em cima da parede
        if (other.transform.position.x > this.transform.position.x && other.transform.position.y > this.transform.position.y) 
        {
            float tamanhoAlturaPeca = gameObject.GetComponent<RectTransform>().rect.height;
            float distanciaEntrePecas_Vert = ((transform.transform.position.y + (tamanhoAlturaPeca / 2)) - (other.transform.position.y - (tamanhoAlturaPeca / 2)));

            if ((distanciaEntrePecas_Vert > 0.5f* tamanhoAlturaPeca) && (transform.transform.position.y < other.transform.position.y))
            {
                float pontoLatEsq_other = (other.transform.position.x - 0.5f*(other.GetComponent<RectTransform>().rect.width / 2));
                float pontoLatDir_other = (other.transform.position.x + 0.5f * (other.GetComponent<RectTransform>().rect.width / 2));
                float pontoLatEsq_peca = (transform.position.x - 0.5f * (gameObject.GetComponent<RectTransform>().rect.width / 2));
                float pontoLatDir_peca = (transform.position.x + 0.5f * (gameObject.GetComponent<RectTransform>().rect.width / 2));

                if (!(pontoLatDir_peca < pontoLatEsq_other || pontoLatEsq_peca > pontoLatDir_other))
                    colidiu = true;
            }
            Debug.Log("À direita em cima da parede");
        }
        // À direita embaixo da parede
        if (other.transform.position.x > this.transform.position.x && other.transform.position.y < this.transform.position.y) {Debug.Log("À direita embaixo da parede");}
        // À esquerda embaixo da parede
        if (other.transform.position.x < this.transform.position.x && other.transform.position.y < this.transform.position.y) {Debug.Log("À esquerda embaixo da parede");}
        // À esquerda em cima da parede
        if (other.transform.position.x < this.transform.position.x && other.transform.position.y > this.transform.position.y) {Debug.Log("À esquerda em cima da parede");}
    }

    // public void MovimentarPeca(char direcao)
    // {
    //     MovimentarPeca(direcao, 1);
    // }

    // public void MovimentarPeca(char direcao, float intensidade)
    // {
    //     //bool bateu = false;
    //     switch (direcao)
    //     {
    //         case 'C':
    //             // Ver se bate em cima
    //             if (!(transform.position.y + 0.5f*(transform.GetComponent<RectTransform>().rect.height / 2) > controller.primeiraPosGrid.position.y))
    //             {
    //                 // Ver se bate em outra peça
    //                 bool colidiu = false;
    //                 foreach(RectTransform otherPiece in controller.pecas)
    //                 {
    //                     if (otherPiece.name != gameObject.name && otherPiece.GetComponent<BlocoImpeditivo>().enabled)
    //                     {
    //                         float tamanhoAlturaPeca = gameObject.GetComponent<RectTransform>().rect.height;
    //                         float distanciaEntrePecas_Vert = ((transform.position.y + (tamanhoAlturaPeca / 2)) - (otherPiece.position.y - (tamanhoAlturaPeca / 2)));

    //                         if ((distanciaEntrePecas_Vert > 0.5f* tamanhoAlturaPeca) && (transform.position.y < otherPiece.position.y))
    //                         {
    //                             float pontoLatEsq_other = (otherPiece.position.x - 0.5f*(otherPiece.rect.width / 2));
    //                             float pontoLatDir_other = (otherPiece.position.x + 0.5f * (otherPiece.rect.width / 2));
    //                             float pontoLatEsq_peca = (transform.position.x - 0.5f * (gameObject.GetComponent<RectTransform>().rect.width / 2));
    //                             float pontoLatDir_peca = (transform.position.x + 0.5f * (gameObject.GetComponent<RectTransform>().rect.width / 2));

    //                             if (!(pontoLatDir_peca < pontoLatEsq_other || pontoLatEsq_peca > pontoLatDir_other))
    //                                 colidiu = true;
    //                         }
    //                     }
    //                 }

    //                 if (!colidiu)
    //                 {
    //                     //transform.position = new Vector3(transform.position.x, transform.position.y - ((controller.ultimaPosGrid.position.y - controller.primeiraPosGrid.position.y) * 0.075f * (1/controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
    //                     StartCoroutine(moverPeca('y', PlayerPrefs.GetInt("Velocidade") * -0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * 0.0001f));
    //                 }
    //             }
    //             break;
    //         case 'B':
    //             // Ver se bate em baixo
    //             if (!(transform.position.y - 0.5f*(transform.GetComponent<RectTransform>().rect.height / 2) < controller.ultimaPosGrid.position.y))
    //             {
    //                 // Ver se bate em outra peça
    //                 bool colidiu = false;
    //                 foreach (RectTransform otherPiece in controller.pecas)
    //                 {
    //                     if (otherPiece.name != gameObject.name && otherPiece.GetComponent<BlocoImpeditivo>().enabled )
    //                     {
    //                         float tamanhoAlturaPeca = gameObject.GetComponent<RectTransform>().rect.height;
    //                         float distanciaEntrePecas_Vert = ((transform.position.y - (tamanhoAlturaPeca / 2)) - (otherPiece.position.y + (tamanhoAlturaPeca / 2)));

    //                         if ((distanciaEntrePecas_Vert < -0.5f * tamanhoAlturaPeca) && (transform.position.y > otherPiece.position.y))
    //                         {
    //                             float pontoLatEsq_other = (otherPiece.position.x - 0.5f * (otherPiece.rect.width / 2));
    //                             float pontoLatDir_other = (otherPiece.position.x + 0.5f * (otherPiece.rect.width / 2));
    //                             float pontoLatEsq_peca = (transform.position.x - 0.5f * (gameObject.GetComponent<RectTransform>().rect.width / 2));
    //                             float pontoLatDir_peca = (transform.position.x + 0.5f * (gameObject.GetComponent<RectTransform>().rect.width / 2));

    //                             if (!(pontoLatDir_peca < pontoLatEsq_other || pontoLatEsq_peca > pontoLatDir_other))
    //                                 colidiu = true;
    //                         }
    //                     }
    //                 }

    //                 if (!colidiu)
    //                 {
    //                     //transform.position = new Vector3(transform.position.x, transform.position.y + ((controller.ultimaPosGrid.position.y - controller.primeiraPosGrid.position.y) * 0.075f * (1 / controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
    //                     StartCoroutine(moverPeca('y', PlayerPrefs.GetInt("Velocidade") * 0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * 0.0001f));
    //                 }
    //             }
    //             break;
    //         case 'E':
    //             // Ver se bate na esquerda
    //             if (!(transform.position.x - 0.5f*(transform.GetComponent<RectTransform>().rect.width / 2) < controller.primeiraPosGrid.position.x))
    //             {
    //                 // Ver se bate em outra peça
    //                 bool colidiu = false;
    //                 foreach (RectTransform otherPiece in controller.pecas)
    //                 {
    //                     if (otherPiece.name != gameObject.name && otherPiece.GetComponent<BlocoImpeditivo>().enabled)
    //                     {
    //                         float tamanhoLarguraPeca = gameObject.GetComponent<RectTransform>().rect.width;
    //                         float distanciaEntrePecas_Horiz = ((transform.position.x - (tamanhoLarguraPeca / 2)) - (otherPiece.position.x + (tamanhoLarguraPeca / 2)));

    //                         if ((distanciaEntrePecas_Horiz < -0.5f * tamanhoLarguraPeca) && (transform.position.x > otherPiece.position.x))
    //                         {
    //                             float pontoLatCima_other = (otherPiece.position.y + 0.5f * (otherPiece.rect.height / 2));
    //                             float pontoLatBaixo_other = (otherPiece.position.y - 0.5f * (otherPiece.rect.height / 2));
    //                             float pontoLatCima_peca = (transform.position.y + 0.5f * (gameObject.GetComponent<RectTransform>().rect.height / 2));
    //                             float pontoLatBaixo_peca = (transform.position.y - 0.5f * (gameObject.GetComponent<RectTransform>().rect.height / 2));

    //                             if (!(pontoLatBaixo_peca > pontoLatCima_other || pontoLatCima_peca < pontoLatBaixo_other))
    //                                 colidiu = true;
    //                         }
    //                     }
    //                 }

    //                 if (!colidiu)
    //                 {
    //                     //transform.position = new Vector3(transform.position.x - ((controller.ultimaPosGrid.position.x - controller.primeiraPosGrid.position.x) * 0.075f * (1 / controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z);
    //                     StartCoroutine(moverPeca('x', PlayerPrefs.GetInt("Velocidade") * -0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * 0.0001f));
    //                 }
    //             }
    //             break;
    //         case 'D':
    //             // Ver se bate na direita
    //             if (!(transform.position.x + 0.5f*(transform.GetComponent<RectTransform>().rect.width / 2) > controller.ultimaPosGrid.position.x))
    //             {
    //                 // Ver se bate em outra peça
    //                 bool colidiu = false;
    //                 foreach (RectTransform otherPiece in controller.pecas)
    //                 {
    //                     if (otherPiece.name != gameObject.name && otherPiece.GetComponent<BlocoImpeditivo>().enabled)
    //                     {
    //                         float tamanhoLarguraPeca = gameObject.GetComponent<RectTransform>().rect.width;
    //                         float distanciaEntrePecas_Horiz = ((transform.position.x + (tamanhoLarguraPeca / 2)) - (otherPiece.position.x - (tamanhoLarguraPeca / 2)));

    //                         if ((distanciaEntrePecas_Horiz > 0.5f * tamanhoLarguraPeca) && (transform.position.x < otherPiece.position.x))
    //                         {
    //                             float pontoLatCima_other = (otherPiece.position.y + 0.5f * (otherPiece.rect.height / 2));
    //                             float pontoLatBaixo_other = (otherPiece.position.y - 0.5f * (otherPiece.rect.height / 2));
    //                             float pontoLatCima_peca = (transform.position.y + 0.5f * (gameObject.GetComponent<RectTransform>().rect.height / 2));
    //                             float pontoLatBaixo_peca = (transform.position.y - 0.5f * (gameObject.GetComponent<RectTransform>().rect.height / 2));

    //                             if (!(pontoLatBaixo_peca > pontoLatCima_other || pontoLatCima_peca < pontoLatBaixo_other))
    //                                 colidiu = true;
    //                         }
    //                     }
    //                 }

    //                 if (!colidiu)
    //                 {
    //                     //transform.position = new Vector3(transform.position.x + ((controller.ultimaPosGrid.position.x - controller.primeiraPosGrid.position.x) * 0.075f * (1 / controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z); ;
    //                     StartCoroutine(moverPeca('x', PlayerPrefs.GetInt("Velocidade")*0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * 0.0001f));
    //                 }
    //             }
    //             break;
    //     }
    //     controller.VerificarPecaIndividual(this);
    // }

    // IEnumerator moverPeca(char eixo, float valorInicial, float decrescimo)
    // {
    //     yield return new WaitForEndOfFrame();

    //     float valorDecrescido = valorInicial;

    //     switch (eixo.ToString().ToUpper())
    //     {
    //         case "X":
    //             if (valorInicial > 0)
    //             {
    //                 while (valorDecrescido > 0)
    //                 {
    //                     if (transform.position.x + 0.5f * (transform.GetComponent<RectTransform>().rect.width / 2) > controller.ultimaPosGrid.position.x)
    //                         valorDecrescido = 0;
    //                     else
    //                     {
    //                         transform.position = new Vector3(transform.position.x + ((controller.ultimaPosGrid.position.x - controller.primeiraPosGrid.position.x) * valorDecrescido * (1 / controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z); ;
    //                         valorDecrescido = valorDecrescido - decrescimo;
    //                         controller.VerificarPecaIndividual(this);
    //                         yield return new WaitForEndOfFrame();
    //                     }                                
    //                 }
    //             }
    //             else
    //             {
    //                 while (valorDecrescido < 0)
    //                 {
    //                     if (transform.position.x - 0.5f * (transform.GetComponent<RectTransform>().rect.width / 2) < controller.primeiraPosGrid.position.x)
    //                         valorDecrescido = 0;
    //                     else
    //                     {
    //                         transform.position = new Vector3(transform.position.x + ((controller.ultimaPosGrid.position.x - controller.primeiraPosGrid.position.x) * valorDecrescido * (1 / controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z); ;
    //                         valorDecrescido = valorDecrescido + decrescimo;
    //                         controller.VerificarPecaIndividual(this);
    //                         yield return new WaitForEndOfFrame();
    //                     }
    //                 }
    //             }

    //             break;
    //         case "Y":
    //             if (valorInicial > 0)
    //             {                
    //                 while (valorDecrescido > 0)
    //                 {
    //                     if (transform.position.y - 0.5f * (transform.GetComponent<RectTransform>().rect.height / 2) < controller.ultimaPosGrid.position.y)
    //                         valorDecrescido = 0;
    //                     else
    //                     {
    //                         transform.position = new Vector3(transform.position.x, transform.position.y - ((controller.ultimaPosGrid.position.x - controller.primeiraPosGrid.position.x) * valorDecrescido * (1 / controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
    //                         valorDecrescido = valorDecrescido - decrescimo;
    //                         controller.VerificarPecaIndividual(this);
    //                         yield return new WaitForEndOfFrame();
    //                     }                            
    //                 }
    //             }
    //             else
    //             {
    //                 while (valorDecrescido < 0)
    //                 {
    //                     if (transform.position.y + 0.5f * (transform.GetComponent<RectTransform>().rect.height / 2) > controller.primeiraPosGrid.position.y)
    //                         valorDecrescido = 0;
    //                     else
    //                     {
    //                         transform.position = new Vector3(transform.position.x, transform.position.y - ((controller.ultimaPosGrid.position.x - controller.primeiraPosGrid.position.x) * valorDecrescido * (1 / controller.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
    //                         valorDecrescido = valorDecrescido + decrescimo;
    //                         controller.VerificarPecaIndividual(this);
    //                         yield return new WaitForEndOfFrame();                            
    //                     }                            
    //                 }
    //             }
    //             break;
    //     }
    // }
}

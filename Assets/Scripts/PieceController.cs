using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PieceController : MonoBehaviour
{
    private float maxX, maxY, minX, minY;
    public RectTransform posicaoAtualNoGrid;
    public GameObject ponto, ponto2;
    public bool ChegouNoAlvo = false;
    public RectTransform sombra;
    
    private Vector3[] cantosSombra = new Vector3[4];
    private float alturaPiece, larguraPiece;

    int _rotationSpeed = 150;

    Vector3[] cantosPrimeiraPosGrid = new Vector3[4];
    Vector3[] cantosUltimaPosGrid = new Vector3[4];

    private void OnEnable()
    {
        if (posicaoAtualNoGrid)
            transform.position = posicaoAtualNoGrid.position;
        else Invoke("PositionHandGear", 0.5f);

        SetInitialMinMaxValues();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime, 0);
        UpdateMinMaxValues();

        sombra.GetWorldCorners(cantosSombra);
        alturaPiece = Mathf.Abs(cantosSombra[0].y - cantosSombra[1].y);
        larguraPiece = Mathf.Abs(cantosSombra[1].x - cantosSombra[2].x);
    }

    void OnDisable() 
    {
        VariaveisGlobais.maxX = maxX;
        VariaveisGlobais.minX = minX;
        VariaveisGlobais.maxY = maxY;
        VariaveisGlobais.minY = minY;
    }

    private void SetInitialMinMaxValues()
    {
        maxX = transform.position.x;
        minX = transform.position.x;
        maxY = transform.position.y;
        minY = transform.position.y;
    }

    private void UpdateMinMaxValues()
    {
        if (transform.position.x > maxX)
            maxX = transform.position.x;
        else if (transform.position.x < minX)
            minX = transform.position.x;

        if (transform.position.y > maxY)
            maxY = transform.position.y;
        else if (transform.position.y < minY)
            minY = transform.position.y;
    }

    public void MovimentarPeca(char direcao)
    {
        MovimentarPeca(direcao, 1);
    }

    public void MovimentarPeca(char direcao, float intensidade)
    {
        MovimentarPeca(direcao,  intensidade, 0.5f);
    }

    public void MovimentarPeca(char direcao, float intensidade, float multArraste)
    {
        VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.GetWorldCorners(cantosPrimeiraPosGrid);
        VariaveisGlobais.atualControllerLabirinto.ultimaPosGrid.GetWorldCorners(cantosUltimaPosGrid);        

        //bool bateu = false;
        switch (direcao)
        {
            case 'C':
                // Ver se bate em cima
                //if (!(transform.position.y + 0.5f*(alturaPiece / 2) > VariaveisGlobais.atualControllerLabirinto.primeiraPosGrid.position.y))
                if (!(transform.position.y + 0.5f*(alturaPiece / 2) > ((cantosPrimeiraPosGrid[1].y + cantosPrimeiraPosGrid[0].y) / 2)))
                StartCoroutine(moverPeca('y', PlayerPrefs.GetInt("Velocidade") * -0.0001f * intensidade , PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
            case 'B':
                // Ver se bate em baixo
                if (!(transform.position.y - 0.5f*(alturaPiece / 2) < ((cantosUltimaPosGrid[1].y + cantosUltimaPosGrid[0].y) / 2)))
                    StartCoroutine(moverPeca('y', PlayerPrefs.GetInt("Velocidade") * 0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
            case 'E':
                // Ver se bate na esquerda
                if (!(transform.position.x - 0.5f*(larguraPiece / 2) < ((cantosPrimeiraPosGrid[2].x + cantosPrimeiraPosGrid[1].x) / 2)))
                    StartCoroutine(moverPeca('x', PlayerPrefs.GetInt("Velocidade") * -0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
            case 'D':
                // Ver se bate na direita
                if (!(transform.position.x + 0.5f * (larguraPiece / 2) > ((cantosUltimaPosGrid[2].x + cantosUltimaPosGrid[1].x) / 2)))
                    StartCoroutine(moverPeca('x', PlayerPrefs.GetInt("Velocidade") * 0.0001f * intensidade, PlayerPrefs.GetInt("Arraste") * multArraste * 0.0001f));
                break;
        }
        //controller.VerificarPecaIndividual(this);
    }

    IEnumerator moverPeca(char eixo, float valorInicial, float decrescimo)
    {
        //yield return new WaitForEndOfFrame();

        float valorDecrescido = valorInicial * (60/ VariaveisGlobais.CurrentFPS());

        switch (eixo.ToString().ToUpper())
        {
            case "X":
                if (valorInicial > 0)
                {
                    while (valorDecrescido > 0)
                    {
                        if (transform.position.x + 0.5f * (larguraPiece / 2) > ((cantosUltimaPosGrid[2].x + cantosUltimaPosGrid[1].x) / 2))
                        {
                            valorDecrescido = 0;
                        }                            
                        else
                        {
                            transform.position = new Vector3(transform.position.x + ((((cantosUltimaPosGrid[2].x + cantosUltimaPosGrid[1].x) / 2) - ((cantosPrimeiraPosGrid[2].x + cantosPrimeiraPosGrid[1].x) / 2)) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z);
                            valorDecrescido = valorDecrescido - decrescimo * (60 / VariaveisGlobais.CurrentFPS());
                            //controller.VerificarPecaIndividual(this);
                            yield return new WaitForEndOfFrame();
                        }                                
                    }
                }
                else
                {
                    while (valorDecrescido < 0)
                    {
                        if (transform.position.x - 0.5f * (larguraPiece / 2) < ((cantosPrimeiraPosGrid[2].x + cantosPrimeiraPosGrid[1].x)/2))
                            valorDecrescido = 0;
                        else
                        {
                            transform.position = new Vector3(transform.position.x + ((((cantosUltimaPosGrid[2].x + cantosUltimaPosGrid[1].x) / 2) - ((cantosPrimeiraPosGrid[2].x + cantosPrimeiraPosGrid[1].x) / 2)) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.y, transform.position.z); ;
                            valorDecrescido = valorDecrescido + decrescimo * (60 / VariaveisGlobais.CurrentFPS());
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
                        if (transform.position.y - 0.5f * (alturaPiece / 2) < ((cantosUltimaPosGrid[1].y + cantosUltimaPosGrid[0].y)/2))
                            valorDecrescido = 0;
                        else
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y - ((((cantosUltimaPosGrid[2].x + cantosUltimaPosGrid[1].x) / 2) - ((cantosPrimeiraPosGrid[2].x + cantosPrimeiraPosGrid[1].x) / 2)) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
                            valorDecrescido = valorDecrescido - decrescimo * (60 / VariaveisGlobais.CurrentFPS());
                            //controller.VerificarPecaIndividual(this);
                            yield return new WaitForEndOfFrame();
                        }                            
                    }
                }
                else
                {
                    while (valorDecrescido < 0)
                    {
                        if (transform.position.y + 0.5f * (alturaPiece / 2) > ((cantosPrimeiraPosGrid[1].y + cantosPrimeiraPosGrid[0].y)/2))
                            valorDecrescido = 0;
                        else
                        {
                            transform.position = new Vector3(transform.position.x, transform.position.y - ((((cantosUltimaPosGrid[2].x + cantosUltimaPosGrid[1].x) / 2) - ((cantosPrimeiraPosGrid[2].x + cantosPrimeiraPosGrid[1].x) / 2)) * valorDecrescido * (1 / VariaveisGlobais.atualControllerLabirinto.webcamInstance.webcamTexture.requestedFPS)), transform.position.z);
                            valorDecrescido = valorDecrescido + decrescimo * (60 / VariaveisGlobais.CurrentFPS());
                            //controller.VerificarPecaIndividual(this);
                            yield return new WaitForEndOfFrame();                            
                        }                            
                    }
                }
                break;
        }
    }

    private void PositionHandGear() {
        Vector3 startPosition = GameObject.FindGameObjectsWithTag("Start")[0].transform.position;
        transform.position = new Vector3(startPosition.x, startPosition.y, transform.position.z);
    }
}

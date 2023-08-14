using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptAgachaLevanta : MonoBehaviour
{
    public float tempoDeEsperaAlternado;
    public Sprite agachado, levantado;
    float tempoGasto;
    bool jaAgachou;
    public bool iniciaAgachado;

    // Start is called before the first frame update
    void Start()
    {
        if (iniciaAgachado)
            tempoGasto = tempoDeEsperaAlternado * 2;
        else
            tempoGasto = 0;
        jaAgachou = false;
        /*
        if (VariaveisGlobais.VelocidadeDefensores == 1)
            fatorVelocidade = 0.7f;
        else if (VariaveisGlobais.VelocidadeDefensores == 2)
            fatorVelocidade = 1;
        else if (VariaveisGlobais.VelocidadeDefensores == 3)
            fatorVelocidade = 1.3f;
        */
    }

    // Update is called once per frame
    void Update()
    {
        tempoGasto += Time.deltaTime;

        if (VariaveisGlobais.VelocidadeDefensores == 4)
        {
            if(tempoGasto > tempoDeEsperaAlternado)
            {
                tempoGasto = 0;
                if (jaAgachou)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = levantado;
                    gameObject.GetComponent<BoxCollider>().enabled = true;
                    jaAgachou = false;
                }
                else
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = agachado;
                    gameObject.GetComponent<BoxCollider>().enabled = false;
                    jaAgachou = true;
                }
            }

        }

        else if (tempoGasto > (1/(0.4f + (0.3f * VariaveisGlobais.VelocidadeDefensores))))
        {
            tempoGasto = 0;
            if (jaAgachou)
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = levantado;
                gameObject.GetComponent<BoxCollider>().enabled = true;
                jaAgachou = false;
            }
            else
            {
                gameObject.GetComponent<SpriteRenderer>().sprite = agachado;
                gameObject.GetComponent<BoxCollider>().enabled = false;
                jaAgachou = true;
            }
        }
    }
}

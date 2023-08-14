using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class ScriptTempo : MonoBehaviour
{
    int faltamQtosSegundos;
    public AudioSource som321, somGanhou, somPerdeu, somEmpatou;
    public GameObject adversarySPH1, adversarySPH2, adversarySPH3, menino, bola, colClicks, desenhoLancar, textoResultado, placarPenalizacao;
    public Sprite spriteGanhou, spritePerdeu, spriteEmpatou;
    bool rotacao;

    public GameObject webcamMini, grupoButtons, btnSair;

    // Start is called before the first frame update
    void Start()
    {
        faltamQtosSegundos = 3;
        rotacao = true;
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(VariaveisGlobais.tempoParaVoltarALancarAposErrar);
        if (rotacao)
        {
            //if (VariaveisGlobais.TipoMarcacao == "Circular")
            //if (VariaveisGlobais.TipoInterface == "C")
            //if (VariaveisGlobais.TipoMarcacao == "Circular")
            //    transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - Time.deltaTime*360*(1F/(VariaveisGlobais.DuracaoJogo)));
            //if (VariaveisGlobais.TipoMarcacao == "Zona")
            //if (VariaveisGlobais.TipoInterface == ("Z") ||
            //    VariaveisGlobais.TipoInterface == ("A") || VariaveisGlobais.TipoInterface == ("M"))
            //else if (VariaveisGlobais.TipoMarcacao == "Zona")
                transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - Time.deltaTime * 360 * (1F / (VariaveisGlobais.DuracaoJogo)));
        }

        //Debug.Log(transform.rotation.eulerAngles.z);

        // ultimos 3 segundos = 6, 12, 18 graus

        /*if ((transform.rotation.eulerAngles.z < 18 && faltamQtosSegundos == 3) ||
            (transform.rotation.eulerAngles.z < 12 && faltamQtosSegundos == 2) ||
            (transform.rotation.eulerAngles.z < 6 && faltamQtosSegundos == 1)) */

        //if (VariaveisGlobais.TipoMarcacao == "Circular")
       // if (VariaveisGlobais.TipoInterface == "C")
        if (((transform.rotation.eulerAngles.z < (360 * 3F / VariaveisGlobais.DuracaoJogo)) && faltamQtosSegundos == 3) ||
            ((transform.rotation.eulerAngles.z < (360 * 2F / VariaveisGlobais.DuracaoJogo)) && faltamQtosSegundos == 2) ||
            ((transform.rotation.eulerAngles.z < (360F / VariaveisGlobais.DuracaoJogo)) && faltamQtosSegundos == 1))
        {
            //Debug.Log("faltam " + faltamQtosSegundos); 
            faltamQtosSegundos -= 1;
            som321.Play();
        }

        //if (VariaveisGlobais.TipoMarcacao == "Zona")
        /*if (VariaveisGlobais.TipoInterface == "Z")
            if (((transform.rotation.eulerAngles.z < (360 * 3F / int.Parse(VariaveisGlobais.DuracaoJogoZona))) && faltamQtosSegundos == 3) ||
                ((transform.rotation.eulerAngles.z < (360 * 2F / int.Parse(VariaveisGlobais.DuracaoJogoZona))) && faltamQtosSegundos == 2) ||
                ((transform.rotation.eulerAngles.z < (360F / int.Parse(VariaveisGlobais.DuracaoJogoZona))) && faltamQtosSegundos == 1))
            {
                //Debug.Log("faltam " + faltamQtosSegundos); 
                faltamQtosSegundos -= 1;
                som321.Play();
            }
        */


        if ((transform.rotation.eulerAngles.z <= 0 || transform.rotation.eulerAngles.z >300) && faltamQtosSegundos == 0)
        {
            faltamQtosSegundos -= 1;
            adversarySPH1.SetActive(false);
            adversarySPH2.SetActive(false);
            adversarySPH3.SetActive(false);

            if (placarPenalizacao)
                placarPenalizacao.SetActive(false);

            bola.SetActive(false);            
            rotacao = false;
            menino.SetActive(true);
            menino.transform.position = new Vector3(0,1,-8.5f);
            menino.transform.localScale = new Vector3(0.15F, 0.15F, 0);
            menino.GetComponent<SpriteRenderer>().flipX = false;

            colClicks.GetComponent<ScriptLancar>().fimDeJogo = true;
            colClicks.SetActive(false);
            desenhoLancar.SetActive(false);


            textoResultado.SetActive(true);

            if (VariaveisGlobais.ScoreBasquito > VariaveisGlobais.ScoreAdversarios)
            {
                somGanhou.Play();
                
                if (VariaveisGlobais.Idioma == "BR")
                    textoResultado.GetComponent<Text>().text = "Você ganhou!";
                else if (VariaveisGlobais.Idioma == "EN")
                    textoResultado.GetComponent<Text>().text = "You win!";


                menino.GetComponent<SpriteRenderer>().sprite = spriteGanhou;
            }
            //else if (VariaveisGlobais.ScoreBasquito < VariaveisGlobais.ScoreAdversarios)
            //{
            //    somPerdeu.Play();
            //    menino.GetComponent<SpriteRenderer>().sprite = spritePerdeu;
            //}
            else
            {
                somEmpatou.Play();

                if (VariaveisGlobais.Idioma == "BR")
                    textoResultado.GetComponent<Text>().text = "Tente novamente!";
                else if (VariaveisGlobais.Idioma == "EN")
                    textoResultado.GetComponent<Text>().text = "Try again!";

                menino.GetComponent<SpriteRenderer>().sprite = spriteEmpatou;
            }            

            if (webcamMini != null)
                webcamMini.SetActive(false);

            if (grupoButtons != null && btnSair != null)
            {
                grupoButtons.SetActive(true);
                btnSair.SetActive(false);
            }
        }

        VariaveisGlobais.tempoParaVoltarALancarAposErrar = Mathf.Max(0, (VariaveisGlobais.tempoParaVoltarALancarAposErrar - Time.deltaTime));
    }
}

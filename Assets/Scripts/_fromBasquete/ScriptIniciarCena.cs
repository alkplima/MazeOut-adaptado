using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptIniciarCena : MonoBehaviour
{
    public GameObject sphHomem1, sphHomem2, sphHomem3, menino, bola, miniTelas, maxiTelas;
    public bool contagem_regressiva, desbloqueio_do_collider;
    private bool temE, temC, temD;
    public AudioSource som321, somJa;
    public GameObject colliderDaTela, botaolancar, ponteiro_do_placar, cont321, bolaWebcamEsq, bolaWebcamCentro, bolaWebcamDir;
    int counter321;

    // Start is called before the first frame update
    void Start()
    {
        VariaveisGlobais.emProcessoDeProtocolo = false;

        cont321.SetActive(true);
        colliderDaTela.SetActive(false);
        botaolancar.SetActive(false);
        ponteiro_do_placar.SetActive(false);
        contagem_regressiva = true;

        if (VariaveisGlobais.SequenciaProgramada.Length == 0)
        {
            VariaveisGlobais.SequenciaProgramada = "ECD";
            PlayerPrefs.SetString("Sequencia_Programada", "ECD");
        }

        if (VariaveisGlobais.DuracaoJogo == 0)
            VariaveisGlobais.DuracaoJogo = 60;

        if (VariaveisGlobais.DuracaoJogo <= 10)
        {
            VariaveisGlobais.DuracaoJogo = 10;
            PlayerPrefs.SetInt("DUR_Jogo", VariaveisGlobais.DuracaoJogo);
        }

        VariaveisGlobais.tempoParaVoltarALancarAposErrar = 0.5f;

        ScriptAgachaLevanta scrAgacha1, scrAgacha2, scrAgacha3;
        ScriptCercandoCesta scrCercando1, scrCercando2, scrCercando3;

        scrAgacha1 = sphHomem1.GetComponentInChildren<ScriptAgachaLevanta>();
        scrAgacha2 = sphHomem2.GetComponentInChildren<ScriptAgachaLevanta>();
        scrAgacha3 = sphHomem3.GetComponentInChildren<ScriptAgachaLevanta>();

        scrCercando1 = sphHomem1.GetComponent<ScriptCercandoCesta>();
        scrCercando2 = sphHomem2.GetComponent<ScriptCercandoCesta>();
        scrCercando3 = sphHomem3.GetComponent<ScriptCercandoCesta>();

        //if (VariaveisGlobais.TipoInterface == "C")
        if (VariaveisGlobais.TipoMarcacao == "Circular")
        {
            if (VariaveisGlobais.QuantidadeDefensores == 1)
            {
                sphHomem1.transform.position = new Vector3(sphHomem1.transform.position.x, 0, sphHomem1.transform.position.z);
                sphHomem2.transform.position = new Vector3(sphHomem2.transform.position.x, -50, sphHomem2.transform.position.z);
                sphHomem3.transform.position = new Vector3(sphHomem3.transform.position.x, -50, sphHomem3.transform.position.z);
            }
            else if (VariaveisGlobais.QuantidadeDefensores == 2)
            {
                sphHomem1.transform.position = new Vector3(sphHomem1.transform.position.x, 0, sphHomem1.transform.position.z);
                sphHomem2.transform.position = new Vector3(sphHomem2.transform.position.x, 0, sphHomem2.transform.position.z);
                sphHomem3.transform.position = new Vector3(sphHomem3.transform.position.x, -50, sphHomem3.transform.position.z);
            }
            else if (VariaveisGlobais.QuantidadeDefensores == 3)
            {
                sphHomem1.transform.position = new Vector3(sphHomem1.transform.position.x, 0, sphHomem1.transform.position.z);
                sphHomem2.transform.position = new Vector3(sphHomem2.transform.position.x, 0, sphHomem2.transform.position.z);
                sphHomem3.transform.position = new Vector3(sphHomem3.transform.position.x, 0, sphHomem3.transform.position.z);
            }
        }

        if (VariaveisGlobais.TipoMarcacao == "Zona")
        //if (VariaveisGlobais.TipoInterface == ("Z")||
        //    VariaveisGlobais.TipoInterface == ("A")|| VariaveisGlobais.TipoInterface == ("M"))
        {
            if (VariaveisGlobais.QuantidadeDefensores == 1)
            {
                sphHomem1.transform.position = new Vector3(sphHomem1.transform.position.x, 0, sphHomem1.transform.position.z);
                sphHomem2.transform.position = new Vector3(sphHomem2.transform.position.x, -50, sphHomem2.transform.position.z);
                sphHomem3.transform.position = new Vector3(sphHomem3.transform.position.x, -50, sphHomem3.transform.position.z);
            }
            else if (VariaveisGlobais.QuantidadeDefensores == 2)
            {
                sphHomem1.transform.position = new Vector3(sphHomem1.transform.position.x, 0, sphHomem1.transform.position.z);
                sphHomem2.transform.position = new Vector3(sphHomem2.transform.position.x, 0, sphHomem2.transform.position.z);
                sphHomem3.transform.position = new Vector3(sphHomem3.transform.position.x, -50, sphHomem3.transform.position.z);
            }
            else if (VariaveisGlobais.QuantidadeDefensores == 3)
            {
                sphHomem1.transform.position = new Vector3(sphHomem1.transform.position.x, 0, sphHomem1.transform.position.z);
                sphHomem2.transform.position = new Vector3(sphHomem2.transform.position.x, 0, sphHomem2.transform.position.z);
                sphHomem3.transform.position = new Vector3(sphHomem3.transform.position.x, 0, sphHomem3.transform.position.z);
            }
        }

        //if (VariaveisGlobais.TipoInterface == "C")
        if (VariaveisGlobais.TipoMarcacao == "Circular")

        {
            scrAgacha1.enabled = false;
            scrAgacha2.enabled = false;
            scrAgacha3.enabled = false;

            scrCercando1.enabled = true;
            scrCercando2.enabled = true;
            scrCercando3.enabled = true;

            // VELOCIDADE

            if (VariaveisGlobais.VelocidadeDefensores == 1)
            {
                sphHomem1.GetComponent<ScriptCercandoCesta>().velocidade = 1F;
                sphHomem2.GetComponent<ScriptCercandoCesta>().velocidade = 1F;
                sphHomem3.GetComponent<ScriptCercandoCesta>().velocidade = 1F;
            }
            else if (VariaveisGlobais.VelocidadeDefensores == 2)
            {
                sphHomem1.GetComponent<ScriptCercandoCesta>().velocidade = 2;
                sphHomem2.GetComponent<ScriptCercandoCesta>().velocidade = 2F;
                sphHomem3.GetComponent<ScriptCercandoCesta>().velocidade = 2F;
            }
            else if (VariaveisGlobais.VelocidadeDefensores == 3)
            {
                sphHomem1.GetComponent<ScriptCercandoCesta>().velocidade = 3F;
                sphHomem2.GetComponent<ScriptCercandoCesta>().velocidade = 3F;
                sphHomem3.GetComponent<ScriptCercandoCesta>().velocidade = 3F;
            }
            else if (VariaveisGlobais.VelocidadeDefensores == 4) //ALTERNADA
            {
                sphHomem1.GetComponent<ScriptCercandoCesta>().velocidade = 1F;
                sphHomem2.GetComponent<ScriptCercandoCesta>().velocidade = 2F;
                sphHomem3.GetComponent<ScriptCercandoCesta>().velocidade = 3F;
            }

        }
        else if (VariaveisGlobais.TipoMarcacao == "Zona") //(VariaveisGlobais.TipoInterface == ("Z") ||
                                                               //VariaveisGlobais.TipoInterface == ("A") || VariaveisGlobais.TipoInterface == ("M"))
        {
            scrAgacha1.enabled = true;
            scrAgacha2.enabled = true;
            scrAgacha3.enabled = true;

            scrCercando1.enabled = false;
            scrCercando2.enabled = false;
            scrCercando3.enabled = false;

            sphHomem1.transform.rotation = Quaternion.Euler(sphHomem1.transform.rotation.eulerAngles.x, 17, sphHomem1.transform.rotation.eulerAngles.z);
            sphHomem2.transform.rotation = Quaternion.Euler(sphHomem1.transform.rotation.eulerAngles.x, 0, sphHomem1.transform.rotation.eulerAngles.z);
            sphHomem3.transform.rotation = Quaternion.Euler(sphHomem1.transform.rotation.eulerAngles.x, -17, sphHomem1.transform.rotation.eulerAngles.z);

            sphHomem1.transform.localScale = new Vector3(10, 10, 10);
            sphHomem2.transform.localScale = new Vector3(8, 8, 8);
            sphHomem3.transform.localScale = new Vector3(6, 6, 10);
         }

        if (VariaveisGlobais.TipoInterface == "Mouse")         //if (VariaveisGlobais.TipoInterface == ("Z") || VariaveisGlobais.TipoInterface == ("C"))
            counter321 = 3;
        else counter321 = 5;

        cont321.GetComponent<TextMesh>().text = ""+(counter321);

        VariaveisGlobais.PosicaoCorrenteSequencia = 0;
        //VariaveisGlobais.PosicaoJogada_Zona = VariaveisGlobais.SequenciaProgramada
        //                                      [VariaveisGlobais.PosicaoCorrenteSequencia].ToString();

        VariaveisGlobais.PosicaoJogada = VariaveisGlobais.SequenciaProgramada
                                         [VariaveisGlobais.PosicaoCorrenteSequencia].ToString();

        //switch (VariaveisGlobais.PosicaoJogada_Zona)
        switch (VariaveisGlobais.PosicaoJogada)
        {
            case "E":
                menino.transform.position = new Vector3(-2.1F, 0F, -6.1F);
                bola.transform.position = new Vector3(-1.923F, 0.611F, -5.762F);
                menino.GetComponent<SpriteRenderer>().flipX = false;

                botaolancar.GetComponent<RectTransform>().anchorMin.Set(0.7f, 0);
                botaolancar.GetComponent<RectTransform>().anchorMax.Set(1, 0.2f);

                bola.GetComponent<ScriptMovimentarBola>().v_xis = 80;
                bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                //if (VariaveisGlobais.TipoInterface == ("M"))
                if (VariaveisGlobais.TipoInterface == ("Webcam2"))
                {
                    bolaWebcamEsq.SetActive(true);
                    bolaWebcamCentro.SetActive(false);
                    bolaWebcamDir.SetActive(false);
                }
                break;
            case "C":
                menino.transform.position = new Vector3(-0.3F, 0F, -7F);
                bola.transform.position = new Vector3(0, 0.6F, -6.6F);
                menino.GetComponent<SpriteRenderer>().flipX = false;

                botaolancar.GetComponent<RectTransform>().anchorMin.Set(0.7f, 0);
                botaolancar.GetComponent<RectTransform>().anchorMax.Set(1, 0.2f);

                bola.GetComponent<ScriptMovimentarBola>().v_xis = 0;
                bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                //if (VariaveisGlobais.TipoInterface == ("M"))
                if (VariaveisGlobais.TipoInterface == ("Webcam2"))
                {
                    bolaWebcamEsq.SetActive(false);
                    bolaWebcamCentro.SetActive(true);
                    bolaWebcamDir.SetActive(false);
                }
                break;
            case "D":
                menino.transform.position = new Vector3(2.1F, 0F, -6.1F);
                bola.transform.position = new Vector3(1.923F, 0.611F, -5.762F);
                menino.GetComponent<SpriteRenderer>().flipX = true;
                
                botaolancar.GetComponent<RectTransform>().anchorMin.Set(0, 0);
                botaolancar.GetComponent<RectTransform>().anchorMax.Set(0.3f, 0.2f);

                bola.GetComponent<ScriptMovimentarBola>().v_xis = -80;
                bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                //if (VariaveisGlobais.TipoInterface == ("M"))
                if (VariaveisGlobais.TipoInterface == ("Webcam2"))
                {
                    bolaWebcamEsq.SetActive(false);
                    bolaWebcamCentro.SetActive(false);
                    bolaWebcamDir.SetActive(true);
                }
                break;
        }

        //if (VariaveisGlobais.TipoInterface == ("A"))
        if (VariaveisGlobais.TipoInterface == ("Webcam1"))
            miniTelas.SetActive(true);

        //if (VariaveisGlobais.TipoInterface == ("M"))
        if (VariaveisGlobais.TipoInterface == ("Webcam2"))
            maxiTelas.SetActive(true);

        // Ver se a sequencia contempla as tres letras e desativar os adversarios;
        temE = false; temC = false; temD = false;

        if (VariaveisGlobais.SequenciaProgramada.ToUpper().IndexOf('E') >= 0)
            temE = true;
        if (VariaveisGlobais.SequenciaProgramada.ToUpper().IndexOf('C') >= 0)
            temC = true;
        if (VariaveisGlobais.SequenciaProgramada.ToUpper().IndexOf('D') >= 0)
            temD = true;

        if (VariaveisGlobais.TipoMarcacao == "Zona")
        {
            sphHomem1.SetActive(temE);
            sphHomem2.SetActive(temC);
            sphHomem3.SetActive(temD);
        }

        // FIM - Ver se a sequencia contempla as tres letras e desativar os adversarios;

    }

    // Update is called once per frame
    void Update()
    {
        if (contagem_regressiva)
        {
            Vector3 vetor321 = cont321.transform.position;
            vetor321.z = cont321.transform.position.z + (Time.deltaTime * 2);
            cont321.transform.position = vetor321;

            if ((cont321.transform.position.z) > -6)
            {
                counter321 -= 1;
                vetor321 = cont321.transform.position;
                vetor321.z = -8;
                cont321.transform.position = vetor321;
                cont321.GetComponent<TextMesh>().text = "" + (counter321);
            }

            if ((cont321.transform.position.z) == -8 && (counter321 > 0))
                som321.Play();

            if (counter321 == 0)
            {

                if (VariaveisGlobais.Idioma == "BR")
                    cont321.GetComponent<TextMesh>().text = ("Vai!");
                else if (VariaveisGlobais.Idioma == "EN")
                    cont321.GetComponent<TextMesh>().text = ("Go!");
                else
                    cont321.GetComponent<TextMesh>().text = ("!!!");

                VariaveisGlobais.DateTime_InicioPartida = VariaveisGlobais.DateTime_Full = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 

                if (desbloqueio_do_collider == false)
                {
                    colliderDaTela.SetActive(true);
                    botaolancar.SetActive(true);
                    ponteiro_do_placar.SetActive(true);
                }
                desbloqueio_do_collider = true;
                //VariaveisGlobais.FirstShot = true;
            }

            if ((cont321.transform.position.z) == -8 && (counter321 == 0))
                somJa.Play();

            if (counter321 == -1)
            {
                contagem_regressiva = false;
                cont321.SetActive(false);
            }
        }
    }
}

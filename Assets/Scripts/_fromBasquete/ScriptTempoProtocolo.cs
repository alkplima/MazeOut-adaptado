using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptTempoProtocolo : MonoBehaviour {
    // Tempo no protocolo
    // Total: 585 segundos
    // 45 segundos na primeira fase
    // 3 minutos em cada uma das 3 fases subsequentes

    // PROTOCOLO 18-05-2021
    // 45 SEGUNDOS DE ECD
    // 3 MINUTOS DE EEEEECCCCCDDDDD
    // 3 MINUTOS DE DDDDDEEEEECCCCC
    // 3 MINUTOS DE DCE
    // Velocidade baixa

    int faltamQtosSegundos, etapaAtual;
    public AudioSource som321, somGanhou, somTransicaoDeEtapa, somTenteNovamente;
    public GameObject adversarySPH1, adversarySPH2, adversarySPH3, menino, bola, colClicks, desenhoLancar, bolaWebcamEsq, bolaWebcamCentro, bolaWebcamDir, placarPenalizacao;
    public Sprite spriteFimDoProtocolo, spriteTenteNovamente;
    bool rotacao;

    // Start is called before the first frame update
    void Start()
    {
        faltamQtosSegundos = 3;
        etapaAtual = 1;
        rotacao = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (rotacao)
            transform.rotation = Quaternion.Euler(0, 0, transform.rotation.eulerAngles.z - Time.deltaTime * 360 * (1F / (VariaveisGlobais.DuracaoJogo)));

        // ETAPA 1 para 2
        if ((transform.rotation.eulerAngles.z < (360 * (180 + 180 + 180) / VariaveisGlobais.DuracaoJogo))&&etapaAtual==1)
        {
            etapaAtual = 2;
            VariaveisGlobais.SequenciaProgramada = "EEEEECCCCCDDDDD";
            ReposicionarJogadorBola();
        }

        // ETAPA 2 para 3
        if ((transform.rotation.eulerAngles.z < (360 * (180 + 180) / VariaveisGlobais.DuracaoJogo))&&etapaAtual==2)
        {
            etapaAtual = 3;
            VariaveisGlobais.SequenciaProgramada = "DDDDDEEEEECCCCC";
            ReposicionarJogadorBola();
        }

        // ETAPA 3 para 4
        if ((transform.rotation.eulerAngles.z < (360 * 180/ VariaveisGlobais.DuracaoJogo))&&etapaAtual==3)
        {
            etapaAtual = 4;
            VariaveisGlobais.SequenciaProgramada = "DCE";
            ReposicionarJogadorBola();
        }

        if (((transform.rotation.eulerAngles.z < (360 * 3F / VariaveisGlobais.DuracaoJogo)) && faltamQtosSegundos == 3) ||
            ((transform.rotation.eulerAngles.z < (360 * 2F / VariaveisGlobais.DuracaoJogo)) && faltamQtosSegundos == 2) ||
            ((transform.rotation.eulerAngles.z < (360F / VariaveisGlobais.DuracaoJogo)) && faltamQtosSegundos == 1))
        {
            faltamQtosSegundos -= 1;
            som321.Play();
        }

        if ((transform.rotation.eulerAngles.z <= 0 || transform.rotation.eulerAngles.z > 300) && faltamQtosSegundos == 0)
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
            menino.transform.position = new Vector3(0, 1, -8.5f);
            menino.transform.localScale = new Vector3(0.15F, 0.15F, 0);
            menino.GetComponent<SpriteRenderer>().flipX = false;
            colClicks.SetActive(true);
            desenhoLancar.SetActive(false);

            if (VariaveisGlobais.ScoreBasquito>VariaveisGlobais.ScoreAdversarios)
            {
                somGanhou.Play();
                menino.GetComponent<SpriteRenderer>().sprite = spriteFimDoProtocolo;
            }
            else
            {
                somTenteNovamente.Play();
                menino.GetComponent<SpriteRenderer>().sprite = spriteTenteNovamente;
            }
            colClicks.GetComponent<ScriptLancar>().fimDeJogo = true;
        }
        VariaveisGlobais.tempoParaVoltarALancarAposErrar = Mathf.Max(0, (VariaveisGlobais.tempoParaVoltarALancarAposErrar - Time.deltaTime));
    }

    void ReposicionarJogadorBola()
    {
        VariaveisGlobais.PosicaoCorrenteSequencia = 0;
        VariaveisGlobais.PosicaoJogada = VariaveisGlobais.SequenciaProgramada
                                     [VariaveisGlobais.PosicaoCorrenteSequencia].ToString();
        Rigidbody rb = bola.GetComponent<Rigidbody>();

        rb.useGravity = false;
        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        colClicks.SetActive(true);
        desenhoLancar.SetActive(true);

        bolaWebcamEsq.SetActive(false);
        bolaWebcamCentro.SetActive(false);
        bolaWebcamDir.SetActive(false);

        switch (VariaveisGlobais.PosicaoJogada)
        {
            case "E":
                menino.transform.position = new Vector3(-2.1F, 0F, -6.1F);
                bola.transform.position = new Vector3(-1.923F, 0.611F, -5.762F);
                menino.GetComponent<SpriteRenderer>().flipX = false;
                bola.GetComponent<ScriptMovimentarBola>().v_xis = 80;
                bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                bolaWebcamEsq.SetActive(true);
                break;
            case "C":
                menino.transform.position = new Vector3(-0.3F, 0F, -7F);
                bola.transform.position = new Vector3(0, 0.6F, -6.6F);
                menino.GetComponent<SpriteRenderer>().flipX = false;
                bola.GetComponent<ScriptMovimentarBola>().v_xis = 0;
                bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                bolaWebcamCentro.SetActive(true);
                break;
            case "D":
                menino.transform.position = new Vector3(2.1F, 0F, -6.1F);
                bola.transform.position = new Vector3(1.923F, 0.611F, -5.762F);
                menino.GetComponent<SpriteRenderer>().flipX = true;
                bola.GetComponent<ScriptMovimentarBola>().v_xis = -80;
                bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                bolaWebcamDir.SetActive(true);
                break;
        }
        somTransicaoDeEtapa.Play();
    }
}

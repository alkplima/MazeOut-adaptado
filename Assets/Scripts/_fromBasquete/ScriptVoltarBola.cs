using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ScriptVoltarBola : MonoBehaviour
{
    public bool Ativar;
    public Rigidbody rb;
    public GameObject botaoLancar, menino, bola, bolaWebcamEsq, bolaWebcamCentro, bolaWebcamDir, colliderDaTela;
    [HideInInspector]
    public string tipoLance_voltar;
    public ScriptPenalizacao penalizacaoControl;

    // Start is called before the first frame update
    void Start()
    {
        Ativar = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Ativar == true)
        {
            Ativar = false;
            rb.useGravity = false;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            colliderDaTela.SetActive(true);
            botaoLancar.SetActive(true);

            bolaWebcamEsq.SetActive(false);
            bolaWebcamCentro.SetActive(false);
            bolaWebcamDir.SetActive(false);

            //switch (VariaveisGlobais.PosicaoJogada_Zona)
            switch (VariaveisGlobais.PosicaoJogada)
            {
                case "E":
                    menino.transform.position = new Vector3(-2.1F, 0F, -6.1F);
                    bola.transform.position = new Vector3(-1.923F, 0.611F, -5.762F);
                    menino.GetComponent<SpriteRenderer>().flipX = false;
                    botaoLancar.GetComponent<RectTransform>().anchorMin.Set(0.7f, 0);
                    botaoLancar.GetComponent<RectTransform>().anchorMax.Set(1, 0.2f);
                    bola.GetComponent<ScriptMovimentarBola>().v_xis = 80;
                    bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                    bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                    //if (VariaveisGlobais.TipoInterface == ("M"))
                    if ((VariaveisGlobais.TipoInterface == ("Webcam2"))|| (VariaveisGlobais.TipoInterface == ("Webcam1")))
                        bolaWebcamEsq.SetActive(true);
                    break;
                case "C":
                    menino.transform.position = new Vector3(-0.3F, 0F, -7F);
                    bola.transform.position = new Vector3(0, 0.6F, -6.6F);
                    menino.GetComponent<SpriteRenderer>().flipX = false;
                    botaoLancar.GetComponent<RectTransform>().anchorMin.Set(0.7f, 0);
                    botaoLancar.GetComponent<RectTransform>().anchorMax.Set(1, 0.2f);
                    bola.GetComponent<ScriptMovimentarBola>().v_xis = 0;
                    bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                    bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                    //if (VariaveisGlobais.TipoInterface == ("M"))
                    if ((VariaveisGlobais.TipoInterface == ("Webcam2")) || (VariaveisGlobais.TipoInterface == ("Webcam1")))
                        bolaWebcamCentro.SetActive(true);
                    break;
                case "D":
                    menino.transform.position = new Vector3(2.1F, 0F, -6.1F);
                    bola.transform.position = new Vector3(1.923F, 0.611F, -5.762F);
                    menino.GetComponent<SpriteRenderer>().flipX = true;
                    botaoLancar.GetComponent<RectTransform>().anchorMin.Set(0, 0);
                    botaoLancar.GetComponent<RectTransform>().anchorMax.Set(0.3f, 0.2f);
                    bola.GetComponent<ScriptMovimentarBola>().v_xis = -80;
                    bola.GetComponent<ScriptMovimentarBola>().v_yps = 440;
                    bola.GetComponent<ScriptMovimentarBola>().v_zeh = 290;
                    //if (VariaveisGlobais.TipoInterface == ("M"))
                    if ((VariaveisGlobais.TipoInterface == ("Webcam2")) || (VariaveisGlobais.TipoInterface == ("Webcam1")))
                        bolaWebcamDir.SetActive(true);
                    break;
            }

            if (VariaveisGlobais.ScoreBasquito + VariaveisGlobais.ScoreAdversarios > 1)
                VariaveisGlobais.FirstShot = false;
            else VariaveisGlobais.FirstShot = true;


            int idProtocolo, idPartidaProtocolo;
            if (VariaveisGlobais.emProcessoDeProtocolo)
            {
                idProtocolo = VariaveisGlobais.protocoloCorrente + 1;
                idPartidaProtocolo = VariaveisGlobais.partidaProtocoloCorrente + 1;
            }
            else
            {
                idProtocolo = 0;
                idPartidaProtocolo = 0;
            }

            if (VariaveisGlobais.estaNaPesquisa)
            {
                ItemEventoDB itemNovo = new ItemEventoDB
                {
                    NumJogada = (VariaveisGlobais.ScoreBasquito + VariaveisGlobais.ScoreAdversarios),
                    TipoLance = tipoLance_voltar,
                    DateTimeInicioPartida = VariaveisGlobais.DateTime_InicioPartida,
                    DateTime = VariaveisGlobais.DateTime_Full,
                    Device_ID = VariaveisGlobais.Device_ID,
                    Player_ID = VariaveisGlobais.IdentificadorNome,
                    BasquetebolKid_Version = Application.version,
                    OperationalSystem = VariaveisGlobais.OperationalSystem,
                    Enemies = VariaveisGlobais.QuantidadeDefensores,
                    Enemy1_Pos = VariaveisGlobais.Enemy1_Pos,
                    Enemy2_Pos = VariaveisGlobais.Enemy2_Pos,
                    Enemy3_Pos = VariaveisGlobais.Enemy3_Pos,
                    ScreenSize_X = VariaveisGlobais.ScreenSize_X,
                    ScreenSize_Y = VariaveisGlobais.ScreenSize_Y,
                    Touch_X = VariaveisGlobais.Touch_X,
                    Touch_Y = VariaveisGlobais.Touch_Y,
                    Velocity = VariaveisGlobais.VelocidadeDefensores,
                    HitTarget = VariaveisGlobais.HitTarget,
                    ScoreBasquito = VariaveisGlobais.ScoreBasquito,
                    ScoreAdversarios = VariaveisGlobais.ScoreAdversarios,
                    TotalTimeOfTheMatch = VariaveisGlobais.DuracaoJogo,
                    RemainingTime = VariaveisGlobais.TempoRestante,
                    RemainingTimeAtDouble = VariaveisGlobais.tempoRestanteEmDouble,
                    //TypeOfMatch = VariaveisGlobais.TipoInterface,
                    InterfaceOfMatch = VariaveisGlobais.TipoInterface,
                    MarkingOfMatch = VariaveisGlobais.TipoMarcacao,
                    Position_E_C_D = VariaveisGlobais.PosicaoJogadaASerRegistrada,
                    FirstShot = VariaveisGlobais.FirstShot,
                    IDProtocolo = idProtocolo,
                    IDPartidaProtocolo = idPartidaProtocolo
                };

                Array.Resize(ref VariaveisGlobais.itensRelatorio, VariaveisGlobais.itensRelatorio.Length + 1);

                VariaveisGlobais.itensRelatorio[VariaveisGlobais.itensRelatorio.Length - 1] = itemNovo;
            }


            if (penalizacaoControl)
                if (penalizacaoControl.isActiveAndEnabled)
                    penalizacaoControl.sustar = false;
            
        }
   }
}

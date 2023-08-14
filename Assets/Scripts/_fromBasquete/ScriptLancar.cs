using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.SceneManagement;
using System.Globalization;

public class ScriptLancar : MonoBehaviour
{
    public GameObject bolabola;
    public ScriptMovimentarBola scr_movbola;
    string stringMouse;
    Vector3 posicaoMouse;
    public Transform inimigo1_trs, inimigo2_trs, inimigo3_trs;
    public bool fimDeJogo = false, fimDaMusicaDeFimDeJogo = true;
    public GameObject relogio, botaoLancar;
    public string NomeCenaMenu = "MenuScene";
    public ScriptPenalizacao penalizacaoControl;

    // Start is called before the first frame update
    void Start()
    {
        fimDeJogo = false;
    }

    public void Lancamento()
    {
        if (fimDeJogo)
        {
            if (relogio.GetComponent<ScriptTempo>() != null)
            {
                if ((!relogio.GetComponent<ScriptTempo>().somGanhou.GetComponent<AudioSource>().isPlaying)
                    && (!relogio.GetComponent<ScriptTempo>().somEmpatou.GetComponent<AudioSource>().isPlaying)
                    && (!relogio.GetComponent<ScriptTempo>().somPerdeu.GetComponent<AudioSource>().isPlaying))
                {


                    fimDeJogo = false;
                    //SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    SceneManager.LoadScene(NomeCenaMenu);
                }
            }
            else
            {
                fimDeJogo = false;
                SceneManager.LoadScene(NomeCenaMenu);
            }
        }
        else if ((botaoLancar.activeSelf == true)&&VariaveisGlobais.tempoParaVoltarALancarAposErrar == 0) 
        {
            bolabola.SetActive(true);

            posicaoMouse = Input.mousePosition;

            VariaveisGlobais.Device_ID = SystemInfo.deviceUniqueIdentifier;
            VariaveisGlobais.ScreenSize_X = Screen.width;
            VariaveisGlobais.ScreenSize_Y = Screen.height;
            VariaveisGlobais.OperationalSystem = SystemInfo.operatingSystem;

            //VariaveisGlobais.DateTime_Full = System.DateTime.Now.ToString("dd'/'MM'/'yyyy' 'HH':'mm':'ss");// System.DateTime.Now.ToString("dd'-'MM'-'yyyy' 'HH'h 'mm'm 'ss's'");
            VariaveisGlobais.DateTime_Full = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); 

            //Posições
            if ((int)inimigo1_trs.rotation.eulerAngles.y < 100)
                VariaveisGlobais.Enemy1_Pos = (int)inimigo1_trs.rotation.eulerAngles.y;
            else
                VariaveisGlobais.Enemy1_Pos = (int)(inimigo1_trs.rotation.eulerAngles.y - 360);

            if ((int)inimigo2_trs.rotation.eulerAngles.y < 100)
                VariaveisGlobais.Enemy2_Pos = (int)inimigo2_trs.rotation.eulerAngles.y;
            else
                VariaveisGlobais.Enemy2_Pos = (int)(inimigo2_trs.rotation.eulerAngles.y - 360);

            if ((int)inimigo3_trs.rotation.eulerAngles.y < 100)
                VariaveisGlobais.Enemy3_Pos = (int)inimigo3_trs.rotation.eulerAngles.y;
            else
                VariaveisGlobais.Enemy3_Pos = (int)(inimigo3_trs.rotation.eulerAngles.y - 360);

            VariaveisGlobais.Touch_X = (int)posicaoMouse.x;
            VariaveisGlobais.Touch_Y = (int)posicaoMouse.y;

            VariaveisGlobais.TempoRestante = (relogio.transform.rotation.eulerAngles.z / 360 * VariaveisGlobais.DuracaoJogo).ToString("00.00", CultureInfo.InvariantCulture);
           
            VariaveisGlobais.tempoRestanteEmDouble = (relogio.transform.rotation.eulerAngles.z / 360 * VariaveisGlobais.DuracaoJogo);


            //if (VariaveisGlobais.TipoInterface == ("Z") ||
            //    VariaveisGlobais.TipoInterface == ("A") || VariaveisGlobais.TipoInterface == ("M"))
            //{
                //VariaveisGlobais.PosicaoJogadaASerRegistrada = VariaveisGlobais.PosicaoJogada_Zona;
                VariaveisGlobais.PosicaoJogadaASerRegistrada = VariaveisGlobais.PosicaoJogada;

                if ((VariaveisGlobais.PosicaoCorrenteSequencia + 1) == VariaveisGlobais.SequenciaProgramada.Length)
                    VariaveisGlobais.PosicaoCorrenteSequencia = 0;
                else
                    VariaveisGlobais.PosicaoCorrenteSequencia += 1;

                //VariaveisGlobais.PosicaoJogada_Zona =
                VariaveisGlobais.PosicaoJogada =
                    VariaveisGlobais.SequenciaProgramada[VariaveisGlobais.PosicaoCorrenteSequencia].ToString();
            //}


            bolabola.GetComponent<ScriptMovimentarBola>().lançamento = true;
            scr_movbola.lançamento = true;
            botaoLancar.SetActive(false);

            if (penalizacaoControl)
                if (penalizacaoControl.isActiveAndEnabled)
                    penalizacaoControl.sustar = true;
        }
    }

    private void OnMouseUp()
    {
        Lancamento();
    }
    
}

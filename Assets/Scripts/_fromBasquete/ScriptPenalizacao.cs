using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPenalizacao : MonoBehaviour {

    [HideInInspector]
    //public bool penalizar = false;
    public bool sustar = false;
    public AudioSource aus;
    public ScriptVoltarBola scr_volta;
    public ScriptPlacar scr_placar;
    public Text scr_texto_tempoRestante;

    public float tempoRestante;

    private void Update()
    {
        if (sustar)
        {
            tempoRestante = VariaveisGlobais.tempoParaPenalizacao + VariaveisGlobais.tempoParaVoltarALancarAposErrar;
            scr_texto_tempoRestante.text = "";
        }
        else
        {
            tempoRestante -= Time.deltaTime;
            scr_texto_tempoRestante.text = (Mathf.Min(VariaveisGlobais.tempoParaPenalizacao,Mathf.CeilToInt(tempoRestante))).ToString();
        }

        if (tempoRestante <= 0)
        {
            sustar = false;
            aus.Play();
            VariaveisGlobais.HitTarget = false;
            VariaveisGlobais.ScoreAdversarios += 1;
            scr_placar.atualizarPlacar = true;
            scr_volta.tipoLance_voltar = "Penalizacao";
            scr_volta.Ativar = true;
            VariaveisGlobais.tempoParaVoltarALancarAposErrar = 0.5f;
            tempoRestante = VariaveisGlobais.tempoParaPenalizacao + VariaveisGlobais.tempoParaVoltarALancarAposErrar;
        }
    }
}

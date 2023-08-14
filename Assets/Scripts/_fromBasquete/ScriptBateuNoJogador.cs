using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBateuNoJogador : MonoBehaviour
{
    public AudioSource aus;
    public ScriptVoltarBola scr_volta;
    public ScriptPlacar scr_placar;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag =="Bola")
        {
            aus.Play();
            VariaveisGlobais.HitTarget = false;
            VariaveisGlobais.ScoreAdversarios += 1;
            scr_placar.atualizarPlacar = true;
            scr_volta.tipoLance_voltar = "Arremesso";
            scr_volta.Ativar = true;
            VariaveisGlobais.tempoParaVoltarALancarAposErrar = 0.5f;
        }

    }
}

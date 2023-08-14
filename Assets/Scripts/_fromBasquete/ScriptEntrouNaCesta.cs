using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptEntrouNaCesta : MonoBehaviour
{
    public AudioSource aus;
    public ScriptVoltarBola scr_volta;
    public ScriptPlacar scr_placar;
    public ScriptBrilhos scr_brilhos;

    private void OnTriggerEnter(Collider other)
    {
        aus.Play();
        VariaveisGlobais.HitTarget = true;
        VariaveisGlobais.ScoreBasquito += 1; 
        scr_placar.atualizarPlacar = true;
        scr_volta.tipoLance_voltar = "Arremesso";
        scr_volta.Ativar = true;
        scr_brilhos.Ativar = true;
    }
}


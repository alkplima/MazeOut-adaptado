using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScriptPlacar : MonoBehaviour
{
    public bool atualizarPlacar, reset, placarBasquito, placarAdversarios;
    public Text textPlacar;
    // Start is called before the first frame update
    void Start()
    {
        VariaveisGlobais.ScoreBasquito = 0;
        VariaveisGlobais.ScoreAdversarios = 0;
        reset = false;
        atualizarPlacar = false;
        //tm = GetComponent<TextMesh>();
    }

    // Update is called once per frame
    void Update()
    {
        if (atualizarPlacar == true)
        {
            atualizarPlacar = false;
            AtualizarPlacar();
        }
        if (reset == true)
        {
            reset = false;
            if (name=="PlacarBasquito")
                VariaveisGlobais.ScoreBasquito = 0;
            else if (name == "PlacarAdversarios")
                VariaveisGlobais.ScoreAdversarios = 0;

            AtualizarPlacar();
        }
    }

    void AtualizarPlacar()
    {
        if (placarBasquito)
            textPlacar.text = ""+VariaveisGlobais.ScoreBasquito;
        else if (placarAdversarios)
            textPlacar.text = ""+VariaveisGlobais.ScoreAdversarios;
    }
}

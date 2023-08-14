using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuxiliarMudarEspelhamento : MonoBehaviour
{
    public bool Horizontal, Vertical;

    public void Mudar()
    {
        if (Horizontal)
            if (VariaveisGlobais.Webcam_espelhar_H)
                VariaveisGlobais.Webcam_espelhar_H = false;
            else VariaveisGlobais.Webcam_espelhar_H = true;

        if (Vertical)
            if (VariaveisGlobais.Webcam_espelhar_V)
                VariaveisGlobais.Webcam_espelhar_V = false;
            else VariaveisGlobais.Webcam_espelhar_V = true;
    }



}

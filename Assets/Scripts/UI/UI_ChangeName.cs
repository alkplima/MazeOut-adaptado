using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ChangeName : MonoBehaviour
{
    public void ChangeName(string name)
    {
        VariaveisGlobais.nomePaciente = name;
    }
}

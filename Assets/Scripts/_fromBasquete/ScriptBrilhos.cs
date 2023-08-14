using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptBrilhos : MonoBehaviour
{
    public bool Ativar;
    ParticleSystem part_sys;
    // Start is called before the first frame update
    void Start()
    {
        Ativar = false;
        part_sys = GetComponent<ParticleSystem>();        
    }

    // Update is called once per frame
    void Update()
    {
        if (Ativar == true)
        {
            Ativar = false;
            part_sys.Play(false);
        }
        
    }
}

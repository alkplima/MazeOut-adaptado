using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadSceneBehaviour : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void Restart()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name);
    }

    public void LoadSceneByName(string name)
    {
        /*if (name=="PrincipalSceneWithWebcam")
        {
            VariaveisGlobais.TipoMarcacao = "Zona";
            VariaveisGlobais.QuantidadeDefensoresZona = 3;
            VariaveisGlobais.SequenciaProgramada = "ECD";
            VariaveisGlobais.DuracaoJogoZona = "60";
        }
        */
            UnityEngine.SceneManagement.SceneManager.LoadScene(name);
    }

    public void LoadSceneByIndex(int index)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(index);
    }
}

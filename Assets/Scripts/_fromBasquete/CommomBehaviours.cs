using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CommomBehaviours : MonoBehaviour {

	public GameObject ChangeMenu_newMenu, ChangeMenu_oldMenu;
	public bool setFocusAtEnabled;
	public void CB_TurnOff()
    {
		Application.Quit();
    }

	public void CB_LoadScene(string SceneName)
	{
		SceneManager.LoadScene(SceneName);
	}

	public void CB_LoadPrincipalScene(InputField inputSenha)
	{
		if ((VariaveisGlobais.IdentificadorNome.Trim().Length > 0) && (VariaveisGlobais.Senha_Pesquisa == "paterusp"))

		{
			VariaveisGlobais.estaNaPesquisa = true;
			SceneManager.LoadScene("PrincipalScene");
		}
			
		else
		{
			if (ChangeMenu_oldMenu)
				ChangeMenu_oldMenu.SetActive(false); 
			if (ChangeMenu_newMenu)
				ChangeMenu_newMenu.SetActive(true);

			if (inputSenha)
				inputSenha.text = "";
		}
	}


	public void CB_LoadPrincipalSceneWithoutPesquisa()
	{
			VariaveisGlobais.estaNaPesquisa = false;
			SceneManager.LoadScene("PrincipalScene");
	}

	public void CB_LoadProtocoloScene(InputField inputSenha)
	{
		if ((VariaveisGlobais.IdentificadorNome.Trim().Length > 0) && (VariaveisGlobais.Senha_Pesquisa == "paterusp"))
		{
			VariaveisGlobais.estaNaPesquisa = true;
			SceneManager.LoadScene("ProtocoloScene");
		}

		else
		{
			if (ChangeMenu_oldMenu)
				ChangeMenu_oldMenu.SetActive(false);
			if (ChangeMenu_newMenu)
				ChangeMenu_newMenu.SetActive(true);

			if (inputSenha)
				inputSenha.text = "";
		}
	}
	public void CB_LoadProtocoloSceneWithoutPesquisa()
	{
			VariaveisGlobais.estaNaPesquisa = false;
			SceneManager.LoadScene("ProtocoloScene");
	}


	public void CB_LoadSceneAsync(string SceneName)
	{
		SceneManager.LoadSceneAsync(SceneName);
	}

	public void CB_ChangeMenu()
    {
		if (ChangeMenu_newMenu)
			ChangeMenu_newMenu.SetActive(true);

		if (ChangeMenu_oldMenu)
			ChangeMenu_oldMenu.SetActive(false);
	}

	private void OnEnable()
    {
		if (setFocusAtEnabled)
        {
			EventSystem.current.SetSelectedGameObject(gameObject);
        }
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MessageScreen : MonoBehaviour
{
    public GameObject currentScreen;
    public GameObject messageScreen;
    public UI_WaitNextScreen waitModal;
    public GameObject endOfChallengeModal;
    public GameObject endOfCustomMatchModal;

    public void ShowSuccessModal()
    {
        if (VariaveisGlobais.partidaCorrente != 0)
        {
            messageScreen.SetActive(true);
            currentScreen.SetActive(false);
            waitModal.gameObject.SetActive(true);
            waitModal.waitSuccess();
        }
        else
        {
            messageScreen.SetActive(true);
            currentScreen.SetActive(false);
            endOfCustomMatchModal.SetActive(true);
        }
    }

    /*
    public void HideSuccessModal()
    {
        currentScreen.SetActive(true);
        successModal.SetActive(false);
        messageScreen.SetActive(false);
    }
    */
    public void ShowFailureModal()
    {
        messageScreen.SetActive(true);
        currentScreen.SetActive(false);
        waitModal.gameObject.SetActive(true);
        waitModal.waitFailure();
    }
    /*
    public void HideFailureModal()
    {
        currentScreen.SetActive(true);
        failureModal.SetActive(false);
        messageScreen.SetActive(false);
    }
    */

    public void ShowEndOfChallengeModal()
    {
        messageScreen.SetActive(true);
        currentScreen.SetActive(false);
        endOfChallengeModal.SetActive(true);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MessageScreen : MonoBehaviour
{
    public GameObject currentScreen;
    public GameObject messageScreen;
    public UI_WaitNextScreen waitModal;

    public void ShowSuccessModal()
    {
        messageScreen.SetActive(true);
        currentScreen.SetActive(false);
        waitModal.gameObject.SetActive(true);
        waitModal.waitSuccess();
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
}

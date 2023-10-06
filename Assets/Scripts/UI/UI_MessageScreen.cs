using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_MessageScreen : MonoBehaviour
{
    public GameObject currentScreen;
    public GameObject messageScreen;
    public GameObject successModal;

    void Star() {}

    void Update() {}

    public void ShowSuccessModal()
    {
        messageScreen.SetActive(true);
        currentScreen.SetActive(false);
        successModal.SetActive(true);
    }

    public void HideSuccessModal()
    {
        currentScreen.SetActive(true);
        successModal.SetActive(false);
        messageScreen.SetActive(false);
    }
}

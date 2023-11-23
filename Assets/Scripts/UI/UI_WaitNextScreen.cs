using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_WaitNextScreen : MonoBehaviour
{
    public GameObject successModal, failureModal;

    public void waitSuccess()
    {
        StartCoroutine(waiting("success"));
    }

    public void waitFailure()
    {
        StartCoroutine(waiting("failure"));
    }

    IEnumerator waiting(string whatModal)
    {
        yield return new WaitForSeconds(1);

        while (VariaveisGlobais.conexaoBD.cr_PostDataCoroutine_running)
            yield return new WaitForSeconds(1);

        switch (whatModal)
        {
            case ("success"):
                successModal.SetActive(true);
                break;

            case ("failure"):
                failureModal.SetActive(true);
                break;
        }
        gameObject.SetActive(false);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewPositionItem : MonoBehaviour
{
    public Controller_Ativ01_QuebraCabeca controller;
    public Text text;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        UpdateLabels();
    }

    public void UpdateLabels()
    {
        switch (gameObject.name)
        {
            case "Peca_01":
                text.text = "Lin " + Mathf.FloorToInt(controller.posIniciais[0].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.posIniciais[0].GetSiblingIndex() % 32).ToString();
                break;
            case "Peca_02":
                text.text = "Lin " + Mathf.FloorToInt(controller.posIniciais[1].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.posIniciais[1].GetSiblingIndex() % 32).ToString();
                break;
            case "Peca_03":
                text.text = "Lin " + Mathf.FloorToInt(controller.posIniciais[2].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.posIniciais[2].GetSiblingIndex() % 32).ToString();
                break;
            case "Peca_04":
                text.text = "Lin " + Mathf.FloorToInt(controller.posIniciais[3].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.posIniciais[3].GetSiblingIndex() % 32).ToString();
                break;
            case "Guia_01":
                text.text = "Lin " + Mathf.FloorToInt(controller.metas[0].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.metas[0].GetSiblingIndex() % 32).ToString();
                break;
            case "Guia_02":
                text.text = "Lin " + Mathf.FloorToInt(controller.metas[1].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.metas[1].GetSiblingIndex() % 32).ToString();
                break;
            case "Guia_03":
                text.text = "Lin " + Mathf.FloorToInt(controller.metas[2].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.metas[2].GetSiblingIndex() % 32).ToString();
                break;
            case "Guia_04":
                text.text = "Lin " + Mathf.FloorToInt(controller.metas[3].GetSiblingIndex() / 32).ToString() + System.Environment.NewLine + "Col " + (controller.metas[3].GetSiblingIndex() % 32).ToString();
                break;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class SetPositionItem : MonoBehaviour
{
    public ToggleGroup toggleGroup;
    public Color corPosInicial = Color.green;
    public Color corAlvos = Color.yellow;
    public Color corNormal = Color.white;

    public void SetPosition()
    {
        Transform transformParent = toggleGroup.ActiveToggles().ToList().First().transform.parent;
        string activeToggleParent = transformParent.name;

        switch (activeToggleParent)
        {
            case "Peca_01":
                VariaveisGlobais.atualControllerLabirinto.posIniciais[0] = VariaveisGlobais.atualControllerLabirinto.posIniciais[0].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
            case "Peca_02":
                VariaveisGlobais.atualControllerLabirinto.posIniciais[1] = VariaveisGlobais.atualControllerLabirinto.posIniciais[1].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
            case "Peca_03":
                VariaveisGlobais.atualControllerLabirinto.posIniciais[2] = VariaveisGlobais.atualControllerLabirinto.posIniciais[2].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
            case "Peca_04":
                VariaveisGlobais.atualControllerLabirinto.posIniciais[3] = VariaveisGlobais.atualControllerLabirinto.posIniciais[3].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
            case "Guia_01":
                VariaveisGlobais.atualControllerLabirinto.metas[0] = VariaveisGlobais.atualControllerLabirinto.metas[0].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
            case "Guia_02":
                VariaveisGlobais.atualControllerLabirinto.metas[1] = VariaveisGlobais.atualControllerLabirinto.metas[1].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
            case "Guia_03":
                VariaveisGlobais.atualControllerLabirinto.metas[2] = VariaveisGlobais.atualControllerLabirinto.metas[2].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
            case "Guia_04":
                VariaveisGlobais.atualControllerLabirinto.metas[3] = VariaveisGlobais.atualControllerLabirinto.metas[3].parent.GetChild(transform.GetSiblingIndex()).GetComponent<RectTransform>();
                break;
        }

        UpdateColorsButtons();

    }
    public void UpdateColorsButtons()
    {
        for (int i = 0; i < transform.parent.childCount; i++)
        {
            ColorBlock normalColors = transform.parent.GetChild(i).GetComponent<Button>().colors;
            normalColors.normalColor = corNormal;
            normalColors.highlightedColor = corNormal;
            transform.parent.GetChild(i).GetComponent<Button>().colors = normalColors;
        }

        for (int i = 0; i < 4; i++)
        {
            ColorBlock newPosInicialColors = transform.parent.GetChild(VariaveisGlobais.atualControllerLabirinto.posIniciais[i].GetSiblingIndex()).GetComponent<Button>().colors;
            newPosInicialColors.normalColor = corPosInicial;
            newPosInicialColors.highlightedColor = corPosInicial;
            transform.parent.GetChild(VariaveisGlobais.atualControllerLabirinto.posIniciais[i].GetSiblingIndex()).GetComponent<Button>().colors = newPosInicialColors;
        }

        for (int i = 0; i < 4; i++)
        {
            ColorBlock newAlvosColors = transform.parent.GetChild(VariaveisGlobais.atualControllerLabirinto.metas[i].GetSiblingIndex()).GetComponent<Button>().colors;
            newAlvosColors.normalColor = corAlvos;
            newAlvosColors.highlightedColor = corAlvos;
            transform.parent.GetChild(VariaveisGlobais.atualControllerLabirinto.metas[i].GetSiblingIndex()).GetComponent<Button>().colors = newAlvosColors;
        }
    }
}

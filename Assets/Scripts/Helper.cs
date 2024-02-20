using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Helper : MonoBehaviour
{
    public float maxScale = 5f; // Valor máximo de escala
    public float scaleSpeed = 0.1f; // Velocidade de mudança de escala
    public float idleTime = 5f; // Tempo para o objeto estar parado antes de aumentar a escala
    public float moveTime = 5f; // Tempo para o objeto estar em movimento antes de reduzir a escala

    private Vector3 originalScale; // Escala original do objeto
    private Vector3 targetScale; // Escala alvo que queremos alcançar
    private bool isIdle = false; // Flag para verificar se o objeto está parado
    private Vector3 lastPosition; // Última posição registrada do objeto

    private void OnEnable()
    {
        VariaveisGlobais.usouAjudaNaReta = 'N';
        VariaveisGlobais.escalaMaxDaAjuda = 1;
        transform.localScale = new Vector3(0.8f, 0.8f, 0.8f);
        originalScale = transform.localScale;
        targetScale = originalScale;
        lastPosition = transform.position;
        InvokeRepeating("CheckIdle", 0f, 1f); // Verifica se o objeto está parado a cada segundo
    }

    private void Update()
    {
        if (isIdle)
        {
            if (transform.localScale.x > VariaveisGlobais.escalaMaxDaAjuda) 
            {
                VariaveisGlobais.escalaMaxDaAjuda = transform.localScale.x;
            }

            if (transform.localScale.x < maxScale) // Aumenta gradualmente a escala até o valor máximo
            {
                transform.localScale += Vector3.one * scaleSpeed * Time.deltaTime;
            }
        }
        else // Reduz gradualmente a escala até a escala original
        {
            transform.localScale = Vector3.Lerp(transform.localScale, originalScale, scaleSpeed * Time.deltaTime);
        }
    }

    private void CheckIdle()
    {
        if ((transform.position - lastPosition).magnitude < 0.01f)
        {
            if (isIdle == false) // Se o objeto parou, inicia o temporizador de escala
            {
                Invoke("StartScaling", idleTime);
            }
        }
        else // Se o objeto se moveu, cancela o temporizador de escala
        {
            CancelInvoke("StartScaling");
            Invoke("StartScaleReduction", moveTime);
            isIdle = false;
            targetScale = transform.localScale;
        }

        lastPosition = transform.position;
    }

    private void StartScaling()
    {
        isIdle = true;
        VariaveisGlobais.usouAjudaNaReta = 'S';
    }

    private void StartScaleReduction()
    {
        isIdle = false;
    }
}
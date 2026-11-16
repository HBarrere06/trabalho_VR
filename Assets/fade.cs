
using UnityEngine;
using System.Collections;

public class FadeTransicao : MonoBehaviour
{
    [Header("Configuracao")]
    public CanvasGroup canvasGroup;

    public float duracaoFade = 3f;

    private bool transicaoIniciada = false;

    private void Start()
    {
        canvasGroup.alpha = 0f;
    }

    public void IniciarFade()
    {
        if (transicaoIniciada)
            return;

        transicaoIniciada = true;

        StartCoroutine(FadeParaPreto());
    }

    private IEnumerator FadeParaPreto()
    {
        float tempo = 0f;

        canvasGroup.alpha = 0f;

        while (tempo < duracaoFade)
        {
            tempo += Time.deltaTime;

            canvasGroup.alpha = Mathf.Clamp01(
                tempo / duracaoFade
            );

            yield return null;
        }

        canvasGroup.alpha = 1f;

        Debug.Log("Tela completamente preta.");
    }
}


using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class abrirPorta : MonoBehaviour
{
    [Header("Coleta")]
    public ContadorColeta contador;

    [Header("Transicao")]
    public FadeTransicao transicao;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable interactable;

    private bool portaUtilizada = false;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRSimpleInteractable>();

        if (interactable != null)
        {
            interactable.selectEntered.AddListener(AoInteragir);
        }
        else
        {
            Debug.LogError(
                "XR Simple Interactable nao encontrado na porta."
            );
        }
    }

    private void AoInteragir(SelectEnterEventArgs args)
    {
        if (portaUtilizada)
            return;

        // Verifica se todas as chaves foram coletadas
        if (contador.QuantidadeColetada >= contador.quantidadeMaxima)
        {
            portaUtilizada = true;

            Debug.Log("Todas as chaves foram coletadas!");

            // Inicia o escurecimento
            transicao.IniciarFade();
        }
        else
        {
            Debug.Log("Voce ainda nao coletou todas as chaves!");
        }
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(AoInteragir);
        }
    }
}

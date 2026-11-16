
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjetoColetavel : MonoBehaviour
{
    [Header("Contador")]
    public ContadorColeta contador;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable interactable;

    private bool coletado = false;

    private void Awake()
    {
        interactable = GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRBaseInteractable>();

        if (interactable == null)
        {
            Debug.LogError(
                "Objeto precisa de um componente XR Interactable: "
                + gameObject.name
            );

            return;
        }

        interactable.selectEntered.AddListener(AoInteragir);
    }

    private void AoInteragir(SelectEnterEventArgs args)
    {
        if (coletado)
            return;

        coletado = true;

        // Incrementa o contador
        if (contador != null)
        {
            contador.AdicionarColeta();
        }

        // Remove o objeto da cena
        Destroy(gameObject);
    }

    private void OnDestroy()
    {
        if (interactable != null)
        {
            interactable.selectEntered.RemoveListener(AoInteragir);
        }
    }
}

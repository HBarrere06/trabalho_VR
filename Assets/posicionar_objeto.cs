
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ObjectPlacementZone : MonoBehaviour
{
    [Header("Objeto a ser posicionado")]
    public GameObject objeto;

    [Header("Pre-visualizacao")]
    public GameObject objetoPreview;

    [Header("Posicao de encaixe")]
    public Transform pontoDeEncaixe;

    [Header("Controlador Dia e Noite")]
    public ObjetoDiaNoite controladorDiaNoite;

    private UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable grabInteractable;
    private Rigidbody rb;

    private bool dentroDaArea = false;
    private bool jaPosicionado = false;

    private void Awake()
    {
        grabInteractable = objeto.GetComponent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();
        rb = objeto.GetComponent<Rigidbody>();

        if (grabInteractable != null)
        {
            grabInteractable.selectExited.AddListener(AoSoltarObjeto);
        }
    }

    private void Start()
    {
        objetoPreview.SetActive(false);
    }

    private void OnTriggerStay(Collider other)
    {
        if (jaPosicionado || grabInteractable == null)
            return;

        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable detectado =
            other.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (detectado == grabInteractable &&
            grabInteractable.isSelected)
        {
            dentroDaArea = true;

            objetoPreview.transform.SetPositionAndRotation(
                pontoDeEncaixe.position,
                pontoDeEncaixe.rotation
            );

            objetoPreview.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable detectado =
            other.GetComponentInParent<UnityEngine.XR.Interaction.Toolkit.Interactables.XRGrabInteractable>();

        if (detectado == grabInteractable)
        {
            dentroDaArea = false;
            objetoPreview.SetActive(false);
        }
    }

    private void AoSoltarObjeto(SelectExitEventArgs args)
    {
        if (jaPosicionado || !dentroDaArea)
            return;

        objeto.transform.SetPositionAndRotation(
            pontoDeEncaixe.position,
            pontoDeEncaixe.rotation
        );

        if (rb != null)
        {
            rb.linearVelocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;

            rb.useGravity = false;
            rb.isKinematic = true;
        }

        jaPosicionado = true;

        objetoPreview.SetActive(false);
        dentroDaArea = false;

        if (controladorDiaNoite != null)
        {
            controladorDiaNoite.MarcarComoPosicionado();
        }

        Debug.Log("O ovo foi posicionado corretamente!");
    }

    private void OnDestroy()
    {
        if (grabInteractable != null)
        {
            grabInteractable.selectExited.RemoveListener(AoSoltarObjeto);
        }
    }
}

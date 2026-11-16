
using UnityEngine;

public class ObjetoDiaNoite : MonoBehaviour
{
    [Header("Objetos")]
    public GameObject elemento;
    public GameObject objetoInterativo;

    private bool elementoPosicionado = false;
    private bool transformacaoRealizada = false;

    private void OnEnable()
    {
        SkyboxManager.EstadoAlterado += AoMudarEstado;
    }

    private void OnDisable()
    {
        SkyboxManager.EstadoAlterado -= AoMudarEstado;
    }

    private void Start()
    {
        // Começa com o elemento visível e o objeto escondido
        elementoPosicionado = false;
        transformacaoRealizada = false;

        elemento.SetActive(true);
        objetoInterativo.SetActive(false);
    }

    public void MarcarComoPosicionado()
    {
        elementoPosicionado = true;

        // Se já estiver de noite, transforma imediatamente
        if (SkyboxManager.NoiteAtiva)
        {
            TransformarObjeto();
        }
    }

    private void AoMudarEstado(bool noite)
    {
        // Só transforma na mudança para noite
        // e somente se o elemento já estiver posicionado
        if (noite && elementoPosicionado && !transformacaoRealizada)
        {
            TransformarObjeto();
        }
    }

    private void TransformarObjeto()
    {
        if (transformacaoRealizada)
            return;

        transformacaoRealizada = true;

        // Retira o elemento permanentemente
        elemento.SetActive(false);

        // Faz o objeto interativo aparecer
        objetoInterativo.SetActive(true);

        Debug.Log("O elemento foi removido permanentemente e o objeto interativo apareceu.");
    }
}

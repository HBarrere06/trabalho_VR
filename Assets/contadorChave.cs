
using UnityEngine;
using TMPro;

public class ContadorColeta : MonoBehaviour
{
    [Header("Configuracao")]
    public int QuantidadeColetada = 0;
    public int quantidadeMaxima = 3;

    [Header("Interface")]
    public TMP_Text textoContador;

    private void Start()
    {
        QuantidadeColetada = 0;
        AtualizarTexto();
    }

    public void AdicionarColeta()
    {
        // Impede ultrapassar o limite
        if (QuantidadeColetada >= quantidadeMaxima)
            return;

        QuantidadeColetada++;

        AtualizarTexto();

        Debug.Log("Chaves coletadas: " +
            QuantidadeColetada + "/" + quantidadeMaxima);
    }

    private void AtualizarTexto()
    {
        if (textoContador != null)
        {
            textoContador.text =
                QuantidadeColetada + "/" + quantidadeMaxima;
        }
    }
}

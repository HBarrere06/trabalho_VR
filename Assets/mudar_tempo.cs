
using UnityEngine;
using System;

public class SkyboxManager : MonoBehaviour
{
    [Header("Materiais da Skybox")]
    public Material skyboxDia;
    public Material skyboxNoite;

    [Header("Estado do ambiente")]
    public bool noiteAtiva = false;

    [Header("Iluminacao")]
    public float intensidadeDia = 1.0f;
    public float intensidadeNoite = 0.3f;

    public static bool NoiteAtiva { get; private set; }

    public static event Action<bool> EstadoAlterado;

    private void Awake()
    {
        noiteAtiva = false;
        NoiteAtiva = false;
    }

    private void Start()
    {
        AplicarEstado();
    }

    public void AlternarSkybox()
    {
        noiteAtiva = !noiteAtiva;
        NoiteAtiva = noiteAtiva;

        AplicarEstado();

        EstadoAlterado?.Invoke(NoiteAtiva);
    }

    private void AplicarEstado()
    {
        if (noiteAtiva)
        {
            RenderSettings.skybox = skyboxNoite;
            RenderSettings.ambientIntensity = intensidadeNoite;
        }
        else
        {
            RenderSettings.skybox = skyboxDia;
            RenderSettings.ambientIntensity = intensidadeDia;
        }

        DynamicGI.UpdateEnvironment();
    }
}

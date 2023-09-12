using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Camera))]
public class RenderParticlesEffect : MonoBehaviour
{
    // Referência da câmera que renderiza as partículas
    [SerializeField] private Camera particlesCamera;

    // Ajusta a resolução da imagem em pixels
    [SerializeField] private Vector2Int imageResolution = new Vector2Int(1080, 1080);

    // Referência da RawImage na UI
    [SerializeField] private RawImage targetImage;

    private RenderTexture renderTexture;

    private void Awake()
    {
        if (!particlesCamera) particlesCamera = GetComponent<Camera>();

        renderTexture = new RenderTexture(imageResolution.x, imageResolution.y, 32);
        particlesCamera.targetTexture = renderTexture;

        targetImage.texture = renderTexture;
    }
}
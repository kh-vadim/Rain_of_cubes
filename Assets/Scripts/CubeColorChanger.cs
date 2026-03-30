using UnityEngine;

public class CubeColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private Color _defaultColor;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _defaultColor = _renderer.sharedMaterial.color;
    }

    public void SetRandomColor()
    {
        _renderer.material.color = Random.ColorHSV();
    }

    public void ResetToDefaultColor()
    {
        _renderer.material.color = _defaultColor;
    }
}

using UnityEngine;

public class Painter : MonoBehaviour
{
    private static readonly int Color1 = Shader.PropertyToID("_Color");
    
    private Renderer _renderer;
    private MaterialPropertyBlock _materialProperty;

    public void SetColor(int id)
    {
        if (_renderer == null || _materialProperty == null)
        {
            _renderer = GetComponent<MeshRenderer>();
            _materialProperty = new MaterialPropertyBlock();
            _renderer.GetPropertyBlock(_materialProperty);
        }
        
        _materialProperty.SetColor(Color1, GetColor(id));
        _renderer.SetPropertyBlock(_materialProperty);
    }
    
    private static Color GetColor(int id)
    {
        switch (id)
        {
            case 1: return Color.blue;
            case 2: return Color.cyan;
            case 3: return Color.green;
            case 4: return Color.magenta;
            case 5: return Color.red;
            case 6: return Color.yellow;
            default: return Color.white;
        }
    }
}
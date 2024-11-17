using UnityEngine;

public class colorController : Singleton<colorController>
{
    [SerializeField] Material[] colorMats;

    // Start is called before the first frame update
    void Start()
    {

    }

    public Material getColorMaterial(ColorType colorType)
    {
        return colorMats[(int)colorType];
    }
}

using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color DoRandomColor()
    {
        float red = Random.value;
        float green = Random.value;
        float blue = Random.value;

        return new Color(red, green, blue);
    }
}
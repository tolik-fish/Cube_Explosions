using UnityEngine;

public class ColorChanger : MonoBehaviour
{
    public Color SetRandomColor()
    {
        float red = Random.value;
        float green = Random.value;
        float blue = Random.value;

        return new Color(red, green, blue);
    }
}
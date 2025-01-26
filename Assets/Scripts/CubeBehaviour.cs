using UnityEngine;

public class CubeBehaviour : MonoBehaviour
{
    private CubeSpawner _spawner;

    private void Awake()
    {
        _spawner = gameObject.GetComponent<CubeSpawner>();
    }

    private void OnMouseDown()
    {
        if (_spawner.ShouldSpawn)
            GetComponent<CubeSpawner>().Spawn();

        Destroy(gameObject);
    }
}
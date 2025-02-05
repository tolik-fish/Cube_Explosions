using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _chanceMultiplier = 0.5f;
    private float _initialChanceSpawn = 1f;
    private float _currentDivisionCount = 0f;

    private CubeSpawner _spawner;
    private Transform _transform;

    private float _currentChanceSpawn => _initialChanceSpawn * Mathf.Pow(_chanceMultiplier, _currentDivisionCount);

    private void Start()
    {
        _spawner = FindObjectOfType<CubeSpawner>();
        _transform = gameObject.transform;
    }

    private void OnMouseDown()
    {
        if (Random.value <= _currentChanceSpawn)
        {
            _spawner?.Spawn(_transform, _currentDivisionCount);
        }

        Destroy(gameObject);
    }

    public void IncreaseCount(float number)
    {
        _currentDivisionCount = number;
        _currentDivisionCount++;
    }
}
using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private float _chanceMultiplier = 0.5f;
    private float _initialChanceSpawn = 1f;


    public event Action<Cube> CubeClicked;
    public Renderer Renderer;

    private float _currentChanceSpawn => _initialChanceSpawn * Mathf.Pow(_chanceMultiplier, CurrentDivisionCount);

    public float CurrentDivisionCount { get; private set; }

    public Transform CubeTransform { get; private set; }

    private void Awake()
    {
        Renderer = GetComponent<Renderer>();
        CubeTransform = gameObject.transform;
        CurrentDivisionCount = 0;
    }

    private void OnMouseDown()
    {
        if (UnityEngine.Random.value <= _currentChanceSpawn)
        {
            CubeClicked?.Invoke(this);
        }

        Destroy(gameObject);
    }

    public void IncreaseCount(float count)
    {
        CurrentDivisionCount = count;
        CurrentDivisionCount++;
    }
}
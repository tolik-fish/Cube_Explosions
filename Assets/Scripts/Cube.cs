using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private float _chanceMultiplier = 0.5f;
    private float _initialChanceSpawn = 1f;

    public event Action<Cube> Clicked;

    private float _currentChanceSpawn => _initialChanceSpawn * Mathf.Pow(_chanceMultiplier, CurrentDivisionCount);

    public float CurrentDivisionCount { get; private set; }

    public Renderer Renderer { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    private void Awake()
    {
        CurrentDivisionCount = 0;
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        Renderer = _renderer;
        Rigidbody = _rigidbody;
    }

    private void OnMouseDown()
    {
        if (UnityEngine.Random.value <= _currentChanceSpawn)
        {
            Clicked.Invoke(this);
        }

        Destroy(gameObject);
    }

    public void IncreaseCount(float count)
    {
        CurrentDivisionCount = count;
        CurrentDivisionCount++;
    }
}
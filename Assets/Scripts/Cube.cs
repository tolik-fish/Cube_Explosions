using System;
using UnityEngine;

public class Cube : MonoBehaviour
{
    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private float _chanceMultiplier = 0.5f;
    private float _chanceSpawn = 1f;

    public event Action<Cube> Clicked;

    public Renderer Renderer { get; private set; }

    public Rigidbody Rigidbody { get; private set; }

    public float ChanceSpawn { get; private set; }

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _rigidbody = GetComponent<Rigidbody>();
        Renderer = _renderer;
        Rigidbody = _rigidbody;
        ChanceSpawn = _chanceSpawn;
    }

    private void OnMouseDown()
    {
        Clicked.Invoke(this);

        Destroy(gameObject);
    }

    public void DecreaseChance(float chance)
    {
        _chanceSpawn = chance;
        _chanceSpawn *= _chanceMultiplier;
        ChanceSpawn = _chanceSpawn;
    }
}
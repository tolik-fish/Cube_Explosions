using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private float _chanceSpawn = 1f;
    private float _chanceMultiplier = 0.5f;
    private float _scaleMultiplier = 0.5f;
    private int _minNumberCubeSpawn = 2;
    private int _maxNumberCubeSpawn = 6;

    public bool ShouldSpawn { get; private set; }

    private void Start()
    {
        ShouldSpawn = Random.value <= _chanceSpawn;
    }

    public void Spawn()
    {
        int randomNumber = Random.Range(_minNumberCubeSpawn, _maxNumberCubeSpawn + 1);

        List<Rigidbody> newCubes = new List<Rigidbody>();

        _chanceSpawn *= _chanceMultiplier;

        for (int i = 0; i < randomNumber; i++)
        {
            GameObject newCube = CreateCube();

            newCubes.Add(newCube.GetComponent<Rigidbody>());

            newCube.GetComponent<ExplodeCreator>().Explode();
        }
    }

    private GameObject CreateCube()
    {
        GameObject newCube = GameObject.CreatePrimitive(PrimitiveType.Cube);

        newCube.transform.position = transform.position;
        newCube.transform.localScale = gameObject.transform.localScale * _scaleMultiplier;

        newCube.AddComponent<Rigidbody>();
        newCube.AddComponent<CubeSpawner>();
        newCube.AddComponent<CubeBehaviour>();
        newCube.AddComponent<ColorChanger>();
        newCube.AddComponent<ExplodeCreator>();

        newCube.GetComponent<Renderer>().material.color = GetComponent<ColorChanger>().SetRandomColor();
        newCube.GetComponent<CubeSpawner>()._chanceSpawn = _chanceSpawn;

        return newCube;
    }
}
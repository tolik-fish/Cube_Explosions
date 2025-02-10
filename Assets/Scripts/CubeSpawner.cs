using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private ExplodeCreator _explodeCreator;

    private float _scaleMultiplier = 0.5f;
    private int _minNumberCubeSpawn = 2;
    private int _maxNumberCubeSpawn = 6;

    private List<Cube> _cubes;

    private void Awake()
    {
        _cubes = FindObjectsOfType<Cube>().ToList();
    }

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
            cube.Clicked += Spawn;
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
            cube.Clicked -= Spawn;
    }


    public void Spawn(Cube cube)
    {
        int randomNumber = Random.Range(_minNumberCubeSpawn, _maxNumberCubeSpawn + 1);

        List<Rigidbody> newCubesRigidbody = new List<Rigidbody>();

        for (int i = 0; i < randomNumber; i++)
        {
            Cube newCube = CreateCube(cube.transform.position, cube.CurrentDivisionCount);

            newCubesRigidbody.Add(newCube.Rigidbody);

            newCube.Clicked += Spawn;
        }

        _explodeCreator.Explode(newCubesRigidbody);

        cube.Clicked -= Spawn;
    }

    private Cube CreateCube(Vector3 position, float currentCount)
    {
        Cube newCube = Cube.Instantiate(_cubePrefab, position, Quaternion.identity);

        float currentScale = _scaleMultiplier * Mathf.Pow(_scaleMultiplier, currentCount);

        newCube.transform.localScale *= currentScale;

        newCube.IncreaseCount(currentCount);

        newCube.Renderer.material.color = _colorChanger.GetRandomColor();

        return newCube;
    }
}
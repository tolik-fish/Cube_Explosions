using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _cubePrefab;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private ExplodeCreator _explodeCreator;

    private float _scaleMultiplier = 0.5f;
    private int _minNumberCubeSpawn = 2;
    private int _maxNumberCubeSpawn = 6;

    private List<Cube> _cubes;

    private void Awake()
    {
        _cubes = new List<Cube>(FindObjectsOfType<Cube>());
    }

    private void OnEnable()
    {
        foreach (Cube cube in _cubes)
            cube.CubeClicked += Spawn;
    }

    private void OnDisable()
    {
        foreach (Cube cube in _cubes)
            cube.CubeClicked -= Spawn;
    }


    public void Spawn(Cube cube)
    {
        int randomNumber = Random.Range(_minNumberCubeSpawn, _maxNumberCubeSpawn + 1);

        List<Rigidbody> newCubesRigidbody = new List<Rigidbody>();

        for (int i = 0; i < randomNumber; i++)
        {
            GameObject newCube = CreateCube(cube.transform, cube.CurrentDivisionCount);

            newCubesRigidbody.Add(newCube.GetComponent<Rigidbody>());
        }

        _explodeCreator.Explode(newCubesRigidbody);
    }

    private GameObject CreateCube(Transform transform, float currentCount)
    {
        GameObject newCube = GameObject.Instantiate(_cubePrefab, transform.position, Quaternion.identity);

        Cube newCubeComponent = newCube?.GetComponent<Cube>();

        float currentScale = _scaleMultiplier * Mathf.Pow(_scaleMultiplier, currentCount);

        newCube.transform.localScale *= currentScale;

        newCubeComponent.IncreaseCount(currentCount);

        newCubeComponent.Renderer.material.color = _colorChanger.GetRandomColor();

        newCubeComponent.CubeClicked += Spawn;

        _cubes.Add(newCubeComponent);

        return newCube;
    }
}
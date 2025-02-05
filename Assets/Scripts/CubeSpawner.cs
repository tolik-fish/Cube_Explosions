using System.Collections.Generic;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    private float _scaleMultiplier = 0.5f;
    private int _minNumberCubeSpawn = 2;
    private int _maxNumberCubeSpawn = 6;

    [SerializeField] private GameObject _prefab;
    [SerializeField] private ColorChanger _colorChanger;

    private List<Cube> _cubes;

    private void Start()
    {
        _cubes = new List<Cube>(FindObjectsOfType<Cube>());
    }

    public void Spawn(Transform transform, float currentCount)
    {
        int randomNumber = Random.Range(_minNumberCubeSpawn, _maxNumberCubeSpawn + 1);

        for (int i = 0; i < randomNumber; i++)
        {
            GameObject newCube = CreateCube(transform, currentCount);

            newCube.GetComponent<ExplodeCreator>().Explode();
        }
    }

    private GameObject CreateCube(Transform transform, float currentCount)
    {
        GameObject newCube = GameObject.Instantiate(_prefab, transform.position, Quaternion.identity);

        newCube.transform.localScale = transform.localScale * _scaleMultiplier;

        newCube.GetComponent<Renderer>().material.color = _colorChanger.DoRandomColor();

        Cube newCubeComponent = newCube.GetComponent<Cube>();

        _cubes.Add(newCubeComponent);

        newCubeComponent.IncreaseCount(currentCount);

        return newCube;
    }
}
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class CubeSpawner : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private ColorChanger _colorChanger;
    [SerializeField] private ExplodeCreator _explodeCreator;
    [SerializeField] private List<Cube> _cubes;

    private float _scaleMultiplier = 0.5f;
    private int _minNumberCubeSpawn = 2;
    private int _maxNumberCubeSpawn = 6;

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
        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        if (Random.value <= cube.ChanceSpawn)
        {
            int randomNumber = Random.Range(_minNumberCubeSpawn, _maxNumberCubeSpawn + 1);

            for (int i = 0; i < randomNumber; i++)
            {
                Cube newCube = Create(cube.transform, cube.ChanceSpawn);

                rigidbodies.Add(newCube.Rigidbody);

                newCube.Clicked += Spawn;
            }

            _explodeCreator.Explode(rigidbodies);

            cube.Clicked -= Spawn;
        }
        else
        {
            _explodeCreator.Explode(cube);
        }
    }

    private Cube Create(Transform transform, float parentChance)
    {
        Cube cube = Cube.Instantiate(_cubePrefab, transform.position, Quaternion.identity);

        cube.transform.localScale = _scaleMultiplier * transform.localScale;

        cube.DecreaseChance(parentChance);

        cube.Renderer.material.color = _colorChanger.GetRandomColor();

        return cube;
    }
}
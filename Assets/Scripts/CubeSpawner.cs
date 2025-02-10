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
        if (Random.value <= cube.ChanceSpawn)
        {
            int randomNumber = Random.Range(_minNumberCubeSpawn, _maxNumberCubeSpawn + 1);

            List<Rigidbody> newCubesRigidbody = new List<Rigidbody>();

            for (int i = 0; i < randomNumber; i++)
            {
                Cube newCube = CreateCube(cube.transform, cube.ChanceSpawn);

                newCubesRigidbody.Add(newCube.Rigidbody);

                newCube.Clicked += Spawn;
            }

            _explodeCreator.Explode(newCubesRigidbody);

            cube.Clicked -= Spawn;
        }
    }

    private Cube CreateCube(Transform transform, float parentChance)
    {
        Cube newCube = Cube.Instantiate(_cubePrefab, transform.position, Quaternion.identity);

        newCube.transform.localScale = _scaleMultiplier * transform.localScale;

        newCube.DecreaseChance(parentChance);

        newCube.Renderer.material.color = _colorChanger.GetRandomColor();

        return newCube;
    }
}
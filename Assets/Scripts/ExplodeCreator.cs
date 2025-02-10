using System.Collections.Generic;
using UnityEngine;

public class ExplodeCreator : MonoBehaviour
{
    private float _explodeRadius = 20f;
    private float _explodePower = 5f;
    private float _explodePowerUp = 2f;

    public void Explode(List<Rigidbody> objects)
    {
        foreach (Rigidbody cube in objects)
            cube.AddExplosionForce(_explodePower, cube.position, _explodeRadius, _explodePowerUp, ForceMode.Impulse);
    }
}
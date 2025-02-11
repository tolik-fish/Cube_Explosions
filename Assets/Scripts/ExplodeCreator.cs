using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ExplodeCreator : MonoBehaviour
{
    private float _explodeRadius = 20f;
    private float _explodePower = 40f;
    private float _explodePowerUp = 0.2f;

    public float SearchRadios { get; private set; }

    private void Awake()
    {
        SearchRadios = _explodeRadius;
    }

    public void Explode(List<Rigidbody> objects)
    {
        foreach (Rigidbody cube in objects)
            cube.AddExplosionForce(_explodePower, cube.position, _explodeRadius, _explodePowerUp, ForceMode.Force);
    }

    public void Explode(Cube cube)
    {
        List<Collider> colliders = Physics.OverlapSphere(cube.transform.position, SearchRadios/ cube.transform.localScale.y).ToList<Collider>();

        List<Rigidbody> rigidbodies = new List<Rigidbody>();

        foreach (Collider collider in colliders)
            rigidbodies.Add(collider?.GetComponent<Rigidbody>());

        foreach (Rigidbody rigidbody in rigidbodies)
        {
            if (rigidbody != null)
            {
                float rangeExplode = _explodeRadius / cube.transform.localScale.y;
                float distance = Vector3.Distance(cube.transform.position, rigidbody.position);
                float relativeDistance = 1 - Mathf.Clamp01(distance / rangeExplode);
                float force = _explodePower * relativeDistance / cube.transform.localScale.y;
                Debug.Log("Дистанция: " + distance);
                Debug.Log("Сила: " + force);
                rigidbody.AddExplosionForce(force, cube.transform.position, _explodeRadius, _explodePowerUp, ForceMode.VelocityChange);
            }
        }
    }
}
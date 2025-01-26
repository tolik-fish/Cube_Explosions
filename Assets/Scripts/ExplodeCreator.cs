using UnityEngine;

public class ExplodeCreator : MonoBehaviour
{
    private float _explodeRadius = 40f;
    private float _explodePower = 10f;
    private float _explodePowerUp = 10f;

    public void Explode()
    {
        gameObject.GetComponent<Rigidbody>().AddExplosionForce(_explodePower, transform.position, _explodeRadius, _explodePowerUp, ForceMode.Impulse);
    }
}

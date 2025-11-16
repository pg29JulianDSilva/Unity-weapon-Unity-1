using UnityEngine;

public class BulletMovement : MonoBehaviour
{
    //This is a primite bullet system

    [SerializeField] private float _bulletSpeed = 30f;

    private Rigidbody _rb;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        //ForceMode.Impulse is a physics preset to create the impuse effects
        _rb.AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);

        GameObject.Destroy(gameObject, 2);
    }

}

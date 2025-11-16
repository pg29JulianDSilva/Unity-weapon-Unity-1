using UnityEngine;

public class DetonateBullet : MonoBehaviour
{
    //This is for the homework

    [Header("Movement")]
    [SerializeField] private float _bulletSpeed = 15f;

    [Header("Explotion")]
    [SerializeField] private GameObject _bulletMesh;
    [SerializeField] private GameObject _bulletExploted;

    private Rigidbody _rb;


    private float _detonateSpeed;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();

        //ForceMode.Impulse is a physics preset to create the impuse effects
        _rb.AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);


        GameObject.Destroy(gameObject, 30);
    }

    private void FixedUpdate()
    {
        _detonateSpeed++;

        if (_detonateSpeed > 25.0f)
        {
            Explote();
        }
    }

    private void Explote()
    {
        Instantiate(_bulletExploted, _bulletMesh.GetComponent<Transform>().position, _bulletMesh.GetComponent<Transform>().rotation);
        GameObject.Destroy(_bulletMesh);
    }
}

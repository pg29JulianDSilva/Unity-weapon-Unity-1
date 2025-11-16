using UnityEngine;

public class ExplotionMovement : MonoBehaviour
{
    //This is a primite bullet system
    [SerializeField] private float _bulletSpeed = 30f;

    private Rigidbody _rb;
    private float _explotionExpantion = 0.2f;
    private Transform _explotionT;

    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _explotionT = GetComponent<Transform>();

        //ForceMode.Impulse is a physics preset to create the impuse effects
        _rb.AddForce(transform.forward * _bulletSpeed, ForceMode.Impulse);

        //GameObject.Destroy(gameObject);
    }

    private void Update()
    {
        if (_explotionExpantion <= 5.0f)
        {
            _explotionExpantion++;
            _explotionT.localScale = new Vector3(_explotionExpantion, _explotionExpantion, _explotionExpantion);
        }

        if(_explotionExpantion > 5.0f)
        {
            GameObject.Destroy(gameObject, 1f);
        }
    }

}

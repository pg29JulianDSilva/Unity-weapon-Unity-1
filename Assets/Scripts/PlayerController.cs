using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private int _speed = 10;
    [SerializeField] private float _mouseSensitivity = 400f;

    private Rigidbody _rigidbody;
    private Vector3 _moveVector;
    private float _xRotation = 0f;

    [Header("Gun")]
    public Transform _gunTip;
    public GameObject _bulletPrefab;
    public GameObject _bulletPrefabAlt;

    private void Start()
    {
        _rigidbody = gameObject.GetComponent<Rigidbody>();
        _moveVector = Vector3.zero;

        //Remove cursor on game
        Cursor.lockState = CursorLockMode.Locked;

        Camera.main.transform.localRotation = Quaternion.Euler(0f,0f,0f);
    }

    //Input system needs to be in the update, for rechecking inside the same fast frame rate
    private void Update()
    {
        //Mouse rotation
        float mouseX = Input.GetAxis("Mouse X") * _mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * _mouseSensitivity * Time.deltaTime;

        //Create stable system and rotation for up and down
        _xRotation -= mouseY;
        _xRotation = Mathf.Clamp(_xRotation, -90f, 90f);

        //Call the camera (When there is only 1), While the Quaternion creates an order to avoid gimball's lock
        Camera.main.transform.localRotation = Quaternion.Euler(_xRotation, 0f, 0f);

        //Rotation for the character
        gameObject.transform.Rotate(Vector3.up * mouseX);


        //Movement
        float zMovement = Input.GetAxis("Vertical");
        float xMovement = Input.GetAxis("Horizontal");

        // _moveVector = new Vector3(xMovement, 0f, zMovement);
        _moveVector = transform.right * xMovement + transform.forward * zMovement;


        //FOR BULLETS

        if (Input.GetButtonDown("Fire1")) Fire(false);
        if (Input.GetButtonDown("Fire2")) Fire(true);

    }


    //For the Prefab instanciation (spawn multiples)
    private void Fire(bool isAlternate)
    {
        Instantiate(isAlternate ? _bulletPrefabAlt : _bulletPrefab, _gunTip.position, _gunTip.rotation);
    }

    private void FixedUpdate()
    {
        _rigidbody.AddForce(_moveVector * _speed);

        if(Input.GetButtonDown("Debug Reset"))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour

{
    [SerializeField]
    private int _speed = 1;
    private float _xOutLeft = -19.5449f;
    private float _xOutRight = 19.5449f;
    private float _yOutUp = 10.83237f;
    private float _yOutDown = -10.83237f;
    public float horizontalInput;
    public float verticalInput;
    
    // Start is called before the first frame update
    void Start()
    {
        //gives player object a starting position = new position (0,0,0)
        transform.position = new Vector3(0,0,0);
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();

    }

    void CalculateMovement()
    {
        //transform.Translate(Vector3.right) is the same as transform.Translate(new Vector3(1, 0, 0));
        //transform.Translate(new Vector3(-1, 0, 0) * _speed * Time.deltaTime); goes left at a speed determined by the speed variable
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * _speed * Time.deltaTime);
        transform.Translate(Vector3.up * verticalInput * _speed * Time.deltaTime);

        // if position.transform.x < -19.5449 transform.position = -19.5449
        // if position.transform.x > 19.5449 transform.position = 19.5449
        // if position.transform.y < -10.83237 transform.positoin = -10.83237
        // if position.transform.y < -10.83237 transform.positoin = -10.83237
        // if position.transform.y > 10.83237 transform.positoin = 10.83237

        if (transform.position.x < _xOutLeft)
        {
            transform.position = new Vector3(_xOutLeft, transform.position.y, transform.position.z);
        }

        else if (transform.position.x > _xOutRight)
        {
            transform.position = new Vector3(_xOutRight, transform.position.y, transform.position.z);
        }

        if (transform.position.y > _yOutUp)
        {
            transform.position = new Vector3(transform.position.x, _yOutUp, transform.position.z);
        }

        else if (transform.position.y < _yOutDown)
        {
            transform.position = new Vector3(transform.position.x, _yOutDown, transform.position.z);
        }
    }
}

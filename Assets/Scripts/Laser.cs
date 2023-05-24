using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Laser : MonoBehaviour
{
    // variable for laser speed around 7-12
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 8.0f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up using transform.translate(Vector3.up * speed * time.DeltaTime)
        transform.Translate(Vector3.up * _speed * Time.deltaTime);

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyLaser : MonoBehaviour
{// variable for laser speed around 7-12
    // Start is called before the first frame update
    [SerializeField]
    private float _speed = 8.0f;
    [SerializeField]
    private float _outofBounds = -12.0f;
    [SerializeField] private Player _player;
    
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        //translate laser up using transform.translate(Vector3.up * speed * time.DeltaTime)
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if position of laser is greater than out of bound marker, destory that laser
        if (transform.position.y < _outofBounds)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);
            }

            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log($"Hit {other.transform.name}");

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage();


        }
    }
}

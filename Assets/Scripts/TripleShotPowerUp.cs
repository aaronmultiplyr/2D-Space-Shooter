using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TripleShotPowerUp : MonoBehaviour
{
    [SerializeField] private int _speed = 1;
    private Player _player;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if(transform.position.y < -12.14f)
        {
            Destroy(this.gameObject);
        }
        
    }

    

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            _player = other.transform.GetComponent<Player>();

            if (_player != null)
            {
                Destroy(this.gameObject);
                _player.TripleShotActive();
            }
            
        }
    }

   
}

using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.UIElements;


public class Enemy : MonoBehaviour
{

    [SerializeField] private float _speed = 4.0f;
    [SerializeField] private float _outOfBound = -12.24f;
    [SerializeField] private float _spawnAgain = 13.52f;
    
    

    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //move down
        //if off screen, respawn at randon x point at top of screen
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //if the position.transform.y < outofBounds, new position up top random location
        float xRangeRandom = Random.Range(-19.67f, 19.65f);
        

        if(transform.position.y < _outOfBound)
        {
            transform.position = new Vector3(xRangeRandom, _spawnAgain, transform.position.z);
        }

    
    }

    private void OnTriggerEnter2D(Collider2D other)
        
    {
        Debug.Log($"Hit {other.transform.name}");

        if (other.tag == "Laser Bolt")
        {
            
            Destroy(other.gameObject);
            Debug.Log("Laser Destroyed");
            Destroy(this.gameObject);
        }

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
            Destroy(this.gameObject);
            //other.transform.GetComponent<Player>().Damage();
            
        }
    }


}

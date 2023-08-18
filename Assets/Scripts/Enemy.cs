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
    [SerializeField] private Player _player;
    [SerializeField] private Animator _animator;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private GameObject _laser;
    [SerializeField] private Vector3 _laserPosition;
    
    

    

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();
        _audioSource = GetComponent<AudioSource>();

        if (_player == null )
        {
            Debug.LogError("Player is Null");
        }
        _animator = GetComponent<Animator>();

        if (_animator == null )
        {
            Debug.LogError("Anim is null");
        }

        if (_audioSource == null)
        {
            Debug.LogError("Audio source is null");
        }

        StartCoroutine(LaserFire());
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
            if (_player != null )
            {
                _player.AddScore(10);
            }

            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
            _audioSource.Play();
            
        }

        if (other.tag == "Player")
        {
            Player player = other.GetComponent<Player>();
            player.Damage();
            _animator.SetTrigger("OnEnemyDeath");
            _speed = 0;
            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.6f);
            _audioSource.Play();
            //other.transform.GetComponent<Player>().Damage();
            
        }
    }

    //coroutine
    //random timer to wait
    // instantiate laser

    IEnumerator LaserFire()
    {
        yield return new WaitForSeconds(Random.Range(2f,4.5f));
        Instantiate(_laser, transform.position + _laserPosition, Quaternion.identity);
    }


}

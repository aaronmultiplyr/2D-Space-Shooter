using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Player : MonoBehaviour

{
    [SerializeField] private int _speed = 1;
    private float _xOutLeft = -19.5449f;
    private float _xOutRight = 19.5449f;
    private float _yOutUp = 10.83237f;
    private float _yOutDown = -10.83237f;
    public float horizontalInput;
    public float verticalInput;
    [SerializeField] private GameObject _laserPrefab;
    [SerializeField] private GameObject _tripleShotPrefab;
    [SerializeField] private bool _isTripleShotActive;
    [SerializeField] private Vector3 laserOffset = new Vector3(0, 1, 0);
    [SerializeField] private Vector3 _triplelaserOffset = new Vector3(2, 1, 0);
    [SerializeField] private float _fireRate = 0.5f;
    [SerializeField] private float _nextFire = 0.0f;
    [SerializeField] private int _lives = 3;
    private SpawnManager _spawnManager;
    [SerializeField] private bool _speedUpActive;
    [SerializeField] private bool _isShieldActive;
    [SerializeField] private GameObject _shieldVisualizer;
    [SerializeField] private int _score;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private GameObject _leftEngine;
    [SerializeField] private GameObject _rightEngine;
    [SerializeField] private AudioClip _laserSoundClip;
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _explsionSoundClip;
    // Start is called before the first frame update
    void Start()
    {
        //gives player object a starting position = new position (0,0,0)
        transform.position = new Vector3(0,0,0);
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _audioSource = GetComponent<AudioSource>();

        if( _audioSource == null )
        {
            Debug.LogError("Audio source is not working");
        }
        

        if (_uiManager == null )
        {
            Debug.Log("The UI Manager is Null");
        }

        else
        {
            _audioSource.clip = _laserSoundClip;
        }
    }

    // Update is called once per frame
    void Update()
    {

        CalculateMovement();
        FireLaser();
        
       

    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log($"Hit {other.transform.name}");

        if (other.tag == "Enemy Laser")
        {

            Destroy(other.gameObject);
           
        }
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
    void FireLaser()
    {
        //if I hold down the space key, fire a laser bolt

        if (Input.GetKey(KeyCode.Space) && Time.time > _nextFire)
        {
            _nextFire = Time.time + _fireRate;
            if (_isTripleShotActive == true)
            {
                Instantiate(_tripleShotPrefab, transform.position + _triplelaserOffset, Quaternion.identity);
                
            }

            else 
            {
                Instantiate(_laserPrefab, transform.position + laserOffset, Quaternion.identity);
               
            }
            _audioSource.Play();
        }
    }

    public void Damage()
    {
        if (_isShieldActive == true)
        {
            Debug.Log("Shield Hit!");
            Debug.Log($"Lives Left: {_lives} ");
            _shieldVisualizer.SetActive(false);
            _isShieldActive = false;
            return;
        }
        _lives--;
        Debug.Log($"Current lives: {_lives}");
        _uiManager.UpdateLives(_lives);
        if(_lives <= 2 && _lives > 0)
        {
            _leftEngine.SetActive(true);
        }

        if(_lives == 1)
        {
            _rightEngine.SetActive(true);
        }

        if (_lives < 1)
        {
            
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
            
        }
    }

    public void TripleShotActive()
    {
        _isTripleShotActive = true;
        StartCoroutine(TripleShotPowerDown());

    }

    public void SpeedUpActive()
    {
        _speedUpActive = true;
       StartCoroutine(SpeedUpPowerDown());
    }

    public void ShieldActive()
    {
        _isShieldActive = true;
        StartCoroutine(ShieldDown());
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore(_score);
    }

   IEnumerator TripleShotPowerDown()
    {
        if (_isTripleShotActive == true)
        {
            yield return new WaitForSeconds(5f);
            Debug.Log("Triple Shot Has Ended");
            _isTripleShotActive = false;
            
        }
    }

    IEnumerator SpeedUpPowerDown()
    {
        if(_speedUpActive == true)
        {
            _speed = 20;
            yield return new WaitForSeconds(5f);
            _speed = 10;
            _speedUpActive = false;
        }
    }

    IEnumerator ShieldDown()
    {
        if (_isShieldActive == true)
        {
            _shieldVisualizer.SetActive(true);
            Debug.Log("Shield is Active");

            yield return new WaitForSeconds(30);
            _isShieldActive = false;
        }

    }
}

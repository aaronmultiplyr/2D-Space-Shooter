using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TripleShotPowerUp : MonoBehaviour
{
    [SerializeField] private int _speed = 1;
    private Player _player;
    private AudioSource _audioSource;
    [SerializeField] private AudioClip _powerUpSoundClip;
    // Start is called before the first frame update

    //player id system
    //0 = triple shot
    //1 = speed
    //2 = shields

    [SerializeField] private int powerupID;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
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

            AudioSource.PlayClipAtPoint(_powerUpSoundClip, transform.position);

            if (_player != null)
            {
                if (powerupID == 0)
                {
                    _audioSource.Play();
                    Destroy(this.gameObject);
                    _player.TripleShotActive();
                    
                }

                else if (powerupID == 1) 
                {
                    _audioSource.Play();
                    Destroy(this.gameObject);
                    _player.SpeedUpActive();
                    
                }

                else if (powerupID == 2)
                {
                    _audioSource.Play();
                    Destroy(this.gameObject);
                    _player.ShieldActive();
                    
                }


            }
            
        }
    }

   
}

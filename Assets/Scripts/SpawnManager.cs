using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{

    [SerializeField] private GameObject _enemy;
    [SerializeField] private GameObject _enemyContainer;
    //[SerializeField] private GameObject _tripleShotPowerUp;
    [SerializeField] private GameObject[] _powerUps;
    [SerializeField] private GameObject _powerUpContainer;


    private bool _stopSpawning = false;
    
    


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartSpawning()
    {
        
        StartCoroutine(SpawnEnemyRoutine());
        StartCoroutine(SpawnPowerUpRoutine());
    }

    IEnumerator SpawnEnemyRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        while (_stopSpawning == false) 
        {
            float _xRandomRange = Random.Range(-19.67f, 19.67f);
            Vector3 spawnPosition = new Vector3(_xRandomRange, 13.52f, 0);
           


            //Instatiate enemy prefab
            GameObject newEnemy = Instantiate(_enemy,spawnPosition, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            //Instantiate(_enemy, transform.position = new Vector3(_xRandomRange, 13.52f, 0), Quaternion.identity);
            //yeild wait for 5 seconds
            yield return new WaitForSeconds (5.0f);
        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        yield return new WaitForSeconds(1.5f);

        while (_stopSpawning == false)
        {
            float _xRandomRange = Random.Range(-19.67f, 19.67f);
            Vector3 spawnPosition = new Vector3(_xRandomRange, 13.52f, 0);
            int _randomPowerUp = Random.Range(0, 3);
            GameObject newTripleShotPowerUp = Instantiate(_powerUps[_randomPowerUp],spawnPosition, Quaternion.identity);
            newTripleShotPowerUp.transform.parent = _powerUpContainer.transform;
            float _randomTimer = Random.Range(5f, 8f);
            yield return new WaitForSeconds (_randomTimer);

        }
    }
        
    public void OnPlayerDeath()
    {
        _stopSpawning = true;
    }
}

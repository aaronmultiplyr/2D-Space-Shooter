using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private Image _LivesImg;
    [SerializeField] private Sprite[] _liveSprite;
    [SerializeField] private TMP_Text _gameOverText;
    [SerializeField] private TMP_Text _restartText;
    [SerializeField] private GameManager _gameManager;
    void Start()
    {
        _scoreText.text = "Score: " + 0;
        _gameOverText.gameObject.SetActive(false);
        _restartText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        if (_gameManager == null )
        {
            Debug.LogError("Game manager is null");
        }
    }

    
    void Update()
    {
        
    }

    public void UpdateScore(int playerScore)
    {
        _scoreText.text = "Score: " + playerScore.ToString();
    }

    public void UpdateLives(int currentLives)
    {
        _LivesImg.sprite = _liveSprite[currentLives];

        if (currentLives <= 0)
        {
            _gameManager.GameOver();
            _gameOverText.gameObject.SetActive(true);
            StartCoroutine(GameOverFlickerRoutine());
            _restartText.gameObject.SetActive(true);
            
        }
    }

    IEnumerator GameOverFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER";
            yield return new WaitForSeconds(.7f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(.7f);
        }
    }
}

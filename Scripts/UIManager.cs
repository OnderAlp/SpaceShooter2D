using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField]
    private Text _scoreText;
    [SerializeField]
    private Text _bestScoreText;
    [SerializeField]
    private Text _gameoverText;
    [SerializeField]
    private Text _restartText;
    [SerializeField]
    private Image _livesImg;
    [SerializeField]
    private Sprite[] _liveSprites;

    public int score, bestScore = 0;


    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        bestScore = PlayerPrefs.GetInt("bestScore", 0);
        _bestScoreText.text = "Best: " + bestScore;
        _scoreText.text = "Score: " + 0;
        _gameoverText.gameObject.SetActive(false);
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
    }

    public void UpdateScore()
    {
        score += 10;

        _scoreText.text = "Score: " + score;
    }

    public void CheckForBestScore()
    {
        if(score > bestScore)
        {
            bestScore = score;
            PlayerPrefs.SetInt("bestScore", bestScore);
            _bestScoreText.text = "Best: " + bestScore;
        }
    }

    public void UpdateLives(int currentLives)
    {
       _livesImg.sprite = _liveSprites[currentLives];
    }

    public void GameOver()
    {
        _gameManager.GameOver();
        _restartText.gameObject.SetActive(true);
        StartCoroutine(gameOverFlicker());
    }

    IEnumerator gameOverFlicker()
    {
        while (true)
        {
            _gameoverText.gameObject.SetActive(true);
            yield return new WaitForSeconds(0.5f);
            _gameoverText.gameObject.SetActive(false);
            yield return new WaitForSeconds(0.5f);
        }
        
    }
}

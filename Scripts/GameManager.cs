using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] 
    public bool _isCoopMode = false;
    [SerializeField]
    private bool _isGameOver;

    [SerializeField]
    private GameObject _pauseMenuPanel;

    private Animator _pauseAnimator;

    private void Start()
    {
        _pauseAnimator = GameObject.Find("Pause_Menu_Panel").GetComponent<Animator>();
        _pauseAnimator.updateMode =AnimatorUpdateMode.UnscaledTime;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.R) && _isGameOver == true)
        {
            if(_isCoopMode == false)
            {
                SceneManager.LoadScene(1); //Current Game Scene
            }
            else
            {
                SceneManager.LoadScene(2);
            }
            
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ReturnMainMenu();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            _pauseMenuPanel.SetActive(true);
            _pauseAnimator.SetBool("isPaused", true);
            Time.timeScale = 0;

        }

    }

    public void ResumeGame()
    {
        _pauseMenuPanel.SetActive(false);
        Time.timeScale = 1;
    }

    public void ReturnMainMenu()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }

    public void GameOver()
    {
        _isGameOver = true;
        GameObject[] Players = GameObject.FindGameObjectsWithTag("Player");
        foreach (var player in Players)
        {
            Destroy(player.gameObject);
        }
    }
}

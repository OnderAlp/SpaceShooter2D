using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField]
    private float _speed = 5.0f;
    [SerializeField]
    private GameObject _laserPrefab;
    [SerializeField]
    private GameObject _tripleShotPrefab;
    [SerializeField]
    private float _fireRate = 0.5f;
    private float _canFire = -1f;
    [SerializeField]
    private int _lives = 3;
    private bool _tripleShotPowerup = false;
    private bool _speedPowerup = false;
    private bool _shieldPowerup = false;
    private SpawnManager _spawnManager;
    [SerializeField]
    private GameObject _shieldVisualizer;
    [SerializeField]
    private GameObject _rightEngine;
    [SerializeField]
    private GameObject _leftEngine;

    private int _score;

    private UIManager _uiManager;
    private GameManager _gameManager;

    [SerializeField]
    private AudioClip _laserSoundClip;
    //[SerializeField]
    //private AudioClip _explosionSoundClip;
    private AudioSource _soundSource;
    [SerializeField]
    private GameObject _explosionPrefab;

    public bool _isPlayerOne = false;
    public bool _isPlayerTwo = false;

    private Animator _animator; 

    // Start is called before the first frame update
    void Start()
    {
        
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        _uiManager = GameObject.Find("Canvas").GetComponent<UIManager>();
        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();
        _soundSource = GetComponent<AudioSource>();
        _animator = GetComponent<Animator>();
        _soundSource.clip = _laserSoundClip;

        if (_gameManager._isCoopMode == false)
        {
            transform.position = new Vector3(0, 0, 0);
        }

        if(_spawnManager == null)
        {
            Debug.LogError("The Spawn Manager is Null");
        }

        if (_uiManager == null)
        {
            Debug.LogError("The UI Manager is Null");
        }

    }


    // Update is called once per frame
    void Update()
    {
        if(_isPlayerOne == true)
        {
            CalculateMovementP1();

            if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
            {
                _animator.SetTrigger("_turnRight");
                _animator.ResetTrigger("_turnLeft");
            }
            else if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                _animator.ResetTrigger("_turnRight");
                _animator.ResetTrigger("_turnLeft");
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A))
            {
                _animator.SetTrigger("_turnLeft");
                _animator.ResetTrigger("_turnRight");
            }
            else if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                _animator.ResetTrigger("_turnLeft");
                _animator.ResetTrigger("_turnRight");
            }

            if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
        
        if(_isPlayerTwo == true)
        {
            CalculateMovementP2();

            if (Input.GetKeyDown(KeyCode.Keypad6))
            {
                _animator.SetTrigger("_turnRight");
                _animator.ResetTrigger("_turnLeft");
            }
            else if (Input.GetKeyUp(KeyCode.Keypad6))
            {
                _animator.ResetTrigger("_turnRight");
                _animator.ResetTrigger("_turnLeft");
            }
            else if (Input.GetKeyDown(KeyCode.Keypad4))
            {
                _animator.SetTrigger("_turnLeft");
                _animator.ResetTrigger("_turnRight");
            }
            else if (Input.GetKeyUp(KeyCode.Keypad4))
            {
                _animator.ResetTrigger("_turnLeft");
                _animator.ResetTrigger("_turnRight");
            }

            if (Input.GetKeyDown(KeyCode.Keypad0) && Time.time > _canFire)
            {
                FireLaser();
            }
        }
        
    }

    void CalculateMovementP1()
    {
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        if(_speedPowerup == true)
        {
            transform.Translate(direction * _speed * 2 * Time.deltaTime);
        }
        else
        {
            transform.Translate(direction * _speed * Time.deltaTime);
        }
        

        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void CalculateMovementP2()
    {
        
        if (_speedPowerup == true)
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * _speed * 2 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad5))
            {
                transform.Translate(Vector3.down * _speed * 2 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * _speed * 2 * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * _speed * 2 * Time.deltaTime);
            }
        }
        else
        {
            if (Input.GetKey(KeyCode.Keypad8))
            {
                transform.Translate(Vector3.up * _speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad5))
            {
                transform.Translate(Vector3.down * _speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad6))
            {
                transform.Translate(Vector3.right * _speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.Keypad4))
            {
                transform.Translate(Vector3.left * _speed * Time.deltaTime);
            }
        }


        transform.position = new Vector3(transform.position.x, Mathf.Clamp(transform.position.y, -3.9f, 0), 0);

        if (transform.position.x > 11.3f)
        {
            transform.position = new Vector3(-11.3f, transform.position.y, 0);
        }
        else if (transform.position.x < -11.3f)
        {
            transform.position = new Vector3(11.3f, transform.position.y, 0);
        }
    }

    void FireLaser()
    {
        _canFire = _fireRate + Time.time;
        if(_tripleShotPowerup == true)
        {
            Instantiate(_tripleShotPrefab, transform.position, Quaternion.identity);
            
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.05f, 0), Quaternion.identity);
        }

        _soundSource.Play();
    }

    public void Damage()
    {
        if(_shieldPowerup == true)
        {
            _shieldPowerup = false;
            _shieldVisualizer.SetActive(false);
            return;
        }

        _lives--;

        if(_lives == 2)
        {
            _rightEngine.SetActive(true);
        }
        if (_lives == 1)
        {
            _leftEngine.SetActive(true);
        }

        _uiManager.UpdateLives(_lives);

        if (_lives < 1)
        {
            _uiManager.GameOver();
            _uiManager.CheckForBestScore();
            _spawnManager.OnPlayerDeath();
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);

            Destroy(this.gameObject);
        }
    }

    public void TripleShotPowActivation()
    {
        _tripleShotPowerup = true;
        StartCoroutine(TripleShotPow());
    }
    IEnumerator TripleShotPow()
    {
        yield return new WaitForSeconds(5.0f);
        _tripleShotPowerup = false;
    }
    public void SpeedPowActivation()
    {
        _speedPowerup = true;
        StartCoroutine(SpeedPow());
    }
    IEnumerator SpeedPow()
    {
        yield return new WaitForSeconds(5.0f);
        _speedPowerup = false;
    }

    public void ShieldActive()
    {
        _shieldPowerup = true;
        _shieldVisualizer.SetActive(true);
    }

    public void AddScore(int points)
    {
        _score += points;
        _uiManager.UpdateScore();
    }

}

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Enemy : MonoBehaviour
{
    private float _enemySpeed = 4.0f;

    //private Player _player;

    private Animator _animator;

    private AudioSource _audioSource;
    [SerializeField]
    private GameObject _laserPrefab;
    private GameManager _gameManager;
    private Player _player;

    private float _fireRate = 3.0f;
    private float _canFire = -1.0f;

    // Start is called before the first frame update
    void Start()
    {

        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _player = GameObject.FindWithTag("Player").GetComponent<Player>();


        transform.position = new Vector3(Random.Range(-10.0f, 10.0f), 10, 0);

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if(Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 5f);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);
            Laser[] lasers = enemyLaser.GetComponentsInChildren<Laser>();

            for(int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();
            }

        }
    }

    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _enemySpeed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            float randomX = Random.Range(-10f, 10f);
            transform.position = new Vector3(randomX, 10, 0);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        

        if(other.tag == "Player")
        {
            _enemySpeed = 0;
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
            }

            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
        else if(other.tag == "Laser")
        {
            _enemySpeed = 0;
            Destroy(other.gameObject);

            if(_player != null)
            {
                _player.AddScore(10);
            }

            _animator.SetTrigger("OnEnemyDeath");
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());
            Destroy(this.gameObject, 2.8f);
        }
    }
}

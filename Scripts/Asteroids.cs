using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using static UnityEditor.Experimental.GraphView.GraphView;

public class Asteroids : MonoBehaviour
{
    private int _rotationSpeed = 1;
    [SerializeField]
    private GameObject _explosionPrefab;
    private SpawnManager _spawnManager;

    // Start is called before the first frame update
    void Start()
    {
        _spawnManager = GameObject.Find("Spawn_Manager").GetComponent<SpawnManager>();
        
    }

    // Update is called once per frame
    void Update()
    {
        gameObject.transform.Rotate(0, 0, _rotationSpeed); 
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.tag == "Laser")
        {
            Instantiate(_explosionPrefab, transform.position, Quaternion.identity);
            Destroy(other.gameObject);
            _spawnManager.StartSpawning();
            Destroy(this.gameObject, 0.25f);
        }
    }
}

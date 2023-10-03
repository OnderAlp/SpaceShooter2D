using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Powerups : MonoBehaviour
{
    private float _speed = 2f;
    [SerializeField]
    private int _powerupID;
    [SerializeField]
    private AudioClip _clip;


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        if (transform.position.y < -5f)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position);

            switch (_powerupID)
            {
                case 0:
                    player.TripleShotPowActivation();
                    break;
                case 1:
                    player.SpeedPowActivation();
                    break;
                case 2:
                    player.ShieldActive();
                    break;
                default:
                    break;
                    
            }

            Destroy(this.gameObject);
        }
    }
}

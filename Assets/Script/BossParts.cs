using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class BossParts : MonoBehaviour
{
    [SerializeField] GameObject[] _bullets = new GameObject[4];
    [SerializeField] PlayerController _player;
    [SerializeField] Transform[] _muzzles = new Transform[4];
    [SerializeField] public int _HP = 400;
    public BoxCollider _box;
    float _timer = 2.0f;
    float _limit = 0.0f;
    void Start()
    {
        _box = GetComponent<BoxCollider>();
    }

   
    void Update()
    {
        if (_box.enabled && _HP > 0)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _player.ShotBullet(transform.position);
            }
            Debug.Log(_box.enabled);
            _timer -= Time.deltaTime;
            if (_timer <= _limit)
            {
                float lifetime = 3.0f;
                GameObject[] bulletobj = new GameObject[4];
                bulletobj[0] = Instantiate(_bullets[0], _muzzles[0].transform.position, Quaternion.identity);
                bulletobj[1] = Instantiate(_bullets[1], _muzzles[1].transform.position, Quaternion.identity);
                bulletobj[2] = Instantiate(_bullets[2], _muzzles[2].transform.position, Quaternion.identity);
                bulletobj[3] = Instantiate(_bullets[3], _muzzles[3].transform.position, Quaternion.identity);

                bulletobj[0].GetComponent<Rigidbody>().velocity = _player.transform.position - bulletobj[0].transform.position;
                bulletobj[1].GetComponent<Rigidbody>().velocity = _player.transform.position - bulletobj[1].transform.position;
                bulletobj[2].GetComponent<Rigidbody>().velocity = _player.transform.position - bulletobj[2].transform.position;
                bulletobj[3].GetComponent<Rigidbody>().velocity = _player.transform.position - bulletobj[3].transform.position;
                foreach (var bullet in bulletobj)
                {
                    Destroy(bullet, lifetime);
                }
                _timer = 1.0f;
            }
        }
        else
        {
            Debug.Log(_box.enabled);
        }
    }
    private void OnMouseUpAsButton()
    {
        if (_box.enabled && _HP != 0)
        {
            _player.ShotBullet(transform.position);
            Debug.Log("Shoot");
        }
    }

    void OnTriggerEnter(Collider collision)
    {
       if(collision.gameObject.tag == "Bullet")
        {
            if (_HP > 0)
            {
                _HP -= 10;
                var sceneManager = FindObjectOfType<SceneManager>();
                sceneManager.AddScore(1000);
            }
            else if(_HP <= 0)
            {
                _HP = 0;
                _box.enabled = false;
            }
            Destroy(collision.gameObject);
        } 
    }
}

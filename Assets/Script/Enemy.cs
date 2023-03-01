using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] float Speed = 10f;
    [SerializeField] float DeadSecond = 10f;
    [SerializeField] int Life = 10;

    float _time;
    float _bulletime = 1.0f;
    [SerializeField] PlayerController _player;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    void Start()
    {
        _time = 0f;
    }

    void Update()
    {
        transform.LookAt(_player.transform);
        _bulletime -= Time.deltaTime;
        if(_time <= 0)
        {
            Instantiate(bullet, muzzle.transform.position, Quaternion.identity);
            _time = 1.0f;
        }

        _time += Time.deltaTime;
        if(_time > DeadSecond)
        {
           Destroy(gameObject);
        }
        else
        {
            var vec = _player.transform.position - transform.position;
            transform.position += vec.normalized * Speed * Time.deltaTime;
        }
    }
    private void OnMouseUpAsButton()
    {
        _player.ShotBullet(transform.position);
        Debug.Log("Shoot");
    }

    private void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "Bullet")
        {
            Life -= 10;
            if(Life == 0)
            {
                Destroy(gameObject);
                var sceneManager = FindObjectOfType<SceneManager>();
                sceneManager.AddScore(1000);
            }
        }
    }
}

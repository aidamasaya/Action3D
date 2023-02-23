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
    PlayerController _player;
    void Start()
    {
        _time = 0f;
        _player = FindObjectOfType<PlayerController>();
    }

    void Update()
    {
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
            if(Life <= 0)
            {
                Destroy(gameObject);
                var sceneManager = FindObjectOfType<SceneManager>();
                sceneManager.AddScore(1000);
            }
        }
    }
}

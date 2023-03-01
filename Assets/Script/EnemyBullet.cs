using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    float lifetime = 3f;
    PlayerController _player;
    Rigidbody _rb;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        Debug.Log("bullet");
        _player = FindObjectOfType<PlayerController>();
        var vec = _player.transform.position - transform.position;
        transform.position +=  vec * _speed * Time.deltaTime;
        Destroy(this.gameObject, lifetime);
    }
}

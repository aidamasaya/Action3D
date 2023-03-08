using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    float lifetime = 3f;
    Rigidbody _rb;
    PlayerController _player;
    void Update()
    {
        Debug.Log("Shoot");
        _player = GameObject.FindObjectOfType<PlayerController>();
        Vector3 vec = _player.transform.position - transform.position;
        vec = vec.normalized * _speed;
        transform.LookAt(_player.transform);

        _rb = GetComponent<Rigidbody>();
        _rb.velocity = vec;
        Destroy(this.gameObject, lifetime);
    }
}

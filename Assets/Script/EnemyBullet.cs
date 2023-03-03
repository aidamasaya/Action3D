using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBullet : MonoBehaviour
{
    [SerializeField] float _speed = 10f;
    float lifetime = 3f;
    Rigidbody _rb;
    void Update()
    {
        Debug.Log("Shoot");
        GameObject _player = GameObject.FindGameObjectWithTag("Player");
        Vector3 vec = _player.transform.position - transform.position;
        vec = vec.normalized * _speed;

        _rb = GetComponent<Rigidbody>();
        _rb.velocity = vec;
        Destroy(this.gameObject, lifetime);
    }
}

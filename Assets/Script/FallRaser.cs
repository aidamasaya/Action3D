using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallRaser : MonoBehaviour
{
    Rigidbody _rb;
    float _gravity = 4.0f;
    Vector3 _vec = new Vector3(0, 0, 0);
    float _lifetime = 3.0f;
    void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _vec = _rb.velocity;
        _vec.y -= transform.position.y * _gravity;
        _rb.velocity = _vec;
        Destroy(this.gameObject, _lifetime * 20); //ë∂ç›Ç≈Ç´ÇÈéûä‘
    }
}

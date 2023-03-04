using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animation : MonoBehaviour
{
    Animator _anim;
    void Start()
    {
        _anim = GetComponent<Animator>();
    }

    
    void Update()
    {
        if(this.gameObject.activeSelf)
        {
            _anim.SetBool("Move", true);
        }
    }
}

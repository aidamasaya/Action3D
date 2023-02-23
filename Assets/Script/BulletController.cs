using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    [Range(0, 10)]
    [SerializeField] float DeadSecond = 3f;
    [Range(0, 200)]
    [SerializeField] float Speed = 100f;

    public Vector3 StartPos { get; set; }
    public Vector3 TargetPos { get; set; }

    public void Init(Vector3 startpos, Vector3 targetpos)
    {
        StartPos = startpos;
        TargetPos = targetpos;
        StartCoroutine(Move());
    }

    IEnumerator Move()
    {
        float time = 0f;
        transform.position = StartPos;
        var vec = (TargetPos - StartPos).normalized;
        while (time < DeadSecond)
        {
            time += Time.deltaTime;
            transform.position += vec * Speed * Time.deltaTime;
            yield return null;
        }

        Destroy(this.gameObject);
    }
}

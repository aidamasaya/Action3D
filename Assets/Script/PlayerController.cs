using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("ポイント地点"), SerializeField] Transform[] RoutePoints;
    bool _isHitRoutePoint;

    [Range(0,50)]
    [SerializeField] float Speed = 10f;
    [SerializeField] float MoveSpeed = 10f; //速度
    [SerializeField] float MoveRange = 40f; //操作範囲

   IEnumerator Move()
    {
        var prevPointPos = transform.position;
        var basePosition = transform.position;
        var movedPos = Vector2.zero;

        foreach(var nextPoint in RoutePoints)
        {
            _isHitRoutePoint = false;

            while (!_isHitRoutePoint)
            {
                //進行方向の計算
                var vec = nextPoint.position - prevPointPos;
                vec.Normalize();

                //プレイヤーの移動
                basePosition += vec * Speed * Time.deltaTime;

                //上下左右に移動する処理
                // 行列によるベクトルの変換
                movedPos.x += Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
                movedPos.y += Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
                movedPos = Vector2.ClampMagnitude(movedPos, MoveRange);
                var worldMovePos = Matrix4x4.Rotate(transform.rotation).MultiplyVector(movedPos);

                //ルート上の位置に上下左右の移動量を加えている
                transform.position = basePosition + worldMovePos;

                //次の処理では進行方向を向くように計算している
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vec, Vector3.up), 0.5f);

                yield return null;
            }
            prevPointPos = nextPoint.position;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if(collider.gameObject.tag == "RoutePoint")
        {
            collider.gameObject.SetActive(false);
            _isHitRoutePoint = true;
        }
    }

    void Start()
    {
        StartCoroutine(Move());
    }
}

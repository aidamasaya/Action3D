using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [Header("�|�C���g�n�_"), SerializeField] Transform[] RoutePoints;
    bool _isHitRoutePoint;

    [Range(0,50)]
    [SerializeField] float Speed = 10f;
    [SerializeField] float MoveSpeed = 10f; //���x
    [SerializeField] float MoveRange = 40f; //����͈�

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
                //�i�s�����̌v�Z
                var vec = nextPoint.position - prevPointPos;
                vec.Normalize();

                //�v���C���[�̈ړ�
                basePosition += vec * Speed * Time.deltaTime;

                //�㉺���E�Ɉړ����鏈��
                // �s��ɂ��x�N�g���̕ϊ�
                movedPos.x += Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
                movedPos.y += Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
                movedPos = Vector2.ClampMagnitude(movedPos, MoveRange);
                var worldMovePos = Matrix4x4.Rotate(transform.rotation).MultiplyVector(movedPos);

                //���[�g��̈ʒu�ɏ㉺���E�̈ړ��ʂ������Ă���
                transform.position = basePosition + worldMovePos;

                //���̏����ł͐i�s�����������悤�Ɍv�Z���Ă���
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

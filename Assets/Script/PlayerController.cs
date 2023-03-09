using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using UniRx.Triggers;
using System;

public class PlayerController : MonoBehaviour
{
    [Header("�|�C���g�n�_"), SerializeField] Transform[] RoutePoints;
    bool _isHitRoutePoint;

    [Range(0,200)]
    [SerializeField] float Speed = 10f;
    [SerializeField] float Xlimit = 8.5f;
    [SerializeField] float Ylimit = 4.5f;
    [SerializeField] float Zlimit = 168.0f;

    [Range(0,200)]
    [SerializeField] float MoveSpeed = 10f; //���x
    [SerializeField] float MoveRange = 5.0f; //����͈�

    [SerializeField] public float _initialLife = 100;
    [SerializeField] public float _Life = 100;

    [SerializeField] BulletController bulletpre;

    int count = 0;
    AnimationHPBar _animHP;
    [SerializeField] PlayerSwitcher _playerSwitcher;
    [SerializeField] GameManager _manager;
    Animator _anim;
    [SerializeField] GameObject _score;
    [SerializeField] GameObject _text;
    public void Start()
    {
        StartCoroutine(Move());
        _anim = GetComponent<Animator>();
        _animHP = GetComponent<AnimationHPBar>();
        _animHP.SetPlayer(this);
    }

    IEnumerator Move()
   {
        Debug.Log("Movement");
        var prevPointPos = transform.position;
        var basePosition = transform.position;
        var movedPos = Vector2.zero;

        foreach (var nextPoint in RoutePoints)
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
                //�s��ɂ��x�N�g���̕ϊ�
                movedPos.x += Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
                movedPos.y += Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
                movedPos = Vector2.ClampMagnitude(movedPos, MoveRange);
                var worldMovePos = Matrix4x4.Rotate(transform.rotation).MultiplyVector(movedPos);

                //���[�g��̈ʒu�ɏ㉺���E�̈ړ��ʂ������Ă���
                transform.position = basePosition + worldMovePos;

            //���̏����ł͐i�s�����������悤�Ɍv�Z���Ă���
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(vec, Vector3.up), 0.1f);

                yield return null;
            }
            prevPointPos = nextPoint.position;
        }
    }

    void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "RoutePoint")
        {
            collider.gameObject.SetActive(false);
            _isHitRoutePoint = true;
            count++;
        }
        else if (collider.gameObject.tag == "Enemy" || collider.gameObject.tag == "EnemyBullet")
        {
            StartCoroutine(Animation());
            _animHP.GaugeReduction(10);
            _Life -= 10f;

            collider.gameObject.SetActive(false);
            Destroy(collider.gameObject);
            if (_Life <= 0)
            {
                Camera.main.transform.SetParent(null);
                gameObject.SetActive(false);
                _score.SetActive(false);
                _text.SetActive(false);
                _manager.ShowGameOver();
            }
        }

        if (collider.gameObject.tag == "BackGround")
        {
            _animHP.GaugeReduction(10);
            _Life = 0;

            if (_Life <= 0)
            {
                Camera.main.transform.SetParent(null);
                _animHP.GreenGauge.gameObject.SetActive(false);
                _animHP.RedGauge.gameObject.SetActive(false);
                gameObject.SetActive(false);
                _score.SetActive(false);
                _text.SetActive(false);
                _manager.ShowGameOver();
            }
        }
    }

    void Update()
    {
        if (count == 6)
        {
            float x = Input.GetAxis("Horizontal") * MoveSpeed * Time.deltaTime;
            float y = Input.GetAxis("Vertical") * MoveSpeed * Time.deltaTime;
            transform.Translate(new Vector3(x, y, 0));

            Vector3 currentPos = transform.position;
            currentPos.x = Mathf.Clamp(currentPos.x, -Xlimit + Xlimit, Xlimit);
            currentPos.y = Mathf.Clamp(currentPos.y, -Ylimit + 80, Ylimit);
            currentPos.z = Mathf.Clamp(currentPos.z, Zlimit, Zlimit);

            transform.position = currentPos;
        }
    }
    public void ShotBullet(Vector3 targetpos)
    {
        var bullet = Instantiate(bulletpre, transform.position, Quaternion.identity);
        bullet.Init(transform.position, targetpos);
    }

    IEnumerator Animation()
    {
        _anim.SetBool("Damage", true);
        yield return new WaitForSeconds(1.0f);
        _anim.SetBool("Damage", false);
    }
}

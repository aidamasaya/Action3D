using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Range(0, 100)]
    [SerializeField] public float Speed = 10f;
    [SerializeField] public float DeadSecond = 10f;
    [SerializeField] public int Life = 10;

    public float _time;
    public float _bulletime = 1.0f;
    [SerializeField] PlayerController _player;
    [SerializeField] GameObject bullet;
    [SerializeField] Transform muzzle;
    [SerializeField] AddScoreController _addScore;
    void Start()
    {
        _time = 0f;
    }

    void Update()
    {
        /*if (Input.GetKeyDown(KeyCode.Space))
        {
            _player.ShotBullet(transform.position);
        }*/
        transform.LookAt(_player.transform);
        _bulletime -= Time.deltaTime;
        if(_bulletime <= 0)
        {
            BallShot();
            _bulletime = 1.0f;
        }

        _time += Time.deltaTime;
        if(_time > DeadSecond)
        {
           this.gameObject.SetActive(false);
        }
        else
        {
            var vec = _player.transform.position - transform.position;
            transform.position += vec.normalized * Speed * Time.deltaTime;
        }
    }
    public virtual void BallShot()
    {
        float lifetime = 3.0f; 
        GameObject shotobj = Instantiate(bullet, muzzle.position, Quaternion.identity);
        shotobj.GetComponent<Rigidbody>().velocity = _player.transform.position - shotobj.transform.position;
        Destroy(shotobj,lifetime);
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
            _addScore.AddScore(1000);
            if (Life == 0)
            {
                Destroy(this.gameObject);
            }
            Destroy(collider.gameObject);
        }
    }
}

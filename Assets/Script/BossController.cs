using System.Collections;
using System.Collections.Generic;
using UniRx.Triggers;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
/// <summary>
/// ボスコンポーネント。一定時間たつと攻撃する所が変わる、HPが0になったら消える
/// </summary>
public class BossController : MonoBehaviour
{
    [SerializeField] int _HP = 400;
    float _currentTime = 3.0f;
    float _stateTime = 0.0f;
    List<int> _ran = new List<int>();
    int _random = 0;
    float lifetime = 2.0f;
    [SerializeField] PlayerController _player;
    [SerializeField] List<Transform> _muzzles = new List<Transform>();
    [SerializeField] List<GameObject> _bullets = new List<GameObject>();
    [SerializeField] BossParts[] _parts = new BossParts[3];
    [SerializeField] AddScoreController _addScore;
    Material _material;
    [SerializeField] GameManager _manager;
    void Start()
    {
        _material = GetComponent<Material>();
        _ran.Add(0);
        _ran.Add(1);
        _ran.Add(2);
        _ran.Add(3);
    }

    void Update()
    {
        transform.LookAt(_player.transform.position);

        if (_parts[0]._HP != 0 || _parts[1]._HP != 0 || _parts[2]._HP != 0)
        {
            _currentTime -= Time.deltaTime;
            if (_currentTime <= _stateTime)
            {
                _random = Random.Range(_ran[0],_ran[3]);
                Debug.Log(_ran);
                _currentTime = 3.0f;
            }

            switch (_random)
            {
                case 0:
                    if(_parts[0]._HP == 0)
                    {
                        _random = 1;
                        break;
                    }
                    _parts[0]._box.enabled = true;
                    _parts[1]._box.enabled = false;
                    _parts[2]._box.enabled = false;
                    break;
                case 1:
                    if(_parts[1]._HP == 0)
                    {
                        _random = 2;
                        break;
                    }
                    _parts[1]._box.enabled = true;
                    _parts[2]._box.enabled = false;
                    _parts[0]._box.enabled = false;
                    break;
                case 2:
                    if(_parts[2]._HP == 0)
                    {
                        _random = 0;
                        break;
                    }
                    _parts[2]._box.enabled = true;
                    _parts[0]._box.enabled = false;
                    _parts[1]._box.enabled = false;
                    break;
            }
        }
        else
        {
            RaserBeam();            
            if (Input.GetKeyDown(KeyCode.Space))
            {
                _player.ShotBullet(transform.position);
            }
        }

        if(_HP <= 0)
        {
            gameObject.SetActive(false);
            _addScore.AddScore(10000);
            Invoke("Call", 3f);
        }
    }
    private void OnMouseUpAsButton()
    {
        _player.ShotBullet(transform.position);
        Debug.Log("Shoot");
    }

    void RaserBeam()
    {
        Debug.Log("Raser");
        GameObject[] bulletobj = new GameObject[2];
        for (int i = 0; i < bulletobj.Length; i++)
        {
            bulletobj[i] = Instantiate(_bullets[0], _muzzles[i].transform.position, Quaternion.identity);
            bulletobj[i].GetComponent<Rigidbody>().velocity = _player.transform.position - bulletobj[i].transform.position;
        }
        foreach (var bullet in bulletobj)
        {
            Destroy(bullet, lifetime);
        }
    }
    void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.tag == "Bullet")
        {
            _HP -= 10;
            _addScore.AddScore(1000);
            Destroy(collision.gameObject);
        }
    }

    void Call()
    {
        _manager.Finish();
    }
}

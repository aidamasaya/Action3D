using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlightEnemy : Enemy
{
    [SerializeField] int num = 3;
    [SerializeField] GameObject[] _bullets = new GameObject[3];
    [SerializeField] Transform[] _muzzles = new Transform[3];
    [SerializeField] PlayerController _prayer;
    public override void BallShot()
    {
        float lifetime = 3.0f;
        GameObject[] bulletobj = new GameObject[3];
        for (int i = 0; i < _bullets.Length; i++) {
            bulletobj[i] = Instantiate(_bullets[i], _muzzles[i].transform.position, Quaternion.identity);
            bulletobj[i].GetComponent<Rigidbody>().velocity = _prayer.transform.position - bulletobj[i].transform.position;
        }
        foreach(var bullet in bulletobj) { 
        Destroy(bullet,lifetime);
    }
  }
    private void OnTriggerEnter(Collider collider)
    {
        if (collider.gameObject.tag == "Bullet")
        {
            Life -= 10;
            if (Life == 0)
            {
                Destroy(this.gameObject);
                var sceneManager = FindObjectOfType<SceneManager>();
                sceneManager.AddScore(1000);
            }
            Destroy(collider.gameObject);
        }
    }
}

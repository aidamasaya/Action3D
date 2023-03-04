using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// 敵や障害物のアクティブを制御するコンポーネント
/// </summary>
public class EnemyArea : MonoBehaviour
{
    public GameObject[] EnemyList;

    void Start()
    {
       foreach(var enemy in EnemyList)
        {
            enemy.SetActive(false);
        } 
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            foreach(var enemy in EnemyList)
            {
                Debug.Log("Active");
                enemy.SetActive(true);
            }

            var collider = GetComponent<BoxCollider>();
            collider.enabled = false;
        }
    }
}

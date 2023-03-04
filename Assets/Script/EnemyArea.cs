using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// �G���Q���̃A�N�e�B�u�𐧌䂷��R���|�[�l���g
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

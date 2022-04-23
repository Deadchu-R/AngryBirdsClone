using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField]
    float HP = 10;
    float currentHP;
    [SerializeField]
    float birdDamage;
    [SerializeField]
    float platformDamage;
    [SerializeField]
    float fallDamage;

    // Start is called before the first frame update
    void Start()
    {
        currentHP = HP;
        Debug.Log("HP" + currentHP);
        birdDamage = HP / 2;
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("bird"))
        {
            currentHP -= birdDamage;
            Debug.Log("GOT BIRD DAMAGE HP IS:" + currentHP) ;
        }

        if (coll.gameObject.CompareTag("platform"))
        {
            currentHP -= platformDamage;
            Debug.Log("GOT pLATFORM DAMAGE HP IS:" + currentHP);
        }
    }

    // Update is called once per frame
    void Update()
    {
         if (currentHP <= 0)
         {
            Destroy(gameObject);
         }

    }
}

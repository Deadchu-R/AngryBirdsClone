using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    [SerializeField]
    float HP = 2;
    float currentHP;

    private void Start()
    {
        currentHP = HP;
    }
    private void OnCollisionEnter2D(Collision2D coll)
    {
       if (coll.gameObject.CompareTag("bird"))
       {
            currentHP -= 1;
       }
    }
    void Update()
    {
        if (currentHP <= 0)
        {
            Destroy(gameObject);
        }

    }
}

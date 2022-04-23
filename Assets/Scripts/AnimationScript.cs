using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator birdsAni1;
    public Animator birdsAni2;
    public Animator birdsAni3;
    public GameObject[] birds = new GameObject[3];

    // Update is called once per frame
    void Update()
    {

        if (birds[0].activeSelf == false)
        {
            birdsAni2.SetBool("isDead", true);
        }
        if (birds[1].activeSelf == false)
        {
            birdsAni3.SetBool("isDead", true);
        }

    }
}

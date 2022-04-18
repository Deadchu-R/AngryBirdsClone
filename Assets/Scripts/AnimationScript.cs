using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationScript : MonoBehaviour
{
    public Animator birdsAni1;
    public Animator birdsAni2;
    public Animator birdsAni3;
    public GameObject[] birds = new GameObject[2];
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        if (birds[0] == null)
        {
            birdsAni2.SetBool("isDead", true);
        }
        if (birds[1] == null)
        {
            birdsAni3.SetBool("isDead", true);
        }

    }
}

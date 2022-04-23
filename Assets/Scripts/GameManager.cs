using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

public class GameManager : MonoBehaviour
{
    bool win = false;
    bool lost = false;
    public GameObject[] birds;
    public List<Animator> animList;
    private int birdsCount;

    private void Start()
    {
        birds = GameObject.FindGameObjectsWithTag("bird");
        for (int i = 0; i < birds.Length; i++)
        {
            animList.Add(birds[i].GetComponent<Animator>());
        }
        birdsCount = birds.Length;
    }
    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < birds.Length; i++) // checking all birds
        {
            if (birds.All(birds => birds.activeSelf == false)) // if all birds are not active set lost to true
            {
                lost = true;
            }

        }

        birdsCount = birds.Length;
        if (birds[0].activeSelf == false)
        {
            animList[1].SetBool("isDead", true);
        }
        if (birds[1].activeSelf == false)
        {
            animList[2].SetBool("isDead", true);
        }

        if (win == true)
        {
            SceneManager.LoadScene("Win");
        }
        if (lost == true)
        {
            SceneManager.LoadScene("Lose");
        }

    }
}


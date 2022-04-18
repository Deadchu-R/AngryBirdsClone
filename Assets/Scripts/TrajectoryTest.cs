using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrajectoryTest : MonoBehaviour
{
    public LineRenderer TrajectoryLineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        TrajectoryLineRenderer.sortingLayerName = "Foreground";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

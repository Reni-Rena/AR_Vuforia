using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleManager : MonoBehaviour
{
    public GameObject Image1 = null;
    public GameObject Image2 = null;
    Vector3 dist;
    float mod = 0;

    void Update()
    {
        markDist SpherePosition = Image1.GetComponent<markDist>();
        markDist CubePosition = Image2.GetComponent<markDist>();

        dist = SpherePosition.delta - CubePosition.delta;

        if (SpherePosition.goIn == true && CubePosition.goIn == true)
        {
            mod = dist.magnitude;
            Image1.transform.GetChild(0).gameObject.transform.localScale = new Vector3(0.15f / mod, 0.15f / mod, 0.15f / mod);
            Debug.Log("module " + mod);
        }
    }
}
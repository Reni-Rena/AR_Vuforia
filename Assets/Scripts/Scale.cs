using UnityEngine;

public class Scale : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {

        // si la touche W est appuyee
        if (Input.GetKeyDown(KeyCode.W))
        {
            if (transform.localScale.x < 1 && transform.localScale.y < 1 && transform.localScale.z < 1)
            {
                transform.localScale += new Vector3(0.01f, 0.01f, 0.01f);
            }
        }

        // si la touche X est appuyee
        if (Input.GetKeyDown(KeyCode.X))
        {
            if (transform.localScale.x > 0.02 && transform.localScale.y > 0.02 && transform.localScale.z > 0.02)
            {
                // alors faire une transformation scale decrementee sur le gameobject
                transform.localScale -= new Vector3(0.01f, 0.01f, 0.01f);
            }
        }
    }
}

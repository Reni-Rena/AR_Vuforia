using UnityEngine;

public class Buttonmanager : MonoBehaviour
{
    private bool rotate = false;
    // Update is called once per frame
    void Update()
    {
        if (rotate){
            transform.Rotate(Vector3.down, Time.deltaTime * 90);
        }
    }

    public void rotat()
    {
        rotate = !rotate;
    }
    public void scale()
    {
        transform.localScale += new Vector3(0.03f, 0.03f, 0.03f);
    }
}

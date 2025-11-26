using UnityEngine;

public class Move : MonoBehaviour
{
    public float speed;           // vitesse à definir dans l'interface unity
    public float rotateSpeed;     // vitesse de rotation à definir dans l'interface unity

    // Update is called once per frame
    void Update()
    {
        // si la touche flèche du haut appuyée → translation "en avant"
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
        }

        // si la touche flèche du bas appuyée → translation "en arrière"
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.Translate(Vector3.back * Time.deltaTime * speed);
        }

        // si la touche flèche gauche appuyée → rotation sur X
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.Rotate(Vector3.down, Time.deltaTime * rotateSpeed);
        }

        // si la touche flèche droite appuyée → rotation sur X
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up, Time.deltaTime * rotateSpeed);
        }
    }
}

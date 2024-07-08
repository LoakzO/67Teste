using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementScript : MonoBehaviour
{
    [Header("Values")]
    public float movementSpeed;
   // public bool rotate;

    [Header("Animation")]
    public Animator animatorRef;
    public GameObject mesh;
    public string moveParameter;

    Vector3 direction;

    void Start()
    {

    }

    void Update()
    {
        Move();
        RotateMesh();
    }

    void Move()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        direction = movementSpeed * Time.deltaTime * new Vector3(horizontal, 0, vertical);

        transform.Translate(direction);

        if (moveParameter != null && animatorRef != null)
        {
            animatorRef.SetFloat(moveParameter, direction.normalized.magnitude);
        }
    }

    void RotateMesh()
    {
        /*if (rotate)
        {
            
        }*/

        if (direction.magnitude > 0.01)
        {
            float angulo = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            mesh.transform.rotation = Quaternion.Euler(0, angulo, 0);
        }
    }
}

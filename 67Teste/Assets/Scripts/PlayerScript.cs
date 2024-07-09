using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerScript : MonoBehaviour
{
    [Header("Moviment")]
    public float movementSpeed;

    [Header("Punch")]
    public float countDown;
    bool punching;
    public BoxCollider handCollider;

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
        PunchTrigger();
        SetPunchCollider();
    }

    void Move()
    {
        float vertical = Input.GetAxisRaw("Vertical");
        float horizontal = Input.GetAxisRaw("Horizontal");

        direction = movementSpeed * Time.deltaTime * new Vector3(horizontal, 0, vertical);

        transform.Translate(direction);
        AnimateMovement();
    }

    void AnimateMovement()
    {
        if (moveParameter != null && animatorRef != null)
        {
            animatorRef.SetFloat(moveParameter, direction.normalized.magnitude);
        }
    }

    void RotateMesh()
    {
        if (direction.magnitude > 0.01)
        {
            float angulo = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
            mesh.transform.rotation = Quaternion.Euler(0, angulo, 0);
        }
    }

    void PunchTrigger()
    {
        if(!punching)
        {
            if (Input.GetButtonDown("Fire1"))
            {
                StartCoroutine(Punch());
            }
        }
    }

    void SetPunchCollider()
    {
        handCollider.enabled = punching;
    }

    IEnumerator Punch()
    {
        animatorRef.SetTrigger("pPunch");
        punching = true;
        yield return new WaitForSeconds(countDown);
        punching = false;
        StopAllCoroutines();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (punching)
        {
            print("Tome!!");
        }
    }
}

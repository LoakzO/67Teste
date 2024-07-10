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

    [Header("Collect")]
    public bool selling;
    public int maxBodyCount;
    public int money;
    public Transform bodySpot;

    int BodyCount;

    [Header("Animation")]
    public Animator animatorRef;
    public GameObject mesh;
    public string moveParameter;

    [Header("Custom")]
    public Material meshMaterial;

    Vector3 direction;
    Vector3 bodySpotInit;

    void Start()
    {
        bodySpotInit = bodySpot.position;
    }

    void Update()
    {
        Move();
        RotateMesh();
        //PunchTrigger();
        SetPunchCollider();

        print(BodyCount);
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
            StartCoroutine(Punch());
        }
    }

    void SetPunchCollider()
    {
        handCollider.enabled = punching;
    }

    void Carry(Collision collisionRef)
    {
        if(BodyCount < maxBodyCount)
        {
            collisionRef.gameObject.GetComponent<EnemyScript>().carring = true;
            collisionRef.gameObject.transform.position = bodySpot.position;
            collisionRef.gameObject.transform.rotation = bodySpot.rotation;
            collisionRef.gameObject.transform.SetParent(mesh.transform);

            bodySpot.position = new Vector3(bodySpot.position.x, bodySpot.position.y + 0.25f, bodySpot.position.z);
            BodyCount++;
        }
    }

    void ResetBodyValues()
    {
        bodySpot.localPosition = bodySpotInit;
        BodyCount = 0;
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
        if(collision.gameObject.tag == "Enemy")
        {
            if (!punching && collision.gameObject.GetComponent<EnemyScript>().fainted)
            {
                Carry(collision);
            }
            else 
            {
                PunchTrigger();
            }
        }

        if(collision.gameObject.tag == "Sellbox")
        {
            selling = true;
            ResetBodyValues();
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Sellbox")
        {
            selling = false;
        }
    }
}

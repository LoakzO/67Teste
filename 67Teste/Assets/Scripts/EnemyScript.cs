using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [Header("States")]
    public bool fainted;
    public bool carring;

    [Header("Refs")]
    public Transform spot;
    public PlayerScript player;

    [Header("Ragdoll")]
    public Rigidbody[] bonesRb;
    public Collider[] colliders;

    BoxCollider boxCollider; 

    public float maxDistance;
    public Vector3 initialSpot;

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        boxCollider = GetComponent<BoxCollider>();

        DisableRigidBody(true);
    }

    void Update()
    {
        Sell();
        DisableCollider();

        InertiaMovement();
    }

    void Sell()
    {
        if (carring && player.selling)
        {
            player.money++;
            Destroy(gameObject);
        }
    }

    void DisableCollider()
    {
        if (carring)
        {
            boxCollider.enabled = false;
            DisableRigidBody(true);

            foreach(var collider in colliders)
            {
                collider.enabled = false;
            }
        }
    }

    public void DisableRigidBody(bool choice)
    {
        foreach(var rigidbody in bonesRb)
        {
            rigidbody.isKinematic = choice;
        }
    }

    void InertiaMovement()
    {
        if (carring)
        {
            if (player.moving)
            {
                StartCoroutine(ApplyInertia(Vector3.back, true));
            }
            else
            {
                StartCoroutine(ApplyInertia(Vector3.forward, false));
            }
        }
    }

    Vector3 InertiaSpot()
    {
        return new Vector3(initialSpot.x, initialSpot.y, initialSpot.z - maxDistance);
    }

    IEnumerator ApplyInertia(Vector3 direction, bool switcher)
    {
        yield return new WaitForSeconds(0);
        if(switcher && transform.localPosition.z > InertiaSpot().z)
        {
            transform.Translate(direction * 2 * Time.deltaTime);
        }
        else if(!switcher && transform.localPosition.z < initialSpot.z)
        {
            transform.Translate(direction * 2 * Time.deltaTime);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            fainted = true;
        }
        else
        {
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), collision.collider); 
        }
    }
}

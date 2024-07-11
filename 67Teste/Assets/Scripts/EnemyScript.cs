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

    BoxCollider boxCollider; //MUDAR

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

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            fainted = true;
        }
        else
        {
            Physics.IgnoreCollision(GetComponent<BoxCollider>(), collision.collider); //AQUI
        }
    }
}

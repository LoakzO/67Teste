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

    BoxCollider boxCollider; //MUDAR

    void Start()
    {
        player = GameObject.Find("Player").GetComponent<PlayerScript>();
        boxCollider = GetComponent<BoxCollider>();
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
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.collider.tag == "Player")
        {
            fainted = true;
        }
    }
}

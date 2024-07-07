using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SonBall : SonBallFather
{
    Rigidbody _rb;

    float impulseSon;

    bool force;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
        impulseSon = 8f;
        force = false;


    }

    void Start()
    {
        force = true;
    }

    
    void Update()
    {
        if(force == true)
        {
            _rb.AddForce(transform.forward * impulseSon, ForceMode.Impulse);
            force = false;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Wall>())
        {
            manager.points += 50;
        }
        if (collision.gameObject.GetComponent<Dynamite>())
        {
            manager.points += 200;
        }
    }

}

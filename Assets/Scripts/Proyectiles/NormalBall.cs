using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NormalBall : Ball /*,IUpdate, IFixUpdate IPlaySound*/
{
    Rigidbody _rb;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
        canon = FindObjectOfType<Cannon>();
    }

    void Start()
    {
        impulse = canon.forceBar;
        StartCoroutine(timeToShoot());
    }

    IEnumerator timeToShoot()
    {
        yield return new WaitForSeconds(0.01f);
        _rb.AddForce(canon.spawn.transform.forward * impulse, ForceMode.Impulse);
        StopCoroutine(timeToShoot());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.GetComponent<Wall>())
        {
            manager.points += 50; 
        }
        if (collision.gameObject.GetComponent<Dynamite>())
        {
            manager.points += 200;
            
        }
    }
}

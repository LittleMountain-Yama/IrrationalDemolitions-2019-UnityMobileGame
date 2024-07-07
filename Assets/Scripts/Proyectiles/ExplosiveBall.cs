using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ExplosiveBall : Ball
{
    Rigidbody _rb;
    AudioSource _au;

    public bool onAir;

    float power = 50f;
    float radius = 10f;

    public GameObject smoke;

    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _au = GetComponent<AudioSource>();
        manager = FindObjectOfType<GameManager>();
        canon = FindObjectOfType<Cannon>();
        onAir = false;
    }

    void Start()
    {
        impulse = canon.forceBar;
        StartCoroutine(timeToShoot());
        onAir = true;
    }

    private void Update()
    {
        if(Input.touchCount > 0 && onAir == true)
        {
            Touch touch = Input.GetTouch(0);
            if(touch.phase == TouchPhase.Began)
            {
                Explode();

                
            }
        }

    }

    void Explode()
    {
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);

            }
        }

        if (!_au.isPlaying)
            _au.Play();

        GameObject Smoke = Instantiate(smoke);
        Smoke.transform.position = transform.position;

        onAir = false;

        Destroy(this.gameObject);
    }

    IEnumerator timeToShoot()
    {
        yield return new WaitForSeconds(0.01f);
        _rb.AddForce(canon.spawn.transform.forward * impulse, ForceMode.Impulse);
        StopCoroutine(timeToShoot());
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<Floor>())
        {
            onAir = false;
            
        }
        if (collision.gameObject.GetComponent<Wall>())
        {
            onAir = false;
            manager.points += 50;
        }
        if (collision.gameObject.GetComponent<Dynamite>())
        {
            onAir = false;
            manager.points += 200;
        }
    }
}

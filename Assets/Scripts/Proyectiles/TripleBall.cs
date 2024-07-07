using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TripleBall : Ball
{
    Rigidbody _rb;
    AudioSource _au;

    public bool onAir; 

    public GameObject spawnA;
    public GameObject spawnB;
    public GameObject spawnC;

    public SonBall son;
    public SonBall sonA;
    public SonBall sonB;

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

    void Update()
    {
        if (Input.touchCount > 0 && onAir == true)
        {
            Touch touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Began)
            {
                Divide();
            }
        }

    }

    void Divide()
    {

        if (!_au.isPlaying)
            _au.Play();

        GameObject Smoke = Instantiate(smoke);
        Smoke.transform.position = transform.position;

        SonBall sonBall = Instantiate(son);
        sonBall.transform.position = spawnA.transform.position;

        SonBall sonBallA = Instantiate(sonA);
        sonBallA.transform.position = spawnB.transform.position;

        SonBall sonBallB = Instantiate(sonB);
        sonBallB.transform.position = spawnC.transform.position;

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

 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dynamite : MonoBehaviour, IUpdate
{
    GameManager manager;
    Rigidbody _rb;
    AudioSource _au;

    public NormalBall normal;
    public ExplosiveBall explosive;
    public TripleBall triple;
    public SonBall son;

    public GameObject smoke;

    public int _life;
    public float power;
    public float radius;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
        _au = GetComponent<AudioSource>();
         

        _life = 1;

        power = 70f;
        radius = 8f;
        manager.dynamite.Add(this.gameObject);
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
        _rb.Sleep(); 
    }

    public void OnUpdate()
    {
        if (_life == 0)
        {
            Death();
        }
    }

    void Death()
    {
        //_gm.points += 10;
        Vector3 explosionPos = transform.position;
        Collider[] colliders = Physics.OverlapSphere(explosionPos, radius);
        foreach (Collider hit in colliders)
        {
            Rigidbody rb = hit.GetComponent<Rigidbody>();

            if (rb != null)
                rb.AddExplosionForce(power, explosionPos, radius, 3.0F);
        }

        Debug.Log("me morii");

        GameObject Smoke = Instantiate(smoke);
        Smoke.transform.position = transform.position;

        if (!_au.isPlaying)
            _au.Play();

        gameObject.SetActive(false);
        manager.dynamite.Remove(this.gameObject);
        _life--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<NormalBall>() || collision.gameObject.GetComponent<ExplosiveBall>() || collision.gameObject.GetComponent<TripleBall>()|| collision.gameObject.GetComponent<SonBall>())
        {            
            if (!_au.isPlaying)
                _au.Play();
            _life -= 1;
        }
    }
}

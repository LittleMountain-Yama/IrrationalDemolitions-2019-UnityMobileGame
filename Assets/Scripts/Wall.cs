using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Wall : MonoBehaviour, IUpdate /*, IFixUpdate, IPlaySound*/
{
    GameManager manager;
    Rigidbody _rb;
    AudioSource _au;

    public NormalBall normal;
    public ExplosiveBall explosive;
    public TripleBall triple;
    public SonBall son;

    public int _life;

    
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        manager = FindObjectOfType<GameManager>();
        _au = GetComponent<AudioSource>();
       

        _life = 2;
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
        manager.points += 100;
        Debug.Log("me morii");

        gameObject.SetActive(false);
        _life--;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<NormalBall>() || collision.gameObject.GetComponent<ExplosiveBall>() || collision.gameObject.GetComponent<TripleBall>() || collision.gameObject.GetComponent<SonBall>())
        {
            if (!_au.isPlaying)
                _au.Play();
            _life -= 1;
        }
    }

}

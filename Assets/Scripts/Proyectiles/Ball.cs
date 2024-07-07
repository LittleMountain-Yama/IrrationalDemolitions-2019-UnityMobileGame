using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ball : MonoBehaviour
{
    public GameManager manager;
    public Cannon canon;

    public Wall wall;
    public Dynamite dynamite;
    public Floor terrain;

    public float impulse;


    void Awake()
    {
        
    }

    void Start()
    {
        
    }

    void Update()
    {
        
    }
}

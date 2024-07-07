using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Cannon : MonoBehaviour//,IPlaySound
{
    AudioSource _au;

    public NormalBall normal;
    public ExplosiveBall explosive;
    public TripleBall triple;

    public Slider force;
    public GameObject spawn;   

    public bool ballNormal;
    public bool ballExplosive;
    public bool ballTriple;

    public bool calculate; //true resta, false suma

    public float initalPosition;
    public float angle;
    public float maxValue;
    public float minValue;
    public float forceBar;      

    private void Awake()
    {
        _au = GetComponent<AudioSource>();

        angle = 5;

        minValue = force.minValue; // 2
        maxValue = force.maxValue; // 20
        forceBar = force.value;    // Valor inicial que se va modificando

        ballNormal = true;
        ballExplosive = false;
        ballTriple = false;        
    }

    public void angleUp()
    {
        Vector3 angleTemp = transform.rotation.eulerAngles;
        angleTemp += new Vector3(0, 0, +angle);
        Vector3 clamp = new Vector3(0, 0, Mathf.Clamp(angleTemp.z, 185f, 245f));
        transform.rotation = Quaternion.Euler(clamp);
        //Debug.Log(angleTemp);
    }
    public void angleDown()
    {
        Vector3 angleTemp = transform.rotation.eulerAngles;
        angleTemp += new Vector3(0, 0, -angle);
        Vector3 clamp = new Vector3(0, 0, Mathf.Clamp(angleTemp.z, 185f, 245f));
        transform.rotation = Quaternion.Euler(clamp);
    }

    public void forceShoot()
    {
        force.value = forceBar;

        if(minValue < forceBar && calculate == true )
        {
            forceBar -= Time.deltaTime * 20;
            if(minValue >= forceBar)
            {
                calculate = false;
            }
        }
        else if(maxValue > forceBar && calculate == false)
        {
            forceBar += Time.deltaTime * 20;
            if(maxValue <= forceBar)
            {
                calculate = true;
            }
        }
        if(forceBar >= maxValue)
        {
            forceBar--;
        }
        else if(forceBar <= minValue)
        {
            forceBar++;
        }
    }

    public void Shoot()
    {
        if (ballNormal == true)
        {
            NormalBall normalShoot = Instantiate(normal);
            normalShoot.transform.position = spawn.transform.position;
            if (!_au.isPlaying)
                _au.Play();

        }        
        else if (ballExplosive == true)
        {
            ExplosiveBall explosiveShoot = Instantiate(explosive);
            explosiveShoot.transform.position = spawn.transform.position;
            if (!_au.isPlaying)
                _au.Play();
        }
        else if(ballTriple == true)
        {
            TripleBall tripleShoot = Instantiate(triple);
            tripleShoot.transform.position = spawn.transform.position;
            if (!_au.isPlaying)
                _au.Play();
        }
        
    }
}

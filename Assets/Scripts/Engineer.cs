using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Engineer : MonoBehaviour, IUpdate
{
    Animator _anim;
   
    public bool isAiming;
    public bool isCharging;   
    public bool isDancing;

    private void Awake()
    {
       _anim = GetComponent<Animator>();
        
        isAiming = true;
        isCharging = false;        
        isDancing = false;
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        _anim.SetBool("isAiming", isAiming);
        _anim.SetBool("isCharging", isCharging);        
        _anim.SetBool("isDancing", isDancing);
    }
}

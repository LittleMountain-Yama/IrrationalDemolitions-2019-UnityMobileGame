using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateManager : MonoBehaviour
{
    static UpdateManager _instance;
    public static UpdateManager Instance
    {
        get { return _instance;}
        private set { }
    }

    List<IUpdate> listUpdate = new List<IUpdate>();
    List<IFixUpdate> listFixUpdate = new List<IFixUpdate>();

    GameManager _gm;

    void Awake()
    {
        _gm = FindObjectOfType<GameManager>();
        _instance = this;        
    }

    void Update()
    {
        if (_gm.pause == true)
        {
            return;
        }
        
        AllUpdates();
    }

    private void FixedUpdate()
    {
        if (_gm.pause == true)
        {
            return;
        }
       
        AllFixedUpdates();
    }

    void AllUpdates()
    { 
        for (int i = 0; i < listUpdate.Count; i++)
        {
           listUpdate[i].OnUpdate();
        }        
    }

    void AllFixedUpdates()
    {       
        for (int i = 0; i < listFixUpdate.Count; i++)
        {
           listFixUpdate[i].OnFixedUpdate();
        }        
    }

    public void AddToUpdate(IUpdate element)
    {
        if (!listUpdate.Contains(element))
            listUpdate.Add(element);
    }

    public void RemoveFromUpdate(IUpdate element)
    {
        if (listUpdate.Contains(element))
            listUpdate.Remove(element);
    }


    public void AddToFixUpdate(IFixUpdate element)
    {
        if (!listFixUpdate.Contains(element))
            listFixUpdate.Add(element);
    }

    public void RemoveFromFixUpdate(IFixUpdate element)
    {
        if (listFixUpdate.Contains(element))
            listFixUpdate.Remove(element);
    }   
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CannonControler : MonoBehaviour, IUpdate /*,IPointerClickHandler*/
{
    public Cannon cannon;

    public GameManager manager;

    //Canvas Interactuable
    public GameObject slider;
    public GameObject up_down;
    public GameObject confirm;

    //Contadores de Balas
    public Text normalAmmoCount;
    public Text explosiveAmmoCount;
    public Text tripleAmmoCount;

    public Text emptyAmmo;

    bool forceBarActive;

    public bool isAiming;   

    public int normalAmmo;
    public int explosiveAmmo;
    public int tripleAmmo;
    public int currentAmmo;

    void Awake()
    {
        cannon = FindObjectOfType<Cannon>();
        manager = FindObjectOfType<GameManager>();        

        isAiming = true;
        forceBarActive = false;
        slider.SetActive(false);

        emptyAmmo.text = "";

        if (SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lvl1"))
        {
            normalAmmo = 3;
            explosiveAmmo = 1;
            tripleAmmo = 2;
        }
        else if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lvl2"))
        {
            normalAmmo = 2;
            explosiveAmmo = 2;
            tripleAmmo = 2;
        }
        else if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lvl3"))
        {
            normalAmmo = 3;
            explosiveAmmo = 2;
            tripleAmmo = 1;
        }
        else if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lvl4"))
        {
            normalAmmo = 3;
            explosiveAmmo = 1;
            tripleAmmo = 1;
        }        
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        if (forceBarActive == true)
        {
            slider.SetActive(true);
            cannon.forceShoot();
        }        
        
        if (cannon.ballNormal == true)
        {
            currentAmmo = normalAmmo;
        }
        else if (cannon.ballExplosive == true)
        {
            currentAmmo = explosiveAmmo;
        }
        else if (cannon.ballTriple == true)
        {
            currentAmmo = tripleAmmo;
        }

        normalAmmoCount.text = "" + normalAmmo;
        explosiveAmmoCount.text = "" + explosiveAmmo;
        tripleAmmoCount.text = "" + tripleAmmo;

        /*
        if (normalAmmo == 0)
        {
            Debug.Log("No hay municion normal");
        }

        if (tripleAmmo == 0)
        {
            Debug.Log("No hay municion triple");
        }

        if (explosiveAmmo == 0)
        {
            Debug.Log("No hay municion explosiva");
        }
        */
    }

    public void Up()
    {
        cannon.angleUp();
    }

    public void Down()
    {
        cannon.angleDown();
    }

    public void ConfirmButton()
    {
        if (isAiming == true && currentAmmo >= 1)
        {
            up_down.SetActive(false);
            forceBarActive = true;
            isAiming = false;
            manager.EngiCharge();
        }
        else if(isAiming == false && currentAmmo >= 1)
        {
            confirm.SetActive(false);
            slider.SetActive(false);
            forceBarActive = false;            
            cannon.Shoot();
            StartCoroutine(Cooldown());
            ReduceAmmo();           
        }
        else
        {
            emptyAmmo.text = "No More Ammo!";
            StartCoroutine(CleanText());
        }
    }

    void DefaultUI()
    {
        up_down.SetActive(true);
        confirm.SetActive(true);
        isAiming = true;
        forceBarActive = false;
        cannon.forceBar = cannon.minValue;
        manager.EngiAim();
    }     

    void ReduceAmmo()
    {
        if (cannon.ballNormal == true)
        {
            normalAmmo--;
        }
        else if (cannon.ballExplosive == true)
        {
            explosiveAmmo--;
        }
        else if (cannon.ballTriple == true)
        {
            tripleAmmo--;
        }
    }

    // seleccion de disparos

    public void NormalBall()
    {
        cannon.ballNormal = true;
        cannon.ballExplosive = false;
        cannon.ballTriple = false;        
    }

    public void TripleBall()
    {
        cannon.ballNormal = false;
        cannon.ballExplosive = false;
        cannon.ballTriple = true;        
    }

    public void ExplosiveBall()
    {
        cannon.ballNormal = false;
        cannon.ballExplosive = true;
        cannon.ballTriple = false;      
    }

    IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(1f);
        DefaultUI();
        StopCoroutine(Cooldown());
    }

    IEnumerator CleanText()
    {
        yield return new WaitForSeconds(1);
        emptyAmmo.text = "";
        StopCoroutine(CleanText());
    }
}

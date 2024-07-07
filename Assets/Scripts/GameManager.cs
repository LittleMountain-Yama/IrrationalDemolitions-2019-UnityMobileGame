using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour, IUpdate
{
    CannonControler _cC;
    Engineer _e;

    //Canvas General
    public GameObject bombOpener; //Botón que abre bombas
    public GameObject bombMenu;  //Selector de bombas
    public GameObject buttonPause; //Botón de pausa
    public GameObject pauseOptions; //Opciones de pausa
    public GameObject console;

    //Canvas Bombas
    public Sprite normalNoS;
    public Sprite normalSelected;

    public Sprite explosiveNoS;
    public Sprite explosiveSelected;

    public Sprite tripleNoS;
    public Sprite tripleSelected;

    //Canvas Victoria/Derrota
    public GameObject win;
    public GameObject lose;

    public Text point;

    public List<GameObject> dynamite = new List<GameObject>();

    public int points;

    public bool pause;

    private void Awake()
    {
        _cC = FindObjectOfType<CannonControler>();
        _e = FindObjectOfType<Engineer>();

        bombOpener.SetActive(true);
        bombMenu.SetActive(false);
        buttonPause.SetActive(true);
        pauseOptions.SetActive(false);
        console.SetActive(false);       
    }

    private void Start()
    {
        UpdateManager.Instance.AddToUpdate(this);
    }

    public void OnUpdate()
    {
        point.text = ("Score " + points);

        if (_cC.normalAmmo == 0 && _cC.explosiveAmmo == 0 && _cC.tripleAmmo == 0 && points >= 1500) //en ese caso gana el nivel
        {
            StartCoroutine(WaitForWin());
        }
        else if (_cC.normalAmmo == 0 && _cC.explosiveAmmo == 0 && _cC.tripleAmmo == 0 && points < 1500)// pierde el nivel
        {
            StartCoroutine(WaitForLose());
        }    
        
        if(dynamite.Count == 0)
        {
            StartCoroutine(WaitForWin());  
        }
    }

    //UI de bombas
    public void OpenBombs()
    {
        bombOpener.SetActive(false);
        bombMenu.SetActive(true);
    }

    public void CloseBombs()
    {
        bombOpener.SetActive(true);
        bombMenu.SetActive(false);
    }   

    public void ChangeNormal()
    {
        Image normalImage = GameObject.Find("Normal").GetComponent<Image>();
        normalImage.sprite = normalSelected;
        Image explosiveImage = GameObject.Find("Explosive").GetComponent<Image>();
        explosiveImage.sprite = explosiveNoS;
        Image tripleImage = GameObject.Find("Triple").GetComponent<Image>();
        tripleImage.sprite = tripleNoS;
    }

    public void ChangeExplosive()
    {
        Image normalImage = GameObject.Find("Normal").GetComponent<Image>();
        normalImage.sprite = normalNoS;
        Image explosiveImage = GameObject.Find("Explosive").GetComponent<Image>();
        explosiveImage.sprite = explosiveSelected;
        Image tripleImage = GameObject.Find("Triple").GetComponent<Image>();
        tripleImage.sprite = tripleNoS;
    }

    public void ChangeTriple()
    {
        Image normalImage = GameObject.Find("Normal").GetComponent<Image>();
        normalImage.sprite = normalNoS;
        Image explosiveImage = GameObject.Find("Explosive").GetComponent<Image>();
        explosiveImage.sprite = explosiveNoS;
        Image tripleImage = GameObject.Find("Triple").GetComponent<Image>();
        tripleImage.sprite = tripleSelected;
    }

    //UI en pausa
    public void OpenPause()
    {
        buttonPause.SetActive(false);
        pauseOptions.SetActive(true);
        bombOpener.SetActive(false);
        bombMenu.SetActive(false);

        _cC.up_down.SetActive(false);
        _cC.confirm.SetActive(false);

        pause = true;
    }

    public void ClosePause()
    {
        buttonPause.SetActive(true);
        pauseOptions.SetActive(false);
        bombOpener.SetActive(true);

        if (_cC.isAiming == true)
            _cC.up_down.SetActive(true);
        
        _cC.confirm.SetActive(true);

        pause = false;
    }    

    public void Restart()
    {
       SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void OpenConsole()
    {
        console.SetActive(true);     
        pauseOptions.SetActive(false);
    }

    public void CloseConsole()
    {
        console.SetActive(false);  
        pauseOptions.SetActive(true);
    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene() != SceneManager.GetSceneByName("Lvl4"))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

    public void GoToScene(int number)
    {
        SceneManager.LoadScene(number);        
    }

    //Corutinas

    IEnumerator WaitForWin()
    {
        yield return new WaitForSeconds(2f);
        win.SetActive(true);
        StopCoroutine(WaitForWin());
        EngiWin();
    }

    IEnumerator WaitForLose()
    {
        yield return new WaitForSeconds(3f);
        if(points < 1500)
        {
            lose.SetActive(true);
        }
        else if(points >= 1500)
        {
            win.SetActive(true);
            EngiWin();
        }
        StopCoroutine(WaitForLose());
    }

    //Nuestro amigo el obrero   

    public void EngiAim()
    {        
        _e.isAiming = true;
        _e.isCharging = false;
        _e.isDancing = false;
    }

    public void EngiCharge()
    {       
        _e.isAiming = false;
        _e.isCharging = true;
        _e.isDancing = false;
    }

    public void EngiWin()
    {       
        _e.isAiming = false;
        _e.isCharging = false;
        _e.isDancing = true;        
    }

    
}

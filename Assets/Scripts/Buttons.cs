using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public int SceneNumber;
    public GameObject Panel;
    public GameObject Sound;
    public bool IsPaused;
    public Behaviour MoveComponent;
    public int Timee;
    
    void Awake(){
        Time.timeScale = 0;
    }
    void Update(){
        if(IsPaused && Input.GetKeyDown(KeyCode.Escape) ){
            OpenPanel();
            
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            MoveComponent.enabled = false;
            Time.timeScale = 0;

            
        }
    }

    public void SceneN(){
        SceneManager.LoadScene(SceneNumber);
        Instantiate(Sound);
    }
    public void ExitGame(){
       Application.Quit();
       Instantiate(Sound);
    }
    public void ClosePanel(){
        if(IsPaused){
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            MoveComponent.enabled = true;
            Time.timeScale = 1;
        }
     Panel.SetActive(false);
     Instantiate(Sound);
    }
    public void OpenPanel(){
     Panel.SetActive(true);
     Instantiate(Sound);
    }
    public void ChangeTime(){
        Time.timeScale = Timee;
    }

}

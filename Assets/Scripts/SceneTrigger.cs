using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTrigger : MonoBehaviour
{
    public int Scene;
    public bool ife = true;

    public void OnTriggerEnter(){
        if(PlayerPrefs.GetInt("KilledBallfish") >= 5 && PlayerPrefs.GetInt("KilledAngyYdilshik") >= 2 && ife){
        SceneManager.LoadScene(Scene);
        }else if(!ife){
            SceneManager.LoadScene(Scene);
        }
    }
}

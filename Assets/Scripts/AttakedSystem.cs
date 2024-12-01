using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttakedSystem : MonoBehaviour
{
    public int HP;
    public GameObject MainObject;
    public GameObject Sound;
    public int Pyli;
    public string Saves;
    public Rigidbody rb;
    public string SavesKilled;
 
    // Update is called once per frame
    void Update()
    {
        if(PlayerPrefs.GetInt(Saves) > 5){
                    PlayerPrefs.SetInt(Saves , 5);
                }
       if(HP <= 0){
                rb.gameObject.GetComponent<AIFish>().enabled = false;
                rb.useGravity = true;

                if(Pyli != -1 && Input.GetKeyDown(KeyCode.E)){
                    PlayerPrefs.SetInt(Saves ,PlayerPrefs.GetInt(Saves) + Pyli);
                    Destroy(MainObject);
                    PlayerPrefs.SetInt(SavesKilled,PlayerPrefs.GetInt(SavesKilled) + 1);
                }
                
            

        }
    }

    public void OnTriggerEnter(Collider col){
        if(col.tag == "Drotik" ){
            Destroy(col.gameObject);
            HP -= 1;
            Instantiate(Sound);
            
             

        
            
        }
    }
}

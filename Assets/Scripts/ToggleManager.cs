using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToggleManager : MonoBehaviour
{
    public Slider Battery;
    public Slider HP;
    public float secondForDamage;
    public float damgeHP;
    public float minusBattery;
    public float AddBatteryValue;
    public TeploViozorPlayer TVP;
    public float nextsecond;
    public GameObject Sound;

    public int SpaceClick;
    public int CurrentClick;

    private bool dam;
    public GameObject PanelClick;
    public GameObject CameraMan;
    public GameObject PanelLose;
    public bool Dangerous3;    // Start is called before the first frame update
    void Start()
    {
       //TVP.TVM.Size = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if(Dangerous3){
            HP.value -= 0.001f;
        }
        if(HP.value <= 0 ){
         PanelLose.SetActive(true);
        }
        if(Battery.value <= 0){
            TVP.TVM.Size = 0f;
           TVP.enabled = false;
        }else{
            TVP.enabled = true;
        }

        if(TVP.TVM.Size > 0){
            Battery.value -= (minusBattery/1000);
        }

        if(dam){
            if(Input.GetKeyDown(KeyCode.Space)){
                CurrentClick++;
            }
            if(CurrentClick >= SpaceClick){
                CurrentClick = 0;
                 transform.parent = null;
                gameObject.GetComponent<Swimming>().enabled = true;
                PanelClick.SetActive(false);
                dam = false;

                 nextsecond = (Time.time + secondForDamage);

            
               }
        }
        
    }


    private void OnTriggerStay(Collider objextes){
        if(objextes.tag == "Light"){
            Battery.value += AddBatteryValue;
        }
          if(objextes.tag == "Damage" && Time.time > nextsecond){

            PanelClick.SetActive(true);
            gameObject.GetComponent<Swimming>().enabled = false;
            gameObject.transform.SetParent(transform);
            dam = true;
            
             
                nextsecond = (Time.time + secondForDamage);
            HP.value -= damgeHP;
            Instantiate(Sound);
          }
    }

    private void OntriggerEnter(Collider objextes){
        if(objextes.tag == "Damage" && Time.time > nextsecond){

            if(objextes.tag == "Vrag"){
            Vector3 direction = objextes.transform.position - CameraMan.transform.position;
            CameraMan.transform.rotation = Quaternion.LookRotation(direction, Vector3.up);
        
            gameObject.GetComponent<Swimming>().enabled = false;
            //gameObject.transform.SetParent(transform);
            }
             if(Input.GetKeyDown(KeyCode.Space)){
                CurrentClick++;
            }
            if(CurrentClick >= SpaceClick){
                CurrentClick = 0;
                 //transform.parent = null;
                gameObject.GetComponent<Swimming>().enabled = true;

                 nextsecond = (Time.time + secondForDamage);

            }

            nextsecond = (Time.time + secondForDamage);
            HP.value -= damgeHP;
            Instantiate(Sound);
            
        }
    }
}

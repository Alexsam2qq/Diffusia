using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeploViozorPlayer : MonoBehaviour
{
    public TeploVizorManager TVM;
    public int time;
    private float nextTime;

    // Start is called before the first frame update
    void Start()
    {
        TVM = GameObject.Find("ManagerC").GetComponent<TeploVizorManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q)){
            TVM.Size = 10f;
            nextTime = Time.time + time;
            TVM.EnableC = true;

        }
        if(Time.time > nextTime){
         TVM.EnableC = false;
        }
        if(Time.time < nextTime &&  TVM.Size > 0){
           
         TVM.Size = TVM.Size - (time / 100f);
        }
    }
}

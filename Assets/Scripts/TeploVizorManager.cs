using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeploVizorManager : MonoBehaviour
{
    public bool MainScript;
    public float Size;
    public bool EnableC;
    private TeploVizorManager TVM;
    private Outline OL;
    // Start is called before the first frame update
    void Start()
    {
        if(!MainScript){
           TVM = GameObject.Find("ManagerC").GetComponent<TeploVizorManager>();
           OL = this.GetComponent<Outline>();

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!MainScript){
            if(TVM.EnableC){
                OL.OutlineWidth = TVM.Size;

            }else{
                OL.OutlineWidth = 0f;
            }
        }
    }
}

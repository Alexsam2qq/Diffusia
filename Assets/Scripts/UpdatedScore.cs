using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpdatedScore : MonoBehaviour
{
    public Weapon w;
    public Text Classic;
    public Text FreeClasuc;
    public Text DrotikEff;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        FreeClasuc.text = PlayerPrefs.GetInt("bulletPrefabBallFish").ToString();
        Classic.text = w.MaxBullet.ToString();
        DrotikEff.text = PlayerPrefs.GetInt("bulletPrefabAngryYdilshik").ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveSystem : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        gameObject.transform.position = new Vector3(PlayerPrefs.GetFloat("PlayerX"),PlayerPrefs.GetFloat("PlayerY"),PlayerPrefs.GetFloat("PlayerZ"));
        gameObject.transform.rotation = Quaternion.Euler(0f, PlayerPrefs.GetFloat("PlayerRotationY"), 0f);
    }

    // Update is called once per frame
    void Update()
    {
        PlayerPrefs.SetFloat("PlayerX", gameObject.transform.position.x);
        PlayerPrefs.SetFloat("PlayerY", gameObject.transform.position.y);
        PlayerPrefs.SetFloat("PlayerZ", gameObject.transform.position.z);
        PlayerPrefs.SetFloat("PlayerRotationY", transform.eulerAngles.y);
        PlayerPrefs.Save();
    }
}

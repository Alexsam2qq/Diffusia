using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    private GameObject bulletPrefab;
    [SerializeField]
    private Transform bulletSpawnPoint;
    [SerializeField]
    
    private GameObject bulletPrefabClassic;
    [SerializeField]
    private GameObject bulletPrefabBallFish;
    [SerializeField]
    private GameObject bulletPrefabAngryYdilshik;
    [SerializeField]
    private float bulletSpeed = 10f;
    private float currenttime;
    public int MaxBullet;
    public GameObject PanelLose;
    private int CurrentBullet;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.F)){
            CurrentBullet++;
        }
        if(CurrentBullet > 2){
            CurrentBullet = 0;
        }
        Debug.Log(PlayerPrefs.GetInt("bulletPrefabBallFish") + " BallFish");
        Debug.Log(PlayerPrefs.GetInt("bulletPrefabAngryYdilshik") + " Angry");
       //Debug.Log(PlayerPrefs.GetInt("bulletPrefabBallFish") + " BallFish");
        HandleInput();
        if(MaxBullet <= 0 ){
            PanelLose.SetActive(true);
            Time.timeScale = 0;
        }

        if(PlayerPrefs.GetInt("bulletPrefabBallFish") > 0&& CurrentBullet == 1 && Time.time >= currenttime&& Input.GetKeyDown(KeyCode.Mouse0) ){
          PlayerPrefs.SetInt("bulletPrefabBallFish", PlayerPrefs.GetInt("bulletPrefabBallFish") - 1);
          currenttime = Time.time + 0.4f;
          bulletPrefab = bulletPrefabBallFish;
            FireBullet();
        }
        if(PlayerPrefs.GetInt("bulletPrefabAngryYdilshik") > 0  && CurrentBullet == 2 && Time.time >= currenttime && Input.GetKeyDown(KeyCode.Mouse0)){
          PlayerPrefs.SetInt("bulletPrefabAngryYdilshik", PlayerPrefs.GetInt("bulletPrefabAngryYdilshik") - 1);
          currenttime = Time.time + 0.4f;
          bulletPrefab = bulletPrefabAngryYdilshik;
            FireBullet();
        }
    }

    private void HandleInput()
    {
        if (CurrentBullet == 0 && Input.GetKeyDown(KeyCode.Mouse0) && Time.time >= currenttime && MaxBullet > 0)
        {
            bulletPrefab = bulletPrefabClassic;
            MaxBullet -= 1;
            
            currenttime = Time.time + 0.4f;
            FireBullet();
        }
    }

    private void FireBullet()
    {
        GameObject bullet = Instantiate(bulletPrefab, bulletSpawnPoint.position, bulletSpawnPoint.rotation);
        Rigidbody bulletRigidbody = bullet.GetComponent<Rigidbody>();
        if (bulletRigidbody != null)
        {
            bulletRigidbody.velocity = transform.forward * bulletSpeed;
        }
    }
}

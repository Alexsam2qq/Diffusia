using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIFish : MonoBehaviour
{
    public float speed = 5f;
    public bool Evil;
    public float MaxRotateX;
    public float MinRotateX;
    public bool Testing;
    public float fleeSpeedMultiplier = 2f; // Умножитель скорости при испуге
    public float searchRadius = 10f;
    public float searchInterval = 20f;
    private float nextSearchTime;
    private Vector3 target;
    private bool isSeeking = false;
    private bool isFleeing = false; // Флаг, показывает, пугается ли рыба
    private GameObject player;



    private void Start()
    {
      //  nextSearchTime = Time.time + searchInterval;
      
    }

    private void Update()
    {if(!Testing){
        if(gameObject.transform.rotation.x > MaxRotateX){
        gameObject.transform.rotation = Quaternion.Euler(MaxRotateX, 0f, 0f);
        }
        if(gameObject.transform.rotation.x < MinRotateX){
        gameObject.transform.rotation = Quaternion.Euler(MinRotateX, 0f, 0f);
        }
    }
       
        if (player == null)
        {
            player = GameObject.FindGameObjectWithTag("Player");
        }


        if (isFleeing)
        {
            //Рыба убегает от игрока
            if(Evil){
               // speed = -1 * speed;
                transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            }
            if (player != null && !Evil)
            {
                Vector3 directionToPlayer = (player.transform.position - transform.position).normalized;
                MoveAndRotate(-directionToPlayer, speed * fleeSpeedMultiplier); // Увеличенная скорость при испуге
                return;
            } else {
                isFleeing = false; //Игрок пропал, прекращаем убегать
            }
        }
        else
        {
            //Рыба ищет точку и плывет к ней
            if (Time.time > nextSearchTime)
            {
                target = FindTarget();
                if (target != Vector3.zero)
                {
                    isSeeking = true;
                    nextSearchTime = Time.time + searchInterval;
                }
            }

            if (isSeeking && target != Vector3.zero)
            {
                MoveAndRotate(target - transform.position, speed);
                if (Vector3.Distance(transform.position, target) < 0.1f)
                {
                    isSeeking = false;
                }
            }
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFleeing = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isFleeing = false;
        }
    }

    private void MoveAndRotate(Vector3 direction, float currentSpeed)
    {
       if (direction.magnitude > 0.1f)
    {
        Quaternion targetRotation = Quaternion.LookRotation(direction);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f); // Используем Slerp для более плавного поворота
        transform.Translate(direction.normalized * currentSpeed * Time.deltaTime, Space.Self);
    }
    }

    private Vector3 FindTarget()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, searchRadius);
        if (colliders.Length == 0) return Vector3.zero;

        int randomIndex = Random.Range(0, colliders.Length);
        Vector3 randomPoint = colliders[randomIndex].ClosestPoint(transform.position) + Random.insideUnitSphere * 1;
        return randomPoint;
    }
}
using UnityEngine;

public class AngryFishAI : MonoBehaviour
{    
    
    public float speed = 5f;
    public float attackRange = 2f;
    public float patrolRange = 5f;
    public float patrolTime = 3f;
    public float obstacleCheckTime = 0.5f; // Время проверки на препятствия
    public LayerMask obstacleLayer; // слой препятствий
    public Transform player;
    private Vector3 patrolTarget;
    private bool isAttacking = false;
    private float timeToChangePatrolPoint;
    private float timeSinceLastObstacleCheck;
    private bool pathBlocked = false;


    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        if (player == null)
        {
            Debug.LogError("No object with tag 'Player' found!");
            enabled = false;
            return;
        }
        SetNewPatrolTarget();
        timeToChangePatrolPoint = Time.time + patrolTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isAttacking = false;
            SetNewPatrolTarget();
            timeToChangePatrolPoint = Time.time + patrolTime;
        }
    }

    void Update()
    {
        if (isAttacking)
        {
            // Смотрим на игрока
            Vector3 direction = (player.position - transform.position).normalized;
            Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 5f);


            // Плывем к игроку
            float distanceToPlayer = Vector3.Distance(transform.position, player.position);
            if (distanceToPlayer > attackRange)
            {
                transform.position = Vector3.MoveTowards(transform.position, player.position, speed * Time.deltaTime);
            }
        }
        else
        {
            // Патрулирование с проверкой на препятствия
            float distanceToTarget = Vector3.Distance(transform.position, patrolTarget);
            if (distanceToTarget > 0.1f)
            {
                Vector3 direction = (patrolTarget - transform.position).normalized;
                transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction, Vector3.up), Time.deltaTime * 5f);
                transform.position = Vector3.MoveTowards(transform.position, patrolTarget, speed * Time.deltaTime);

                // Проверка на препятствие
                if (timeSinceLastObstacleCheck >= obstacleCheckTime)
                {
                    if (Physics.Raycast(transform.position, direction, out RaycastHit hit, distanceToTarget, obstacleLayer))
                    {
                        pathBlocked = true;
                        Debug.Log("Path blocked!");
                    }
                    else
                    {
                        pathBlocked = false;
                    }
                    timeSinceLastObstacleCheck = 0;
                }
            }
            else if (Time.time >= timeToChangePatrolPoint || pathBlocked)
            {
                SetNewPatrolTarget();
                timeToChangePatrolPoint = Time.time + patrolTime;
                pathBlocked = false; // Сбрасываем флаг после выбора новой точки
            }
        }
    }
    void SetNewPatrolTarget()
    {
        Vector3 randomDirection = Random.insideUnitSphere * patrolRange;
        randomDirection.y = 0; // перемещение только по плоскости XY
        patrolTarget = transform.position + randomDirection;
    }
}
using UnityEngine;

public class Swimming : MonoBehaviour
{
   public float swimSpeed = 5f;
    public float mouseSensitivity = 2f;
    public Transform playerCamera; // Ссылка на камеру

    private CharacterController controller;
    private float verticalRotation = 0f;
    public float buoyancyForce = 10f; // Сила выталкивания
    public float RangeView = 1f;

    void Start()
    { 
        controller = GetComponent<CharacterController>();
        if (controller == null)
        {
            Debug.LogError("CharacterController component is missing!");
        }
        Cursor.lockState = CursorLockMode.Locked; // Блокировка курсора
    }

    void Update()
    {
        
        transform.position += new Vector3(0, buoyancyForce * Time.deltaTime, 0); 
        // Поворот камеры (без изменений)
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        transform.Rotate(Vector3.up * mouseX);
        playerCamera.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Движение с учетом вертикального направления взгляда камеры
        float forwardInput = Input.GetAxis("Vertical");
        float horizontalInput = Input.GetAxis("Horizontal");

        Vector3 moveDirection = playerCamera.forward * forwardInput * RangeView + playerCamera.right * horizontalInput * RangeView;
        moveDirection = moveDirection.normalized * swimSpeed;

        // Добавляем вертикальное движение в зависимости от направления взгляда камеры
        moveDirection += playerCamera.up * moveDirection.z * 0.5f; // 0.5f - множитель, регулирующий силу вертикального движения

        controller.Move(moveDirection * Time.deltaTime);
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public CharacterController characterController;
    public float speed = 5f;
    public float turnSmoothTime = 0.1f;
    public Transform cam;

    float turnSmoothVelocity;
    Vector3 velocity;

    // Update is called once per frame
    void Update()
    {
        MovePlayer();
    }

    private void MovePlayer()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        if(direction.magnitude >= 0.1f)
        {
            direction = RotatePlayer(direction);

            //multiplicamos por Time.deltaTime para que no le afecte el frameRate
            characterController.Move(speed * Time.deltaTime * direction.normalized);

            //aplicamos gravedad al jugador
            velocity.y += Physics.gravity.y * Time.deltaTime;
            characterController.Move(velocity * Time.deltaTime);
        }
    }

    private Vector3 RotatePlayer(Vector3 direction)
    {
        //Atan2 nos da el ángulo en el que se está moviendo nuestro personaje.
        //cam.aulerAngles.y es para que el personaje se mueva en la dirección de la cámara
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

        float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
        transform.rotation = Quaternion.Euler(0f, angle, 0f);
        Vector3 moveDirection = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        return moveDirection;
    }
}

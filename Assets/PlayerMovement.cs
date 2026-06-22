using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 5f;
    public float gravity = -9.81f;
    public float jumpHeight = 3f;

    private CharacterController controller;
    private Animator animator;

    private Vector3 velocity;
    private bool isGrounded;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        isGrounded = controller.isGrounded;

        // Mantener pegado al piso
        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;

            // Termina salto SOLO cuando cae
            animator.SetBool("isJumping", false);
        }

        // Movimiento
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

        controller.Move(move * speed * Time.deltaTime);

        // Animación caminar
        animator.SetFloat("Speed", move.magnitude);

        // Salto
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);

            animator.SetBool("isJumping", true);
        }

        // Gravedad
        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);
    }
}
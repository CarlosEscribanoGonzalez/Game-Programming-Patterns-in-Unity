using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    //Lógica:
    [SerializeField] private float speed;
    [SerializeField] private float jumpSpeed;
    [SerializeField] private float climbSpeed;
    [SerializeField] private float threadsPerShot; //Hilos que gasta en cada ataque a distancia
    [SerializeField] private GameObject meleeAttack;
    private InventoryManager inventory;
    private Rigidbody2D rb;
    private Animator anim;
    private float directionX = 0; //-1 -> izquierda; 1 -> derecha
    private float directionY = 0; //-1 -> abajo; 1 -> arriba
    private bool jump = false; //Indica si se ha pulsado el botón de saltar
    private bool isGrounded = false; //El valor se altera en el script GroundDetector
    private bool canClimb = false;
    private bool isClimbing = false;
    private bool canMove = true;
    //ObjectPool:
    private ObjectPool pool;

    // Start is called before the first frame update
    void Awake()
    {
        inventory = GameObject.FindObjectOfType<InventoryManager>().GetComponent<InventoryManager>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        pool = GameObject.FindObjectOfType<ObjectPool>();
    }

    // Update is called once per frame
    void Update()
    {
        if (canMove) ManageInput();
        AnimateCharacter();
    }

    private void FixedUpdate()
    {
        if (canMove && !isClimbing) rb.velocity = new Vector2(directionX * speed, rb.velocity.y);
        else if (canMove && isClimbing) rb.velocity = new Vector2(directionX / 2 * speed, directionY * climbSpeed);

        if(rb.velocity.y < 0) rb.velocity += Vector2.up * Physics2D.gravity.y*2 * Time.fixedDeltaTime; //La caída es más rápida que la subida

        if (jump)
        {
            jump = false;
            rb.AddForce(transform.up * jumpSpeed, ForceMode2D.Impulse);
        }
    }

    private void ManageInput()
    {
        directionX = Input.GetAxisRaw("Horizontal");
        if (directionX < 0 && !isClimbing) GetComponent<SpriteRenderer>().flipX = true;
        else if (directionX > 0 && !isClimbing) GetComponent<SpriteRenderer>().flipX = false;
        directionY = Input.GetAxisRaw("Vertical");

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded) //Saltar
        {
            jump = true;
        }
        if (Input.GetKeyDown(KeyCode.Mouse0)) //Ataque melé
        {
            meleeAttack.SetActive(true);
            ToggleMovement();
            Invoke("ToggleMovement", 0.3f); 
            anim.SetTrigger("Attack");
            meleeAttack.SetActive(false);
        }
        if (Input.GetKeyDown(KeyCode.Mouse1)) //Lanzar aguja
        {
            Invoke("ThrowNeedle", 0.15f);
        }
        if (Input.GetKeyDown(KeyCode.R)) //Lanzar bola
        {
            if (inventory.UseThreads(threadsPerShot))
            {
                ToggleMovement();
                anim.SetTrigger("ThrowBall");
                Invoke("ToggleMovement", 0.2f); //Durante un breve tiempo se queda quieto
                Invoke("Shoot", 0.15f); //Dejamos un pequeño tiempo para la animación de batear la bola de hilo
            }   
        }
        if (canClimb && Input.GetKeyDown(KeyCode.W)) //Trepar
        {
            isClimbing = true;
        }
        else if (canClimb && Input.GetKeyUp(KeyCode.W)) //Soltarse de la pared
        {
            isClimbing = false;
            rb.velocity = Vector2.zero; //Al dejar de trepar se evitan impulsos, simplemente se suelta al jugador en caída libre
        }
        else if (!canClimb) isClimbing = false;    
        //GameObject.FindObjectOfType<SaveController>().SetCanSave(isGrounded); //TEMPORAL: encontrar forma de sacarlo de un Update
    }

    private void AnimateCharacter()
    {
        //Aquí sólo están presentes los booleanos del movimiento básico
        if (isGrounded) anim.SetBool("Grounded", true);
        else anim.SetBool("Grounded", false);
        if (isGrounded && directionX != 0) anim.SetBool("Walk", true);
        else anim.SetBool("Walk", false);
        if (jump) anim.SetBool("Jump", true);
        else anim.SetBool("Jump", false);
        if (isClimbing) anim.SetBool("Climb", true);
        else anim.SetBool("Climb", false);
        if (rb.velocity.y < 0) anim.SetBool("Fall", true);
        else anim.SetBool("Fall", false);
        if (canMove) anim.SetBool("CanMove", true);
        else anim.SetBool("CanMove", false);
        //Los de los ataques se activan cuando se hacen y se desactivan cuando se restablece el movimiento
    }

    private void Shoot()
    {
        IPooleableObject ballToShoot = pool.Get("ThreadBall");
        if (ballToShoot == null)
        {
            GameObject.FindObjectOfType<ThreadBall>().Clone();
            pool.Get("ThreadBall");
        }
        //Ahora hay una nueva bola que se ha añadido al object pool (en su propio Awake de manera automática)
    }

    private void ThrowNeedle()
    {
        IPooleableObject needle = pool.Get("Needle");
        if (needle != null)
        {
            ToggleMovement();
            anim.SetTrigger("ThrowNeedle");
        }
    }

    public void SetIsGrounded(bool grounded)
    {
        isGrounded = grounded;
    }
    
    public void SetCanClimb(bool canClimb)
    {
        this.canClimb = canClimb;
        if(isClimbing && !canClimb) rb.velocity = Vector2.zero;
    }
    
    public void ToggleMovement()
    {
        canMove = !canMove;
        if (!canMove)
        {
            rb.velocity = new Vector2(0, 0); //Se para al jugador en seco si no se puede mover
        }
    }

    public void ApplyImpulse(float impulse, int direction, float knockedTime)
    {
        ToggleMovement();
        rb.AddForce(transform.right * direction * impulse, ForceMode2D.Impulse);
        anim.SetTrigger("Hit");
        if (direction < 0) GetComponent<SpriteRenderer>().flipX = false;
        else GetComponent<SpriteRenderer>().flipX = true;
        Invoke("ToggleMovement", knockedTime);
    }

    public void FlipCharacter(bool flip)
    {
        GetComponent<SpriteRenderer>().flipX = flip;
    }
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Needle : MonoBehaviour, IPooleableObject
{
    //Aguja:
    private Rigidbody2D rb;
    [SerializeField] private float speed;
    [SerializeField] private float threadDistance; //Distancia a la que se puede lanzar la aguja antes de que vuelva al jugador
    [SerializeField] private float attractionForce; //Fuerza con la que se atraen los objetos o el jugador se propulsa hacia ellos
    [SerializeField] private float attractionHeight; //Además de aplicar attractionForce en la dirección de la aguja se aplica un poco de fuerza hacia arriba para que de un pequeño "salto"
    [SerializeField] private float damage; //Daño al clavarse
    //Hilo:
    private LineRenderer lineRenderer;
    private Transform threadOrigin;
    //Otros:
    private GameObject player;
    private bool noHit; //Booleano que indica que la aguja ha llegado al final de su recorrido sin haber colisionado con ningún objeto
    //ObjectPool:
    private ObjectPool pool;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        player = this.transform.parent.gameObject;
        threadOrigin = transform.Find("ThreadOrigin");
        lineRenderer = GetComponent<LineRenderer>();
        lineRenderer.positionCount = 2;
        pool = GameObject.FindObjectOfType<ObjectPool>();
        AddToPool();
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (Vector3.Distance(this.transform.position, player.transform.position) > threadDistance && !noHit)
        {
            noHit = true;
            player.GetComponent<PlayerController>().ToggleMovement();
        }
        else if (noHit)
        {
            transform.right = (this.transform.position - player.transform.position).normalized;
            rb.velocity = transform.right * speed*-1;
            if(Vector3.Distance(this.transform.position, player.transform.position) < 1)
            {
                pool.Release(this);
            }
        }
        //Dibujamos el hilo:
        lineRenderer.SetPosition(0, player.transform.position);
        lineRenderer.SetPosition(1, threadOrigin.position);
    }

    private void OnEnable()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        mousePosition.z = 0;
        transform.right = (mousePosition - player.transform.position).normalized;
        rb.velocity = transform.right * speed;
        if(mousePosition.x < player.transform.position.x) player.GetComponent<PlayerController>().FlipCharacter(true);
        else player.GetComponent<PlayerController>().FlipCharacter(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        rb.velocity = Vector2.zero;
        StartCoroutine(ProcessCollision(collision.gameObject));
        IDamageTaker damagedObject = collision.gameObject.GetComponent<IDamageTaker>();
        if (damagedObject != null)
        {
            Vector3 director = collision.gameObject.transform.position - this.transform.position;
            int direction = (int)Mathf.Sign(director.x);
            damagedObject.TakeDamage(damage);
        }
    }

    IEnumerator ProcessCollision(GameObject objectCollided)
    {
        yield return new WaitForSeconds(0.15f);
        if(objectCollided == null) //Si se ha matado al enemigo lanzando la aguja:
        {
            player.GetComponent<PlayerController>().ToggleMovement();
            pool.Release(this);
        }
        else //Si el enemigo no ha muerto por la aguja:
        {
            Rigidbody2D playerRb = player.GetComponent<Rigidbody2D>();
            Rigidbody2D objectRb = objectCollided.GetComponent<Rigidbody2D>();
            float seconds;
            if (objectRb != null && playerRb.mass > objectRb.mass) //Si el objeto tiene menos masa
            {
                ApplyForce(objectRb, -1);
                seconds = 0.25f;
            }
            else //Si tiene más masa o no tiene rigidbody
            {
                ApplyForce(playerRb, 1);
                seconds = 0.5f;
            }
            yield return new WaitForSeconds(seconds);
            if(!noHit)player.GetComponent<PlayerController>().ToggleMovement();
            pool.Release(this);
        }
    }

    private void ApplyForce(Rigidbody2D rb, float directionInverter) //DirectionInverter = 1 -> se propulsa al jugador
    {
        rb.AddForce(transform.right * attractionForce * directionInverter, ForceMode2D.Impulse);
        rb.AddForce(rb.gameObject.transform.up * attractionHeight, ForceMode2D.Impulse);
        if (directionInverter == 1) player.GetComponent<Animator>().SetTrigger("Propulsed");
    }

    //Patrón ObjectPool:
    public void Reset()
    {
        this.transform.localPosition = Vector3.zero;
        noHit = false;
    }

    public string GetName()
    {
        return "Needle";
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }

    public void AddToPool()
    {
        pool.Add(this);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }
}

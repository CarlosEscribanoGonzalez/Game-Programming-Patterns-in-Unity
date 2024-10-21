using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ThreadBall : MonoBehaviour, IPooleableObject, IPrototype
{
    private FlyweightProjectile flyweight;
    private GameObject player;
    private Rigidbody2D rb;
    private ObjectPool pool;
    
    void Awake()
    {
        flyweight = ProjectileFactory.GetFlyweightProjectile("ThreadBall");
        player = GameObject.FindWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        pool = GameObject.FindObjectOfType<ObjectPool>();
        AddToPool();
        this.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, flyweight.rotationSpeed * Time.deltaTime));
    }

    private void OnEnable()
    {
        Vector3 mousePos = Input.mousePosition;
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, 0));
        mousePosition.z = 0;
        transform.right = (mousePosition - player.transform.position).normalized;
        rb.velocity = transform.right * flyweight.speed;
        if (mousePosition.x < player.transform.position.x) player.GetComponent<PlayerController>().FlipCharacter(true);
        else player.GetComponent<PlayerController>().FlipCharacter(false);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IPooleableObject explosion = pool.Get("ThreadExplosion", collision.contacts[0].point);
        if (explosion == null)
        {
            GameObject.FindObjectOfType<ThreadExplosion>().Clone();
            explosion = pool.Get("ThreadExplosion", collision.contacts[0].point);
        }
        explosion.GetGameObject().transform.forward = this.transform.position - collision.gameObject.transform.position;
        explosion.GetGameObject().GetComponent<ParticleSystem>().Play();
        flyweight.ProcessCollision(collision, this.transform.position);
        pool.Release(this);
    }

    //Object Pool:
    public void AddToPool()
    {
        pool.Add(this);
    }

    public string GetName()
    {
        return "ThreadBall";
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf; 
    }

    public void Reset()
    {
        this.transform.localPosition = new Vector3(0.2f, 0.1f, 0);
    }

    public void SetActive(bool active)
    {
        this.gameObject.SetActive(active);
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    //Prototype:
    public IPrototype Clone()
    {
        GameObject newBall = Instantiate(this.gameObject);
        newBall.transform.parent = this.transform.parent;
        newBall.GetComponent<IPooleableObject>().Reset();
        return newBall.GetComponent<IPrototype>();
    }
}

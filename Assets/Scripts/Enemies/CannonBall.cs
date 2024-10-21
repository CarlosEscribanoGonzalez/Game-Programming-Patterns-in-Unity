using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CannonBall : MonoBehaviour, IPooleableObject, IPrototype
{
    FlyweightProjectile flyweight;
    private ObjectPool pool;
    private Rigidbody2D rb;
    [SerializeField] private Transform parent; //El disparo se efectúa con la dirección del cañón.
                                               //Una vez se dispara la bala se desvincula de él, pero debe volver a vincularse en Reset()
    // Start is called before the first frame update
    void Awake()
    {
        flyweight = ProjectileFactory.GetFlyweightProjectile("CannonBall");
        rb = GetComponent<Rigidbody2D>();
        pool = GameObject.FindObjectOfType<ObjectPool>();
        AddToPool();
        this.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.Rotate(new Vector3(0, 0, flyweight.rotationSpeed * Time.deltaTime));
    }

    private void OnEnable()
    {
        rb.AddForce(-transform.parent.right * flyweight.speed, ForceMode2D.Impulse);
        this.transform.parent = null;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IPooleableObject explosion = pool.Get("CannonExplosion", collision.contacts[0].point);
        if(explosion == null)
        {
            GameObject.FindObjectOfType<CannonExplosion>().Clone();
            explosion = pool.Get("CannonExplosion", collision.contacts[0].point);
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
        return "CannonBall";
    }

    public bool IsActive()
    {
        return this.gameObject.activeSelf;
    }

    public void Reset()
    {
        rb.velocity = new Vector2(0, 0);
        this.transform.parent = parent; //Necesita tener el mismo padre porque tiene que resetearse con localPosition
        this.gameObject.transform.localPosition = Vector3.zero;
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
        newBall.GetComponent<IPooleableObject>().Reset();
        return newBall.GetComponent<IPrototype>();
    }
}

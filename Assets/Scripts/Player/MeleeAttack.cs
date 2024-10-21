using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] private float damage;
    private SpriteRenderer pRenderer;

    void Awake()
    {
        pRenderer = GameObject.FindWithTag("Player").GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if (pRenderer.flipX) this.transform.localScale = new Vector3(-1, 1, 1);
        else this.transform.localScale = new Vector3(1, 1, 1);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageTaker taker = collision.gameObject.GetComponent<IDamageTaker>();
        if(taker != null)
        {
            taker.TakeDamage(damage);
            taker.ApplyImpulse(damage, (int)this.transform.localScale.x);
        }
    }
}

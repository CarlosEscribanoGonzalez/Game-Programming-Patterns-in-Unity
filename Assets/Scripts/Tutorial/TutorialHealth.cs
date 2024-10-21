using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialHealth : MonoBehaviour
{

    private ObjectPool pool;


    // Start is called before the first frame update
    void Start()
    {
        pool = GameObject.FindObjectOfType<ObjectPool>();

        IPooleableObject healingBall = pool.Get("HealingBall", this.transform.position);
        if (healingBall == null)
        {
            GameObject.FindObjectOfType<HealingBall>().Clone();
            pool.Get("HealingBall", this.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

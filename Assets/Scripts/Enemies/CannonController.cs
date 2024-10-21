using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CannonController : MonoBehaviour, ICannon, ISaveableObject
{
    private IState currentState;
    [SerializeField] private float shootingDistance;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float secondsToShoot;
    [SerializeField] private float cooldown;
    private bool dirty = false;

    void Awake()
    {
        SetState(new CannonIdle(this));
    }

    void Update()
    {
        currentState.Update();
    }
    
    private void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Walkable")) dirty = true;
    }

    //ICannon:
    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public IState GetState()
    {
        return currentState;
    }

    public void SetState(IState state)
    {
        if (currentState != null) currentState.Exit();
        currentState = state;
        currentState.Enter();
    }
    public float GetRotationSpeed() { return rotationSpeed; }
    public float GetSecondsToShoot() { return secondsToShoot; }
    public float GetCooldown() { return cooldown; }
    public float GetShootingDistance() { return shootingDistance; }

    //DirtyFlag:
    public bool IsDirty()
    {
        return dirty;
    }

    public void SetDirty(bool d)
    {
        dirty = d;
    }

    public object GetData()
    {
        return new CannonData
        {
            positionX = this.transform.position.x,
            positionY = this.transform.position.y
        };
    }

    public void RestoreData(object data)
    {
        CannonData cData = (CannonData)data;
        this.transform.position = new Vector3(cData.positionX, cData.positionY, 0);
        dirty = true;
    }
}

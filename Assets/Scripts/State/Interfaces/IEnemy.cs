using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy 
{
    public void SetState(IState state);
    public IState GetState();
    public float GetWalkingSpeed();
    public float GetChaseSpeed();
    public void SetSpeed(float speed);
    public float GetIdleTime();
    public float GetWanderTime();
    public float GetAttackCooldown();
    public float GetAttackingDistance();
    public bool PlayerAtSight();
    public GameObject GetGameObject();
    public void Attack();
    public bool GetCanMove();
    public void SetCanMove(bool c);
    public Animator GetAnimator();
}

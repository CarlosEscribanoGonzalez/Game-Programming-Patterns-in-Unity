using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public interface ICannon
{
    public GameObject GetGameObject();
    public void SetState(IState state);
    public IState GetState();
    public float GetRotationSpeed();
    public float GetSecondsToShoot();
    public float GetCooldown();
    public float GetShootingDistance();
}

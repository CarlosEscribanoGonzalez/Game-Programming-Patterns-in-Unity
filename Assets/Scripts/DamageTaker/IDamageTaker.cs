using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IDamageTaker
{
    public void TakeDamage(float damage);
    public void ApplyImpulse(float damage, int direction);
}

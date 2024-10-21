using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableObject
{
    public bool IsDirty();
    public void SetDirty(bool d);
    public object GetData();
    public void RestoreData(object data);
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISaveableOption
{
    public object GetData();
    public void RestoreData(object data);
}

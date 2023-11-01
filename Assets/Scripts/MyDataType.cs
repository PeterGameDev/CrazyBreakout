using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MyDataType
{
    [Serializable]
    public struct SpawnList
    {
        public List<Vector3> spawnPoints;
    }
}

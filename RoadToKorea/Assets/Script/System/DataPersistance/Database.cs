using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;


[Serializable]
public class Database
{
    public PlayerData playerData;



    public Database()
    {
        playerData = new();
    }
}

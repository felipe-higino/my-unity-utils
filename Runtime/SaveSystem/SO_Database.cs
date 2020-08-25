using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

[CreateAssetMenu(fileName = "Database_instance", menuName = "Felipe Utils/Save/DataToSave_test")]
[Serializable]
public class SO_Database : ScriptableObject
{
    public TDatabaseMain Database;
}

//modeling

[Serializable]
public class TDatabaseMain
{
    public List<TAppAccount> Accounts;
}

[Serializable]
public class TAppAccount
{
    public string id;

    public TCharacterStatus status;
    [Serializable] public class TCharacterStatus
    {
        public string name;
        public int strength;
        public int agility;
        public int magic;
    }

    public TCharacterLogin login;
    [Serializable] public class TCharacterLogin
    {
        public string email;
        public string password;
    }
}
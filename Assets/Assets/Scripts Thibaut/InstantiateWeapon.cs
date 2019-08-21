using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System.Linq;

public class InstantiateWeapon : TNBehaviour
{
    public string weaponPrefab;
    private Vector3 playerPos;
    private Quaternion playerRot;
    private int Id;

    private void Start()
    {
        playerPos = gameObject.transform.position;
        playerRot = gameObject.transform.rotation;
        Id = tno.ownerID;
        TNManager.Instantiate(tno.channelID, "Weapon", weaponPrefab, false, playerPos, playerRot, Id);
        Destroy(this);
    }

    [RCC]
    static GameObject Weapon(GameObject prefab, Vector3 position, Quaternion rotation, int id)
    {
        GameObject weapon = prefab.Instantiate();
        weapon.transform.position = position;
        weapon.transform.rotation = rotation;
        return weapon;
    }

    
}

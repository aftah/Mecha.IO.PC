using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StaticData : MonoBehaviour
{
    public static StaticData DataInstance;


    public int hpMax;
    public int hpMaxPerLevel;
    public int pointsThreshold;
    public int pointsThresholdPerLevel;
    public int healPerItem;
    public int pointsPerItem;
    public float characterMoveSpeed;
    public float characterMoveSpeedPerLevel;
    public float characterRotationSpeed;
    public float characterRotationSpeedPerLevel;
    public float spherCastRange;
    public float sphereCastRangePerLevel;
    public float sphereCastWidth;
    public float sphereCastWidthevel;
    public float weaponFireRate;
    public float weaponFireRatePerLevel;
    public float networkSendRate;
    public float cockpitMoveSmoothTime;
    public int numberOfTeleportableObjects;
    public string teleportableObject;
    public string destroyableObject;
    public float deathRangeLootOffset;
    public float autoDestroyDelay;

   
    private void Awake()
    {
        if (DataInstance != null)
        {
            Destroy(this);
        }
        else
        {
            DataInstance = this;
        }

    }
    

}

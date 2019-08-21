using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System;

public class HealthManager : TNBehaviour
{
    private int currentHealth;
    private int maxHealth;
    private int maxHealthBuff;
    private string destroyableObject;
    private float offset;
    private float autoDestroyDelay;

    public int MaxHealth
    {
        get { return maxHealth; }
        set { maxHealth = value; }
    }

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<PointsManager>().onLevelUp += OnLevelUpEventHandler;
        maxHealth = StaticData.DataInstance.hpMax;
        maxHealthBuff = StaticData.DataInstance.hpMaxPerLevel;
        offset = StaticData.DataInstance.deathRangeLootOffset;
        destroyableObject = StaticData.DataInstance.destroyableObject;
        autoDestroyDelay = StaticData.DataInstance.autoDestroyDelay;
    }

    private void OnLevelUpEventHandler(object sender, PointsManager.LevelUpEventArgs e)
    {
        if (tno.isMine)
            tno.Send("NetworkAddMaxHealth", Target.AllSaved, maxHealthBuff);
    }

    public void RemoveHealth(int damage)
    {
        if (tno.isMine)
            tno.Send("NetworkRemoveHealth", Target.AllSaved, damage);
    }

    public void AddHealth(int heal)
    {
        if (tno.isMine)
            tno.Send("NetworkAddHealth", Target.AllSaved, heal);
    }

    [RFC]
    private void NetworkRemoveHealth(int damage)
    {
        currentHealth -= damage;
        Debug.Log(currentHealth);

        if (currentHealth <= 0)
        {
            for (int i = 0; i < FindObjectOfType<PointsManager>().CurrentLevel; i++)
            {
                float randomX = UnityEngine.Random.Range(gameObject.transform.position.x - offset, gameObject.transform.position.x + offset);
                float randomZ = UnityEngine.Random.Range(gameObject.transform.position.z - offset, gameObject.transform.position.z + offset);
                TNManager.Instantiate(tno.channelID, "Item", destroyableObject, true, randomX, randomZ, autoDestroyDelay);
                DestroyImmediate(gameObject, true);
            }
        }

    }

    [RFC]
    private void NetworkAddHealth(int heal)
    {
        currentHealth += heal;
        Debug.Log("health = " + currentHealth + "/" + maxHealth);
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
    }

    [RFC]
    private void NetworkAddMaxHealth(int heal)
    {
        maxHealth += heal;
        currentHealth += heal;
    }

    [RCC]
    static GameObject Item(GameObject prefab, float XPos, float ZPos, float autoDestroyDelay)
    {
        prefab.Instantiate();
        prefab.transform.position = new Vector3(XPos, 0, ZPos);
        prefab.DestroySelf(autoDestroyDelay);
        return prefab;
    }
}

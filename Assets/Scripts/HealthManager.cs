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

        if (currentHealth <= 0 && tno.isMine)
        {
            Die();
        }
    }

    private void Die()
    {
        if (tno.isMine)
            tno.Send("ResetLife", Target.AllSaved);
    }


    [RFC]
    private void ResetLife()
    {
        GetComponent<DeathManager>().Die(tno.uid);
        maxHealth = StaticData.DataInstance.hpMax;
        currentHealth = maxHealth;
    }

    [RFC]
    private void NetworkAddHealth(int heal)
    {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
            currentHealth = maxHealth;
        Debug.Log("health = " + currentHealth + "/" + maxHealth);
    }

    [RFC]
    private void NetworkAddMaxHealth(int heal)
    {
        maxHealth += heal;
        currentHealth += heal;
        Debug.Log("health = " + currentHealth + "/" + maxHealth);
    }
}

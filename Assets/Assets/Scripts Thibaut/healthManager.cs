using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using UnityEngine.UI;

public class healthManager : TNBehaviour
{

    public int health;
    public int maxHealth;
    public int maxHealthBuff;

    public void RemoveHealth(int damage)
    {
        if (tno.isMine)
            tno.Send("NetworkRemoveHealth", Target.AllSaved, damage);
    }

    public void AddHealth(int heal)
    {
        if (tno.isMine)
            tno.Send("NetworkGiveHealth", Target.AllSaved, heal);
    }

    [RFC]
    private void NetworkRemoveHealth(int damage)
    {
        health -= damage;
        Debug.Log(health);
        if (health <= 0)
            DestroySelf();
    }

    [RFC]
    private void NetworkGiveHealth(int heal)
    {
        health += heal;
        Debug.Log(health);
        if (health >= maxHealth)
        {
            GetComponent<bigBoi>().GetBig();
            maxHealth += maxHealthBuff;
        }

    }

}

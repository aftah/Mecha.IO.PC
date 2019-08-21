using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;

public class HealthManagerExample : TNBehaviour
{
    public int currentHealth;
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
            tno.Send("NetworkAddHealth", Target.AllSaved, heal);
    }

    [RFC]
    private void NetworkRemoveHealth(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
            DestroySelf();
    }

    [RFC]
    private void NetworkAddHealth(int heal)
    {
        currentHealth += heal;
        if (currentHealth >= maxHealth)
        {
            GetComponent<GetBiggerExample>().GetBig();
            maxHealth += maxHealthBuff;
        }

    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TNet;

public class healthDisplay : TNBehaviour
{
    public System.Collections.Generic.List<Text> healthDisplays = new System.Collections.Generic.List<Text>();
    public System.Collections.Generic.List<healthManager> charHealth = new System.Collections.Generic.List<healthManager>();

    private void Update()
    {
        for (int i = 0; i < charHealth.Count; i++)
        {
        //    tno.Send("UpdateHealthText", Target.AllSaved, i, charHealth[i]._nHealth);
        }
    }

    [RFC]
    private void UpdateHealthText(int character, int health)
    {
        healthDisplays[character].text = health.ToString();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System;

public class ChangeCharacterSize : TNBehaviour
{
    [SerializeField] private System.Collections.Generic.List<GameObject> characterModels = new System.Collections.Generic.List<GameObject>();
    public System.Collections.Generic.List<GameObject> CharacterModels { get => characterModels; private set => characterModels = value; }
    private int currentSize = 0;

    protected override void Awake()
    {
        base.Awake();
        FindObjectOfType<PointsManager>().onLevelUp += OnLevelUpEventHandler;
        FindObjectOfType<DeathManager>().onDeathEvent += OnDeathEventHandler;
    }

    private void OnDeathEventHandler(object sender, DeathManager.OnDeathEventArgs e)
    {
        ResetSize();
    }

    private void OnLevelUpEventHandler(object sender, PointsManager.LevelUpEventArgs e)
    {
        IncreaseSize();
    }

    public void ResetSize()
    {
        if (tno.isMine)
        {
            tno.Send("NetworkResetSize", Target.AllSaved);
        }
    }

    public void IncreaseSize()
    {
        if (tno.isMine)
        {
            tno.Send("NetworkIncreaseSize", Target.AllSaved);
        }
    }

    [RFC]
    private void NetworkResetSize()
    {
        CharacterModels[currentSize].SetActive(false);
        currentSize = 0;
        CharacterModels[currentSize].SetActive(true);
    }

    [RFC]
    private void NetworkIncreaseSize()
    {
        CharacterModels[currentSize].SetActive(false);
        currentSize++;
        CharacterModels[currentSize].SetActive(true);
    }

}

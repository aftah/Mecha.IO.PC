using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TNet;
using System;

public class PointsManager : TNBehaviour
{
    private int pointsThreshold;

    public int PointsThreshold
    {
        get { return pointsThreshold; }
        set { pointsThreshold = value; }
    }

    private int pointsThresholdPerLevel;

    private int currentPoints = 0;

    public int CurrentPoints
    {
        get { return currentPoints; }
        set { currentPoints = value; }
    }

    public int CurrentLevel { get => currentLevel; set => currentLevel = value; }

    private int currentLevel = 1;
    private int maxLevel;

    public class LevelUpEventArgs : EventArgs
    {
    }

    public event EventHandler<LevelUpEventArgs> onLevelUp;

    public void OnLevelUp(LevelUpEventArgs e)
    {
        onLevelUp?.Invoke(this, e);
    }


    protected override void Awake()
    {
        base.Awake();
        pointsThreshold = StaticData.DataInstance.pointsThreshold;
        pointsThresholdPerLevel = StaticData.DataInstance.pointsThresholdPerLevel;
        maxLevel = FindObjectOfType<ChangeCharacterSize>().CharacterModels.Count;
    }

    public void PointsAdd(int points)
    {
        currentPoints += points;
        Debug.Log("Current Points = " + currentPoints);
        if (currentPoints >= pointsThreshold && CurrentLevel < maxLevel)
        {
            OnLevelUp(new LevelUpEventArgs());
            pointsThreshold += pointsThresholdPerLevel;
            CurrentLevel++;
            Debug.Log("Current Level = " + CurrentLevel);
        }

    }

}

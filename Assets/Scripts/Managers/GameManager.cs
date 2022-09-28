using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using DG.Tweening;

public class GameManager : MonoSingleton<GameManager>
{
    [Header("Changeable Game Settings")]
    [Range(0, 5)]
    public float WaterFillingSpeed = 1f;
    [Range(0,5)]
    public float CameraChangeTime = 2f;
    [Space]

    [HideInInspector]
    public GameStates GameState;

    [Header("Other Settings")]
    public CinemachineBrain cinemachineBrain;

    void Start()
    {
        cinemachineBrain.m_DefaultBlend.m_Time = CameraChangeTime;
    }

    public enum GameStates
    {
        Start,
        Play,
        Wait,
        Failed,
        Completed
    }

    public void LevelCompleted()
    {
        PlayerPrefs.SetInt("Level", PlayerPrefs.GetInt("Level", 1) + 1);
        UIManager.Instance.CompletedPanel.SetActive(true);
        UIManager.Instance.CompletedPanel.transform.DOScale(Vector3.zero,1f).SetEase(Ease.InOutBack).From();
    }
}

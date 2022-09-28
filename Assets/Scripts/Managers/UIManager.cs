using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
using DG.Tweening;

public class UIManager : MonoSingleton<UIManager>
{
    public GameObject StartPanel;
    public GameObject GamePanel;
    public GameObject CompletedPanel;
    public TextMeshProUGUI TutorialText;
    public TextMeshProUGUI LevelText;
    public ParticleSystem firstParticleSystem;
    int level;

    void Start()
    {
        StartPanel.SetActive(true);
        GamePanel.SetActive(true);
        CompletedPanel.SetActive(false);
        LevelText.gameObject.SetActive(true);
        TutorialText.gameObject.SetActive(false);
        level = PlayerPrefs.GetInt("Level", 1);
        LevelText.text = "Level " + level.ToString();

    }

    public void TutorialTextControl(bool Open, string text)
    {
        if (level == 1)
        {
            TutorialText.text = text;
            TutorialText.gameObject.SetActive(Open);
        }
    }


    public void StartButton()
    {
        TutorialTextControl(true, "click the pen");
        StartPanel.transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutBack);
        GameManager.Instance.GameState = GameManager.GameStates.Play;
    }

    public void CompletedButton()
    {
        SceneManager.LoadScene(0);
    }
}

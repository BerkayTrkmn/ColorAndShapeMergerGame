using System;
using System.Collections.Generic;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using HelloScripts;
using System.Collections;

public class UIManager : MonoBehaviour
{
    [Header("Managers")]
    [SerializeField] private MoneyCreator moneyCreator;

    [Header("Canvas")]
    [SerializeField] private RectTransform canvas;

    [Header("Panels")]
    public List<GameObject> panelList;

    [Header("TextMeshPro")]
    [SerializeField] public TMP_Text levelText;

    public TextMeshProUGUI moneyText;

    [SerializeField] private Slider remainingMoveSlider;
    [SerializeField] private Slider destroyedLineSlider;
    [SerializeField] private TextMeshProUGUI remainingMoveText;

    private void Start()
    {
        SetLevelUI();
    }

    private void OnEnable()
    {
        Config.OnLevelCreationEnded += OnLevelCreationEnded;
        Config.OnLevelStarted += OnLevelStarted;
        Config.OnLevelFailed += OnLevelFailed;
        Config.OnLevelCompleted += OnLevelCompleted;
        Config.OnItemPlaced += OnItemPlaced;
    }
    private void OnDisable()
    {
        Config.OnLevelCreationEnded -= OnLevelCreationEnded;
        Config.OnLevelStarted -= OnLevelStarted;
        Config.OnLevelFailed -= OnLevelFailed;
        Config.OnLevelCompleted -= OnLevelCompleted;
        Config.OnItemPlaced -= OnItemPlaced;
    }
    private void OnLevelCompleted()
    {
        PanelActivityChange(2, true);
        destroyedLineSlider.gameObject.SetActive(false);
        remainingMoveSlider.gameObject.SetActive(false);
    }

    private void OnLevelFailed()
    {
        PanelActivityChange(3, true);
        TouchManager.Instance.isActive = false;
        destroyedLineSlider.gameObject.SetActive(false);
        remainingMoveSlider.gameObject.SetActive(false);
    }

   
    public void SetLevelUI()
    {
        levelText.text = "LEVEL " + Config.VAR_LEVELNUMBER;
        destroyedLineSlider.maxValue = Config.CONST_NEEDEDLINEDESTROY;
        remainingMoveSlider.maxValue = Config.CONST_TOTALMOVE;
        Config.VAR_REMAININGMOVE = Config.CONST_TOTALMOVE;
        StartCoroutine(SetRemainingText(0f));
    }
    private void OnItemPlaced(Item obj)
    {
        Config.VAR_REMAININGMOVE--;
       StartCoroutine(SetRemainingText(0.25f));
       StartCoroutine(SetDestroyedLineText(0.25f));
        if(Config.VAR_REMAININGMOVE == 0 && Config.VAR_CURRENTLINEDESTROYCOUNT != Config.CONST_NEEDEDLINEDESTROY)
            Config.OnLevelFailed.Invoke();
    }

    public IEnumerator SetRemainingText(float _delay)
    {
        yield return _delay.GetWait();
        remainingMoveSlider.value = Config.VAR_REMAININGMOVE;
        remainingMoveText.text = Config.VAR_REMAININGMOVE + "/" + Config.CONST_TOTALMOVE;
    }
    public IEnumerator SetDestroyedLineText(float _delay)
    {
        yield return _delay.GetWait();
        destroyedLineSlider.value = Config.VAR_CURRENTLINEDESTROYCOUNT;
    }
    private void OnLevelStarted()
    {
        PanelActivityChange(1, true);
    }

    private void OnLevelCreationEnded()
    {
        PanelActivityChange(0, true);
    }
 

    public void NextLevel()
    {
        Config.VAR_LEVELNUMBER++;
        PlayerPrefs.SetInt(Config.PREF_LEVELNUMBER, Config.VAR_LEVELNUMBER);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void RetryLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
  
    public void GameStartButton()
    {
        PanelActivityChange(0, false);
        Config.OnLevelStarted?.Invoke();
    }
    public void PanelActivityChange(int index, bool isActive)
    {
        panelList[index].SetActive(isActive);
    }
  
   
}
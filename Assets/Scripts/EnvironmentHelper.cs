using UnityEngine;

public class EnvironmentHelper : MonoBehaviour
{
    [SerializeField] private GameObject[] environmentObjects;
    [SerializeField] private Color[] environmentColors;

    private void Start()
    {
        var currentLevelNo = (PlayerPrefs.GetInt("LevelNo", 1) - 1) % 2;
        environmentObjects[currentLevelNo].SetActive(true);
        RenderSettings.fogColor = environmentColors[currentLevelNo];
    }
}
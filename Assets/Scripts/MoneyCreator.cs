using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;
using DG.Tweening;
using System;
using  Random = UnityEngine.Random; 

public class MoneyCreator : MonoBehaviour
{
    public int goldCount = 5;
    public float goldCreateInterval = 0.2f;

    public GameObject goldPrefab;
    public Transform endPoint;
    public static Action<int> onGetGold;
    private void OnEnable()
    {
        onGetGold += CreateGold;
    }
    private void OnDisable()
    {
        onGetGold -= CreateGold;
    }

    public void CreateGold(int goldCount)
    {
        for (int i = 0; i < goldCount; i++)
        {
            GameObject goldGO = Instantiate(goldPrefab);
            goldGO.transform.SetParent(transform);
            goldGO.transform.position = transform.position;
            goldGO.transform.DOMove(goldGO.transform.position + new Vector3(Random.Range(-190, 190), Random.Range(-190, 190), 0),Random.Range(0.3f,0.5f)).OnComplete(()=>
            { goldGO.transform.DOMove(endPoint.position, 0.5f).SetDelay(0.3f).OnComplete(()=>
            {  Destroy(goldGO); });});
            goldGO.transform.localScale = Vector3.one;
            
        }
    }
    public static void GoldUICreate(int goldNumber)
    {
        onGetGold?.Invoke(goldNumber);
    } 
}

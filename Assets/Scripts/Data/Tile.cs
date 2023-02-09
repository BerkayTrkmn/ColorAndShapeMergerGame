using DG.Tweening;
using HelloScripts;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour, IPlaceable
{

    public int x;
    public int y;
    [SerializeField] private float startAnimationStartPointDifference = 1.5f;
    [SerializeField] private float animationTime = 0.5f;
    public Item placedItem = null;

    public bool Place(Item _item)
    {
        if (placedItem == null)
        {
            _item.PlaceItemMovement(transform.position);
            _item.ItemCollider.enabled = true;
            _item.IsPlaced = true;
            placedItem = _item;
            _item.PlacedTile = this;
            Config.OnItemPlaced?.Invoke(_item);
            return true;
        }
        return false;
    }

    public void StartingAnimation(Vector3 endPosition, float endScale, Action endAnction = null)
    {
        gameObject.SetActive(true);
        transform.position = transform.position.ChangeY(endPosition.y + startAnimationStartPointDifference);
        transform.DOMoveY(endPosition.y, animationTime).SetEase(Ease.InOutSine);
        transform.localScale = transform.localScale.ChangeY(transform.localScale.y * 1.5f);
        transform.localScale = Vector3.zero;
        transform.DOScale(endScale, animationTime).SetEase(Ease.InOutSine).OnComplete(() => { endAnction?.Invoke(); });
    }

    public void ClearTile()
    {
        placedItem.destroyParticle.CreateGameObjectandPlaceIt(placedItem.transform);
        placedItem.gameObject.Destroy();
        placedItem = null;
    }

}

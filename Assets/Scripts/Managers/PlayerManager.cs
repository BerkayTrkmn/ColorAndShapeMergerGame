using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HelloScripts;
using System;
using DG.Tweening;

public class PlayerManager : MonoBehaviour
{

    private Item holdingItem;
    private Camera cameraMain;
    [SerializeField] private float speed=0.001f;
    private Vector3 startingPosition;
    private void OnEnable()
    {
        cameraMain = Camera.main;
        TouchManager.Instance.onTouchEnded += OnTouchEnded;
        TouchManager.Instance.onTouchMoved += OnTouchMoved;
        TouchManager.Instance.onTouchBegan += OnTouchBegan;
    }
    //Get Item control
    private void OnTouchBegan(TouchInput touch)
    {
        Ray ray = cameraMain.ScreenPointToRay(touch.ScreenPosition);
        Debug.DrawRay(touch.ScreenPosition, cameraMain.transform.forward * 10, Color.red);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            if(hit.transform.parent.TryGetComponent<Item>(out Item _item )&& !_item.IsPlaced)
            {
                startingPosition = _item.transform.position;
                holdingItem = _item;
                holdingItem.MoveItemAtYAxis(1f);
                holdingItem.ItemCollider.enabled = false;
            }
            Debug.Log(hit.transform.root.gameObject.name);
        }
    }
    //Item Move with touch
    private void OnTouchMoved(TouchInput touch)
    {
        if (!ReferenceEquals(holdingItem, null))
        {
        holdingItem.transform.position = holdingItem.transform.position.ChangeXZ(holdingItem.transform.position.x +(touch.DeltaScreenPosition.x*speed), holdingItem.transform.position.z + (touch.DeltaScreenPosition.y*speed));
        }
    }
    //Touch end control of hit 
    private void OnTouchEnded(TouchInput touch)
    {
        if (!ReferenceEquals(holdingItem, null))
        {
            Ray ray = cameraMain.ScreenPointToRay(touch.ScreenPosition);
            Debug.DrawRay(touch.ScreenPosition, cameraMain.transform.forward * 10, Color.red);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                if (hit.transform.parent.TryGetComponent<IPlaceable>(out IPlaceable _placeable))
                {
                    if (!_placeable.Place(holdingItem))
                        holdingItem.PlaceItemMovement(startingPosition);
                }
                else
                    holdingItem.PlaceItemMovement(startingPosition);

                Debug.Log(hit.transform.root.gameObject.name);
            }
            else
                holdingItem.PlaceItemMovement(startingPosition);

            holdingItem.ItemCollider.enabled = true;
            holdingItem = null;
        }
    }

    private void OnDisable()
    {
        TouchManager.Instance.onTouchEnded -= OnTouchEnded;
        TouchManager.Instance.onTouchMoved -= OnTouchMoved;
        TouchManager.Instance.onTouchBegan -= OnTouchBegan;
    }


}

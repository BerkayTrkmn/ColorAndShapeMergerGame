using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using System;
using Random = UnityEngine.Random;
using System.Linq;
using HelloScripts;

public class Item : MonoBehaviour,IPlaceable
{
    [SerializeField]private int id;
    public int Id { get => id; }
    public ColorType ColorType;
    public Material ItemMaterial;
    public MeshRenderer MeshRenderer;
    [HideInInspector]public Collider ItemCollider;
    public bool IsPlaced = false;
    public Tile PlacedTile { get; set; }
    public GameObject destroyParticle;



    private void Start()
    {
        ItemCollider = MeshRenderer.GetComponent<Collider>();
        RotateItem();
    }
    private void RotateItem()
    {
        transform.DORotate(Vector3.up*360f, 2f, RotateMode.Fast).SetLoops(-1).SetEase(Ease.Linear);
    }
    public void PlaceItemMovement(Vector3 endPoint)
    {
        transform.DOMove(endPoint, 0.2f);
    }
    public void SetRandomColor()
    {
        int _colorTypeCount = Enum.GetNames(typeof(ColorType)).Length;
        ColorType _randomType = (ColorType)Random.Range(0, _colorTypeCount);
        SetColor(_randomType);
    }
    public void SetDifferentColor(ColorType _colorType)
    {
        int _colorTypeCount = Enum.GetNames(typeof(ColorType)).Length;
        ColorType _differentType = (ColorType)Random.Range(0, _colorTypeCount);
        while (_colorType == _differentType)
        {
            _differentType = (ColorType)Random.Range(0, _colorTypeCount);
        }
        SetColor(_differentType);
    }
    public void SetColor(ColorType _colorType)
    {
        Material _tempMat = MeshRenderer.material;
        _tempMat.color = Config.VAR_COLORS[_colorType];
        ColorType = _colorType;
    }
    public void MoveItemAtYAxis(float difference)
    {
        transform.DOMoveY(transform.position.y + difference, 0.15f);
    }
    public virtual bool Place(Item _item)
    {
        _item.PlacedTile = this.PlacedTile;
        return ChangeObject(_item);
    }
    /// <summary>
    /// Change placed object according to dragged object
    /// </summary>
    /// <param name="_item">dragged object</param>
    /// <returns></returns>
    public bool ChangeObject(Item _item)
    {

        if (_item.ColorType == this.ColorType && _item.Id == this.id)
        {
          Item currentItem = ChangePlacedObject(_item);
            currentItem.SetDifferentColor(this.ColorType);
            Config.OnItemPlaced?.Invoke(currentItem);
            gameObject.Destroy();
            return true;
        }
        else if (_item.ColorType == this.ColorType)
        {
            ChangePlacedObjectColor(_item);
            Config.OnItemPlaced?.Invoke(this);
            return true;
        }
        else if (_item.Id == this.id)
        {
            Item currentItem =  ChangePlacedObject(_item);
            Config.OnItemPlaced?.Invoke(currentItem);
            gameObject.Destroy();
            return true;
        }

        return false;
    }
    /// <summary>
    /// Is equal with another item
    /// </summary>
    /// <param name="_item">Another Item</param>
    public bool CheckEquality(Item _item)
    {
        if (_item.ColorType == this.ColorType && _item.Id == this.id)
            return true;
        return false;
    }
    public Item ChangePlacedObject(Item _item)
    {
        List<Item> _itemPrefabs = Config.VAR_ITEMPREFABS.ToList();
        for (int i = 0; i < _itemPrefabs.Count; i++)
        {
            if (_itemPrefabs[i].GetType() == this.GetType())
                _itemPrefabs.RemoveAt(i);
        }
        Item newItem = Instantiate(_itemPrefabs[Random.Range(0, _itemPrefabs.Count)], _item.transform.position, Quaternion.identity);
       
        newItem.SetColor(ColorType);
        newItem.PlacedTile = this.PlacedTile;
        newItem.IsPlaced = true;
        newItem.PlaceItemMovement(PlacedTile.transform.position);
        PlacedTile.placedItem = newItem;
        _item.gameObject.Destroy();
        
        return newItem;
    }
    public Item ChangePlacedObjectColor(Item _item)
    {

        SetDifferentColor(this.ColorType);
        _item.gameObject.Destroy();
       
        return this;
    }
}

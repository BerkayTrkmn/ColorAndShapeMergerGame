using UnityEngine;
using UnityEditor;

namespace HelloScripts
{
    public abstract class Array2DObjectDrawer<T> : Array2DDrawer where T : Object
    {
        protected override Vector2Int GetDefaultCellSizeValue() => new Vector2Int(64, 64);

        protected override object GetDefaultCellValue() => null;

        protected override object GetCellValue(SerializedProperty cell) => cell.objectReferenceValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.objectReferenceValue = (T)obj;
        }
    }
}
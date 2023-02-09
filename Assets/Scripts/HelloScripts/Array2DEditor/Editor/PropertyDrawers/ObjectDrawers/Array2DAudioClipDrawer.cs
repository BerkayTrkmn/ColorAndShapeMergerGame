using UnityEngine;
using UnityEditor;

namespace HelloScripts
{
    [CustomPropertyDrawer(typeof(Array2DAudioClip))]
    public class Array2DAudioClipDrawer : Array2DObjectDrawer<AudioClip>
    {
        protected override Vector2Int GetDefaultCellSizeValue() => new Vector2Int(96, 16);
    }
}

using UnityEngine;
using UnityEditor;

namespace HelloScripts
{
    [CustomPropertyDrawer(typeof(Array2DSprite))]
    public class Array2DSpriteDrawer : Array2DObjectDrawer<Sprite> { }
}

using UnityEditor;

namespace HelloScripts
{
    [CustomPropertyDrawer(typeof(Array2DFloat))]
    public class Array2DFloatDrawer : Array2DDrawer
    {
        protected override object GetDefaultCellValue() => 0f;

        protected override object GetCellValue(SerializedProperty cell) => cell.floatValue;

        protected override void SetValue(SerializedProperty cell, object obj)
        {
            cell.floatValue = (float) obj;
        }
    }
}

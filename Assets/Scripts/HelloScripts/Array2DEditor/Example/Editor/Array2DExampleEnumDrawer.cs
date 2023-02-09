using UnityEditor;

namespace HelloScripts
{
    [CustomPropertyDrawer(typeof(Array2DExampleEnum))]
    public class Array2DExampleEnumDrawer : Array2DEnumDrawer<ExampleEnum> {}
}

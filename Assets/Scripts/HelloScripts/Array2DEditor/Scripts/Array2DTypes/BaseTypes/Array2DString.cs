using UnityEngine;

namespace HelloScripts
{
    [System.Serializable]
    public class Array2DString : Array2D<string>
    {
        [SerializeField]
        CellRowString[] cells = new CellRowString[Consts.defaultGridSize];

        protected override CellRow<string> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}

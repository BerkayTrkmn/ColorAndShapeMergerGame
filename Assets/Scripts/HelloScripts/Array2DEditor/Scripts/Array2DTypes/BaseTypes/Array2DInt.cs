using UnityEngine;

namespace HelloScripts
{
    [System.Serializable]
    public class Array2DInt : Array2D<int>
    {
        [SerializeField]
        CellRowInt[] cells = new CellRowInt[Consts.defaultGridSize];

        protected override CellRow<int> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}

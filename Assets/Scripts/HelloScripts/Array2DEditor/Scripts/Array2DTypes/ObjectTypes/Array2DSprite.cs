using UnityEngine;

namespace HelloScripts
{
    [System.Serializable]
    public class Array2DSprite : Array2D<Sprite>
    {
        [SerializeField]
        CellRowSprite[] cells = new CellRowSprite[Consts.defaultGridSize];

        protected override CellRow<Sprite> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}

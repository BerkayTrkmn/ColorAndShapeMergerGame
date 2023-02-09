using UnityEngine;

namespace HelloScripts
{
    [System.Serializable]
    public class Array2DAudioClip : Array2D<AudioClip>
    {
        [SerializeField]
        CellRowAudioClip[] cells = new CellRowAudioClip[Consts.defaultGridSize];

        protected override CellRow<AudioClip> GetCellRow(int idx)
        {
            return cells[idx];
        }
    }
}
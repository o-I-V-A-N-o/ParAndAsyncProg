using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellsPool : MonoBehaviour
    {
        public GameObject[] _CellsInTable;
        public ChipComponent _SelectedChip;
        public ChipComponent _ChipForDelete;

        private void Start()
        {
            InitCells();
        }

        private void InitCells()
        {
            foreach (GameObject _cells in _CellsInTable)
            {
                CellComponent _cell = _cells.GetComponent<CellComponent>();
                _cell.SetNeighbors();
            }
        }
    }
}

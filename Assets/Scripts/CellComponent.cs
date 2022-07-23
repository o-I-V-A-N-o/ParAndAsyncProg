using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Assets.Scripts
{
    public class CellComponent : BaseClickComponent
    {
        private Dictionary<NeighborType, CellComponent> _neighbors = new Dictionary<NeighborType, CellComponent>();

        public bool isNeighbor = false;

        /// <summary>
        /// Возвращает соседа клетки по указанному направлению
        /// </summary>
        /// <param name="type">Перечисление направления</param>
        /// <returns>Клетка-сосед или null</returns>
        public CellComponent GetNeighbors(NeighborType type) => _neighbors[type];

        public void SetNeighbors()
        {
            var _cells = transform.parent.gameObject.GetComponent<CellsPool>()._CellsInTable;
            foreach (GameObject neighbor in _cells)
            {
                float myX = transform.position.x;           //X координата ячейки
                float nX = neighbor.transform.position.x;   //X координата возможного соседа
                float myZ = transform.position.z;           //Z координата ячейки
                float nZ = neighbor.transform.position.z;   //Z координата возможного соседа
                if ((myX - 1 == nX) && (myZ + 1 == nZ))
                {
                    _neighbors.Add(NeighborType.TopLeft, neighbor.GetComponent<CellComponent>());
                }
                else if ((myX + 1 == nX) && (myZ + 1 == nZ))
                {
                    _neighbors.Add(NeighborType.TopRight, neighbor.GetComponent<CellComponent>());
                }
                else if ((myX - 1 == nX) && (myZ - 1 == nZ))
                {
                    _neighbors.Add(NeighborType.BottomLeft, neighbor.GetComponent<CellComponent>());
                }
                else if ((myX + 1 == nX) && (myZ - 1 == nZ))
                {
                    _neighbors.Add(NeighborType.BottomRight, neighbor.GetComponent<CellComponent>());
                }
            }
            Debug.Log("sosed " + _neighbors);
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            CallBackEvent(this, true);
            
            if (GetColor == ColorType.Black)
            {
                AddAdditionalMaterial(Resources.Load<Material>("Materials/CellBlackOnPointer"), 1);
            }
            //Debug.Log(this.transform.position + " --- " + this.transform);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            CallBackEvent(this, false);
            RemoveAdditionalMaterial(1);
        }

        public override void OnClickMe(PointerEventData eventData)
        {
            ChipComponent _chip = transform.parent.gameObject.GetComponent<CellsPool>()._SelectedChip;
            Transform _child = null;
            
            if (_chip != null) // && isNeighbor
            {
                if (this.transform.childCount > 0)
                {
                    _child = this.transform.GetChild(0);
                }
                else
                {
                    _chip.GetComponent<ChipComponent>().MoveChipTo(eventData.pointerPress.transform.position);
                    _chip.transform.SetParent(this.transform);
                    transform.parent.gameObject.GetComponent<CellsPool>()._SelectedChip = null;
                    CellComponent _neighborLeft = GetNeighbors(NeighborType.TopLeft);
                    CellComponent _neighborRight = GetNeighbors(NeighborType.TopRight);
                    CellComponent _neighborBLeft = GetNeighbors(NeighborType.BottomLeft);
                    CellComponent _neighborBRight = GetNeighbors(NeighborType.BottomRight);
                    Debug.Log("соседи - " + _neighborLeft.name + " - " + _neighborRight.name + " - " + _neighborBLeft.name + " - " + _neighborBRight.name);
                }
            }
        }
        /// <summary>
        /// Конфигурирование связей клеток
        /// </summary>
		public void Configuration(Dictionary<NeighborType, CellComponent> neighbors)
        {
            if (_neighbors != null) return;
            _neighbors = neighbors;
        }

        public void Logg(string name)
        {
            Debug.Log("WOW " + name);
        }
    }

    /// <summary>
    /// Тип соседа клетки
    /// </summary>
    public enum NeighborType : byte
    {
        /// <summary>
        /// Клетка сверху и слева от данной
        /// </summary>
        TopLeft,
        /// <summary>
        /// Клетка сверху и справа от данной
        /// </summary>
        TopRight,
        /// <summary>
        /// Клетка снизу и слева от данной
        /// </summary>
        BottomLeft,
        /// <summary>
        /// Клетка снизу и справа от данной
        /// </summary>
        BottomRight
    }
}
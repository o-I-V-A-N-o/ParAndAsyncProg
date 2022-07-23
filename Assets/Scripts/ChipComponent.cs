using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

namespace Assets.Scripts
{
    public class ChipComponent : BaseClickComponent
    {
        GameObject _parentObject;
        public void GetParentObject()
        {
            _parentObject = transform.parent.gameObject;
        }

        public override void OnPointerEnter(PointerEventData eventData)
        {
            CallBackEvent((CellComponent)Pair, true);
            Debug.Log("ENTER Chip " + name);
            
            if (GetColor == ColorType.Black)
            {
                AddAdditionalMaterial(Resources.Load<Material>("Materials/BlackChipOnPointer"), 1);
            }else if (GetColor == ColorType.White)
            {
                AddAdditionalMaterial(Resources.Load<Material>("Materials/WhiteChipOnPointer"), 1);
            }
            transform.parent.gameObject.GetComponent<CellComponent>().OnPointerEnter(eventData);
        }

        public override void OnPointerExit(PointerEventData eventData)
        {
            CallBackEvent((CellComponent)Pair, false);
            RemoveAdditionalMaterial(1);
        }

        public override void OnClickMe(PointerEventData eventData)
        {
            transform.parent.gameObject.GetComponent<CellComponent>().OnClickMe(eventData);
            transform.parent.gameObject.transform.parent.gameObject.GetComponent<CellsPool>()._SelectedChip = this;
        }

        public void MoveChipTo(Vector3 endPosition)
        {
            StartCoroutine(MoveChip(endPosition));
        }

        private IEnumerator MoveChip(Vector3 endPosition)
        {
            var currentTime = 0f;
            var time = 2f;
            while (currentTime < time)
            {
                transform.position = Vector3.Lerp(this.transform.position, endPosition, 1 - (time - currentTime) / time);
                currentTime += Time.deltaTime;
                yield return null;
            }
            transform.position = endPosition;
        }
    }
}

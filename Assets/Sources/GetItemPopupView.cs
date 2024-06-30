using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Sources
{
    public class GetItemPopupView : MonoBehaviour
    {

        [SerializeField] private GameObject _arrow;
        [SerializeField] private ItemRenderer _leftItem;
        [SerializeField] private ItemRenderer _rightItem;
        [SerializeField] private ItemRenderer _middleItem;
        [SerializeField] public Button buttonGet;
        [SerializeField] public Button buttonSell;


        [Inject] private ItemsData _itemsData;

        public void Enable(Item newItem, Item oldItem=null)
        {
            gameObject.SetActive(true);
            
            _middleItem.gameObject.SetActive(oldItem == null);
            _leftItem.gameObject.SetActive(oldItem != null);
            _rightItem.gameObject.SetActive(oldItem != null);
            _arrow.SetActive(oldItem != null);
            if (oldItem == null)
            {
                _middleItem.SetItem(newItem, _itemsData);
            }
            else
            {
                _leftItem.SetItem(oldItem, _itemsData);
                _rightItem.SetItem(newItem, _itemsData);
            }
            
        }
        
        public void Disable()
        {
          gameObject.SetActive(false);  
        }

        
    }
}
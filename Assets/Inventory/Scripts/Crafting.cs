using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Crafting : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    private Image _image;
    private string _resourceName;

    public UnityEvent<string> OnCraft;

    private void Start()
    {
        _image = GetComponent<Image>();
        _resourceName = transform.GetChild(2).GetComponent<TextMeshProUGUI>().text;

        OnCraft.AddListener(gameObject.GetComponentInParent<InventoryUI>().Craft);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = new Color(1, 1, 1, 1);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = new Color(1, 1, 1, 0.5f);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnCraft.Invoke(_resourceName);
    }
}

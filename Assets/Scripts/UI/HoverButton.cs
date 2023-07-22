using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class HoverButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField]
    private GameObject arrow;
    [SerializeField]
    private GameObject enabledSprite;
    [SerializeField]
    private GameObject originalSprite;
    void IPointerEnterHandler.OnPointerEnter(PointerEventData eventData)
    {
        arrow.transform.position = transform.position;
        enabledSprite.SetActive(true);
        originalSprite.SetActive(false);
    }

    void IPointerExitHandler.OnPointerExit(PointerEventData eventData)
    {
        originalSprite.SetActive(true);
        enabledSprite.SetActive(false);
    }
}

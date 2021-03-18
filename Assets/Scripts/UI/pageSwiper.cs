using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class pageSwiper : MonoBehaviour, IDragHandler, IEndDragHandler
{
    private Vector3 panelLocation;
    private void Start()
    {
        panelLocation = transform.position;
    }

    public void OnDrag(PointerEventData data)
    {
        
        float difference = data.pressPosition.y - data.position.y;
        transform.position = panelLocation - new Vector3(0, difference, 0);
        Debug.Log(data.pressPosition - data.position);
    }
    public void OnEndDrag(PointerEventData data)
    {
        panelLocation = transform.position;
    }
}

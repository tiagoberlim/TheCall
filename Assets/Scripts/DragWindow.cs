using UnityEngine;
using UnityEngine.EventSystems;

/// <summary>
/// Class responsible for that functionality of dragging UI elements.
/// </summary>
public class DragWindow : MonoBehaviour, IDragHandler, IPointerDownHandler
{
    /// <summary>
    /// Reference to the UI element's <c>RectTransform</c>.
    /// </summary>
    [SerializeField] private RectTransform dragRecTransform;

    /// <summary>
    /// Reference to the canvas where the UI element is being displayed.
    /// </summary>
    [SerializeField] private Canvas canvas;

    /// <summary>
    /// Drags the UI element according to the cursor movement.
    /// </summary>
    /// <param name="eventData">Cursor data.</param>
    public void OnDrag(PointerEventData eventData)
    {
        dragRecTransform.anchoredPosition += eventData.delta / canvas.scaleFactor;
        //throw new System.NoImplementedException();
        //Debug.Log("draging");
    }

    /// <summary>
    /// Releases the UI element.
    /// </summary>
    /// <param name="eventData">Cursor data.</param>
    public void OnPointerDown(PointerEventData eventData)
    {
        dragRecTransform.SetAsLastSibling();
    }
}

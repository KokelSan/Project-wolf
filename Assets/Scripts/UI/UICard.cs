using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class UICard : UIBehaviour, IPointerEnterHandler, IPointerExitHandler, IDragHandler, IBeginDragHandler, IEndDragHandler
{
    [Header("Components references")]
    public RectTransform RectTransform;
    public CanvasGroup CanvasGroup;

    [Header("Interaction & drag configuration")]
    public GameObject ContentToDrag;
    public GameObject Outline;

    [Header("Visual and content")]
    public TMP_Text NameText;

    private int index;
    private UICardDisplayPanel displayPanel;

    private bool isDragged => draggedElement != null;
    private UICard draggedElement;
    private Vector2 dragOffset;

    public void Initialize(UICardDisplayPanel displayPanel = null, int? index = null)
    {
        this.displayPanel = displayPanel;
        this.index = index ?? transform.GetSiblingIndex();
        name = $"Card {this.index}";
        NameText.text = name;

        if (index != null)
        {
            name += " (dragged)";
            SetInteractability(false);
        }
    }

    private void SetInteractability(bool isInteractable)
    {
        CanvasGroup.interactable = isInteractable;
        CanvasGroup.blocksRaycasts = isInteractable;
        SetOutlineVisibility(isInteractable);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        SetOutlineVisibility(true);
        displayPanel.RegisterHoveredCard(this);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        SetOutlineVisibility(false);
        displayPanel.UnregisterHoveredCard(this);
    }

    private void SetOutlineVisibility(bool isVisible)
    {
        Outline.SetActive(isVisible);
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if (isDragged) return;

        CreateDraggedContent();
        ContentToDrag.gameObject.SetActive(false);
        dragOffset = eventData.position - (Vector2)RectTransform.position;
        displayPanel.RegisterDraggedCard(this);
    }    

    private void CreateDraggedContent()
    {
        draggedElement = Instantiate(this, FindObjectOfType<UICardDisplayPanel>().transform);
        draggedElement.Initialize(index:index);
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (!isDragged) return;

        draggedElement.transform.position = eventData.position - dragOffset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!isDragged) return;

        Destroy(draggedElement.gameObject);
        draggedElement = null;
        ContentToDrag.gameObject.SetActive(true);

        displayPanel.UnregisterDraggedCard(this);
    }
}

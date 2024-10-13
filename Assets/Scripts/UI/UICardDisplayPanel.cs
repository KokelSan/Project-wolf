using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class UICardDisplayPanel : MonoBehaviour
{
    [Header("Components references")]
    public ScrollRect ScrollRect;

    [Header("Cards configuration")]
    public UICard CardPrefab;

    [Header("Debug")]
    public int CardsCountOnStart = 6;

    private List<UICard> cards = new List<UICard>();
    private bool hasCards => cards.Count > 0;

    private UICard draggedCard;
    private UICard hoveredCard;

    private void Start()
    {
#if UNITY_EDITOR
        for (int i = 0; i < CardsCountOnStart; i++)
        {
            AddCard();
        } 
#endif
    }

    //[Button("Add card")]
    public void AddCard()
    {
        UICard card = Instantiate(CardPrefab, ScrollRect.content);
        card.Initialize(this);
        cards.Add(card);

        StartCoroutine(ComputeScrollMovementType());
    }

    //[Button(nameof(hasCards),ConditionResult.EnableDisable, buttonLabel:"Remove card")]
    public void RemoveCard()
    {
        if (!hasCards) return;

        UICard card = cards[cards.Count - 1];
        cards.Remove(card);
        DestroyImmediate(card.gameObject);

        StartCoroutine(ComputeScrollMovementType());
    }

    //[Button(nameof(hasCards), ConditionResult.EnableDisable, buttonLabel: "Reset cards")]
    public void ResetCards()
    {
        cards = ScrollRect.content.GetComponentsInChildren<UICard>().ToList();
        while (cards.Count > 0)
        {
            RemoveCard();
        }
    }

    private IEnumerator ComputeScrollMovementType()
    {
        if (ScrollRect == null) ScrollRect = transform.GetComponentInChildren<ScrollRect>();

        yield return new WaitForEndOfFrame();

        ScrollRect.movementType = ScrollRect.content.rect.width > ScrollRect.viewport.rect.width ? ScrollRect.MovementType.Elastic : ScrollRect.MovementType.Clamped;
    }

    public void RegisterHoveredCard(UICard card)
    {
        if (hoveredCard != null)
        {
            Debug.LogWarning("An hovered card is already registered !");
            return;
        }

        if (card == draggedCard) return;

        hoveredCard = card;
    }

    public void UnregisterHoveredCard(UICard card)
    {
        if (hoveredCard == null) return;

        if (card != hoveredCard) 
        {
            Debug.LogWarning("Trying to unregister a different hovered card !");
            return;
        }

        hoveredCard = null;
    }

    public void RegisterDraggedCard(UICard card)
    {
        draggedCard = card;
        if (card == hoveredCard) hoveredCard = null;
    }

    public void UnregisterDraggedCard(UICard card)
    {
        if (card != draggedCard)
        {
            Debug.LogWarning("Trying to unregister a different dragged card");
            return;
        }

        if (hoveredCard != null) 
        {
            SwapCard(draggedCard, hoveredCard);
        }

        draggedCard = null;
    }

    public void SwapCard(UICard card1, UICard card2)
    {
        int card1Siblingindex = card1.transform.GetSiblingIndex();
        card1.transform.SetSiblingIndex(card2.transform.GetSiblingIndex());
        card2.transform.SetSiblingIndex(card1Siblingindex);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardGridLayout : LayoutGroup
{
    public int rows; 
    public int columns; 
    public int topPadding; 
    public int sidePadding; 
    public Vector2 spacing; 
    public Vector2 cardSize; 

    public override void CalculateLayoutInputVertical()
    {
        if (rows == 0 || columns == 0)
        {
            rows = 4; 
            columns = 5; 
        }

        float parentWidth = rectTransform.rect.width; 
        float parentHeight = rectTransform.rect.height; 

        float cardHeight = (parentHeight - 2 * topPadding - spacing.y * (rows - 1)) / rows;
        float cardWidth = cardHeight;

        if (cardWidth * columns + spacing.x * (columns - 1) > parentWidth)
        {
            cardWidth = (parentWidth - 2 * sidePadding - (columns - 1) * spacing.x) / columns;
            cardHeight = cardWidth;
        }

        cardSize = new Vector2(cardWidth, cardHeight);

        padding.left = Mathf.FloorToInt((parentWidth - (columns * cardWidth + spacing.x * (columns - 1))) / 2);
        padding.right = padding.left;
        padding.top = Mathf.FloorToInt((parentHeight - (rows * cardHeight + spacing.y * (rows - 1))) / 2);
        padding.bottom = padding.top;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            int rowIndex = i / columns; 
            int columnIndex = i % columns; 

            var item = rectChildren[i];
            var xPos = padding.left + cardSize.x * columnIndex + spacing.x * columnIndex;
            var yPos = padding.top + cardSize.y * rowIndex + spacing.y * rowIndex;

            SetChildAlongAxis(item, 0, xPos, cardSize.x); 
            SetChildAlongAxis(item, 1, yPos, cardSize.y); 
        }
    }

    public override void SetLayoutHorizontal()
    {
        CalculateLayoutInputVertical();
    }

    public override void SetLayoutVertical()
    {
        CalculateLayoutInputVertical();
    }
}

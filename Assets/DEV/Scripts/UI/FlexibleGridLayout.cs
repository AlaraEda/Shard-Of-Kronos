using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlexibleGridLayout : LayoutGroup
{
    public Vector2 spacing;
    public Vector2 maxCellSize;
    public RectTransform rectTransformOverride;
    
    private int rows;
    private int columns;
    private Vector2 cellSize;

    
    public override void CalculateLayoutInputHorizontal()
    {
        base.CalculateLayoutInputHorizontal();

        float sqrRt = Mathf.Sqrt(transform.childCount);
        rows = Mathf.CeilToInt(sqrRt);
        columns = Mathf.CeilToInt(sqrRt);

        var rectTrans = rectTransform.rect;
        if (rectTransformOverride != null)
        {
            rectTrans = rectTransformOverride.rect;
        }

        float parentWidth = rectTrans.width;
        float parentHeight = rectTrans.height;

        var pad = padding;
        float cellWidth = (parentWidth / columns) - ((spacing.x / columns)* (columns - 1)) - (pad.right / (float)columns);
        float cellHeight = (parentHeight / rows) - ((spacing.y / rows)* (rows - 1)) - (pad.bottom /(float)rows);

        if (maxCellSize != Vector2.zero)
        {
            if (cellWidth >= maxCellSize.x)
            {
                cellWidth = maxCellSize.x;
            }

            if (cellHeight >= maxCellSize.y)
            {
                cellHeight = maxCellSize.y;
            }
        }

        cellSize.x = cellWidth;
        cellSize.y = cellHeight;

        for (int i = 0; i < rectChildren.Count; i++)
        {
            var rowCount = i / columns;
            var columnCount = i % columns;

            var item = rectChildren[i];
            var xPos = (cellSize.x * columnCount) + (spacing.x * columnCount) + pad.left;
            var yPos = (cellSize.y * rowCount) + (spacing.y * rowCount) + pad.top;
            
            SetChildAlongAxis(item, 0, xPos, cellSize.x);
            SetChildAlongAxis(item, 1, yPos, cellSize.y);
        }
    }
    
    public override void CalculateLayoutInputVertical()
    {
        
    }

    public override void SetLayoutHorizontal()
    {

    }

    public override void SetLayoutVertical()
    {
        
    }
}

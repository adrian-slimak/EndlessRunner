using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScrollRectSnap : MonoBehaviour
{
    public LevelPreviewUI levelUI;
    public RectTransform scrollPanel;
    public RectTransform[] levelPanels;
    public RectTransform center;
    public float distanceToStartSnap;

    private float[] distances;
    private float[] distanceRepositions;
    private bool dragging = false;
    private int panelDistance;
    private int currentDragPanel = 0;

    void Start()
    {
        int panelLength = levelPanels.Length;
        distances = new float[panelLength];
        distanceRepositions = new float[panelLength];
        panelDistance = (int)Mathf.Abs(levelPanels[1].anchoredPosition.x - levelPanels[0].anchoredPosition.x);
    }

    void Update()
    {
        if (dragging)
        {
            for (int i = 0; i < levelPanels.Length; i++)
            {
                distanceRepositions[i] = center.position.x - levelPanels[i].position.x;
                distances[i] = Mathf.Abs(distanceRepositions[i]);
            }

            float minDistance = Mathf.Min(distances);

            for (int i = 0; i < levelPanels.Length; i++)
                if (minDistance == distances[i])
                    currentDragPanel = i;

            levelUI.LevelClicked(currentDragPanel);
        }

        currentDragPanel = Mathf.Clamp(currentDragPanel, 0, levelPanels.Length - 1);

        if (!dragging)
            LerpToPanel(currentDragPanel * -panelDistance);
    }

    private void LerpToPanel(int position)
    {
        float newX = Mathf.Lerp(scrollPanel.anchoredPosition.x, position, Time.deltaTime * 10f);
        Vector2 newPosition = new Vector2(newX, scrollPanel.anchoredPosition.y);

        scrollPanel.anchoredPosition = newPosition;
    }

    public void SelectLevel(int level)
    {
        currentDragPanel = level;
    }

    public void StartDrag()
    {
        dragging = true;
    }

    public void EndDrag()
    {
        dragging = false;
    }
}

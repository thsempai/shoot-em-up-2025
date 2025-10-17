using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Canvas))]
public class LifeViewer : MonoBehaviour
{
    private Image image;
    private Canvas canvas;
    private Vector2 firstImagePosition;
    private Vector2 imageOffSet;

    private List<Image> images = new();

    public void Initialize(Image image, int initialLife, Vector2 firstImagePosition, Vector2 imageOffSet)
    {
        this.image = image;
        this.firstImagePosition = firstImagePosition;
        this.imageOffSet = imageOffSet;

        canvas = GetComponent<Canvas>();

        UpdateImages(initialLife);
    }

    public void UpdateImages(int life)
    {
        if (images.Count < life)
        {
            for (int index = 0; index < images.Count; index++)
            {
                images[index].gameObject.SetActive(true);
            }
            for (int index = images.Count; index < life; index++)
            {
                Image newImage = Instantiate(image, canvas.transform);
                images.Add(newImage);
                Vector2 position = firstImagePosition + index * imageOffSet;
                newImage.rectTransform.anchoredPosition = position;
            }
        }
        else
        {
            for (int index = 0; index < images.Count; index++)
            {
                images[index].gameObject.SetActive(index < life);
            }
        }

    }
}

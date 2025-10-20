using UnityEngine;
using TMPro;
[RequireComponent(typeof(TMP_Text))]
public class ScoreViewer : MonoBehaviour
{

    private int digit;
    public void Initialize(int digit, Canvas canvas, Vector2 position)
    {
        this.digit = digit;
        transform.parent = canvas.transform;
        GetComponent<TMP_Text>().rectTransform.anchoredPosition = position;
    }

    public void UpdateScore(int score)
    {
        GetComponent<TMP_Text>().text = $"score: {score.ToString().PadLeft(digit, '0')}";
    }



}

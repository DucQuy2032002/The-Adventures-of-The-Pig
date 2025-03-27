using System.Collections;
using UnityEngine;
using TMPro;
using System.ComponentModel;

public class CreditScroller : MonoBehaviour
{
    [SerializeField] private float speedScroll;
    private float texPosBegin = -3000f;
    private float texPosEnd = 3000f;

    private RectTransform creditTransform;
    [SerializeField] private TextMeshProUGUI creditText;
    [SerializeField] private bool isLooping;

    private void Start()
    {
        creditTransform = GetComponent<RectTransform>();
        StartCoroutine(ScrollText());
    }

    IEnumerator ScrollText()
    {
        while (creditTransform.localPosition.y < texPosEnd)
        {
            creditTransform.Translate(Vector3.up * speedScroll * Time.deltaTime);
            if (creditTransform.localPosition.y > texPosEnd)
            {
                if (isLooping)
                {
                    creditTransform.localPosition = Vector3.up * texPosBegin;
                }
                else
                {
                    break;
                }
            }
            
            yield return null;
        }
    }
}


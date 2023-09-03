using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BlinkingText : MonoBehaviour
{
    TMP_Text titleText;
    // Start is called before the first frame update
    void Start()
    {
        titleText = GetComponent<TMP_Text>();
        StartBlinking();
    }
    IEnumerator Blink()
    {
        while (true)
        {
            switch(titleText.color.a.ToString())
            {
                case "0":
                    titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 1);
                    yield return new WaitForSeconds(0.5f);
                    break;
                case "1":
                    titleText.color = new Color(titleText.color.r, titleText.color.g, titleText.color.b, 0);
                    yield return new WaitForSeconds(0.5f);
                    break;
            };
        }
    }
    void StartBlinking()
    {
        StopCoroutine("Blink");
        StartCoroutine("Blink");
    }
    void StopBlinking()
    {
        StopCoroutine("Blink");
    }
}

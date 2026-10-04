using UnityEngine;
using System.Collections;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    public GameObject countdownTextObject;
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        countdownTextObject.SetActive(true);
        StartCoroutine(BeginGameCountdown());
        StartCoroutine(CountdownText(3));
    }

    IEnumerator CountdownText(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            countdownTextObject.GetComponent<TextMeshProUGUI>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        countdownTextObject.GetComponent<TextMeshProUGUI>().text = "Go!";
        StartCoroutine(HideCountdown());
    }
    
    IEnumerator BeginGameCountdown()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
    }

    IEnumerator HideCountdown()
    {
        yield return new WaitForSeconds(1);
        countdownTextObject.SetActive(false);
    }
    
}

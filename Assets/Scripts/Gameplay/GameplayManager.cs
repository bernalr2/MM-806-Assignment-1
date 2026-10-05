using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine.Analytics;

public class GameplayManager : MonoBehaviour
{
    public int maxPoints = 0;
    
    [Header("Game Object References")]
    public GameObject spawnManagerReference;
    public GameObject pickUpPrefab;
    
    // UI object to display winning text and menu buttons.
    [Header("UI")]
    public GameObject countdownTextObject;
    public GameObject winTextObject;
    public GameObject playAgainButton;
    public GameObject mainMenuButton;
    
    // UI text component to display count of "PickUp" objects collected.
    public TextMeshProUGUI countText;
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        ToggleUI(false);
        
        countdownTextObject.SetActive(true);
        StartCoroutine(BeginGameCountdown());
        StartCoroutine(CountdownText(3));
    }

    private IEnumerator CountdownText(int seconds)
    {
        for (int i = seconds; i > 0; i--)
        {
            countdownTextObject.GetComponent<TextMeshProUGUI>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        countdownTextObject.GetComponent<TextMeshProUGUI>().text = "Go!";
        StartCoroutine(HideCountdown());
    }
    
    private static IEnumerator BeginGameCountdown()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
    }

    private IEnumerator HideCountdown()
    {
        yield return new WaitForSeconds(1);
        countdownTextObject.SetActive(false);
    }
    
    public void SignalSpawn()
    {
        var spawnManager = spawnManagerReference.GetComponent<SpawnManager>();
        if (!spawnManager)
        {
            return;
        }
        spawnManager.SpawnObject(pickUpPrefab);
    } 

    private void ToggleUI(bool bIsActive)
    {
        winTextObject.SetActive(bIsActive);
        playAgainButton.SetActive(bIsActive);
        mainMenuButton.SetActive(bIsActive);
    }

    public void SetCountText(int count)
    {
        // Update the count text with the current count.
        countText.text = "Count: " + count.ToString();

        // Check if the count has reached or exceeded the win condition.
        if (count >= maxPoints)
        {
            // Display the win text and menu buttons.
            ToggleUI(true);
            
            // Destroy the enemy GameObject.
            Destroy(GameObject.FindGameObjectWithTag("Enemy"));
        }
    }
    
    public void GameOver()
    {
        // Update the winText to display "You Lose!"
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        ToggleUI(true);
    }
}

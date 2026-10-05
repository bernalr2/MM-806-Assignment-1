using UnityEngine;
using System.Collections;
using TMPro;

public class GameplayManager : MonoBehaviour
{
    // The highest amount of points that can be collected in the game mode.
    // NOTE: Standard Mode is set at 10 points. Endless Mode is set at 1000 points so the player can keep going.
    public int maxPoints = 0;
    
    [Header("Game Object References")]
    /* NOTE: You may notice in that the spawnManagerReference variable is assigned in the EndlessGame scene but not
    the MiniGame (Standard) scene. This is to ensure that Standard Mode only has 10 PickUps. Endless Mode is the
    only mode that utilizes the spawning features. The Debug Logs will flag the missing reference in Standard Mode,
    but this can be safely ignored. */
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
        
        // Begin the countdown and change the text on screen.
        StartCoroutine(BeginGameCountdown());
        StartCoroutine(CountdownText(3));
    }

    // Start a countdown before play begins.
    private IEnumerator CountdownText(int seconds)
    {
        // For each second, display the number of seconds left on the screen.
        for (int i = seconds; i > 0; i--)
        {
            countdownTextObject.GetComponent<TextMeshProUGUI>().text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        
        // Countdown is over and the player is told to start.
        countdownTextObject.GetComponent<TextMeshProUGUI>().text = "Go!";
        StartCoroutine(HideCountdown());
    }
    
    // Pause the scene and for a set amount of time before starting the scene again.
    private static IEnumerator BeginGameCountdown()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
    }

    // Remove the countdown text from the screen once it has been finished.
    private IEnumerator HideCountdown()
    {
        yield return new WaitForSeconds(1);
        countdownTextObject.SetActive(false);
    }
    
    // Inform the spawnManager to spawn a PickUp object.
    public void SignalSpawn()
    {
        var spawnManager = spawnManagerReference.GetComponent<SpawnManager>();
        if (!spawnManager)
        {
            return;
        }
        spawnManager.SpawnPickUp(pickUpPrefab);
    } 

    // Show or hide the UI.
    private void ToggleUI(bool bIsActive)
    {
        winTextObject.SetActive(bIsActive);
        winTextObject.SetActive(bIsActive);
        playAgainButton.SetActive(bIsActive);
        mainMenuButton.SetActive(bIsActive);
    }
    
    // Change the total count displayed on the screen.
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
    
    // Change the text to show the game has ended.
    public void GameOver()
    {
        // Update the winText to display "You Lose!"
        winTextObject.GetComponent<TextMeshProUGUI>().text = "You Lose!";
        ToggleUI(true);
    }
}

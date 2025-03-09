using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Goal : MonoBehaviour
{
    public Text winText;
    public Text loseText;
    public Button playAgainButton;
    public Button quitButton;
    public Button nextLevelButton;
    public Slingshot slingshot;

    private int currentLevel;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        winText.enabled = false;
        loseText.enabled = false;

        // Hide the buttons initially
        playAgainButton.gameObject.SetActive(false);
        quitButton.gameObject.SetActive(false);
        nextLevelButton.gameObject.SetActive(false);

        // Add listeners for buttons
        playAgainButton.onClick.AddListener(PlayAgain);
        quitButton.onClick.AddListener(QuitGame);
        nextLevelButton.onClick.AddListener(NextLevel);

        // Set currentLevel based on the scene's index
        currentLevel = SceneManager.GetActiveScene().buildIndex;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Ball"))
        {
            // Goal Scored!
            winText.enabled = true;
            nextLevelButton.gameObject.SetActive(true);
            quitButton.gameObject.SetActive(true);
        }
    }

    void Update()
    {
        if (slingshot.shots >= 5 && !winText.enabled) // Check if shots reached 5 and winText is not shown
        {
            loseText.enabled = true; // Display "You Lose"
            playAgainButton.gameObject.SetActive(true); // Show Play Again and Quit when player loses
            quitButton.gameObject.SetActive(true);
        }

        this.transform.localRotation = Quaternion.Euler(0, 1, 0) * this.transform.localRotation;
    }

    // Play Again - reload the current scene
    void PlayAgain()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name); // Reload current scene
    }

    // Quit the game
    void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false; // If you're in the editor, stop the play mode
#endif
    }

    // Next Level - load the next level in the build settings
    void NextLevel()
    {
        int nextLevelIndex = currentLevel + 1;

        // Ensure the next level exists in the build settings
        if (nextLevelIndex < SceneManager.sceneCountInBuildSettings)
        {
            SceneManager.LoadScene(nextLevelIndex); // Load next scene in the build index
        }
        else
        {
            Debug.Log("No more levels to load!"); // Or you can go to a win screen
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class HUDController : MonoBehaviour, IEndGameObserver
{
	#region Field Declarations

	[Header("UI Components")]
    [Space]
	public Text scoreText;
    public StatusText statusText;
    public Button restartButton;

    [Header("Ship Counter")]
    [SerializeField]
    [Space]
    private Image[] shipImages;

    #endregion
    
    private GameSceneController gameSceneController;

    #region Startup

    private void Awake()
    {
        statusText.gameObject.SetActive(false);
       
        
    }

    /// <summary>
    /// Start is called on the frame when a script is enabled just before
    /// any of the Update methods is called the first time.
    /// </summary>
    void Start()
    {
        gameSceneController = FindObjectOfType<GameSceneController>();
        gameSceneController.AddObserver(this);
        gameSceneController.ScoreUpdateOnKill += GameSceneController_ScoreUpdateOnKill;
        gameSceneController.LifeLost += HideShip;
    }


    private void GameSceneController_ScoreUpdateOnKill(int pointValue)
    {
        UpdateScore(pointValue);
    }

    #endregion

    #region Public methods

    private void UpdateScore(int score)
    {
        scoreText.text = score.ToString("D5");
    }

    public void ShowStatus(string newStatus)
    {
        statusText.gameObject.SetActive(true);
        StartCoroutine(statusText.ChangeStatus(newStatus));
    }

    public void HideShip(int imageIndex)
    {
        shipImages[imageIndex].gameObject.SetActive(false);
    }

    public void ResetShips()
    {
        foreach (Image ship in shipImages)
            ship.gameObject.SetActive(true);
    }

    public void Notify()
    {
        ShowStatus("Gameover");
    }

    #endregion
}

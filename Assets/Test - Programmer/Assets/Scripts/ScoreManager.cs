using UnityEngine;
using UnityEngine.UI;
public class ScoreManager : MonoBehaviour
{
    public enum GameState { Playing, Death, Win, Tutorial }
    public GameState currentState;
    [SerializeField] private  GameObject _deathScreen;
    [SerializeField] private  GameObject _tutorialScreen;
    [SerializeField] private  GameObject _winScreen;    
    [SerializeField] private RectTransform healthBarFill; 
    [SerializeField] private float maxWidth = 200f; 
    
    private static int _score = 40, _maxScore = 150;
    public int score => _score;
    
    
        
    private void Start()
    {
            _deathScreen.SetActive(false);
            _winScreen.SetActive(false);
            _tutorialScreen.SetActive(true);
            ChangeState(GameState.Tutorial);
    }
    public void UpdateHealth(float percentage)
    {
        float width = maxWidth * (percentage);
        healthBarFill.sizeDelta = new Vector2(Mathf.Clamp(width, 0, maxWidth),healthBarFill.sizeDelta.y);
    }
        void Update()
    {
        UpdateHealth((float)score / _maxScore);
        
        if (_score < 1)
        {
            DeathScreen();
        }
        
        switch (currentState)
        {
            case GameState.Playing:
                {
                    _deathScreen.SetActive(false);
                    _winScreen.SetActive(false);
                    _tutorialScreen.SetActive(false);
                    Debug.Log("111");
                }
                break;
            case GameState.Death:
                {
                    _deathScreen.SetActive(true);
                }
                
                break;
            case GameState.Win:
                {
                    _winScreen.SetActive(true);
                }
                break;
            case GameState.Tutorial:
                {
                    _tutorialScreen.SetActive(true);
                }
                break;
        }
    }
    public void ChangeState(GameState newState)
    {
        currentState = newState;
    }
    public void AddScore(int count)
    {
        _score += count;
    }
    public void DeathScreen()
    {
        ChangeState(GameState.Death);        
    }
    public void StartTutorial()
    {
        ChangeState(GameState.Tutorial);
    }
    public void StopTutorial()
    {
        
        ChangeState(GameState.Playing);        
    }
    public void EndDeathScreen()
    {
        _deathScreen.SetActive(false);
        StartTutorial();
    }
    
    
}

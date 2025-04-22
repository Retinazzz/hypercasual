using UnityEngine;

public class Money : MonoBehaviour
{
    [SerializeField] private ScoreManager _score;    
    
    

    private void OnTriggerEnter(Collider other)
    {
        
        _score.AddScore(2);
        Destroy(gameObject);
    }
    
}

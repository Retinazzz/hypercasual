using UnityEngine;

public class Debuff : MonoBehaviour
{
    [SerializeField] private ScoreManager _score;
    
    
    

    private void OnTriggerEnter(Collider other)
    {
        _score.AddScore(-20);        
        Destroy(gameObject);
    }
    
}

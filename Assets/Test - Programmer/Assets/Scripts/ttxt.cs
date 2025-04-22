using UnityEngine;
using UnityEngine.UI;

public class ttxt : MonoBehaviour
{
    [SerializeField] private ScoreManager _score;
    private Text _Hpview;
    void Awake()
    {
        _Hpview = GetComponent<Text>();
    }


    void Update()
    {
        _Hpview.text = _score.score.ToString();
    }
}

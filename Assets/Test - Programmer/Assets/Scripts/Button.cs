using UnityEngine;
using UnityEngine.UI;

public class Button : MonoBehaviour
{
    void Start()
    {
        Button button = GetComponent<Button>();
        //button.OnCLi.AddListener(() => Debug.Log("Кнопка работает!"));
    }
}
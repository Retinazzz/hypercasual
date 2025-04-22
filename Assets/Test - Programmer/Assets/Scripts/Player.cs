using System.Collections.Generic;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{    
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip _money;
    [SerializeField] private float volume = 0.5f;
    [SerializeField] private AudioClip _debuff;
    [SerializeField] private GameObject[] Skins;
    [SerializeField] private Camera _camera;
    [SerializeField] private Transform _campos;
    [SerializeField] private int _moveSpeed;
    [SerializeField] private ScoreManager _score;

    private bool isTurning = false;
    private int _currentScore = 1;
    private float nextTurn;
    private Transform _playerpos;
    private float currentTurnDirection = 0f;
    private float lateralInput = 0f;
    private bool isMovingLeft = false;
    private bool isMovingRight = false;

    public float moveSpeed = 5f;
    public float lateralSpeed = 3f;    
    public float turnDuration = 1f;
    public float turnAngle = 90f;
    private void Start()
    {
        _playerpos = GetComponent<Transform>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
            audioSource.playOnAwake = false;
        }
    }
    
    private void Update()
    {
        
        CameraUpdate();
        _currentScore = _score.score;
        UpdateSkin(_currentScore);
        if (_score.currentState == ScoreManager.GameState.Playing) 
        {
            lateralInput = Input.GetAxis("Horizontal");
            LeftRightMove();
            MoveLateral();
            transform.Translate(Vector3.forward * moveSpeed * Time.deltaTime);
        } 

    }
    IEnumerator TurnCharacter(float angle)
    {
        isTurning = true;
        Quaternion startRotation = transform.rotation;
        Quaternion endRotation = startRotation * Quaternion.Euler(0, angle, 0);
        float elapsedTime = 0f;

        while (elapsedTime < turnDuration)
        {           
            transform.rotation = Quaternion.Slerp(startRotation,endRotation, elapsedTime / turnDuration);
            elapsedTime += Time.deltaTime;
            yield return null; 
        }

        transform.rotation = endRotation;
        isTurning = false;
    }
    void LeftRightMove()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                if (touch.position.x < Screen.width / 2)
                    isMovingLeft = true;
                else
                    isMovingRight = true;
            }
            else if (touch.phase == TouchPhase.Ended)
            {
                isMovingLeft = false;
                isMovingRight = false;
            }
        }

        // Движение
        if (isMovingLeft)
            transform.Translate(Vector3.left * moveSpeed * Time.deltaTime);
        else if (isMovingRight)
            transform.Translate(Vector3.right * moveSpeed * Time.deltaTime);
    }
    public void UpdateSkin(int Score)
    {
        _currentScore = Score;
        int skin = 0;
        if (_currentScore >= 40)
        {
            if (_currentScore >= 65)
            {
                if (_currentScore >= 100)
                {
                    skin = 3;
                }
                else skin = 2;
            }
            else skin = 1;
        }

        foreach (var otherSkin in Skins)
            otherSkin.SetActive(false);

        Skins[skin].SetActive(true);
    }
    void CameraUpdate()
    {
        _camera.transform.position = _campos.transform.position;
        _camera.transform.rotation = _campos.transform.rotation;
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("LeftTurn") && !isTurning)
        {
            currentTurnDirection = -1f;
            StartCoroutine(TurnCharacter(-turnAngle));
        }
        else if (other.CompareTag("RightTurn") && !isTurning)
        {
            currentTurnDirection = 1f;
            StartCoroutine(TurnCharacter(turnAngle));
        }
        if (other.CompareTag("Money"))
        {
            PlaySound(_money);
        }
        else if (other.CompareTag("Debuff"))
        {
            PlaySound(_debuff);
        }
        if (other.CompareTag("Finish"))
        {
            _score.ChangeState(ScoreManager.GameState.Win);
        }
        
    }

    void PlaySound(AudioClip clip)
    {
        if (clip != null)
        {
            audioSource.PlayOneShot(clip, volume);
        }
        else
        {
            Debug.LogWarning("?");
        }
    }
    void MoveLateral()
    {
        Vector3 lateralMovement = transform.right * lateralInput * lateralSpeed * Time.deltaTime;
        transform.Translate(lateralMovement, Space.World);
    }
}



using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
 //TODO Major rewrite

    public PlayerScript player;
    [SerializeField] private GameObject Enemies;
    [SerializeField] private GameObject MusicBox;
    [SerializeField] private GameObject leftDoor;
    [SerializeField] private GameObject rightDoor;
    [SerializeField] private GameObject leftLight;
    [SerializeField] private GameObject rightLight;
    [SerializeField] private GameObject secCameras;
    [SerializeField] private Camera playerCamera;
    [SerializeField] private GameObject lights;
    [SerializeField] private AudioSource camToggleSound, leftDoorAudio, rightDoorAudio;
    [SerializeField] private GameObject _toggleCameraButton;
    [SerializeField] private GameObject cameraUi;
    [SerializeField] private GameObject doorLightUi;
    [SerializeField] private GameObject startNightScreen;
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private GameObject victoryScreen;
    [SerializeField] private AudioSource scareAudio;
    [SerializeField] private AudioSource clockAudio;
    [SerializeField] private Text clockUi;
    [SerializeField] private Text nightUi;
    [SerializeField] private Text powerUi;
    [SerializeField] private Image powerIndicator;

    public bool hasGameStarted;
    public bool isPlayerAlive;
    private bool _isLightOn;
    private bool _hasPlayerWon;
    public float _powerReserve;
    private float _powerUsage;
    private float _powerInterval;
    private float _powerTimer;
    private float _lightTimer;
    private bool _isPowerDown;

    public static float hourLength = 89;
    private static int nightHours = 5;
    public bool _isLeftDoorClosed;
    public bool _isRightDoorClosed;
    public bool _isEnemyAtLeftDoor, _isEnemyAtRightDoor;
    private float _timer;
    public int _hour;
    private int _night;
    
    // Start is called before the first frame update
    void Start()
    {
        hasGameStarted = false;
        Enemies.SetActive(false);
        isPlayerAlive = true;
        _hasPlayerWon = false;
        _isPowerDown = false;
        _isLeftDoorClosed = false;
        _isRightDoorClosed = false;
        MusicBox.SetActive(false);

        _hour = 0;
        _night = Singleton.Instance.selectedNight;
        print("Night " + _night);
        _powerReserve = 100;
        Time.timeScale = 1.0f;
        _timer = Time.time;
        _powerTimer = Time.time;
        DisplayNight();
        startNightScreen.SetActive(true);       
    }

    // Update is called once per frame
    void Update()
    {
        if (hasGameStarted)
        {
            UpdateGameRules();
            if (isPlayerAlive)
            {
                HandleInput();
                UpdateDoor();
                UpdateClock();
                UpdateCameras();
                if (_isLightOn)
                {
                    if (_lightTimer + 1  < Time.time)
                    {
                        _isLightOn = false;
                        leftLight.SetActive(false);
                        rightLight.SetActive(false);
                    }
                }
                if (!_isPowerDown)
                {
                    UpdatePower();
                }
                else
                {
                    MusicBox.SetActive(true);
                    lights.SetActive(false);
                } 
            }
        }
        else
        {
            StartNight();
        }
    }
    
    void HandleInput()
    {
        if (Input.GetKeyDown(KeyCode.A) && !player.isInCamera && _powerReserve > 0)
        {
            CloseLeftDoor();
        }

        if (Input.GetKeyDown(KeyCode.D) && !player.isInCamera && _powerReserve > 0)
        {
            CloseRightDoor();
        }

        if (Input.GetKeyDown(KeyCode.Q) && !player.isInCamera && _powerReserve > 0)
        {
            TurnOnLeftLight();
        }

        if (Input.GetKeyDown(KeyCode.E) && !player.isInCamera && _powerReserve > 0)
        {
            TurnOnRightLight();
        }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            ToggleSecCameras();
        }
        else if (_powerReserve <= 0)
        {
            player.isInCamera = false;
        }
    }

    void UpdateGameRules()
    {
        if (_hasPlayerWon)
        {
            //Time.timeScale = 0.0f; <- disables animations :/
            Enemies.SetActive(false);
            MusicBox.SetActive(false);
            if (_night > Singleton.Instance.completedNight)
            {
                Singleton.Instance.completedNight = Mathf.Clamp(_night, 1, 7);
                PlayerPrefs.SetInt("completedNight", Singleton.Instance.completedNight);
            }
            Singleton.Instance.canRetryNight = false;
            victoryScreen.SetActive(true);
        }
        if (!isPlayerAlive)
        {
            Enemies.SetActive(false);
            gameOverScreen.SetActive(true);
            Singleton.Instance.canRetryNight = true;
            Time.timeScale = 0.0f;
        }
    }
    
    void UpdateClock() //Updates the UI clock and ends the game if it's 6AM
    {
        if (_hour == 0)
        {
            clockUi.text = 12 + " AM";
        }
        else
        {
            clockUi.text = _hour + " AM";
        }
        
        if (_timer + hourLength <= Time.time)
        {
            if (_hour < nightHours)
            {
                _timer = Time.time;
                _hour += 1;
                clockAudio.Play();
            }
            else //Display victory screen!
            {
                _hasPlayerWon = true;
            }
        }
    }

    void UpdatePower()
    {
        if (_powerReserve > 0)
        {
            CalculatePower();
            powerIndicator.fillAmount = (0.2f * _powerUsage);
            if (_powerUsage == 1)
            {
                _powerInterval = 9.6f;
            }
            else if (_powerUsage == 2)
            {
                _powerInterval = 4.8f;
            }
            else if (_powerUsage == 3)
            {
                _powerInterval = 3.2f;
            }
            else if (_powerUsage == 4)
            {
                _powerInterval = 2.4f;
            }

            if (_powerTimer + _powerInterval < Time.time)
            {
                _powerTimer = Time.time;
                _powerReserve--;
                powerUi.text = _powerReserve + "%";
            }
        }
        else
        {
            _powerUsage = 0;
            powerIndicator.fillAmount = 0;
            _isLeftDoorClosed = false;
            _isRightDoorClosed = false;
            leftDoorAudio.Play();
            rightDoorAudio.Play();
            Enemies.SetActive(false);
            _isPowerDown = true;
        }
    }
    
    void CalculatePower()
    {
        int doorPower = 0;
        if (_isLeftDoorClosed && _isRightDoorClosed)
        {
            doorPower = 2;
        }
        else if (_isLeftDoorClosed || _isRightDoorClosed)
        {
            doorPower = 1;
        }

        int cameraPower = 0;
        if (player.isInCamera)
        {
            cameraPower = 1;
        }
        else
        {
            cameraPower = 0;
        }

        int lightPower = 0;
        if (_isLightOn)
        {
            lightPower = 1;
        }
        
        _powerUsage = doorPower + cameraPower + lightPower + 1;
    }

    void UpdateDoor()
    {
        if (_isLeftDoorClosed)
        {
            //close door
            leftDoor.SetActive(true);
            
        }
        else
        {
            //open 
            leftDoor.SetActive(false);
        }

        if (_isRightDoorClosed)
        {
            //close door
           rightDoor.SetActive(true);
        }
        else
        {
            //open door
            rightDoor.SetActive(false);
        }
    }

    void UpdateCameras()
    {
        if (_powerReserve <= 0)
        {
            _toggleCameraButton.SetActive(false);
            cameraUi.SetActive(false);
            secCameras.SetActive(false);
        }
        
        if (_powerReserve > 0 && player.isInCamera)
        {
            cameraUi.SetActive(true);
            secCameras.SetActive(true);
        }
        else
        {
            cameraUi.SetActive(false);
            secCameras.SetActive(false);
        }
    }

    public void ToggleSecCameras()
    {
        player.isInCamera = !player.isInCamera;
        doorLightUi.SetActive(!doorLightUi.activeSelf);
        camToggleSound.Play();
    }

    public void CloseLeftDoor()
    {
        _isLeftDoorClosed = !_isLeftDoorClosed;
        leftDoorAudio.Play();
    }

    public void CloseRightDoor()
    {
        _isRightDoorClosed = !_isRightDoorClosed;
        rightDoorAudio.Play();
    }

    public void TurnOnLeftLight()
    {
        _isLightOn = true;
        leftLight.SetActive(true);
        _lightTimer = Time.time;

        if (_isEnemyAtLeftDoor && !_isLeftDoorClosed)
        {
            scareAudio.Play();
        }
    }

    public void TurnOnRightLight()
    {
        _isLightOn = true;
        rightLight.SetActive(true);
        _lightTimer = Time.time;

        if (_isEnemyAtRightDoor && !_isRightDoorClosed)
        {
            scareAudio.Play();
        }
    }

    void DisplayNight()
    {
        nightUi.text = "Night " + _night;
    }

    void StartNight()
    {
        if (_timer + 4 < Time.time)
        {
            startNightScreen.SetActive(false);
            _timer = Time.time;
            _powerTimer = Time.time;
            hasGameStarted = true;
            Enemies.SetActive(true);
        }
    }
}

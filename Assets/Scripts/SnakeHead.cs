using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
//using UnityEngine.Advertisements;
using TMPro;

public class SnakeHead : MonoBehaviour
{
    enum Direction
    {
        right,
        left, 
        up,
        down  
    }
    Direction direction;   

    public List<Transform> tail = new List<Transform>();

    public float frameRate = 0.2f;
    public float step = 1f;
    public float time = 0.2f;

    

    public GameObject tailPrefab;

    public static Vector3 horizontalRange = new Vector3 (-10f, 10f, 0f);
    public static Vector3 verticalRange = new Vector3(-15f, 17f, 0f);

    private int noBack = 1; // 0=up, 1=right, 2=down, 3=left

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI overScoreText;
    public TextMeshProUGUI maxResultText;
    private int score = 0;
    private int maxResult = 0;

    public AudioSource appleCrunch;
    public AudioClip clip;

    private int i;

    //private string gameId = "3809647";
    //private string placementId = "video";
    //private bool testMode = true;

    public GameObject GameOverMenuUI;
    public GameObject pauseButton;

    void Start()
    {
        //Advertisement.Initialize(gameId, testMode);

        InvokeRepeating("Move", time, frameRate);

        if (PlayerPrefs.HasKey("maxResult")) maxResult = PlayerPrefs.GetInt("maxResult");          
        else maxResult = 0;       
        
        maxResultText.text = "Max result: " + maxResult.ToString();        
    }

    void Move()
    {
        lastPos = transform.position;
        lastRotateZ = transform.rotation.eulerAngles.z;
       

        Vector3 nextPos = Vector3.zero;
        float nextRotateZ = 0.0f;
        

        if (direction == Direction.up) { nextPos = Vector3.up; nextRotateZ = 90.0f; }
        else if (direction == Direction.down) { nextPos = Vector3.down; nextRotateZ = -90.0f; }
        else if (direction == Direction.left) { nextPos = Vector3.left; nextRotateZ = -180.0f; }
        else if (direction == Direction.right) { nextPos = Vector3.right; nextRotateZ = 0.0f; }

        nextPos *= step;
        transform.position += nextPos;
        transform.rotation = Quaternion.Euler(0, 0, nextRotateZ);        

        MoveTail();
    }

    private Vector3 lastPos;
    private float lastRotateZ;
    void MoveTail()
    {
        for (int i = 0; i < tail.Count; i++)
        {
            Vector3 temp = tail[i].position;
            float tempRotateZ = tail[i].rotation.eulerAngles.z;

            tail[i].position = lastPos;
            tail[i].rotation = Quaternion.Euler(0, 0, lastRotateZ);

            lastPos = temp;
            lastRotateZ = tempRotateZ;            
        }
    }
        
    void Update()
    {
        

        //Touch
        if (Input.touchCount > 0)
        {
            Vector2 delta = Input.GetTouch(0).deltaPosition;

            if (Mathf.Abs(delta.x) > Mathf.Abs(delta.y))
            {
                if (delta.x > 0 && noBack != 3 && noBack != 1)
                {
                    direction = Direction.right;
                    noBack = 1;                    
                }
                else if (delta.x < 0 && noBack != 1 && noBack != 3)
                {
                    direction = Direction.left;
                    noBack = 3;                    
                }
            }
            else 
            {
                if (delta.y > 0 && noBack != 2 && noBack != 0)
                {
                    direction = Direction.up;
                    noBack = 0;                    
                }
                else if (delta.y < 0 && noBack != 0 && noBack != 2)
                {
                    direction = Direction.down;
                    noBack = 2;                   
                }
            }            
        }

        //Keyboard
        if (Input.GetKeyDown(KeyCode.UpArrow) && noBack != 2 && noBack != 0) { direction = Direction.up; noBack = 0; }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && noBack != 0 && noBack != 2) { direction = Direction.down; noBack = 2; }
        else if (Input.GetKeyDown(KeyCode.LeftArrow) && noBack != 1 && noBack != 3) { direction = Direction.left; noBack = 3; } 
        else if (Input.GetKeyDown(KeyCode.RightArrow) && noBack != 3 && noBack != 1) { direction = Direction.right; noBack = 1; }
    }

    void OnTriggerEnter2D (Collider2D col)
    {
        if (col.CompareTag("Block"))
        {            
            i = PlayerPrefs.GetInt("Vibro");
            if (i == 1) Handheld.Vibrate();

            overScoreText.text = "Your Score: " + score.ToString();

            GameOverPause();
        }
        else if (col.CompareTag("Food"))
        {
            score++;
            scoreText.text = " Score: " + score.ToString();
            
            if (score > maxResult)
            {
                //Advertisement
                maxResult = score;
                maxResultText.text = "Max result: " + maxResult.ToString();
                PlayerPrefs.SetInt("maxResult", maxResult);
            }

            tail.Add(Instantiate(tailPrefab, tail[tail.Count - 1].position, Quaternion.identity).transform);
            appleCrunch.PlayOneShot(clip); // можно внести изменения

            i = PlayerPrefs.GetInt("Vibro");
            if (i == 1) Handheld.Vibrate();         

            col.transform.position = new Vector2(Mathf.Floor(Random.Range(horizontalRange.x, horizontalRange.y)), Mathf.Floor(Random.Range(verticalRange.x, verticalRange.y)));
        }
    }

    public void GameOverPause()
    {
        GameOverMenuUI.SetActive(true);
        Time.timeScale = 0f;
        PauseMenu.GameIsPause = true;
        pauseButton.SetActive(false);

        //if (Advertisement.IsReady(placementId)) Advertisement.Show(placementId);
    }

    public void Restart()
    {
        GameOverMenuUI.SetActive(false);
        Time.timeScale = 1f;
        PauseMenu.GameIsPause = false;
        UnityEngine.SceneManagement.SceneManager.LoadScene(1);
        pauseButton.SetActive(true);
    }
}

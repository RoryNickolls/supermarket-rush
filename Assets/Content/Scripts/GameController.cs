using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;

    private GameTimer gameTimer;
    private bool started = false;

    private int currentLevel = 0;

    [SerializeField]
    private int shoppingListItems = 3;

    [SerializeField]
    private AudioClip winClip;

    [SerializeField]
    private AudioClip loseClip;

    private Player player;

    private ShoppingList shoppingList;

    [SerializeField]
    private UIController gameCanvasPrefab;

    private UIController uiController;

    private void Awake()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += StartLevel;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= StartLevel;
    }

    public void StartLevel(Scene scene, LoadSceneMode mode)
    {
        if(scene.name == "Menu")
        {
            return;
        }

        uiController = Instantiate(gameCanvasPrefab);

        gameTimer = FindObjectOfType<GameTimer>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        foreach (Shelf shelf in FindObjectsOfType<Shelf>())
        {
            shelf.SpawnItems();
        }

        shoppingList = FindObjectOfType<ShoppingList>();
        shoppingList.Create(currentLevel + 1);
    }

    public void StartGame()
    {
        gameTimer.enabled = true;
        started = true;
    }

    public void LoseGame()
    {
        uiController.ShowLose();
        Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.5f);
        started = false;

        AudioManager.PlayOnce(loseClip);
    }

    public void WinGame()
    {
        uiController.ShowWin();
        Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.5f);
        started = false;

        gameTimer.Pause();

        AudioManager.PlayOnce(winClip);

        NextLevel(4f);
    }

    private IEnumerator ChangeLevel(int level, float delay)
    {
        yield return new WaitForSeconds(delay);
        SceneManager.LoadScene("Level"+level);
        currentLevel = level;
    }

    public void CrossFinishLine()
    {
        if (!started)
            return;

        Dictionary<ItemData, int> playerItems = player.Trolley.Items;
        Dictionary<ItemData, int> target = shoppingList.Items;

        bool win = true;
        foreach(ItemData item in target.Keys)
        {
            if(!playerItems.ContainsKey(item) || playerItems[item] < target[item])
            {
                win = false;
                break;
            }
        }

        if(win)
        {
            WinGame();
        }
        else
        {
            LoseGame();
        }
    }

    public void NextLevel(float delay)
    {
        StartCoroutine(ChangeLevel(currentLevel+1, delay));
    }

    public bool HasStarted
    {
        get { return started; }
    }
}

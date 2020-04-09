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

    private bool together = false;

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

    private int crossedFinishLine = 0;

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

        if(together)
        {
            GameObject together = GameObject.Find("Together");
            if (together != null)
            {
                foreach (Transform transform in together.GetComponentsInChildren<Transform>(true))
                {
                    transform.gameObject.SetActive(true);
                }
            }
        }

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
        crossedFinishLine = 0;
        gameTimer.enabled = true;
        started = true;
    }

    public void LoseGame(string message)
    {
        uiController.ShowLose(message);
        Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.5f);
        started = false;

        AudioManager.PlayOnce(loseClip);
        RestartLevel();
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

        crossedFinishLine++;

        if(!together || crossedFinishLine == 2)
        {
            if (EvaluateWin())
            {
                WinGame();
            }
            else
            {
                LoseGame("WRONG ITEMS!");
            }
        }
    }

    private bool EvaluateWin()
    {
        if(together && crossedFinishLine == 2)
        {
            Player[] players = FindObjectsOfType<Player>();

            // Evaluate multiplayer win, same as singleplayer win but items can be split across players
            Dictionary<ItemData, int> target = shoppingList.Items;

            Dictionary<ItemData, int> totalItems = new Dictionary<ItemData, int>();
            foreach (Player player in players)
            {
                foreach (ItemData key in player.Trolley.Items.Keys)
                {
                    if (totalItems.ContainsKey(key))
                    {
                        totalItems[key] += player.Trolley.Items[key];
                    }
                    else
                    {
                        totalItems.Add(key, 1);
                    }
                }
            }

            // Player must have exactly the items on the list to win
            foreach (ItemData item in target.Keys)
            {
                if (!totalItems.ContainsKey(item) || totalItems[item] != target[item])
                {
                    return false;
                }
            }

            return true;
        }
        else if(!together && crossedFinishLine == 1)
        {
            // Evaluate singleplayer win
            Dictionary<ItemData, int> playerItems = player.Trolley.Items;
            Dictionary<ItemData, int> target = shoppingList.Items;

            // Player must have exactly the items on the list to win
            foreach (ItemData item in target.Keys)
            {
                if (!playerItems.ContainsKey(item) || playerItems[item] != target[item])
                {
                    return false;
                }
            }

            return true;
        }

        return false;
    }



    public void NextLevel(float delay)
    {
        StartCoroutine(ChangeLevel(currentLevel+1, delay));
    }

    public void RestartLevel()
    {
        StartCoroutine(ChangeLevel(currentLevel, 3f));
    }

    public bool HasStarted
    {
        get { return started; }
    }

    public bool Together
    {
        get { return together; }
    }

    public void EnablePlayTogether()
    {
        together = true;
    }
}

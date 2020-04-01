using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    private GameTimer gameTimer;
    private bool started = false;

    [SerializeField]
    private string nextScene;

    [SerializeField]
    private int shoppingListItems = 3;

    [SerializeField]
    private GameObject lose;

    [SerializeField]
    private GameObject win;

    [SerializeField]
    private AudioClip winClip;

    [SerializeField]
    private AudioClip loseClip;

    private Player player;

    private ShoppingList shoppingList;

    private void Start()
    {
        gameTimer = FindObjectOfType<GameTimer>();

        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        foreach(Shelf shelf in FindObjectsOfType<Shelf>())
        {
            shelf.SpawnItems();
        }

        shoppingList = FindObjectOfType<ShoppingList>();
        shoppingList.Create(shoppingListItems);
    }

    public void StartGame()
    {
        gameTimer.enabled = true;
        started = true;
    }

    public void LoseGame()
    {
        lose.SetActive(true);
        Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.5f);
        started = false;

        AudioManager.PlayOnce(loseClip);
    }

    public void WinGame()
    {
        win.SetActive(true);
        Camera.main.GetComponent<CameraShake>().Shake(1.0f, 0.5f);
        started = false;

        gameTimer.Pause();

        AudioManager.PlayOnce(winClip);

        StartCoroutine(ChangeLevel(nextScene));
    }

    private IEnumerator ChangeLevel(string level)
    {
        if(level != "")
        {
            yield return new WaitForSeconds(4f);
            SceneManager.LoadScene(nextScene);
        }
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

    public bool HasStarted
    {
        get { return started; }
    }
}

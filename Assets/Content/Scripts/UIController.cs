using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject losePrefab;

    [SerializeField]
    private GameObject winPrefab;

    public void ShowLose(string message)
    {
        GameObject lose = Instantiate(losePrefab, transform);
        lose.GetComponent<Text>().text = message;
    }

    public void ShowWin()
    {
        Instantiate(winPrefab, transform);
    }
}

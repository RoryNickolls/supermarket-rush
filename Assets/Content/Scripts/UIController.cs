using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private GameObject losePrefab;

    [SerializeField]
    private GameObject winPrefab;

    public void ShowLose()
    {
        Instantiate(losePrefab, transform);
    }

    public void ShowWin()
    {
        Instantiate(winPrefab, transform);
    }
}

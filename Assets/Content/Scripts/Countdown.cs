using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Countdown : MonoBehaviour
{

    [SerializeField]
    private int countdownSeconds;

    [SerializeField]
    private AudioClip shortBlip;

    [SerializeField]
    private AudioClip longBlip;

    private int remaining;
    private float countdownTimer = 0.0f;

    private Text countdownText;

    // Start is called before the first frame update
    void Start()
    {
        countdownText = GetComponentInChildren<Text>();
        remaining = countdownSeconds;
        UpdateText();
    }

    // Update is called once per frame
    void Update()
    {
        if(remaining > 0)
        {
            countdownTimer += Time.deltaTime;
            if(countdownTimer >= 1.0f)
            {
                remaining--;
                UpdateText();
                countdownTimer = 0.0f;
            }
        }
    }

    private void UpdateText()
    {
        if(remaining == 0)
        {
            countdownText.text = "GO!";
            GameController.Instance.StartGame();
            Destroy(gameObject, 1.0f);
        }
        else
        {
            countdownText.text = remaining.ToString();
        }

        StartCoroutine(FlyIn(0.25f));
    }

    private IEnumerator FlyIn(float time)
    {
        float targetScale = 1.0f;
        float startScale = 3.0f;

        float elapsed = 0.0f;
        while(elapsed < time)
        {
            float scale = Mathf.Lerp(startScale, targetScale, elapsed / time);
            countdownText.transform.localScale = Vector3.one * scale;
            elapsed += Time.deltaTime;
            yield return null;
        }
        Camera.main.GetComponent<CameraShake>().Shake();

        if(remaining == 0)
        {
            GetComponent<AudioSource>().PlayOneShot(longBlip);
        }
        else
        {
            GetComponent<AudioSource>().PlayOneShot(shortBlip);
        }
    }
}

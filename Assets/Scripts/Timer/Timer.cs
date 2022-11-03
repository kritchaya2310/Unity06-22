using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private Image timerBar;
    [SerializeField] private float maxTime;
    [SerializeField] private float timeLeft;
    [SerializeField] PlayerController playerController;

    private void Start()
    {
        timeLeft = maxTime;
    }

    private void Update()
    {
        TimerBar();
    }

    public void TimerBar()
    {
        if (timeLeft > 0)
        {
            timeLeft -= Time.deltaTime;
            timerBar.fillAmount = timeLeft / maxTime;
        }
        else
        {
            playerController.PlayerDead();
            //Time.timeScale = 0;
        }
    }
}

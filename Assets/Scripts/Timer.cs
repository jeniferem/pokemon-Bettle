using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Events;

public class Timer : MonoBehaviour
{
    [SerializeField]
    private SecondData[] secondsData;
    [SerializeField]
    private  Image timerImage;
    [SerializeField]
    private string timerAnimationName;
    [SerializeField]
    private UnityEvent onTimerEnd;
    private Animator  timerAnimator;
    private Coroutine timerCourotine;
    private void Awake()
    {
        timerAnimator = timerImage.GetComponent<Animator>();
    }
    public void StartTimer(int duration)
    {
        if (timerCourotine != null)
        {
            StopCoroutine(timerCourotine);
        }
        timerCourotine = StartCoroutine(TimerCourotine(duration));
    }
    private IEnumerator TimerCourotine(int duration)
    {
        for (int i = 0; i < duration +1; i++)
        {
            SoundManager.instance.Play(secondsData[i].soundName);
            timerImage.sprite = secondsData[i].image;
            timerImage.SetNativeSize();
            timerAnimator.Play(timerAnimationName, 0, 0f);
            yield return new WaitForSeconds(1f);
        }
        onTimerEnd?.Invoke();
    }
    public void StopTimer()

    {
        if (timerCourotine != null)
        {
            StopCoroutine(timerCourotine);
            timerCourotine = null;
        }
    }
}
[System.Serializable]
public class SecondData
{
    public string soundName;
    public Sprite image;
}

using UnityEngine;
using UnityEngine.UI;
using  System.Collections;
public class DialogSystem : MonoBehaviour
{
   public static DialogSystem Instance {get; private set; }
   [SerializeField]
   private Text dialogText;
   [SerializeField]
   private float timeBetweenWords = 0.25f;
   [SerializeField]
   private Animator animator;
   [SerializeField]
   private string showAnimationName = "ShowDialog";
   [SerializeField]
   private string hideAnimationName= "HideDialog";
   private Coroutine showDialogCouroutine;
   private void Awake()
    {
        Instance = this;
    }
    public void  ShowDialog(string dialog)
    {
        if(showDialogCouroutine != null)
        {
            StopCoroutine(showDialogCouroutine);
        }
        showDialogCouroutine = StartCoroutine(ShowDialogCoroutine(dialog));
    }
    private IEnumerator ShowDialogCoroutine(string dialog)
    {
        dialogText.text = "";
        animator.Play(showAnimationName,0, 0f);
        yield return new  WaitForSeconds(animator.GetCurrentAnimatorStateInfo(0).length);
        dialogText.text = "";
        foreach (char latter in dialog.ToCharArray())
        {
            dialogText.text += latter;
            yield return new  WaitForSeconds(timeBetweenWords);
        }
        yield return new WaitForSeconds(1f);
        animator.Play(hideAnimationName, 0,0f);
    }
    public void StopDialog()
    {
        if(showDialogCouroutine != null)
        {
            StopCoroutine(showDialogCouroutine);
            showDialogCouroutine = null;
        }
        dialogText.text = "";
        animator.Play(hideAnimationName, 0,0f);
    }
}

using UnityEngine;

public class DamageTextManager : MonoBehaviour
{
    [SerializeField]
    private InstantiatePoolObjects damageTextpool;
    [SerializeField]
    private Transform canvasTransform;
    public void ShowDamage (DamageTarget damageTarget)
    {
        damageTextpool.InstantiateObject(Vector3.zero);
        DamageText damageText = damageTextpool.GetCurrentObject().GetComponent<DamageText>();
        damageText.transform.SetParent(canvasTransform, false);
        damageText.ShowDamage(damageTarget);
    }
}

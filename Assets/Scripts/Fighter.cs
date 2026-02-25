using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Events;
using Unity.VisualScripting;
public class Fighter : MonoBehaviour
{
    [SerializeField]
    private CharacterData characterData;
    [SerializeField]
    private UnityEvent onInitialize;
    private Animator animator;
    public Animator Animator => animator;
    private Health health;
    public Health Health => health;
    private List <Attack> attacks;
    private Attack[] Attacks => attacks.ToArray();
    private void Awake()
    {
        health = GetComponent<Health>();
        animator = GetComponent<Animator>();
        attacks = new List<Attack>();
        foreach (AttackData attackData in characterData.attacks)
        {
            Attack attack = new Attack();
            attack.attackData = attackData;
            GameObject instantiateObject= new GameObject(attackData.attackName + "pool");
            InstantiatePoolObjects pool = instantiateObject.AddComponent<InstantiatePoolObjects>();
            pool.Setprefab(attackData.attackParticles); 
            attack.particlesPool = pool;                                      
            attacks.Add(attack);
            attack.particlesPool.transform.SetParent(transform);

            GameObject hitInstantiateObject = new GameObject(attackData.attackName + "Hit Pool");
            InstantiatePoolObjects hitPool = hitInstantiateObject.AddComponent<InstantiatePoolObjects>();
            hitPool.Setprefab(attackData.attackHitParticles);
            attack.hitparticlesPool = hitPool;
            hitPool.transform.SetParent(transform);
            attacks.Add(attack);
        }
    }
    public Attack GetRamdomAttack()
    {
        int index = Random.Range(0, attacks.Count);
        return attacks[index];
    }
    public void Initialize()
    {
        onInitialize.Invoke();
    }
}             
[System.Serializable]
public class Attack
{
    public AttackData attackData;
    public InstantiatePoolObjects particlesPool;
    public InstantiatePoolObjects hitparticlesPool;
}

using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem.EnhancedTouch;

public class BattleManager : MonoBehaviour
{
    [SerializeField]

    private int minimumFighters = 2;
    [SerializeField]
    private int maximumFighters = 4;
    [SerializeField]
    private UnityEvent onStartBattleCount;
    [SerializeField]
    private UnityEvent onStartBattle;
    [SerializeField]
    private UnityEvent onStopBattle;
    [SerializeField]
    private UnityEvent<string> onWinBattle;                        
    private Coroutine battleCoroutine;
    private List<Fighter>fighters = new List<Fighter>();
    public void AddFighter (Fighter fighter)
    {
        if (fighters.Count < maximumFighters)
        {
            fighters.Add(fighter);
            fighter.Initialize();
            if (fighters.Count >= minimumFighters)
            {
                onStartBattleCount?.Invoke();
            }
        }
    }
    public void RemoveFigthter(Fighter fighter)
    {
        if (fighters.Contains(fighter))
        {
            fighters.Remove(fighter);
            if (fighters.Count < minimumFighters)
            {
                onStopBattle?.Invoke();
            }
        }
    }
    public void StartBattle()
    {
        onStartBattle?.Invoke();
        battleCoroutine = StartCoroutine(BattleCoroutine());
    }
    private IEnumerator BattleCoroutine()
    {
        while (fighters.Count > 1)
        {
            Fighter attacker = fighters[Random.Range(0, fighters.Count)];
            Fighter defender = fighters[Random.Range(0, fighters.Count)];
            while (defender == attacker)
            {
                defender = fighters[Random.Range(0, fighters.Count)];
            }
            attacker.transform.LookAt(defender.transform);
            defender.transform.LookAt(attacker.transform);
            Attack attack = attacker.GetRamdomAttack();
            attacker.Animator.Play(attack.attackData.animationName);
            attack.particlesPool.InstantiateObject(attacker.transform.position);
            float damage = Random.Range(attack.attackData.minDamage,attack.attackData.maxDamage);
            yield return new WaitForSeconds(attack.attackData.attackDuration);
            attack.hitparticlesPool.InstantiateObject(defender.transform.position);
            defender.Health.TakeDamage(damage);
            if (defender.Health.CurrentHealth <= 0)
            {
                RemoveFigthter(defender);
            }
            else
            {
                yield return new WaitForSeconds(1.5f);
            }
        }
        if (fighters.Count == 1)
        {
           WinBattle(fighters[0]);
        }
    }
    public void WinBattle(Fighter winner)
    {
       onWinBattle?.Invoke(winner.CharacterData.characterName);
       winner.Animator.Play(winner.CharacterData.WinAnimationName);
       winner.transform.LookAt(Camera.main.transform);
    }
    public void StopBattle()
    {
        if (battleCoroutine != null)
        {
            StopCoroutine(battleCoroutine);
            battleCoroutine = null;
        }
    }

}

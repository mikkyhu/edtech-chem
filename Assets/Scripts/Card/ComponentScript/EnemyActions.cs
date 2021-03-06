﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class EnemyActions {

    public List<Action> actions;
    private System.Random random;
    private Action nextAction;
    private MonoBehaviour mono;
    private int turn = 0;

    // @TODO: maybe consider using the automatic constructor? how would that work?
    public EnemyActions(EnemyActions enemyActions) {
        actions = enemyActions.actions;
        random = new System.Random();
        nextAction = GetAction();
    }

    public void PerformAction()
    {
        switch (nextAction.action)
        {
            case ComponentType.Armor:
                {
                    EnemyStats.enemyStatsInstance.Armor(nextAction.modifier);
                    break;
                }
            case ComponentType.Attack:
                {
                    EnemyStats.enemyStatsInstance.StartCoroutine(EnemyAttackAnim());
                    PlayerStats.playerStatsInstance.Damage(nextAction.modifier + EnemyStats.enemyStatsInstance.baseAttack);
                    // Debug.Log("Enemy Attacked");
                    break;
                }
            case ComponentType.Heal:
                {
                    EnemyStats.enemyStatsInstance.Heal(nextAction.modifier);
                    break;
                }
            case ComponentType.Buff:
                {
                    EnemyStats.enemyStatsInstance.StartCoroutine(EnemyBuffAnim());

                    EnemyStats.enemyStatsInstance.baseAttack += nextAction.modifier;
                    // Debug.Log("Enemy buffed");
                    break;
                }
            case ComponentType.Debuff:
                {
                    Debug.Log("Not implemented");
                    break;
                }
            case ComponentType.Mana:
                {
                    PlayerStats.playerStatsInstance.IncreaseMana(nextAction.modifier);
                    break;
                }
        }
        nextAction = GetAction();
    }

    IEnumerator EnemyAttackAnim()
    {
        EnemyStats.enemyStatsInstance.animator.SetTrigger("Attack");
        yield return new WaitForSeconds(1.5f);
        PlayerStats.playerStatsInstance.animator.SetTrigger("On Hit");
        yield return new WaitForSeconds(1f);
    }

    IEnumerator EnemyBuffAnim () 
    {
        EnemyStats.enemyStatsInstance.animator.SetTrigger("Heal");
        yield return new WaitForSeconds(1f);
    }

    public Action GetNextAction()
    {
        return nextAction;
    }

    private Action GetAction()
    {
        // Action ret = actions[random.Next(actions.Count)];
        // Debug.Log("Next action is " + ret.explanation);
        return actions[turn++ % 2];
    }
        
}

[System.Serializable]
public class Action
{
    public string explanation;
    public ComponentType action;
    public int modifier;
}
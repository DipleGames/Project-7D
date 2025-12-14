using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    public enum TowerState { IdleState, AttackState }

    [Header("타워 설정")]
    [SerializeField] private float attackDamage = 5f;
    [SerializeField] private float attackRange = 4f;
    [SerializeField] private LayerMask targetLayer;
    [SerializeField] private TowerState currentState;
    [SerializeField] private bool isDestroyed = false;
    [SerializeField] private Collider[] targetList;
    public float MaxHealth = 500f;

    public event Action<bool> OnChangedTargetList;


    private float _towerHealth;
    public float TowerHealth
    {
        get => _towerHealth;
        private set
        {
            _towerHealth = Mathf.Clamp(value, 0, MaxHealth);
            Debug.Log($"{_towerHealth}");
            if (_towerHealth <= 0 && !isDestroyed)
            {
                Destroyed();
            }
        }
    }

    void Start()
    {
        OnChangedTargetList += ChangedMode;
    }

    void Update()
    {
        OnChangedTargetList(IsTargetInRange());

        switch (currentState)
        {
            case TowerState.IdleState:
                break;
            case TowerState.AttackState:
                OnAttackMode();
                break;
        }
    }

    bool IsTargetInRange()
    {
        targetList = Physics.OverlapSphere(transform.position, attackRange, targetLayer);
        return targetList.Length > 0;
    }

    void ChangedMode(bool IsTargetInRange)
    {
        switch (IsTargetInRange)
        {
            case true:
                currentState = TowerState.AttackState;
                break;
            case false:
                currentState = TowerState.IdleState;
                break;
        }
    }

    private float attackDelay = 0f;
    void OnAttackMode()
    {
        attackDelay += Time.deltaTime;
        if (attackDelay >= 3f)
        {
            foreach (var target in targetList)
            {
                if (target == null) continue;

                ZombieController zombie = target.GetComponent<ZombieController>();
                if (zombie == null || zombie.isDead) continue;

                zombie.TakeDamage(attackDamage);
            }

            attackDelay = 0f;
        }
    }

    void Destroyed()
    {
        isDestroyed = true;
        Destroy(gameObject);
    }
}

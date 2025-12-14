using UnityEngine;
using System;

public class ZombieController : MonoBehaviour
{
    [Header("좀비 상태")]
    public float maxHealth = 100f;
    public bool isDead = false;

    private float _zombieHealth;
    public float ZombieHealth
    {
        get => _zombieHealth;
        private set
        {
            _zombieHealth = Mathf.Clamp(value, 0, maxHealth);
            if (_zombieHealth <= 0 && !isDead)
            {
                Die();
            }
        }
    }

    // 좀비가 활성화될 때 상태 초기화
    void OnEnable()
    {
        ResetZombieState();
    }

    /// <summary>
    /// 좀비 체력 및 상태 초기화
    /// </summary>
    public void ResetZombieState()
    {
        isDead = false;
        ZombieHealth = maxHealth;
        // 필요 시 애니메이션 초기화, 위치 초기화도 여기에 추가
    }

    /// <summary>
    /// 데미지를 받아 체력이 감소
    /// </summary>
    /// <param name="damage">입는 데미지</param>
    public void TakeDamage(float damage)
    {
        if (isDead) return;

        ZombieHealth -= damage;
        Debug.Log($"좀비가 {damage} 피해를 입음. 남은 체력: {ZombieHealth}");
    }

    /// <summary>
    /// 사망 처리: 오브젝트 풀로 되돌리기 위해 비활성화
    /// </summary>
    void Die()
    {
        isDead = true;
        Debug.Log("좀비가 죽었습니다.");
        gameObject.SetActive(false); // 오브젝트 풀에 의해 비활성화 후 대기
    }
}

using UnityEngine;
using UnityEngine.UIElements;
using System;

public class ZombieController : MonoBehaviour
{
   
    [Header("좀비 상태")]
    public float maxHealth = 100f;
    public bool isDead = false;

    //public event Action<float> OnZombieHealthChanged; // 체력 이벤트

    private float _zombieHealth;
    public float ZombieHealth
    {
        get => _zombieHealth;
        private set
        {
            _zombieHealth = Mathf.Clamp(value, 0, maxHealth);
            //OnZombieHealthChanged.Invoke(_zombieHealth);
            if (_zombieHealth <= 0 && !isDead)
            {
                Die();
            }
        }
    }

    void Start()
    {
        SetZombieConditon();
    }

    void SetZombieConditon()
    {
        ZombieHealth = maxHealth;
    }

    public void TakeDamage(float damage)
    {
        if (isDead) return;

        ZombieHealth -= damage;
        Debug.Log($"좀비가 {damage}를 입음 남은 체력 : {ZombieHealth}");
    }

    void Die()
    {
        isDead = true;
        Debug.Log("좀비가 죽었습니다.");
        Destroy(gameObject);
    }
}

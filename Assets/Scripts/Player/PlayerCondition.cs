using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public interface IDamageable
{
    void TakeDamage(int damageamount);
}

public class PlayerCondition : MonoBehaviour , IDamageable
{
    public UICondition uiCondition;

    //[SerializeField] private Image gameOver;

    Condition health { get { return uiCondition.health; } }
    Condition hunger { get { return uiCondition.hunger; } }
    Condition stamina { get { return uiCondition.stamina; } }

    public float hungerDamage;
    public event Action onTakeDamage;

    private void Update()
    {
        hunger.Subtract(hunger.decreaseValue * Time.deltaTime);
        stamina.Add(stamina.decreaseValue * Time.deltaTime);

        if(hunger.curValue <= 0)
        {
            health.Subtract(hungerDamage * Time.deltaTime);
        }

        if(health.curValue <= 0)
        {
            Die();
        }
    }

    public void Heal(float amount)
    {
        health.Add(amount);
    }

    public void Eat(float amount)
    {
        hunger.Add(amount);
    }

    private void Die()
    {
        //gameOver.gameObject.SetActive(true);
    }

    public void TakeDamage(int damageamount)
    {
        health.Subtract(damageamount);
        onTakeDamage?.Invoke();
    }

    public bool UseStamina(float amount)
    {
        if (stamina.curValue - amount < 0f) return false;

        stamina.Subtract(amount);
        return true;
    }
}

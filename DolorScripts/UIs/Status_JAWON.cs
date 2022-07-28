using System.Collections;
using UnityEngine;
using Photon.Pun;

public class Status_JAWON : MonoBehaviourPun/*,  IDamageable */
{
    public event System.Action<float, float> OnHpValueChange;
    public int maxHP = 100;
    [SerializeField]
    private float hp;
    bool hpValueFixed;
    public float HP
    {
        get{return hp;}
        private set
        {
            if (hpValueFixed) return;

            float prevHp = hp;
            hp = (value <= 0 ? 0 : (value > maxHP ? maxHP : value));

            if (prevHp != hp)
            {
                OnHpValueChange?.Invoke(hp, maxHP);
                if (hp == 0) OnDeath();
            }
        }
    }

    private void Awake() {
        HP = maxHP;
    }

    public void TakeDamage(float damage)
    {
        Debug.Log("Damage" + damage);
        HP -= damage;

        StartCoroutine(test());
    }

    private void OnDeath()
    {
        hpValueFixed = true;
    }

    IEnumerator test()
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();

        Color origin = renderers[0].material.color;

        foreach(var renderer in renderers)
        {
            renderer.material.color = Color.red;
        }

        yield return new WaitForSeconds(0.01f);

        foreach(var renderer in renderers)
        {
            renderer.material.color = origin;
        }
    }
}

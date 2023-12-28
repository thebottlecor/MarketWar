using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SkillButton : EventListener
{
    protected override void AddListeners()
    {
        PlayerController.skillButtonEvent += OnSkillButtonClicked;
    }
    protected override void RemoveListeners()
    {
        PlayerController.skillButtonEvent -= OnSkillButtonClicked;
    }
    private void OnSkillButtonClicked(object sender, int e)
    {
        SkillUse();
    }

    public Image cooldownImage;
    public TextMeshProUGUI cooldownTMP;

    // 스킬 정보
    public float cooldown = 10f;
    private float current_cooldown;
    public float castingTime = 0f;
    private bool triggered;

    public void SkillUse()
    {
        if (PlayerController.Instance.IsCasting)
            return;

        if (current_cooldown <= 0f)
        {
            triggered = false;
            PlayerController.Instance.SetCast(castingTime);
            current_cooldown = cooldown;
        }
        else
        {
            //if (IsCasting)
            //{
            //    // 캐스팅 취소
            //    current_casting = 0f;
            //    current_cooldown = 0f;
            //    return;
            //}

            // 쿨다운 때문에 스킬 미발동
        }
    }

    private void SkillTrigger()
    {
        triggered = true;

        // 연출
        EffectManager.Instance.CallEffect(2, PlayerController.Instance.player.transform.position);

        // 효과
        var towers = BuildingManager.Instance.towers;
        foreach (var tower in towers)
        {
            if (tower is PlayerUnit) continue;

            float dist = Vector3.Distance(tower.transform.position, PlayerController.Instance.player.transform.position);
            if (dist <= 2f)
            {
                tower.AddHp(tower.maxHp * 0.1f);
            }
        }
    }

    private void Update()
    {
        if (!PlayerController.Instance.IsCasting)
        {
            if (current_cooldown > 0f)
            {
                if (!triggered)
                    SkillTrigger();

                current_cooldown += -1f * Time.deltaTime;
            }
        }
        UpdateUI();
    }

    private void UpdateUI()
    {
        if (current_cooldown > 0f)
        {
            cooldownTMP.gameObject.SetActive(true);
            cooldownTMP.text = $"{current_cooldown:F0}s";
            cooldownImage.fillAmount = (current_cooldown / cooldown);
        }
        else
        {
            cooldownTMP.gameObject.SetActive(false);
            triggered = false;
        }
    }

}

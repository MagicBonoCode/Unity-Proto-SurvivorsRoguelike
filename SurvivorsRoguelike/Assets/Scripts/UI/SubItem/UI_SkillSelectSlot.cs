using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_SkillSelectSlot : UI_Base
{
    private enum Texts
    {
        Text_SkillName,
        Text_SkillLevel,
        Text_SkillDec,
    }

    private enum Images
    {
        Image_SkillIcon,
    }

    private Text _skillNameText;
    private Text _skillLevelText;
    private Text _skillDecText;
    private Image _skillIconImage;
    private Define.ActiveSkillType _activeSkillType = Define.ActiveSkillType.Default;
    private Define.PassiveSkillType _passiveSkillType = Define.PassiveSkillType.Default;

    public override void Init()
    {
        Bind<Text>(typeof(Texts));
        Bind<Image>(typeof(Images));

        BindEvent(gameObject, OnClickSkillSelectSlot);

        _skillNameText = GetText((int)Texts.Text_SkillName);
        _skillLevelText = GetText((int)Texts.Text_SkillLevel);
        _skillDecText = GetText((int)Texts.Text_SkillDec);
        _skillIconImage = GetImage((int)Images.Image_SkillIcon);

        GetComponent<RectTransform>().localScale = Vector3.one;
    }

    public void SetSkillSelectSlot(Define.ActiveSkillType activeSkillType)
    {
        _activeSkillType = activeSkillType;

        BaseActiveSkill activeSkill = null;
        if (Managers.Skill.ActiveSkills.TryGetValue(activeSkillType, out activeSkill))
        {
            if (activeSkill.Level == activeSkill.MaxLevel)
            {
                // TODO: Change Other Skill.
                Managers.Resource.Destroy(gameObject);
            }
            else
            {
                _skillNameText.text = activeSkill.Name;
                _skillLevelText.text = string.Format("lv.{0}", activeSkill.Level);
                _skillDecText.text = Managers.Data.ActiveSkillInfoDictionary[activeSkillType].Descriptions[activeSkill.Level];
                _skillIconImage.sprite = activeSkill.Icon;
            }
        }
        else
        {
            _skillNameText.text = Managers.Data.ActiveSkillInfoDictionary[activeSkillType].Name;
            _skillLevelText.text = string.Format("lv.{0}", 1);
            _skillDecText.text = Managers.Data.ActiveSkillInfoDictionary[activeSkillType].Descriptions[0];
            _skillIconImage.sprite = Managers.Data.ActiveSkillInfoDictionary[activeSkillType].Icon;
        }
    }

    public void SetSkillSelectSlot(Define.PassiveSkillType passiveSkillType)
    {
        _passiveSkillType = passiveSkillType;

        PassiveSkill passiveSkill = null;
        if (Managers.Skill.PassiveSkills.TryGetValue(passiveSkillType, out passiveSkill))
        {
            if (passiveSkill.Level == passiveSkill.MaxLevel)
            {
                // TODO: Change Other Skill.
                Managers.Resource.Destroy(gameObject);
            }
            else
            {
                _skillNameText.text = passiveSkill.Name;
                _skillLevelText.text = string.Format("lv.{0}", passiveSkill.Level);
                _skillDecText.text = Managers.Data.PassiveSkillInfoDictionary[passiveSkillType].Descriptions[passiveSkill.Level];
                _skillIconImage.sprite = passiveSkill.Icon;
            }
        }
        else
        {
            _skillNameText.text = Managers.Data.PassiveSkillInfoDictionary[passiveSkillType].Name;
            _skillLevelText.text = string.Format("lv.{0}", 1);
            _skillDecText.text = Managers.Data.PassiveSkillInfoDictionary[passiveSkillType].Descriptions[0];
            _skillIconImage.sprite = Managers.Data.PassiveSkillInfoDictionary[passiveSkillType].Icon;
        }
    }

    private void OnClickSkillSelectSlot(PointerEventData data)
    {
        if (_activeSkillType != Define.ActiveSkillType.Default)
        {
            Managers.Skill.AddActiveSkill(_activeSkillType, Managers.Object.Player);
        }
        else
        {
            Managers.Skill.AddPassiveSkill(_passiveSkillType);
        }

        Managers.UI.CloseAllPopupUI();
        Managers.Scene.GetCurrentScene<GameScene>().State = Define.GameSceneState.Play;
    }
}

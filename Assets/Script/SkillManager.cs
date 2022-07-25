using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillManager : MonoBehaviour
{

    // Start is called before the first frame update

    private DataBaseManager theDB;
    private UnitManager theUNIT;
    private EnemyManager theENEMY;
    private CameraShake shakingCamera;
    public ParticleSystem[] pss;
    public ParticleSystem[] enemyPss;

    public string[] skillDes;

    public GameObject[] targetFriendly;
    public GameObject[] targetEnemies;

    public bool ActiveButtons;

    public int targetUnit;
    public int targetEnemy;

    public Image attackImage;

    public int usingSkill;

    public GameObject cancelButton;
    public GameObject waitPanel;

    public Image[] effectPanels;
    public Image[] effectLights;
    public Image[] enemyEffectPanels;
    public Image[] enemyEffectLights;

    public Image[] animations;

    public Sprite[] guardAnime;

    public Sprite healingEffect;

    private WaitForSeconds waitTime = new WaitForSeconds(0.01f);

    private void Awake()
    {

    }
    void Start()
    {
        theDB = FindObjectOfType<DataBaseManager>();
        theUNIT = FindObjectOfType<UnitManager>();
        theENEMY = FindObjectOfType<EnemyManager>();
        shakingCamera = FindObjectOfType<CameraShake>();
        WritingSkillDes();
    }

    public void UseSkill(int skillID)
    {
        theDB.myUnit[theUNIT.selectedUnit].unitAP -= theDB.skillList[skillID].skillCost;
        int atk = theDB.myUnit[theUNIT.selectedUnit].currentATK;
        int def = theDB.myUnit[theUNIT.selectedUnit].currentDEF;
        switch (skillID)
        {
         
            case 0:
                break;
            case 1:
                Attack(atk, targetEnemy);
                break;
            case 2:
                PierceAttack(atk, targetEnemy);
                break;
            case 3:
                Attack(atk * 2, targetEnemy);
                break;
            case 4:
                Defence(def, theUNIT.selectedUnit);
                break;
            case 5:
                Attack(atk * 3, targetEnemy);
                break;
            case 6:
                Attack(atk * 5, targetEnemy);
                break;
            case 7:
                Attack(atk * 10, targetEnemy);
                break;
            case 8:
                Attack(atk * theDB.myUnit[theUNIT.selectedUnit].unitAP, targetEnemy);
                theDB.myUnit[theUNIT.selectedUnit].unitAP = 0;
                break;
            case 9:
                int a = theENEMY.enemyList[targetEnemy].hP / 20;
                PierceAttack(a, targetEnemy);
                break;
            case 10:
                PierceAttack(atk * 3, targetEnemy);
                break;
            case 11:
                Attack(atk, targetEnemy);
                int critical = Random.Range(1, 11);
                if (critical <= 3)
                {
                    AttackOne(atk * 5, targetEnemy);
                }
                break;
            case 12:
                Attack(atk, targetEnemy);
                int critical2 = Random.Range(1, 11);
                if (critical2 <= 7)
                {
                    AttackOne(atk * 9, targetEnemy);
                }
                break;
            case 13:
                Defence(def * 3, theUNIT.selectedUnit);
                break;
            case 14:
                Defence(def * 5, theUNIT.selectedUnit);
                break;
            case 15:
                Defence(def * 10, theUNIT.selectedUnit);
                break;
            case 16:
                DefenceAll(def);
                break;
            case 17:
                DefenceAll(def * 2);
                break;
            case 18:
                DefenceAll(def * 4);
                break;
            case 19:
                AttackOneself(atk * 5, theUNIT.selectedUnit);
                break;
            case 20:
                HealEnemy(atk * 5, targetEnemy);
                break;
            case 21:
                AttackAll(atk);
                break;
            case 22:
                AttackAll(atk * 2);
                break;
            case 23:
                SerialAttack(atk / 2, 6, targetEnemy);
                break;
            case 24:
                SerialAttack(atk, 5, targetEnemy);
                break;
            case 25:
                StatusAllUnit(1, 2);
                break;

        }
    } 

    public void Attack(int _damage, int _targetEnemy)
    {

        StartCoroutine(AttackCoroutine(_targetEnemy));
        enemyPss[_targetEnemy].Play();

        if (_damage <= theENEMY.enemyList[_targetEnemy].guard)
        {
            theENEMY.enemyList[_targetEnemy].guard -= _damage;
        }
        else
        {
            _damage -= theENEMY.enemyList[_targetEnemy].guard;
            theENEMY.enemyList[_targetEnemy].hP -= _damage;

            theENEMY.enemyList[_targetEnemy].guard = 0;
        }

        StartCoroutine(AttackMotionCoroutine());
    }

    public void PierceAttack (int _damage, int _targetEnemy)
    {
        StartCoroutine(AttackCoroutine(_targetEnemy));
        enemyPss[_targetEnemy].Play();

        theENEMY.enemyList[_targetEnemy].hP -= _damage;
        StartCoroutine(AttackMotionCoroutine());
    }

    public void AttackOne(int _damage, int _targetEnemy)
    {

        StartCoroutine(AttackCoroutine(_targetEnemy));
        enemyPss[_targetEnemy].Play();

        if (_damage <= theENEMY.enemyList[_targetEnemy].guard)
        {
            theENEMY.enemyList[_targetEnemy].guard -= _damage;
        }
        else
        {
            _damage -= theENEMY.enemyList[_targetEnemy].guard;
            theENEMY.enemyList[_targetEnemy].hP -= _damage;

            theENEMY.enemyList[_targetEnemy].guard = 0;
        }

    }

    public void Attacked(int _damage, int _targetUnit)
    {
        pss[_targetUnit].Play();
        StartCoroutine(AttackedCoroutine(_targetUnit));
        shakingCamera.VibrateForTime(0.2f);
        if (_damage <= theDB.myUnit[_targetUnit].guard)
        {
            theDB.myUnit[_targetUnit].guard -= _damage;
        }
        else
        {
            _damage -= theDB.myUnit[_targetUnit].guard;
            theDB.myUnit[_targetUnit].unitHP -= _damage;

            theDB.myUnit[_targetUnit].guard = 0;
        }
    }

    public void AttackOneself(int _damage, int _targetUnit)
    {
        pss[_targetUnit].Play();
        StartCoroutine(AttackedCoroutine(_targetUnit));
        shakingCamera.VibrateForTime(0.2f);
        if (_damage <= theDB.myUnit[_targetUnit].guard)
        {
            theDB.myUnit[_targetUnit].guard -= _damage;
        }
        else
        {
            _damage -= theDB.myUnit[_targetUnit].guard;
            theDB.myUnit[_targetUnit].unitHP -= _damage;

            theDB.myUnit[_targetUnit].guard = 0;
        }
    }


    public void Defence(int _defence, int _targetUnit)
    {
        StartCoroutine(DefenceAnimeCoroutine(_targetUnit, _defence));
    }

    public void DefenceAll(int _defence)
    {
        for (int i = 0; i < 5; i++)
        {
            if (theDB.myUnit[i].unitID != 0)
            {
                StartCoroutine(DefenceAnimeCoroutine(i, _defence));
            }
        }
    }

    public void StatusAllUnit(int _statusID, int _stack)
    {
        for (int i = 0; i < 5; i++)
        {
            if (theDB.myUnit[i].unitID != 0)
            {
                StatusUnit(_statusID, _stack, i);
            }
        }
    }

    public void AttackAll(int _damage)
    {
        StartCoroutine(AttackAllCoroutine(_damage));
    }

    public void SerialAttack(int _damage, int _times, int _targetEnemy)
    {
        StartCoroutine(SerialAttackCoroutine(_damage, _times, _targetEnemy));
    }

    public void HealEnemy(int _amount, int _targetEnemy)
    {
        StartCoroutine(EnemyHealingCoroutine(_targetEnemy));
        theENEMY.enemyList[_targetEnemy].hP += _amount;

    }

    public void StatusUnit(int _statusID, int _stack, int _targetUnit)
    {
        for (int i = 0; i < theDB.myUnit[_targetUnit].unitStatus.Count; i++)
        {
            if (theDB.myUnit[_targetUnit].unitStatus[i].statusID == _statusID)
            {
                theDB.myUnit[_targetUnit].statusStack[i] += _stack;

                theUNIT.DrawingStatus();
                StartCoroutine(StatusCoroutine(_statusID, _targetUnit));
                return;
            }
        }


        theDB.myUnit[_targetUnit].unitStatus.Add(theDB.statusList[_statusID]);

        for (int i = 0; i < theDB.myUnit[_targetUnit].unitStatus.Count; i++)
        {
            if (theDB.myUnit[_targetUnit].unitStatus[i].statusID == _statusID)
            {
                theDB.myUnit[_targetUnit].statusStack[i] = _stack;

                theUNIT.DrawingStatus();
                StartCoroutine(StatusCoroutine(_statusID, _targetUnit));
                return;
            }
        }
    }

    IEnumerator SerialAttackCoroutine(int _damage, int _times, int _targetEnemy)
    {
        StartCoroutine(AttackMotionCoroutine());
        for (int i = 0; i < _times; i++)
        {
            AttackOne(_damage, _targetEnemy);
            yield return new WaitForSeconds(0.1f);
        }


    }

    IEnumerator AttackAllCoroutine(int _damage)
    {
        StartCoroutine(AttackMotionCoroutine());
        for (int i = 0; i < 5; i++)
        {
            if (theENEMY.enemyList[i].Null == false)
            {
                AttackOne(_damage, i);
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    IEnumerator AttackedCoroutine(int _targetUnit)
    {
        Character chara;
        chara = theUNIT.character[_targetUnit].transform.GetComponentInChildren<Character>();
        chara.Attacked();
        yield return new WaitForSeconds(0.01f) ;
    }
    IEnumerator AttackCoroutine(int _targetUnit)
    {

        theENEMY.enemyChara[_targetUnit].Attacked();
        yield return new WaitForSeconds(0.1f);

    }

    IEnumerator DefenceAnimeCoroutine(int _targetUnit, int _defence)
    {
        waitPanel.SetActive(true);
        Color color = animations[_targetUnit].color;
        color.a = 1;
        animations[_targetUnit].color = color;
        for (int i = 0; i < guardAnime.Length; i++)
        {

            animations[_targetUnit].sprite = guardAnime[i];
            yield return new WaitForSeconds(0.05f);
        }

        theDB.myUnit[_targetUnit].guard += _defence;
        while (color.a > 0)
        {
            color.a -= 0.2f;
            animations[_targetUnit].color = color;

            yield return new WaitForSeconds(0.05f);
        }

        waitPanel.SetActive(false);
    }

    IEnumerator AttackMotionCoroutine()
    {
        waitPanel.SetActive(true);
        Character chara;
        chara = theUNIT.character[theUNIT.selectedUnit].transform.GetComponentInChildren<Character>();
        chara.Attack();

        attackImage.sprite = theDB.myUnit[theUNIT.selectedUnit].unitAttack;

        Color color2 = attackImage.color;
        color2.a = 1;
        attackImage.color = color2;
        yield return new WaitForSeconds(0.4f);
        while (color2.a > 0)
        {
            
            color2.a -= 0.03f;
            attackImage.color = color2;

            yield return waitTime;
        }
        waitPanel.SetActive(false);

    }

    IEnumerator HealingCoroutine(int _targetEnemy)
    {
        waitPanel.SetActive(true);
        effectPanels[_targetEnemy].sprite = healingEffect;
        Color color = effectLights[_targetEnemy].color;
        color.a = 1;
        effectLights[_targetEnemy].color = color;
        Color color2 = effectPanels[_targetEnemy].color;
        color2.a = 1;
        effectPanels[_targetEnemy].color = color2;

        Vector3 originVector = new Vector3(0, 0, 0);
        originVector = effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition;

        while (color2.a > 0)
        {
            color.a -= 0.02f;
            effectLights[_targetEnemy].color = color;
            color2.a -= 0.02f;
            effectPanels[_targetEnemy].color = color2;

            effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition = new Vector3(effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition.x, effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition.y + 2, 0);

            yield return waitTime;
        }

        if (color2.a <= 0)
        {
            waitPanel.SetActive(false);
            effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition = originVector;
        }

    }

    IEnumerator StatusCoroutine(int _statusID, int _targetEnemy)
    {
        waitPanel.SetActive(true);
        effectPanels[_targetEnemy].sprite = theDB.statusList[_statusID].statusImage; ;
        Color color = effectLights[_targetEnemy].color;
        color.a = 1;
        effectLights[_targetEnemy].color = color;
        Color color2 = effectPanels[_targetEnemy].color;
        color2.a = 1;
        effectPanels[_targetEnemy].color = color2;

        Vector3 originVector = new Vector3(0, 0, 0);
        originVector = effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition;

        while (color2.a > 0)
        {
            color.a -= 0.02f;
            effectLights[_targetEnemy].color = color;
            color2.a -= 0.02f;
            effectPanels[_targetEnemy].color = color2;

            effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition = new Vector3(effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition.x, effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition.y + 2f, 0);

            yield return waitTime;
        }

        if (color2.a <= 0)
        {
            waitPanel.SetActive(false);
            effectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition = originVector;
        }

    }

    IEnumerator EnemyHealingCoroutine(int _targetEnemy)
    {
        waitPanel.SetActive(true);
        enemyEffectPanels[_targetEnemy].sprite = healingEffect;
        Color color = enemyEffectLights[_targetEnemy].color;
        color.a = 1;
        enemyEffectLights[_targetEnemy].color = color;
        Color color2 = enemyEffectPanels[_targetEnemy].color;
        color2.a = 1;
        enemyEffectPanels[_targetEnemy].color = color2;

        Vector3 originVector = new Vector3(0, 0, 0);
        originVector = enemyEffectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition;

        while (color2.a > 0)
        {
            color.a -= 0.02f;
            enemyEffectLights[_targetEnemy].color = color;
            color2.a -= 0.02f;
            enemyEffectPanels[_targetEnemy].color = color2;

            enemyEffectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition = new Vector3(enemyEffectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition.x, enemyEffectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition.y + 0.02f, 0);

            yield return waitTime;
        }

        if (color2.a <= 0)
        {
            waitPanel.SetActive(false);
            enemyEffectPanels[_targetEnemy].GetComponent<RectTransform>().anchoredPosition = originVector;
        }

    }

    public void WritingSkillDes()
    {
        int atk = theDB.myUnit[theUNIT.selectedUnit].currentATK;
        int def = theDB.myUnit[theUNIT.selectedUnit].currentDEF;
        skillDes[1] = "적에게" + atk + "만큼의 피해를 입힌다.";
        skillDes[2] = "적에게" + atk + "만큼의 방어도를 무시한 피해를 입힌다.";
        skillDes[3] = "적에게" + atk * 2 + "만큼의 피해를 입힌다.";
        skillDes[4] = "자신에게" + def + "만큼의 방어도를 추가한다.";
        skillDes[5] = "적에게" + atk * 3 + "만큼의 피해를 입힌다.";

        skillDes[6] = "적에게" + atk * 5 + "만큼의 피해를 입힌다.";
        skillDes[7] = "적에게" + atk * 10 + "만큼의 피해를 입힌다.";
        skillDes[8] = "현재 행동력을 모두 소비하고 적에게" + atk * 2 * theDB.myUnit[theUNIT.selectedUnit].unitAP + "만큼의 피해를 입힌다.";
        skillDes[9] = "적의 최대 체력의 5%만큼의 피해를 입힌다.";
        skillDes[10] = "적에게" + atk * 3 + "만큼의 방어도를 무시한 피해를 입힌다.";

        skillDes[11] = "적에게" + atk + "만큼의 피해를 입히며, 30% 확률로" + atk * 5 + "만큼의 추가타를 가한다.";
        skillDes[12] = "적에게" + atk + "만큼의 피해를 입히며, 70% 확률로" + atk * 9 + "만큼의 추가타를 가한다.";
        skillDes[13] = "자신에게" + def * 3 + "만큼의 방어도를 추가한다.";
        skillDes[14] = "자신에게" + def * 5 + "만큼의 방어도를 추가한다.";
        skillDes[15] = "자신에게" + def * 10 + "만큼의 방어도를 추가한다.";

        skillDes[16] = "아군 전체에게" + def + "만큼의 방어도를 추가한다.";
        skillDes[17] = "아군 전체에게" + def * 2 + "만큼의 방어도를 추가한다.";
        skillDes[18] = "아군 전체에게" + def * 4 + "만큼의 방어도를 추가한다.";
        skillDes[19] = "자신에게" + atk * 5 + "만큼의 피해를 입힌다.";
        skillDes[20] = "적을" + atk * 5 + "만큼회복시킨다.";

        skillDes[21] = "적 전체에게" + atk + "만큼의 피해를 입힌다.";
        skillDes[22] = "적 전체에게" + atk * 2 + "만큼의 피해를 입힌다.";
        skillDes[23] = "적에게" + atk / 2 + "만큼의 피해를 6번 입힌다.";
        skillDes[24] = "적에게" + atk + "만큼의 피해를 5번 입힌다.";
        skillDes[25] = "아군 전체의 공격력을 2턴간 1.5배 증가시킨다. 중첩 불가.";
    }

    public void DrawingButtons()
    {
        ActiveButtons = true;
        for (int i = 0; i < 5; i++)
        {
            if (theDB.myUnit[i].unitID != 0)
            {
                targetFriendly[i].SetActive(true);
            }
            else
            {
                targetFriendly[i].SetActive(false);
            }
        }
    }

    public void DrawingEnemyButtons()
    {
        ActiveButtons = true;
        for (int i = 0; i < 5; i++)
        {
            if (theENEMY.enemyList[i].Null != true)
            {
                targetEnemies[i].SetActive(true);
            }
            else
            {
                targetEnemies[i].SetActive(false);
            }
        }
    }

    public void ClearButtons()
    {
        ActiveButtons = false;
        for (int i = 0; i < 5; i++)
        {
            targetFriendly[i].SetActive(false);
        }
    }

    public void ClearEnemyButtons()
    {
        ActiveButtons = false;
        for (int i = 0; i < 5; i++)
        {
            targetEnemies[i].SetActive(false);
        }
    }

    public void Using(int skillID)
    {
        usingSkill = skillID;
        if (theDB.myUnit[theUNIT.selectedUnit].unitAP >= theDB.skillList[skillID].skillCost)
        {


            switch (theDB.skillList[skillID].skillType)
            {
                case Skill.SkillType.attackOne:
                    StartCoroutine(TargetEnemyCoroutine());
                    break;
                case Skill.SkillType.attackAll:
                    UseSkill(skillID);
                    break;
                case Skill.SkillType.healOne:
                    StartCoroutine(TargetFriendlyCoroutine());
                    break;
                case Skill.SkillType.healAll:
                    UseSkill(skillID);
                    break;
                case Skill.SkillType.auto:
                    UseSkill(skillID);
                    break;
            }
        }
        else
        {
            Debug.Log("사용 불가");
        }
    }

    public void Canceling()
    {
        cancelButton.SetActive(false);
        StopAllCoroutines();
        ClearButtons();
        ClearEnemyButtons();
    }

    IEnumerator TargetFriendlyCoroutine()
    {
        cancelButton.SetActive(true);
        DrawingButtons();
        yield return new WaitUntil(() => !ActiveButtons);
        UseSkill(usingSkill);
        cancelButton.SetActive(false);
    }

    IEnumerator TargetEnemyCoroutine()
    {
        cancelButton.SetActive(true);
        DrawingEnemyButtons();
        yield return new WaitUntil(() => !ActiveButtons);
        UseSkill(usingSkill);
        cancelButton.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}

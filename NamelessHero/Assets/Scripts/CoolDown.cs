using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CoolDown : MonoBehaviour
{
    #region UnityEvents
    void Start()
    {

    }

    void Update()
    {
        bool coolDownComplete = (Time.time > _NextReadyTime);
        if(coolDownComplete) 
        {
            Ready();
        } else 
        {
            ExecuteCoolDown();
        }
    }

    #endregion

    #region Attributes
    [SerializeField] public Image _DarkMask;
    [SerializeField] public Text _CoolDownTextDisplay;
    [SerializeField] public Image _MyButtonImage;
    [SerializeField] public AudioSource _AudioSource;
    [SerializeField] public float _CoolDownDuration = 2.0f;
    [HideInInspector] public float _NextReadyTime;
    [HideInInspector] public float _CoolDownTimeLeft;

    #endregion

    #region Methods
    private void Ready()
    {
        _CoolDownTextDisplay.enabled = false;
        _DarkMask.enabled = false;
    }

    private void ExecuteCoolDown()
    {
        _CoolDownTimeLeft -= Time.deltaTime;
        float roundedCd = Mathf.Round(_CoolDownTimeLeft);
        _CoolDownTextDisplay.text = roundedCd.ToString();
        _DarkMask.fillAmount = (_CoolDownTimeLeft / _CoolDownDuration);
    }

    public bool Triggered()
    {
        if(Time.time > _NextReadyTime)
        {
            //Debug.Log("CoolDownTriggered");

            _NextReadyTime = _CoolDownDuration + Time.time;
            _CoolDownTimeLeft = _CoolDownDuration;
            _DarkMask.enabled = true;
            _CoolDownTextDisplay.enabled = true;

            _AudioSource.enabled = true;
            _AudioSource.Play();
            return true;
        }
        return false;
    }

    #endregion
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Door : MonoBehaviour 
{
    [SerializeField] private GameObject _house;
    [SerializeField] private AudioSource _alarm;
    private float _runingTime;
    private float _startVolume = 0;
    private float _maxVolume = 1;
    private float _duration = 3;
    private bool _isInTrigger;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.TryGetComponent<Goblin>(out Goblin goblin)) 
        {
            _house.SetActive(false);
            _alarm.PlayOneShot(_alarm.clip);
            _runingTime = 0;
            _isInTrigger = true;
               
        }
    }   

    private void OnTriggerExit2D(Collider2D collision)
    {
        _house.SetActive(true);
        _isInTrigger = false;

        if(_runingTime > _duration) 
        {
            _runingTime = _duration;
        }
    }

    private void Update()
    {
        if(_isInTrigger) 
        {
            IncreaseSound();
        }
        else 
        {
            TurnDownSound();
        }
    }

    private void IncreaseSound() 
    {
        if(_runingTime <= _duration) 
        {
            _runingTime += Time.deltaTime;
            Debug.Log(_runingTime);
            float normalizeRunningTime = _runingTime / _duration;
            _alarm.volume = Mathf.MoveTowards(_startVolume, _maxVolume, normalizeRunningTime);
        }
    }

    private void TurnDownSound() 
    {
        if(_runingTime <= _duration && _runingTime > 0) 
        {
            _runingTime -= Time.deltaTime;
            Debug.Log(_runingTime);
            float normalizeRunningTime = _runingTime / _duration;
            _alarm.volume = Mathf.MoveTowards(_startVolume, _maxVolume, normalizeRunningTime);
        }

        if(_runingTime <= 0) 
        {
            _alarm.Stop();
        }
    }
}

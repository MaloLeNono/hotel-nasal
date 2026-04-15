using Enums;
using UnityEngine;

[RequireComponent(typeof(Animator), typeof(Collider2D), typeof(AudioSource))]
public class Dancer : Trap
{
    [SerializeField] private DancerDance dance;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private float spinRadius;
    [SerializeField] private float pushForce;
    [SerializeField] private bool clockwise;
    
    private Vector3 _center;
    private float _angle;
    private Animator _animator;
    private AudioSource _audioSource;
    private float _angleSign;
    
    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _audioSource = GetComponent<AudioSource>();
        _angleSign = clockwise
            ? -1f
            : 1f;
    }

    private void Start()
    {
        _center = transform.position;
        switch (dance)
        {
            case DancerDance.Shuffle:
                _animator.Play("Dancer1");
                break;
            case DancerDance.WhateverThatIs:
                _animator.Play("Dancer2");
                break;
            default:
                Debug.LogWarning($"Invalid dance type {dance.ToString()}");
                break;
        }
    }

    private void Update()
    {
        _angle += Time.deltaTime * rotationSpeed;
        
        float xPosition = Mathf.Cos(_angle * _angleSign) * spinRadius + _center.x;
        float yPosition = Mathf.Sin(_angle * _angleSign) * spinRadius + _center.y;

        transform.position = new Vector3(xPosition, yPosition, _center.z);
    }

    protected override void Activate(Rigidbody2D player)
    {
        _audioSource.Play();
        Vector3 playerDirection = player.transform.position - transform.position;
        player.AddForce(playerDirection * pushForce, ForceMode2D.Impulse);
        Wallet.Instance.money.Amount -= damage;
    }
}

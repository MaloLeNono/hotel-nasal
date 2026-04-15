using UnityEngine;

public class Bombot : MonoBehaviour
{
    private static readonly int ExplodeTrigger = Animator.StringToHash("Explode");

    [SerializeField] private int damage;
    [SerializeField] private float acceleration;
    [SerializeField] private float explosionAcceleration;
    [SerializeField] private float maxSpeed;
    [SerializeField] private float explosionTime;
    [SerializeField] private float explosionForce;
    [SerializeField] private float maxBeepTime;
    [SerializeField] private float minBeepTime;
    [SerializeField] private AudioClip beep;

    public bool triggered;
    
    private Vector3 _directionToPlayer;
    private float _explosionTimer;
    private float _beepTimer;
    private Animator _animator;
    private bool _exploded;
    private Rigidbody2D _playerRigidbody;
    private Rigidbody2D _rigidbody;
    private AudioSource _audioSource;
    private Transform _player;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
        _player = GameObject.FindGameObjectWithTag("Player").transform;
        _playerRigidbody = _player.GetComponent<Rigidbody2D>();
        _rigidbody = GetComponent<Rigidbody2D>();
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        if (!triggered) return;
        
        _directionToPlayer = (_player.transform.position - transform.position).normalized;
        float distanceToPlayer = Vector3.Distance(_player.position, transform.position);
        
        _explosionTimer += Time.deltaTime;

        if (_explosionTimer >= explosionTime)
        {
            Explode(false);
            return;
        }

        float t = _explosionTimer / explosionTime;
        float timeBetweenBeeps = Mathf.Lerp(1f, 0.02f, t);

        _beepTimer += Time.deltaTime;

        if (_beepTimer >= timeBetweenBeeps && !_exploded)
        {
            _beepTimer = 0f;
            _audioSource.PlayOneShot(beep);
        }

        if (distanceToPlayer < 1f) return;
        
        float accelerationToUse = _exploded
            ? explosionAcceleration
            : acceleration;
        
        _rigidbody.AddForce(_directionToPlayer * (accelerationToUse * _rigidbody.mass));
        _rigidbody.velocity = Vector2.ClampMagnitude(_rigidbody.velocity, maxSpeed);
    }

    private void Explode(bool explodedOnPlayer)
    {
        if (_exploded) return;
        _exploded = true;
        _animator.SetTrigger(ExplodeTrigger);
        _audioSource.Play();
        
        if (!explodedOnPlayer) return;
        _playerRigidbody.AddForce(_directionToPlayer * explosionForce, ForceMode2D.Impulse);
        Wallet.Instance.money.Amount -= damage;
    }

    // Animation Event
    public void DestorySelf() => Destroy(gameObject);

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.CompareTag("Bombot"))
            Explode(false);
        else if (other.transform.CompareTag("Player"))
            Explode(true);
    }
}
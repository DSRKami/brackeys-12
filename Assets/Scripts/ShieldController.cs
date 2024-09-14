using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class ShieldController : MonoBehaviour
{
    private float _shieldAngle;
    private Camera _main;
    private RocketInput _rocketInput;
    public Transform rocketTransform;

    public Transform shieldTransform;
    private SpriteRenderer _shieldRenderer;
    public float orbitRadius = 2f;
    [SerializeField]
    private float _windup = 1f;

    private bool _isSheilding = false;
    
    private void Awake()
    {
        _rocketInput = new RocketInput();
    }

    private void OnEnable()
    {
        _rocketInput.Rocket.Shield.Enable();
        _rocketInput.Rocket.Shield.performed += Shield;
    }

    private void OnDisable()
    {
        _rocketInput.Rocket.Shield.Enable();
        _rocketInput.Rocket.Shield.canceled += StopShield;
    }

    // Start is called before the first frame update
    void Start()
    {
        _main = Camera.main;
        // Cursor.visible = false;
        _shieldRenderer = gameObject.GetComponent<SpriteRenderer>();
        
        if (_shieldRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the GameObject.");
        }
        else
        {
            _shieldRenderer.color = Color.grey;
        }

        Metres.energy = 100f;
        Metres.scrap = 100f;

    }

    // Update is called once per frame
    void Update()
    {
        var mousePosition = Mouse.current.position.ReadValue();
        _shieldAngle = GetRelativeAngle(mousePosition);
        UpdateShieldPosition();
        // Debug.Log(_shieldAngle);
    }

    float GetRelativeAngle(Vector2 currentPosition)
    {
        Vector3 mouseWorldPosition = _main.ScreenToWorldPoint(new Vector3(currentPosition.x, currentPosition.y, _main.nearClipPlane));
        Vector2 directionToMouse = (mouseWorldPosition - rocketTransform.position).normalized;
        float angle = Mathf.Atan2(directionToMouse.y, directionToMouse.x) * Mathf.Rad2Deg;
        return angle;
    }
    
    void UpdateShieldPosition()
    {
        float angleInRadians = _shieldAngle * Mathf.Deg2Rad;
        Vector3 offset = new Vector3(Mathf.Cos(angleInRadians) * orbitRadius, Mathf.Sin(angleInRadians) * orbitRadius, 0);
        shieldTransform.position = rocketTransform.position + offset;
        shieldTransform.rotation = Quaternion.Euler(0, 0, (_shieldAngle-90));
    }

    private void Shield(InputAction.CallbackContext callbackContext){
    
        if (!_isSheilding)
        {
            StartCoroutine(ActivateShield());
        }
    }
    
    private void StopShield(InputAction.CallbackContext callbackContext){
            StopCoroutine(ActivateShield());
    }
    
    private IEnumerator ActivateShield()
    {
        _isSheilding = true;

        while (Metres.energy >= 1)
        {
            Metres.energy -= 1;
            Debug.Log("Shield activated, energy left: " + Metres.energy);

            yield return new WaitForSeconds(0.5f);
        }

        Debug.Log("Shield deactivated, not enough energy");
        _isSheilding = false;
    }
}

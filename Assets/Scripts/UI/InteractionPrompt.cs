
using UnityEngine;
using UnityEngine.UI;

public class InteractionPrompt : MonoBehaviour
{
    // Container to hide / unhide prompt
    [SerializeField] private GameObject _container;
    
    // the circle timer
    [SerializeField] private Image _circleImage;
    
    // Current anchor in the world
    private Interactable _interactable;

    // State 
    private bool _isActive = false;
    private bool _isKeyDown = false;
    private float _timePressed;
    [SerializeField] private float _activationTime;

    private void OnEnable()
    {
        EventManager.Instance.HoldingInteractionKey += OnInteractionKey;
        EventManager.Instance.ReleasedInteractionKey += OnInteractionKeyRelease;
        EventManager.Instance.EnteredInteractionArea += OnInteractionAreaEnter;
        EventManager.Instance.LeftInteractionArea += OnInteractionAreaExit;
    }

    private void OnDisable()
    {
        EventManager.Instance.HoldingInteractionKey -= OnInteractionKey;
        EventManager.Instance.ReleasedInteractionKey -= OnInteractionKeyRelease;
        EventManager.Instance.EnteredInteractionArea -= OnInteractionAreaEnter;
        EventManager.Instance.LeftInteractionArea -= OnInteractionAreaExit;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (_isActive)
        {
            transform.position = Camera.main.WorldToScreenPoint(_interactable.GetPromptWorldAnchor());

            if (_isKeyDown)
            {
                _timePressed += Time.deltaTime;
                _circleImage.fillAmount = _timePressed / _activationTime;
                if (_timePressed >= _activationTime)
                {
                    _interactable.CompleteInteraction();
                    _timePressed = 0;
                }
            }
        }
    }

    private void OnInteractionAreaEnter(Interactable interactable)
    {
        _interactable = interactable;
        _isActive = true;
        _circleImage.fillAmount = 0;
        _container.SetActive(true);
    }

    private void OnInteractionAreaExit(Interactable interactable)
    {
        _isActive = false;
        _container.SetActive(false);
    }

    private void OnInteractionKey()
    {
        if (_isActive)
        {
            _isKeyDown = true;
            _timePressed = 0;
        }
    }

    private void OnInteractionKeyRelease()
    {
        _isKeyDown = false;
        _circleImage.fillAmount = 0;
    }
}

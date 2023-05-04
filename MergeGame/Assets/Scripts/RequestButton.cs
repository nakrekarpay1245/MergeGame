using DG.Tweening;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class RequestButton : MonoBehaviour
{
    [Header("Display")]
    [SerializeField]
    private Image requestImage;
    [SerializeField]
    private Image rightDeliverDisplayer;

    private Button buttonComponent;

    private Human humanIconInButton;

    private Entity requestEntity;

    private void Awake()
    {
        buttonComponent = GetComponent<Button>();
        humanIconInButton = GetComponentInChildren<Human>();
        buttonComponent.onClick.AddListener(() =>
        {
            SetRequest();
        });
    }

    private void Start()
    {
        Request();
        RequestManager.singleton.AddRequestButton(this);
        ChangeDeliverIconVisibility(0);
    }

    private void Request()
    {
        requestEntity = RequestManager.singleton.GetRequestEntity();
        requestImage.sprite = requestEntity.GetSprite();
        humanIconInButton.RegenerateHuman();
    }

    public void SetRequest()
    {
        if (RequestManager.singleton.SetRequest(requestEntity, transform))
        {
            //Debug.Log("Request True!");
            RequestManager.singleton.DisplayRequest(requestEntity.GetRequestSprite());
            StopCoroutine(ReRequestRoutine());
            StartCoroutine(ReRequestRoutine());
        }
        else
        {
            //Debug.Log("Request False!");
            FalseRequestEffect();
        }
    }

    private void FalseRequestEffect()
    {
        Sequence rotateSequence = DOTween.Sequence();
        rotateSequence.Append(transform.DOLocalRotate(Vector3.forward * 10,
            TimeManager.singleton.GetUIDelay() / 5));

        rotateSequence.Append(transform.DOLocalRotate(Vector3.forward * -10,
            TimeManager.singleton.GetUIDelay() / 5));

        rotateSequence.Append(transform.DOLocalRotate(Vector3.forward * 10,
            TimeManager.singleton.GetUIDelay() / 5));
        rotateSequence.Append(transform.DOLocalRotate(Vector3.forward * -10,
            TimeManager.singleton.GetUIDelay() / 5));

        rotateSequence.Append(transform.DOLocalRotate(Vector3.zero * 10,
            TimeManager.singleton.GetUIDelay() / 5));
    }

    private IEnumerator ReRequestRoutine()
    {
        ChangeButtonInteractibility(false);
        transform.DOScale(0, TimeManager.singleton.GetUIDelay());
        yield return new WaitForSeconds(TimeManager.singleton.GetUIDelay());

        Transform parentTransform = transform.parent;
        transform.parent = null;
        yield return new WaitForSeconds(TimeManager.singleton.GetUIDelay());

        TitleManager.singleton.IncreaseExperience();
        transform.parent = parentTransform;
        transform.DOScale(1, TimeManager.singleton.GetUIDelay());
        ChangeButtonInteractibility(true);
        Request();

        RequestManager.singleton.ControlRequestButton();
    }

    /// <summary>
    /// This method changes the visibility of the deliver button on the game screen.
    /// If the value parameter is set to true, the deliver button is activated and becomes visible. 
    /// If it is set to false, the button is deactivated and hidden.
    /// </summary>
    /// <param name="value"></param>
    private void ChangeButtonInteractibility(bool value)
    {
        buttonComponent.interactable = value;
    }

    public void ControlRequest()
    {
        if (RequestManager.singleton.ContolRequest(requestEntity))
        {
            ChangeDeliverIconVisibility(1);
        }
        else
        {
            //Debug.Log("NON - DisplayRightDeliverIcon");
            ChangeDeliverIconVisibility(0);
        }
    }

    private void ChangeDeliverIconVisibility(float value)
    {
        StopCoroutine(ChangeDeliverIconVisibilityRoutine(value));
        StartCoroutine(ChangeDeliverIconVisibilityRoutine(value));
    }

    private IEnumerator ChangeDeliverIconVisibilityRoutine(float value)
    {
        while (Mathf.Abs(rightDeliverDisplayer.fillAmount - value) > 0.01f)
        {
            rightDeliverDisplayer.fillAmount =
                Mathf.MoveTowards(rightDeliverDisplayer.fillAmount, value, Time.deltaTime * 4);
            yield return null;
        }

        rightDeliverDisplayer.fillAmount = value;
    }
}

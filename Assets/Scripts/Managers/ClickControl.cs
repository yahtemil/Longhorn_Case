using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickControl : MonoSingleton<ClickControl>
{
    [SerializeField]
    [Tooltip("You can choose slide factor to make it slow or fast")]
    [Range(0f, 1f)]
    private float slidingFactor = .015f;

    private Vector2 touchStart = Vector2.zero;
    private Vector2 touchEnd = Vector2.zero;
    private float upDownSlide;
    private float leftRightSlide;

    private const int ResolutionReferenceY = 1920;
    private const int ResolutionReferenceX = 1080;

    private float resolutionFactorX = 1;
    private float resolutionFactorY = 1;

    public Interactable selectInteractable;
    public IDrag selectIDrag;

    private void Start()
    {
        resolutionFactorX = (float)ResolutionReferenceX / Screen.width;
        resolutionFactorY = (float)ResolutionReferenceY / Screen.height;
    }


    void Update()
    {
        if (GameManager.Instance.GameState != GameManager.GameStates.Play)
        {
            return;
        }
        if (Input.GetMouseButtonDown(0))
        {
            touchStart = Input.mousePosition;
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hit;
            if (Physics.Raycast(ray, out hit, 100f))
            {
                selectInteractable = hit.transform.gameObject.GetComponent<Interactable>();
                selectIDrag = hit.transform.gameObject.GetComponent<IDrag>();
            }
            if (selectInteractable != null)
            {
                selectInteractable.Interact();
            }
            if (selectIDrag != null)
            {
                selectIDrag.Down();
            }
        }
        if (Input.GetMouseButton(0))
        {
            touchEnd = Input.mousePosition;

            upDownSlide = (touchEnd.y - touchStart.y) * resolutionFactorY * slidingFactor;
            leftRightSlide = (touchEnd.x - touchStart.x) * resolutionFactorX * slidingFactor;
            if (selectIDrag != null)
            {
                selectIDrag.Drag(new Vector2(leftRightSlide,upDownSlide));
            }

        }
        if (Input.GetMouseButtonUp(0))
        {
            if (selectIDrag != null)
            {
                selectIDrag.Break();
            }
            selectInteractable = null;
            selectIDrag = null;
        }

    }
}

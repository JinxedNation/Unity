using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

/**
 * @class RayCastHandler
 * @brief Class to handle Ray Casting for clickable items
 *
 * @author Chase Percy
 * @date 26/04//2022
 *
 */
public class RayCastHandler : MonoBehaviour
{
    [SerializeField] [Tooltip("Name of the action to get movement from")]
    private string ActionLocationName = "Player/Action Location";

    [SerializeField] [Tooltip("The component to get player input from")]
    private PlayerInput input;

    [SerializeField] [Tooltip("The layer mask that contains the clickable items")]
    private LayerMask layerMask;

    [SerializeField] [Tooltip("The distance rays will be cast")]
    private int RayDistance = 400;

    private List<GraphicRaycaster> _uiRaycasters = new List<GraphicRaycaster>();

    private ClickBlock _clickBlock;

    /**
    * Input Action relating to the screen position of a primary or secondary action
    */
    private InputAction _actionLocation;

    private float _clickCooldown;

    private void Awake()
    {
        _actionLocation = input.actions[ActionLocationName];
        _clickBlock = FindObjectOfType<ClickBlock>();

        var canvases = FindObjectsOfType<Canvas>();
        foreach (var canvas in canvases)
        {
            _uiRaycasters.Add(canvas.GetComponent<GraphicRaycaster>());
        }
    }

    void Update()
    {
        if (_clickCooldown >= 0) _clickCooldown -= 1 * Time.deltaTime;
    }

    /**
     * Ray cast to represent a Primary ClickType, if a hit is detected a clickable object component is called.
     */
    public void PrimaryRay()
    {
        RaycastHit hitData;
        if (_clickCooldown < 0 && IsHit(out hitData))
        {
            hitData.collider.GetComponentInParent<IClickable>().Clicked(IClickable.ClickType.Primary);
            _clickCooldown = 0.5f;
        }
    }

    /**
     * Ray cast to represent a Secondary ClickType, if a hit is detected a clickable object component is called.
     */
    public void SecondaryRay()
    {
        RaycastHit hitData;
        if (_clickCooldown < 0 && IsHit(out hitData))
        {
            hitData.collider.GetComponentInParent<IClickable>().Clicked(IClickable.ClickType.Secondary);
            _clickCooldown = 0.5f;
        }
    }

    /**
    * Checks if a clickable object was clicked.
    * @param hitData the RayCastData to fill
    * @return bool true if hit, else false
    */
    private bool IsHit(out RaycastHit hitData)
    {
        var clickPos = _actionLocation.ReadValue<Vector2>();

        if (!_clickBlock.IsActive() && !UiIsBlocking(clickPos))
        {
            var ray = Camera.main.ScreenPointToRay(new Vector3(clickPos.x, clickPos.y, 0));
            return Physics.Raycast(ray, out hitData, RayDistance, layerMask);
        }

        hitData = new RaycastHit();
        return false;
    }

    /**
    * Checks if the ui is blocking a raycast
    * @param the position of the click
    * @return true if blocking, else false
    */
    private bool UiIsBlocking(Vector2 clickPos)
    {
        var pData = new PointerEventData(EventSystem.current);
        var results = new List<RaycastResult>();

        pData.position = clickPos;
        foreach (var raycaster in _uiRaycasters)
        {
            raycaster.Raycast(pData, results);
            if (results.Count != 0) return true;
        }

        return false;
    }
}
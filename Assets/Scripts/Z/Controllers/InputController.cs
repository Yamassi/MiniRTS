using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;


public class InputController : MonoBehaviour
{

    [SerializeField] private LayerMask _positionSelect;
    [SerializeField] private LayerMask _interactable;
    [SerializeField] private LayerMask _enemies;
    private InputEventManager _inputEventManager;
    private RaycastHit _hit;
    private Touch _touch;
    private Ray _ray;
    private Vector3 _clickPosition;
    public void Init(InputEventManager inputEventManager)
    {
        _inputEventManager = inputEventManager;
    }
    private void Update()
    {
        TouchClick();
    }

    private void TouchClick()
    {
        if (Input.touchCount > 0)
        {
            _touch = Input.GetTouch(0);
            _ray = Camera.main.ScreenPointToRay(_touch.position);

            if (_touch.phase == TouchPhase.Ended)
            {
                if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _interactable) && !IsPointerOverUIObject())
                {
                    _clickPosition = _hit.point;

                    if (_hit.collider.gameObject.TryGetComponent<IInteractable>(out IInteractable interactable))
                    {
                        if (interactable is WoodController wood)
                        {
                            _inputEventManager.ChopWood(wood, _clickPosition);
                        }
                        else if (interactable is TowerController tower)
                        {
                            _inputEventManager.ConquerTower(tower, _clickPosition);
                        }
                        else if (interactable is BarracksController barracks)
                        {
                            _inputEventManager.BuildBarrack(barracks, _clickPosition);
                        }
                    }
                }
                else if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _enemies) && !IsPointerOverUIObject())
                {
                    _clickPosition = _hit.point;

                    if (_hit.collider.TryGetComponent<AIUnit>(out AIUnit unit))
                    {
                        _inputEventManager.AttackUnit(unit, _clickPosition);
                    }

                }
                else if (Physics.Raycast(_ray, out _hit, Mathf.Infinity, _positionSelect) && !IsPointerOverUIObject())
                {
                    _clickPosition = _hit.point;
                    _inputEventManager.MoveUnit(_clickPosition);
                }

            }
        }
    }
    private bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = new Vector3(Input.mousePosition.x, Input.mousePosition.y);
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }

}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Credit Dan Pos (he made a tutorial on how to do this which was really nice given the time constraint)
public class Interactor : MonoBehaviour
{
    [SerializeField] private Transform _interactionPoint;
    [SerializeField] private float _interactionPointRadius = 0.5f;
    [SerializeField] private LayerMask _interactionMask;
    [SerializeField] private InteractionPromptUI _interactionPromptUI;
    private readonly Collider2D[] _colliders = new Collider2D[3];
    [SerializeField] private int _numFound;
    private IInteractable _interactable;

    private void Update() {
        _numFound = Physics2D.OverlapCircleNonAlloc(_interactionPoint.position, _interactionPointRadius, _colliders, _interactionMask);
        if (_numFound > 0) {
            _interactable = _colliders[0].GetComponent<IInteractable>();
            if (_interactable != null) {
                if (!_interactionPromptUI.IsDisplayed) _interactionPromptUI.SetUp(_interactable.InteractionPrompt);
                if (Input.GetKeyDown(KeyCode.E)) {
                    _interactable.Interact(this);
                    if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
                }
            }
        } else {
            if (_interactable != null) _interactable = null;
            if (_interactionPromptUI.IsDisplayed) _interactionPromptUI.Close();
        }
    }

    private void OnDrawGizmos() {
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(_interactionPoint.position, _interactionPointRadius);
    }
}

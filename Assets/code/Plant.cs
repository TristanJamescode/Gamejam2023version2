using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Plant : MonoBehaviour, IInteractable
{
    [SerializeField] private string _prompt;
    private bool _watered = false;
    [SerializeField] float _maxHeight = 0.0f;
    public string InteractionPrompt => _prompt;

    private Vector3 scaleChange = new Vector3(0.0f, 0.005f, 0.0f);
    private Vector3 positionChange = new Vector3(0.0f, 0.0025f, 0.0f);
    public bool Interact(Interactor interactor)
    {
        if (!_watered) {
            Debug.Log("Watering Plant");
            _watered = true;
            _prompt = "null";
        }
        return true;
    }

    private void Update()
    {
        if (_watered && this.transform.localScale.y < _maxHeight) {
            this.transform.localScale += scaleChange;
            this.transform.position += positionChange;
            if (this.transform.localScale.y >= _maxHeight) {
                gameObject.layer = LayerMask.NameToLayer("Ground");
            }
        }
    }
}

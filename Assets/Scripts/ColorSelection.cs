using System;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class ColorSelection : MonoBehaviour
{
    [SerializeField] private Image _selectionColorImage;
    [SerializeField] private PointerClickHold _nextButton;

    [SerializeField] private Transform _content; 

    [SerializeField] private GameObject _panelColor;

    [SerializeField] private PlayerReferences _playerReferences;

    private void Start()
    {
        FindAndAddListeners(_content);
        
        _nextButton.Hold.AddListener(NextButton);
    }

    private void NextButton()
    {
        Debug.Log(_selectionColorImage.color);
        _playerReferences.Initialize(_selectionColorImage.color);
        _panelColor.SetActive(false);
    }

    private void FindAndAddListeners(Transform currentTransform)
    {
        foreach (Transform child in currentTransform)
        {
            Debug.Log(child.name);
            PointerClickHold clickHoldComponent = child.GetComponent<PointerClickHold>();

            if (clickHoldComponent != null)
            {
                clickHoldComponent.Hold.AddListener(() =>
                {
                    _selectionColorImage.color = clickHoldComponent.GetComponent<Image>().color;
                    Debug.Log("Color updated: " + _selectionColorImage.color);

                });
            }
            FindAndAddListeners(child);
        }
    }
}
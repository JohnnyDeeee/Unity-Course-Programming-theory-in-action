using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField]
    private Text _castleHealthText;
    [SerializeField]
    private Entity _castle;

    private void Update()
    {
        _castleHealthText.text = $"Castle health: {_castle.Health}"; 
    }
}

using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Dropdown))]
[DisallowMultipleComponent]
public class GenerationOptionDropdown : MonoBehaviour
{
    [SerializeField] private GenerationOption[] options;
    [SerializeField] private UnityEvent<GenerationOption> onOptionSelected;

    private Dropdown dropdown;

    private void Awake()
    {
        dropdown = GetComponent<Dropdown>();
    }

    private void Start()
    {
        GameController gameController = GameObject.FindWithTag("GameController")?.GetComponent<GameController>();

        if (gameController != null)
        {
            dropdown.value = dropdown.options.FindIndex(data => data.text == gameController.GenerationOption.GetName());
        }
    }

    public void OnDropdownValueSelected(int index)
    {
        onOptionSelected.Invoke(options[index]);
    }
}

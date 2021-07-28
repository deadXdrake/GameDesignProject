using UnityEngine;

[CreateAssetMenu(fileName = "BoolVariable", menuName = "ScriptableObjects/BoolVariable", order = 4)]
public class BoolVariable : ScriptableObject
{
#if UNITY_EDITOR
    [Multiline]
    public string DeveloperDescription = "";
#endif
    private bool _value = false;
    public bool Value
    {
        get
        {
            return _value;
        }
    }

    public void SetValue(bool value)
    {
        _value = value;
    }

    // overload
    public void SetValue(BoolVariable value)
    {
        _value = value._value;
    }

    public void ApplyChange()
    {
        _value = !_value;
    }

}

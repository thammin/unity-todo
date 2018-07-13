using System.Reflection;
using UnityEngine;
using UnityEngine.UI;
using UniRx;
using System;

public class POCBind : MonoBehaviour
{
    enum POCPropertyType { _string, _int, _float, _bool };


    private object _data;
    private POCPropertyType _setterType;

    private Func<string> _stringGetter;
    private Action<string> _stringSetter;

    private Func<bool> _boolGetter;
    private Action<bool> _boolSetter;

    private Func<int> _intGetter;
    private Action<int> _intSetter;

    private Func<float> _floatGetter;
    private Action<float> _floatSetter;

    private PropertyInfo m_CachedDestinationProperty;
    private PropertyInfo m_CachedSourceProperty;

    [SerializeField]
    private Component m_Destination;
    [SerializeField]
    private string m_DestinationProperty;
    [SerializeField]
    private Component m_Source;
    [SerializeField]
    private string m_SourceProperty;

    public Component source
    {
        get { return m_Source; }
        set { m_Source = value; }
    }

    public Component destination
    {
        get { return m_Destination; }
        set { m_Destination = value; }
    }

    public string sourceProperty
    {
        get { return m_SourceProperty; }
        set { m_SourceProperty = value; }
    }

    public string destinationProperty
    {
        get { return m_DestinationProperty; }
        set { m_DestinationProperty = value; }
    }

    private void Update()
    {
        UpdateBind();
    }

    public void UpdateBind()
    {
        if (m_SourceProperty == null || m_DestinationProperty == null)
        {
            return;
        }

        if (m_CachedSourceProperty == null || m_CachedSourceProperty.Name != m_SourceProperty
            || m_CachedDestinationProperty == null || m_CachedDestinationProperty.Name != m_DestinationProperty)
        {
            Cache();
        }


        if (_setterType == POCPropertyType._string)
        {
            _stringSetter(_stringGetter());
        }
        else if (_setterType == POCPropertyType._bool)
        {
            _boolSetter(_boolGetter());
        }
        else if (_setterType == POCPropertyType._int)
        {
            _boolSetter(_boolGetter());
        }
        else if (_setterType == POCPropertyType._float)
        {
            _boolSetter(_boolGetter());
        }
        else
        {
            m_CachedDestinationProperty.SetValue(m_Destination, m_CachedSourceProperty.GetValue(_data));
        }
    }

    public void Cache()
    {
        var type = m_Source.GetType();

        var temp = type.GetProperty("DTO");
        _data = temp.GetValue(m_Source);

        m_CachedSourceProperty = temp.PropertyType.GetProperty(m_SourceProperty);

        var sourceType = m_CachedSourceProperty.PropertyType;
        if (sourceType == typeof(string))
        {
            _stringGetter = (Func<string>)Delegate.CreateDelegate(
                typeof(Func<string>),
                _data,
                m_CachedSourceProperty.GetMethod
            );
        }
        else if (sourceType == typeof(bool))
        {
            _boolGetter = (Func<bool>)Delegate.CreateDelegate(
                typeof(Func<bool>),
                _data,
                m_CachedSourceProperty.GetMethod
            );
        }
        else if (sourceType == typeof(int))
        {
            _intGetter = (Func<int>)Delegate.CreateDelegate(
                typeof(Func<int>),
                _data,
                m_CachedSourceProperty.GetMethod
            );
        }
        else if (sourceType == typeof(float))
        {
            _floatGetter = (Func<float>)Delegate.CreateDelegate(
                typeof(Func<float>),
                _data,
                m_CachedSourceProperty.GetMethod
            );
        }

        m_CachedDestinationProperty = m_Destination.GetType().GetProperty(m_DestinationProperty);

        var destType = m_CachedDestinationProperty.PropertyType;
        if (destType == typeof(string))
        {
            _setterType = POCPropertyType._string;
            _stringSetter = (Action<string>)Delegate.CreateDelegate(
                typeof(Action<string>),
                m_Destination,
                m_CachedDestinationProperty.SetMethod
            );
        }
        else if (sourceType == typeof(bool))
        {
            _setterType = POCPropertyType._bool;
            _boolSetter = (Action<bool>)Delegate.CreateDelegate(
                typeof(Action<bool>),
                m_Destination,
                m_CachedDestinationProperty.SetMethod
            );
        }
        else if (destType == typeof(int))
        {
            _setterType = POCPropertyType._int;
            _intSetter = (Action<int>)Delegate.CreateDelegate(
                typeof(Action<int>),
                m_Destination,
                m_CachedDestinationProperty.SetMethod
            );
        }
        else if (sourceType == typeof(float))
        {
            _setterType = POCPropertyType._float;
            _floatSetter = (Action<float>)Delegate.CreateDelegate(
                typeof(Action<float>),
                m_Destination,
                m_CachedDestinationProperty.SetMethod
            );
        }
    }
}
using System;
using UnityEngine;

namespace MooshieGames.Common
{
	[Serializable]
	public class Observable<T>
	{
		[SerializeField] private T _value;
		public event Action<T, T> OnValueChanged = delegate { };

		public T Value
		{
			get => _value;
			set
			{
				if (_value.Equals(value)) return;

				OnValueChanged.Invoke(_value, value);
				_value = value;
			}
		}

		public Observable(T value)
		{
			_value = value;
		}

		public void SetValueWithoutNotify(T value) => _value = value;
	}
}
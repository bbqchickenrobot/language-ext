﻿using LanguageExt;
using LanguageExt.Prelude;

namespace LanguageExt
{
    public struct Some<T>
    {
        readonly bool initialised;

        public Some()
        {
            initialised = false;
            Value = default(T);
            throw new SomeNotInitialisedException(typeof(T));
        }

        public Some(T value)
        {
            if (value == null)
            {
                throw new ValueIsNullException("Value is null when expecting Some(x)");
            }
            Value = value;
            initialised = true;
        }

        public T Value { get; }

        private U CheckInitialised<U>(U value) =>
            initialised
                ? value
                : raise<U>( new SomeNotInitialisedException(typeof(T)) );

        public static implicit operator Option<T>(Some<T> value) =>
            Option<T>.Some(value.Value);

        public static implicit operator Some<T>(T value) => 
            new Some<T>(value);

        public static implicit operator T(Some<T> value) => 
            value.CheckInitialised(value.Value);

        public override string ToString() =>
            CheckInitialised(Value.ToString());

        public override int GetHashCode() =>
            CheckInitialised(Value.GetHashCode());

        public override bool Equals(object obj) =>
            CheckInitialised(Value.Equals(obj));
    }
}

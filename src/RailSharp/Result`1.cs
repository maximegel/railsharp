﻿using RailSharp.Internal.Result;

namespace RailSharp
{
    public abstract class Result<TFailure>
    {
        public static implicit operator Result<TFailure>(TFailure failure) =>
            new Failure<TFailure>(failure);

        public static implicit operator Result<TFailure>(Success success) =>
            new Success<TFailure>();
    }
}
using System;
using RailSharp.Internal.Result;

namespace RailSharp
{
    public abstract class Result<TFailure, TSuccess> : Result<TFailure>
    {
        public static implicit operator Result<TFailure, TSuccess>(TFailure failure) => 
            new Failure<TFailure, TSuccess>(failure);

        public static implicit operator Result<TFailure, TSuccess>(Failure<TFailure> failure) =>
            (TFailure)failure;

        public static implicit operator Result<TFailure, TSuccess>(TSuccess success) =>
            new Success<TFailure, TSuccess>(success);
    }
}
using RailSharp.Internal.Result;

namespace RailSharp
{
    public static class Result
    {
        public static Success Success =>
            new Success();
    }
}
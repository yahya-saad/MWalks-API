﻿namespace MWalks.API.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public UnauthorizedException(string? message) : base(message)
        {
        }
    }
}

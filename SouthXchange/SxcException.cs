using System;

namespace SouthXchange
{
    public class SxcException : Exception
    {
        public SxcException(string message)
            : base(message)
        { 
        }
    }
}

using System;

namespace Bank.Service.Exeptions;

public class CustomExeption : Exception
{
    public int Code;

    public CustomExeption (int code, string message) : base(message)
    {
        Code = code;
    }
}

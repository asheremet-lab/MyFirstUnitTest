using System;
using MyFirstUnitTest.Interfaces;

namespace MyFirstUnitTest.Services;

public class EmailSender : IEmailSender
{
    public void Send(string to, string text)
    {
        Console.WriteLine($"Sending mail to {to}: {text}");
    }
}
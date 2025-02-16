﻿using System;
using System.Linq;

namespace ETLLibrary.Authentication
{
    public static class TokenGenerator
    {
        private static readonly Random Random = new Random();
        public static string Generate(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[Random.Next(s.Length)]).ToArray());
        }
    }
}
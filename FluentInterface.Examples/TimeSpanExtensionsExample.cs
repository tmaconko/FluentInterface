using System;

namespace FluentInterface.Examples
{
    // ReSharper disable SuggestVarOrType_SimpleTypes
    // ReSharper disable MemberCanBePrivate.Global
    // ReSharper disable ReturnValueOfPureMethodIsNotUsed
    // ReSharper disable UnusedMember.Global

    public static class TimeSpanExtensions
    {    
        public static TimeSpan Minutes(this int minutes)
        {
            return TimeSpan.FromMinutes(minutes);
        }

        public static void Usage()
        {
            TimeSpan someMinutes = 5.Minutes();

            someMinutes.Add(5.Minutes());
        }
    }
}

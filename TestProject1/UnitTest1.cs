using System;
using HttpModels;
using Xunit;

namespace TestProject1;

public class UnitTest1
{
    [Fact]
    public void Test_TimeTest_CorrectValue()
    {
        var time = new Clock();
        
        time.TimeZone = TimeZoneInfo.Local;
        var result = time.LocalTimeNow;
        var expect = DateTime.Now;
        Assert.True(result.Hour == expect.Hour && result.Date == expect.Date);
    }
}
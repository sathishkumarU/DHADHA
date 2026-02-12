using System;
namespace FirstControllerProject.Services.CommonMethods
{
    public static class ExtnsMethods
    {
        public static int StringCnt(this string str)
        {
            if (string.IsNullOrWhiteSpace(str))
                return 0;
            return str.Split(' ', StringSplitOptions.RemoveEmptyEntries).Length;
        }
    }
}

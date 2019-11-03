using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace JdCat.Basketball.Common
{
    public static class UtilHelper
    {
        /// <summary>
        /// 生成随机数字
        /// </summary>
        /// <param name="len"></param>
        /// <returns></returns>
        public static string RandNum(int len = 6)
        {
            var arrChar = new char[] { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            var num = new StringBuilder();
            var rnd = new Random(DateTime.Now.Millisecond);

            for (int i = 0; i < len; i++)
            {
                num.Append(arrChar[rnd.Next(0, 9)].ToString());
            }
            return num.ToString();
        }

    }
}

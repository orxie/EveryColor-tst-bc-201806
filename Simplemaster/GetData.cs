using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Model;
using DAL;

namespace Simplemaster
{
    public class GetData
    {
        public static void Updata() 
        {
            string url = "https://www.km28.com/lottery-gp/cqssc.html";
            string HtmlCode = GetHtmlSource(url);
            ////使用正则表达式清洗网页源代码中的数据
            String withoutNString = HtmlCode.Replace("\n", "");
            //过滤\r 转换成空
            String withoutRString = withoutNString.Replace("\r", "");
            //过滤\t 转换成空
            String withoutTString = withoutRString.Replace("\t", "");
            //过滤\ 转换成空
            String newString = withoutTString.Replace("\\", "");
            //获取html中的body标签
            String result = Regex.Match(newString, @"<body.*>.*</body>").ToString();
            //过滤注释
            String result2 = Regex.Replace(result, @"<!--(?s).*?-->", "", RegexOptions.IgnoreCase);
            //过滤nbsp标签
            String result3 = Regex.Replace(result2, @"&nbsp;", "", RegexOptions.IgnoreCase);
            String result4 = result3.Replace(" ", "");
            //获取body中的所有table
            Regex regex = new Regex(@"<table.*?>[\s\S]*?<\/table>");

            MatchCollection mc = regex.Matches(result4);
            //获取集合类中自己需要的某个table
            String newHtmlStr = mc[0].ToString() + mc[1].ToString() + mc[2].ToString();
            DataTable dataTable = new DataTable();
            //String strings = "a,b,c";
            //String[] stringArr = strings.Split(",");
            DateTime Dt = DateTime.Now;

            string dt = Dt.ToString("yyyyMMdd");
            string dt1 = Dt.ToString("yyyy-MM-dd");
            string[] sArray = Regex.Split(newHtmlStr, "<tr>", RegexOptions.IgnoreCase);

            LotteryTcketInfo lotteryTcketInfo = new LotteryTcketInfo();

          
            for (int i = 2; i < sArray.Length; i++)
             {

                string ds = Regex.Replace(sArray[i], @"<[^>]*", "");//用Regex的replace 先将<str </str 给过滤掉
                ds = ds.Replace('>', ',');
                string[] stringArr = ds.Split(',');

                if (Regex.IsMatch(stringArr[5], @"^[0-9]{0,}$") == false)
                {   // 判断用户名的长度（4-20个字符）及内容（只能是数字）是否合法
                    continue;
                }
                lotteryTcketInfo.LotteryTcket = dt + (stringArr[1].Length == 3 ? stringArr[1] : "0" + stringArr[1]);

                long numtimer = Convert.ToInt64(DAL.InfoServer.SelectNewLotteryTcket());
                //判重
                if (numtimer > Convert.ToInt64(lotteryTcketInfo.LotteryTcket))
                {
                    continue;
                }
                else if (numtimer == Convert.ToInt64(lotteryTcketInfo.LotteryTcket))
                {
                    //修改最新一期的开奖号码，时间

                    lotteryTcketInfo.LotteryTcketNmber = stringArr[5];
                    lotteryTcketInfo.Timer = dt1 + " " + stringArr[3] + ":00";
                    lotteryTcketInfo.Explain = "";
                    lotteryTcketInfo.LotteryTcketID = 0;
                    DAL.InfoServer.UpNewNum(lotteryTcketInfo);
                    //添加一期
                    lotteryTcketInfo.LotteryTcket = (numtimer + 1).ToString();
                    lotteryTcketInfo.BigRate = "1.8";
                    lotteryTcketInfo.DoubleRate = "1.8";
                    lotteryTcketInfo.Explain = "";
                    lotteryTcketInfo.LotteryTcketID = 0;
                    lotteryTcketInfo.SingleRate = "1.8";
                    lotteryTcketInfo.SmallRate = "1.8";
                    DAL.InfoServer.ADDSate(lotteryTcketInfo);

                }
                else
                {
                    if (i < sArray.Length)
                    {
                        lotteryTcketInfo.LotteryTcketNmber = stringArr[5];
                        lotteryTcketInfo.Timer = dt1 + " " + stringArr[3] + ":00";
                        lotteryTcketInfo.BigRate = "1.8";
                        lotteryTcketInfo.DoubleRate = "1.8";
                        lotteryTcketInfo.Explain = "";
                        lotteryTcketInfo.LotteryTcketID = 0;
                        lotteryTcketInfo.SingleRate = "1.8";
                        lotteryTcketInfo.SmallRate = "1.8";
                        DAL.InfoServer.ADDSate(lotteryTcketInfo);
                    }
                    //添加最新一期的倍率
                    else if (i == sArray.Length)
                    {
                        lotteryTcketInfo.LotteryTcket = (numtimer + 1).ToString();
                        lotteryTcketInfo.BigRate = "1.8";
                        lotteryTcketInfo.DoubleRate = "1.8";
                        lotteryTcketInfo.Explain = "";
                        lotteryTcketInfo.LotteryTcketID = 0;
                        lotteryTcketInfo.SingleRate = "1.8";
                        lotteryTcketInfo.SmallRate = "1.8";
                        DAL.InfoServer.ADDSate(lotteryTcketInfo);
                    }
                }
            }
        }

        public static string GetHtmlSource(string url)
        {
            //处理内容  
            string html = "";
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Accept = "*/*"; //接受任意文件
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.1.4322)"; // 模拟使用IE在浏览 http://www.52mvc.com
            request.AllowAutoRedirect = true;//是否允许302
            //request.CookieContainer = new CookieContainer();//cookie容器，
            request.Referer = url; //当前页面的引用


            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream, Encoding.Default);
            html = reader.ReadToEnd();
            stream.Close();
            return html;
        }
    }

}

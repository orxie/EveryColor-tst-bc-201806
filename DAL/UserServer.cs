using Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;



namespace DAL
{
    public class UserServer
    {
        static string connection = ConfigurationManager.ConnectionStrings["LotteryTicketDATA"].ConnectionString;
        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int Login(UserInfo userInfo)
        {

            string sqltext = "select count(*) from UserInfo where UserName=@UserName and UserPass=@UserPass";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName),
                    new SqlParameter("@UserPass", userInfo.UserPass)

                };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 判断是否管理员
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int AdminLogin(UserInfo userInfo)
        {

            string sqltext = "select UserAuthority  from UserInfo where UserName=@UserName";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName)

                };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查重
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int AddUserName(UserInfo userInfo)
        {

            string sqltext = "select count(*) from UserInfo where UserName=@UserName";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName)

                };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int AdminAddUser(UserInfo userInfo)
        {

            string sqltext = "insert into UserInfo([UserName],[UserPass],[UserInvite],[UserAuthority],[UserIntegration])values(@UserName,@UserPass,@UserInvite,@UserAuthority,@UserIntegration)";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName),
                    new SqlParameter("@UserPass", userInfo.UserPass),
                    new SqlParameter("@UserInvite", userInfo.UserInvite),
                    new SqlParameter("@UserAuthority", userInfo.UserAuthority),
                    new SqlParameter("@UserIntegration", userInfo.UserIntegration)
                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查询ID
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int AddselectID(UserInfo userInfo)
        {

            string sqltext = "select UserID from UserInfo where UserName=@UserName";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName)

                };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 添加自己的邀请码
        /// </summary>
        /// <param name="invitelInfo"></param>
        /// <returns></returns>
        public static int AddinvitelID(InvitelInfo invitelInfo)
        {

            string sqltext = "insert into InviteInfo(UserID,Invite) values(@UserID,@Invite)";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserID", invitelInfo.UserID),
                    new SqlParameter("@Invite", invitelInfo.Invite)

                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查询单行
        /// </summary>
        /// <param name="studentInfo"></param>
        /// <returns></returns>
        public static DataSet SelectoneUser(UserInfo Userinfo)
        {

            string sqltext = "select UserID as ID,UserName as 用户名,UserPass as 密码,UserInvite as 邀请码,case when [UserAuthority] =0 then '管理员' when [UserAuthority] =1 then '二级代理' when [UserAuthority] =2 then '普通用户' END as 身份,UserIntegration as 积分 from UserInfo where UserName=@UserName";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", Userinfo.UserName)

                };
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 修改积分
        /// </summary>
        /// <param name="invitelInfo"></param>
        /// <returns></returns>
        public static int UPdateUserIntegration(UserInfo Userinfo)
        {

            string sqltext = "update [dbo].[UserInfo] set [UserIntegration]=@UserIntegration where UserID=@UserID";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserID", Userinfo.UserID),
                    new SqlParameter("@UserIntegration", Userinfo.UserIntegration)

                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 修改密码
        /// </summary>
        /// <param name="invitelInfo"></param>
        /// <returns></returns>
        public static int UPDataPass(UserInfo Userinfo)
        {

            string sqltext = "update [dbo].[UserInfo] set [UserPass]=@UserPass where UserName=@UserName";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserPass", Userinfo.UserPass),
                    new SqlParameter("@UserName", Userinfo.UserName)
                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查询开奖状态
        /// </summary>
        /// <param name="invitelInfo"></param>
        /// <returns></returns>
        public static int SelectSate()
        {

            string sqltext = "select [SateState] from [dbo].[SateInfo] ORDER BY [SateID] DESC";
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext);
        }
        /// <summary>
        /// 修改开奖状态
        /// </summary>
        /// <param name="invitelInfo"></param>
        /// <returns></returns>
        public static int UPdateSate(SateInfo sateInfo)
        {

            string sqltext = "insert into SateInfo(SateDate,SateBgin,SateEnd,SateState,SateRmarks) values(@SateDate,@SateBgin,@SateEnd,@SateState,@SateRmarks)";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@SateDate", sateInfo.SateDate),
                    new SqlParameter("@SateBgin", sateInfo.SateBgin),
                    new SqlParameter("@SateEnd", sateInfo.SateEnd),
                    new SqlParameter("@SateState", sateInfo.SateState),
                    new SqlParameter("@SateRmarks", sateInfo.SateRmarks)
                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 开奖
        /// </summary>
        /// <param name="invitelInfo"></param>
        /// <returns></returns>
        public static int ADDSate(LotteryTcketInfo lotteryTcketInfo)
        {

            string sqltext = "insert into LotteryTcketInfo ([LotteryTcket],[LotteryTcketNmber],[BigRate],[SmallRate],[SingleRate],[DoubleRate],[Timer]) values(@LotteryTcket,@LotteryTcketNmber,@BigRate,@SmallRate,@SingleRate,@DoubleRate,@Timer)";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@LotteryTcket", lotteryTcketInfo.LotteryTcket),
                    new SqlParameter("@LotteryTcketNmber", lotteryTcketInfo.LotteryTcketNmber),
                    new SqlParameter("@BigRate", lotteryTcketInfo.BigRate),
                    new SqlParameter("@SmallRate", lotteryTcketInfo.SmallRate),
                    new SqlParameter("@SingleRate", lotteryTcketInfo.SingleRate),
                    new SqlParameter("@DoubleRate", lotteryTcketInfo.DoubleRate),
                    new SqlParameter("@Timer", lotteryTcketInfo.Timer)

                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }

        ///////////////////////////////////////////////////////////////

        /// <summary>
        /// 查询账号积分
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string SelectUserInt(UserInfo userInfo)
        {
            string sqltext = "select UserIntegration  from UserInfo	 where UserName=@UserName";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName),
                };
            return SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext, para).ToString();
        }

        /// <summary>
        /// 查询最新的开奖期号
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static string SelectNewLotteryTcket()
        {
            string sqltext = "select top 1  [LotteryTcket]   from	[dbo].[LotteryTcketInfo] order by [LotteryTcketID] desc;";

            return (string)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext);
        }
        /// <summary>
        /// 修改最新一期的时间和开奖号码
        /// </summary>
        /// <param name="lotteryTcketInfo"></param>
        /// <returns></returns>
        public static int UpNewNum(LotteryTcketInfo lotteryTcketInfo)
        {
            string sqltext = @"begin tran tran_ADDSate declare @tran_error int;set @tran_error=0;declare @NewNumber nvarchar(50);declare @NewNumberID INT;declare @NewLastNumer int;declare @BigRate FLOAT;declare @SmallRate FLOAT;declare @SingleRate FLOAT;declare @DoubleRate FLOAT;declare @UserIntegration  INT;declare @NewUserIntegration int;declare @minID int;declare @MaxID int ;set  @minID=0;set @MaxID=0;declare @NtesID int;declare @LotteryTcketID int;declare @UserID int;begin try UPDATE [dbo].[LotteryTcketInfo] set [LotteryTcketNmber] =32568 ,[Timer]='2018-09-11 22:22' WHERE [LotteryTcket] = 20180909071 select top 1 @NewNumber=LotteryTcket  from	[dbo].[LotteryTcketInfo] order by [LotteryTcketID] desc if(@NewNumber=20180909071) begin select	@NewNumberID=[LotteryTcketID] from  [dbo].[LotteryTcketInfo] where  LotteryTcket= @NewNumber  select top(1) @minID=[BuyID] from  BuyInfo WHERE  [LotteryTcketID]=@NewNumberID	order  by [BuyID]  select top(1) @MaxID=[BuyID] from  BuyInfo WHERE  [LotteryTcketID]=@NewNumberID	order  by [BuyID] desc while @minID<= @MaxID begin select @NewLastNumer =RIGHT(LotteryTcketNmber, 1),@BigRate=BigRate,@SmallRate=SmallRate,@SingleRate=SingleRate,@DoubleRate=DoubleRate from LotteryTcketInfo	where LotteryTcket=@NewNumber; select @UserID=UserID,@LotteryTcketID=NtesItegration,@NtesID=NtesID  from  BuyInfo WHERE  BuyID=@minID if @NtesID=1 begin if @NewLastNumer>=5 begin update [dbo].[UserInfo] set UserIntegration=UserIntegration+@BigRate*@LotteryTcketID where UserID=@UserID end else begin update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID end end else if	@NtesID=2 begin if @NewLastNumer>=5 begin update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID end else begin  update [dbo].[UserInfo] set UserIntegration=UserIntegration+@SmallRate*@LotteryTcketID where UserID=@UserID end end else if	@NtesID=3 begin if (@NewLastNumer+ 4) % 2 = 1 begin update [dbo].[UserInfo] set UserIntegration=UserIntegration+@SingleRate*@LotteryTcketID where UserID=@UserID end else begin update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID end end else begin if (@NewLastNumer+ 4) % 2 = 1 begin update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID end else begin update [dbo].[UserInfo] set UserIntegration=UserIntegration+@DoubleRate*@LotteryTcketID where UserID=@UserID end end set	@minID=@minID+1;end end end try begin catch set @tran_error=@tran_error+1;end catch if(@tran_error>0) begin rollback tran tran_ADDSate;print @tran_error;end else begin commit tran tran_ADDSate;print @tran_error;end";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@LotteryTcketNmber", lotteryTcketInfo.LotteryTcketNmber),
                    new SqlParameter("@Timer", lotteryTcketInfo.Timer),
                    new SqlParameter("@LotteryTcket", lotteryTcketInfo.LotteryTcket),

                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查询当天的开奖信息
        /// </summary>
        /// <param name="lotteryTcketInfo"></param>
        /// <returns></returns>
        public static DataSet SelectNewS()
        {
            string sqltext = "  select [LotteryTcket],[LotteryTcketNmber],[Timer] from [dbo].[LotteryTcketInfo]  where DATEDIFF(D,[Timer],GETDATE())=0	ORDER BY[LotteryTcket] DESC";

            return SqlHelper.ExecuteDataset(connection, CommandType.Text, sqltext);
        }
        /// <summary>
        /// 查询是否可以投注
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int SelectSateState()
        {
            string sqltext = "select top 1 [SateState] from [dbo].[SateInfo]  ORDER BY[SateID] DESC";

            return (int)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext);
        }
        /// <summary>
        /// 购买
        /// </summary>
        /// <param name="invitelInfo"></param>
        /// <returns></returns>
        public static int ADDbuy(BuyInfo buyInfo)
        {

            string sqltext = "insert into BuyInfo ([UserID],[LotteryTcketID],[NtesItegration],[NtesID],[NtesGade],[NtesTimer]) values(@UserID,@LotteryTcketID,@NtesItegration,@NtesID,@NtesGade,@NtesTimer)";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserID", buyInfo.UserID),
                    new SqlParameter("@LotteryTcketID", buyInfo.LotteryTcketID),
                    new SqlParameter("@NtesItegration", buyInfo.NtesItegration),
                    new SqlParameter("@NtesID", buyInfo.NtesID),
                    new SqlParameter("@NtesGade", buyInfo.NtesGade),
                    new SqlParameter("@NtesTimer", buyInfo.NtesTimer)

                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 减少账号积分
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int jianUserInt(UserInfo userInfo)
        {
            string sqltext = "UPdate [dbo].[UserInfo]set [UserIntegration] = @UserIntegration  where [UserName]=@UserName";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName),
                    new SqlParameter("@UserIntegration", userInfo.UserIntegration)
                };
            return (int)SqlHelper.ExecuteNonQuery(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// ID
        /// </summary>
        /// <param name="userInfo"></param>
        /// <returns></returns>
        public static int selectID(UserInfo userInfo)
        {

            string sqltext = "select UserID from UserInfo where UserName=@UserName and UserPass=@UserPass";

            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName),
                    new SqlParameter("@UserPass", userInfo.UserPass)

                };
            return (int)SqlHelper.ExecuteScalar(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查询历史记录
        /// </summary>
        /// <param name="lotteryTcketInfo"></param>
        /// <returns></returns>
        public static DataSet Selectlishi(UserInfo userInfo)
        {
            string sqltext = "select c.UserName as 名字,a.NtesTimer as 时间,a.LotteryTcketID as 期号,a.NtesItegration as 倍率,d.NtesID as 注类 from [dbo].[BuyInfo]as a,[dbo].[LotteryTcketInfo]as b,[dbo].[UserInfo]as c,[dbo].[NtesGalInfo] as d where a.LotteryTcketID=b.LotteryTcket and a.UserID=c.UserID and a.UserID=@UserID and d.NtesGalID=a.NtesID  order by a.BuyID desc";
            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserID", userInfo.UserID)
                };
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查询名下用户
        /// </summary>
        /// <param name="lotteryTcketInfo"></param>
        /// <returns></returns>
        public static DataSet SelectMYUser(UserInfo userInfo)
        {
            string sqltext = " select [UserName] as 名字,[UserAuthority] as 用户权限 from [dbo].[UserInfo] where [UserInvite]=( select b.[Invite] from [dbo].[UserInfo]as a,[dbo].[InviteInfo]as b where  a.UserID=b.UserID and a.UserName=@UserName)";
            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@UserName", userInfo.UserName)
                };
            return SqlHelper.ExecuteDataset(connection, CommandType.Text, sqltext, para);
        }
        /// <summary>
        /// 查询名下用户管理员
        /// </summary>
        /// <param name="lotteryTcketInfo"></param>
        /// <returns></returns>
        public static DataSet AdminSelectMYUser()
        {
            string sqltext = " select [UserName] as 名字,[UserAuthority] as 用户权限 from [dbo].[UserInfo] where [UserInvite]=''";

            return SqlHelper.ExecuteDataset(connection, CommandType.Text, sqltext);
        }


    }
}

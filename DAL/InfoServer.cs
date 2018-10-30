using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Model;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;


namespace DAL
{
    public class InfoServer
    {

        static string connection = ConfigurationManager.ConnectionStrings["LotteryTicketDATA"].ConnectionString;
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
            string sqltext = @"begin tran tran_ADDSate
	declare @tran_error int;
	set @tran_error=0;

	declare @NewNumber nvarchar(50);--最新一期的期号
	declare @NewNumberID INT;--最新一期期号的ID
	declare @NewLastNumer int;--开奖的号码最后一位
	declare @BigRate FLOAT;--大倍率
	declare @SmallRate FLOAT;--小倍率
	declare @SingleRate FLOAT;--单倍率
	declare @DoubleRate FLOAT;--双倍率

	declare @UserIntegration  INT; --用户积分
	declare @NewUserIntegration int; --操作中间变量
	declare @minID int;
	declare @MaxID int ;
	set  @minID=0;
	set @MaxID=0;

	declare @NtesID int;	  --状态(大0，小1，单2，双3)
	declare @LotteryTcketID int; --投注积分
	declare @UserID int;  --用户ID

begin try
	UPDATE [dbo].[LotteryTcketInfo] set [LotteryTcketNmber] =@LotteryTcketNmber ,[Timer]=@Timer WHERE [LotteryTcket] = @LotteryTcket
	select top 1 @NewNumber=LotteryTcket  from	[dbo].[LotteryTcketInfo] order by [LotteryTcketID] desc 

	if(@NewNumber=@LotteryTcket)
	begin
		 select	@NewNumberID=[LotteryTcketID] from  [dbo].[LotteryTcketInfo] where  LotteryTcket= @NewNumber
		 select top(1) @minID=[BuyID] from  BuyInfo WHERE  [LotteryTcketID]=@NewNumberID	order  by [BuyID]
		 select top(1) @MaxID=[BuyID] from  BuyInfo WHERE  [LotteryTcketID]=@NewNumberID	order  by [BuyID] desc
		 while @minID<= @MaxID
			begin
			   select @NewLastNumer =RIGHT(LotteryTcketNmber, 1),@BigRate=BigRate,@SmallRate=SmallRate,@SingleRate=SingleRate,@DoubleRate=DoubleRate from LotteryTcketInfo	where LotteryTcket=@NewNumber;
			   select @UserID=UserID,@LotteryTcketID=NtesItegration,@NtesID=NtesID  from  BuyInfo WHERE  BuyID=@minID
			   if @NtesID=1
					begin
						if @NewLastNumer>=5
							begin
								update [dbo].[UserInfo] set UserIntegration=UserIntegration+@BigRate*@LotteryTcketID where UserID=@UserID
							end
						else
							begin
								update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID
							end
					end
				else if	@NtesID=2
					begin
						if @NewLastNumer>=5
								begin
									update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID
								end
						else
								begin
								    update [dbo].[UserInfo] set UserIntegration=UserIntegration+@SmallRate*@LotteryTcketID where UserID=@UserID
								end
					end
				else if	@NtesID=3
					begin
						if (@NewLastNumer+ 4) % 2 = 1
								begin
									update [dbo].[UserInfo] set UserIntegration=UserIntegration+@SingleRate*@LotteryTcketID where UserID=@UserID
								end
						else
								begin
								    update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID
								end
					end
				else
					begin
						 if (@NewLastNumer+ 4) % 2 = 1
								begin
									update [dbo].[UserInfo] set UserIntegration=UserIntegration-@LotteryTcketID where UserID=@UserID
								end
						else
								begin
								    update [dbo].[UserInfo] set UserIntegration=UserIntegration+@DoubleRate*@LotteryTcketID where UserID=@UserID
								end
					end
				set	@minID=@minID+1;
		end
	end
end try
begin catch
		 set @tran_error=@tran_error+1	;
end catch
if(@tran_error>0)
	begin
		rollback tran tran_ADDSate; --执行出错，回滚事务(指定事务名称)
		print @tran_error;
	end 
else										
	begin
	  commit tran tran_ADDSate; --没有异常，提交事务(指定事务名称)
	  print @tran_error;
	end

			  										 
";
            SqlParameter[] para = new SqlParameter[]
                {
                    new SqlParameter("@LotteryTcketNmber", lotteryTcketInfo.LotteryTcketNmber),
                    new SqlParameter("@Timer", lotteryTcketInfo.Timer),
                    new SqlParameter("@LotteryTcket", lotteryTcketInfo.LotteryTcket),

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
    }
}

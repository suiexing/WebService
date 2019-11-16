using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Services;
using WBClient2;

namespace WBService2
{
    /// <summary>
    /// WebService2 的摘要说明
    /// </summary>
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    // 若要允许使用 ASP.NET AJAX 从脚本中调用此 Web 服务，请取消注释以下行。 
    // [System.Web.Script.Services.ScriptService]
    public class WebService2 : System.Web.Services.WebService
    {

        public APISoapHeader header { get; set; }



        [System.Web.Services.Protocols.SoapHeader("header")]

        [WebMethod]

        public string HelloWorld(string xml, out string synState, out string synRemark, string opearType, string TABLE)

        {
            DataSet ds = null;

            synState = "0";
            synRemark = "";

            if (header != null && TokenHelper.TokenVerify(header.signature, header.timestamp, header.nonce))
            {
                string sql = @"DECLARE	@return_value int,
                    @synState CHAR(1),
                    @synRemark NVARCHAR(20)
                    EXEC  @return_value=dbo.WebServiceBasicDataDownloadInterface 
                                               @synState =@synState OUTPUT, 
                                               @synRemark = @synRemark OUTPUT,
                                               @opearType = '" + opearType + @"',                
                                               @TABLE = '" + TABLE + @"',                   
                                               @XML = '" + xml
                                               + "'select @synState synState,@synRemark synRemark";
                try
                {
                    ds = GetDataSetWithSQLString(sql);
                    synRemark = "OK";
                    synState = "1";
                }
                catch (Exception ex)
                {
                    synState = "0";
                    synRemark = "NG：验证通过，XML内容解析失败！";
                }
            }

            else
            {
                DateTime timestamp2 = Convert.ToDateTime(header.timestamp.Replace('T', ' '));
                TimeSpan ts = DateTime.Now.Subtract(timestamp2).Duration();

                synState = "0"; 
                synRemark= "NG：验证不通过！"+ TokenHelper.GetSignature(timestamp2, header.nonce);

            }

            if (ds != null)
            {
                if (ds.Tables.Count >= 1)
                {
                    return "OK";
                }
                else
                {
                    return "NG";
                }
            }
            return "NG";
        }
    


        /// <summary>
        /// 默认的连接字符串，用于获取指定的连接字符串
        /// </summary>
        /// <param name="SQLString"></param>
        /// <returns></returns>
        public DataSet GetDataSetWithSQLString(string SQLString)
        {
                if (true)
                {
                    string ConnectionString = @"Data Source = 192.168.28.162; Initial Catalog = OrBitMOMBY; password =BaiyaMES@2019; Persist Security Info = True; User ID = sa";
                    using (SqlConnection conn = new SqlConnection(ConnectionString))
                    {
                        conn.Open();
                        SqlCommand comm = new SqlCommand();
                        comm.Connection = conn;
                        comm.CommandText = SQLString;

                        comm.CommandType = CommandType.Text;
                        comm.CommandTimeout = 600;

                        DataSet ds = new DataSet("SQLDataSet");
                        SqlDataAdapter adapter = new SqlDataAdapter();
                        adapter.SelectCommand = comm;
                        adapter.Fill(ds, "SQLDataSet");

                        conn.Close();
                        return ds;
                    }
                }
                else
                {
                  
                    return null;
                }
            }
        
    }
    /// <summary>

    /// WebService接口 SoapHeader类

    /// </summary>

    public class APISoapHeader : System.Web.Services.Protocols.SoapHeader

    {

        /// <summary>

        ///  加密签名

        /// </summary>

        public string signature { get; set; }

        /// <summary>

        /// 时间戳

        /// </summary>

        public string timestamp { get; set; }

        /// <summary>

        /// 随机数

        /// </summary>

        public string nonce { get; set; }

    }


}

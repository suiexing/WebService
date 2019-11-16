using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace WBClient2
{
    class Program
    {
        static void Main(string[] args)
        {


            //string msg = Console.ReadLine();

            //ServiceReference1.WebService2SoapClient client = new ServiceReference1.WebService2SoapClient();



           // ServiceReference1.APISoapHeader header = new ServiceReference1.APISoapHeader();

           // Random random = new Random();

           // header.timestamp = DateTime.Now;

           // header.nonce = random.Next(0, 100).ToString();

           // header.signature = TokenHelper.GetSignature(header.timestamp, header.nonce);

           // //Thread.Sleep(7000);//如果大于7秒则失败；
           // string xml= "<row><PersonInfoId>PEIN0000001X</PersonInfoId><PersonNumber>Y0022</PersonNumber><PersonName>何光威</PersonName><PersonDescription></PersonDescription><PersonSex>M</PersonSex><IsActivity>1</IsActivity><Position></Position><Phone>11111111111</Phone><MailAddress></MailAddress><DivisionId>DIV10000000K</DivisionId><WorkcenterId>&#x20;</WorkcenterId><ModifyDate>2018-02-27T13:38:33.390</ModifyDate><CreateDate>2017-12-05T10:59:38.710</CreateDate></row><row><PersonInfoId>PEIN0000001Y</PersonInfoId><PersonNumber>Y00332</PersonNumber><PersonName>舒晓君</PersonName><PersonDescription></PersonDescription><PersonSex>M</PersonSex><IsActivity>1</IsActivity><Position></Position><Phone>11111111111</Phone><MailAddress></MailAddress><DivisionId>DIV10000000K</DivisionId><WorkcenterId>&#x20;</WorkcenterId><ModifyDate>2018-02-27T13:38:36.970</ModifyDate><CreateDate>2017-12-05T12:01:31.433</CreateDate></row><row><PersonInfoId>PEIN0000001Z</PersonInfoId><PersonNumber>Y003</PersonNumber><PersonName>吴桂林</PersonName><PersonDescription></PersonDescription><PersonSex>M</PersonSex><IsActivity>1</IsActivity><Position></Position><Phone>11111111111</Phone><MailAddress></MailAddress><DivisionId>DIV10000000K</DivisionId><WorkcenterId>&#x20;</WorkcenterId><ModifyDate>2018-02-27T13:38:43.423</ModifyDate><CreateDate>2017-12-05T12:02:20.583</CreateDate></row><row><PersonInfoId>PEIN00000020</PersonInfoId><PersonNumber>Y004</PersonNumber><PersonName>谢林丽</PersonName><PersonDescription></PersonDescription><PersonSex>M</PersonSex><IsActivity>1</IsActivity><Position></Position><Phone>11111111111</Phone><MailAddress></MailAddress><DivisionId>DIV10000000K</DivisionId><WorkcenterId>&#x20;</WorkcenterId><ModifyDate>2018-02-27T13:38:47.450</ModifyDate><CreateDate>2017-12-05T12:03:27.217</CreateDate></row>";
           // string opearType="A"; string TABLE= "PersonInfo";
           //string synState = "";
           //string synRemark = "";
           // DateTime dt = Convert.ToDateTime("2019/9/5 09:42:09");

           // string token = System.Configuration.ConfigurationManager.AppSettings["APIToken"];

            string formattimestamp = string.Format("{0:yyyy/MM/dd HH:mm:ss.fff}", DateTime.Now);
            string str = string.Format("{0}{1}{2}", "", formattimestamp, "32");

            //bool a = TokenHelper.TokenVerify("9671BF0CAB38949FEE00B3A9AC6B0A7D", dt, "32");
            //msg = client.HelloWorld(header, xml, opearType, TABLE, out synState, out synRemark);







           TEST();

            Console.ReadKey();
        }

        public static void TEST()
        {
            // String opearType = "opearType";
            string opearType = "D";
            String xml = "<row><WorkcenterId>WKC1000000BY</WorkcenterId><WorkcenterName>SMT03</WorkcenterName><WorkcenterDescription>SMT 1 线</WorkcenterDescription><SiteId>SIT100000005</SiteId><Worktime>0.00</Worktime><Overtime>0.00</Overtime><IsClosed>0</IsClosed><ProductStoreTypeId>1           </ProductStoreTypeId><WorkcenterTypeId>URC100000A6C</WorkcenterTypeId><ModifyDate>2018-08-17T11:38:45.443</ModifyDate><CreateDate>2017-10-26T21:03:29.703</CreateDate><WorkcenterGroup>SMT</WorkcenterGroup></row><row><WorkcenterId>WKC1000000BZ</WorkcenterId><WorkcenterName>SMT04</WorkcenterName><WorkcenterDescription>SMT 2 线</WorkcenterDescription><SiteId>SIT100000005</SiteId><Worktime>0.00</Worktime><Overtime>0.00</Overtime><IsClosed>0</IsClosed><ProductStoreTypeId>1           </ProductStoreTypeId><WorkcenterTypeId>URC100000A6C</WorkcenterTypeId><ModifyDate>2018-08-17T11:38:50.333</ModifyDate><CreateDate>2017-10-26T21:46:19.340</CreateDate><WorkcenterGroup>SMT</WorkcenterGroup></row>";
            string TABLE = "WorkcenterLine";
            String SQL = @"DECLARE	@return_value int,
                    @synState CHAR(1),
                    @synRemark NVARCHAR(20)
                    EXEC  @return_value=dbo.WebServiceBasicDataDownloadInterface 
                                               @synState =@synState OUTPUT, 
                                               @synRemark = @synRemark OUTPUT,
                                               @opearType = '" + opearType + @"',                
                                               @TABLE = '" + TABLE + @"',                   
                                               @XML = '" + 
                                               xml.Replace("&", "&amp;")
                                               .Replace("<", "&lt;")
                                               .Replace("'", "&apos;")
                                               .Replace("\"", "&quot;")
                                               .Replace(">", "&gt;")+
                                                   "'select @synState synState,@synRemark synRemark";
        }
    }
}

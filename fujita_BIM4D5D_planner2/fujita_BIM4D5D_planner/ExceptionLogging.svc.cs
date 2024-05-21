using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.IO;
using context = System.Web.HttpContext;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service17" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service17.svc or Service17.svc.cs at the Solution Explorer and start debugging.
    public  class Service17 : ExceptionLogging
    {
         private  String ErrorlineNo, Errormsg, extype, exurl, hostIp, ErrorLocation, HostAdd;

            public  void SendErrorToText(System.Exception ex)
            {
                var line = Environment.NewLine + Environment.NewLine;

                ErrorlineNo = ex.StackTrace.Substring(ex.StackTrace.Length - 7, 7);
                Errormsg = ex.GetType().Name.ToString();
                extype = ex.GetType().ToString();
                exurl = context.Current.Request.Url.ToString();
                ErrorLocation = ex.Message.ToString();

                try
                {
                // string filepath = context.Current.Server.MapPath("~/BIM4D5DExceptionDetailsFile/");  //Text File Path
                //string filepath = "C:\\inetpub\\wwwroot\\fujita_BIM4D5D_planner2\\fujita_BIM4D5D_planner2\\BIM4D5DExceptionDetailsFile";
                //string filepath = "C:/inetpub/Log/BIM4D5DExceptionDetailsFile/";
                    string CommonPublicDocumentFolder = Environment.GetFolderPath(Environment.SpecialFolder.CommonDocuments);
                    string CommonLocalFolder = CommonPublicDocumentFolder + "\\Fujita4D_5D_ProjectDetails_2021\\";
                    string filepath = CommonLocalFolder + "FujitaBIM4D5DWebService.log";
                if (!Directory.Exists(filepath))
                    {
                        Directory.CreateDirectory(filepath);

                    }
                    filepath = filepath + DateTime.Today.ToString("dd-MM-yy") + ".txt";   //Text File Name
                    if (!File.Exists(filepath))
                    {


                        File.Create(filepath).Dispose();

                    }
                    using (StreamWriter sw = File.AppendText(filepath))
                    {
                        string error = "Log Written Date:" + " " + DateTime.Now.ToString() + line + "Error Line No :" + " " + ErrorlineNo + line + "Error Message:" + " " + Errormsg + line + "Exception Type:" + " " + extype + line + "Error Location :" + " " + ErrorLocation + line + " Error Page Url:" + " " + exurl + line + "User Host IP:" + " " + hostIp + line;
                        sw.WriteLine("-----------Exception Details on " + " " + DateTime.Now.ToString() + "-----------------");
                        sw.WriteLine("-------------------------------------------------------------------------------------");
                        sw.WriteLine(line);
                        sw.WriteLine(error);
                        sw.WriteLine("--------------------------------*End*------------------------------------------");
                        sw.WriteLine(line);
                        sw.Flush();
                        sw.Close();

                    }

                }
                catch (System.Exception e)
                {
                    e.ToString();

                }
            }

        }
}

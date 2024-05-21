using ENJPDictionary.Connectivity.API.Framework;
using ENJPDictionary.Connectivity.API.Framework.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service25" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service25.svc or Service25.svc.cs at the Solution Explorer and start debugging.
    public class Service25 : DictionarySync
    {
        public void DictionarySync()
        {
            DictionaryContext dc = new DictionaryContext("http://10.1.33.101:8080/");
            string pwd = "sahana@2020";
            string uid = "sahana@2020";
            pwd = ENJPDictionary.Connectivity.API.Framework.CryptoHelper.SymEncryptText(pwd);
            string token = dc.LogIn(uid, pwd);
            string sText = "";

            string sSourceOperator = "";

            string sSource = "Sales Support iPad";

            string sClassOperator = "";

            string sClass = "";

            string sTypeOperator = "";

            string sType = "";

            string lng = "1";//English=1  Japanese=2

            dc.AuthToken = token;//token generated at time of login

            List<Dictionary> dictionary = dc.DictionaryManager.SearchDictionary(sText, sSourceOperator,

                                                    sSource, sClassOperator, sClass, sTypeOperator, sType, lng);

            List<Dictionary> dictionary121 = new List<Dictionary>();
            for (int i = 0; i < dictionary.Count; i++)
            {
                Dictionary Dictionary11 = new Dictionary();
                Dictionary11.DictionaryID = dictionary[i].DictionaryID;
                Dictionary11.TypeID = dictionary[i].TypeID;
                // Dictionary11.ClassName = dictionary[i].ClassName;
                Dictionary11.EnglishText = dictionary[i].EnglishText;
                Dictionary11.JapaneseText = dictionary[i].JapaneseText;
                Dictionary11.CreatedBy = dictionary[i].CreatedBy;
                Dictionary11.IsDeleted = dictionary[i].IsDeleted;

                dictionary121.Add(Dictionary11);
            }

        }
    }
}

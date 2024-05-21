using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace fujita_BIM4D5D_planner
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "Service5" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select Service5.svc or Service5.svc.cs at the Solution Explorer and start debugging.
    public class Service5 : ReadProjDetails
    {
        string connection_string = ConfigurationManager.ConnectionStrings["fujita_BIM4D5D_PlannerConnectionString"].ConnectionString.ToString();
        SqlConnection con;
        SqlCommand cmd;
        SqlDataAdapter sda;
        DataTable dt;
        proj_dtl proj = new proj_dtl();
        public proj_dtl showallprojdetail([Optional]string proj_id,[Optional] string proj_name,[Optional] int? f_show_all )
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {

                using (con = new SqlConnection(connection_string))
                {
                    if (f_show_all == 1)
                    {
                        cmd = new SqlCommand(@"select proj_guid,pj.name name,revit_project_file,ms_project_file,site_manager_name,site_manager_email,designer_name,designer_email,pj.created_on,country,cc.name city,currency,f_create_base,construction_type,construction_start_date,ms_proj_file_path,revit_version from project_details pd,project pj,country_code c,City cc where pd.project_id = pj.id and  c.id = cc.country_id and pj.city_id = cc.id and f_active = 1 and created_on >='2022-09-23';", con);

                    }
                    else
                    {
                        cmd = new SqlCommand(@"select proj_guid,pj.name name,revit_project_file,ms_project_file,site_manager_name,site_manager_email,designer_name,designer_email,revit_version,pj.created_on,country,cc.name city,currency,f_create_base,construction_type,construction_start_date,ms_proj_file_path from project_details pd,project pj,country_code c,City cc where project_id in (select id from project where proj_guid = '" + proj_id + "' and name = N'" + proj_name + "') and pd.project_id = pj.id and   and  c.id = cc.country_id and pj.city_id = cc.id and f_active = 1 and created_on >='2022-09-23';", con);
                    }
                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("projdtl");
                    sda.Fill(dt);
                    proj.proj_detail = dt;
                }

                return proj;

            }
            catch (System.Exception ex)
            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
                return null;

            }
        }

        public DataTable showallprojCountry(string country,string city)
        {
            SqlConnection conn = new SqlConnection(connection_string);
            ConnectionState state = conn.State;
            try
            {

                using (con = new SqlConnection(connection_string))
                {

                    cmd = new SqlCommand(@"select proj_guid,name,created_on from Project where city_id = (select id from City where name = N'" + city + "' and country_id =(select id from Country_Code where country = N'" + country + "')) and f_active = 1 and created_on >='2022-09-23';", con);

                    sda = new SqlDataAdapter(cmd);
                    dt = new DataTable("projdtl");
                    sda.Fill(dt);
                }

                return dt;

            }
            catch (System.Exception ex)
            {


                if (state == ConnectionState.Open)
                {
                    conn.Close();
                }
                return null;

            }
        }
    }
}
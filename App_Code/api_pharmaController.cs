using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using Bus_EReport;
using System.Security.Cryptography;
using Newtonsoft.Json;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Collections;
using System.Configuration;


public class api_pharmaController : ApiController
{
    DataTable dt = new DataTable();
    Hashtable ht = new Hashtable();
    Hashtable ht3 = new Hashtable();
    Hashtable ht4 = new Hashtable();
    [HttpPost]
    public Object SecondarySales_Analysis()
    {


        string tst = HttpContext.Current.Request.Form["data"];
        string tp_month = HttpContext.Current.Request.Params["tp_month"];
        string tp_year = HttpContext.Current.Request.Params["tp_year"];
        string cdate = HttpContext.Current.Request.Params["cdate"];
        string rSF = HttpContext.Current.Request.Params["rSF"];
        string axn = HttpContext.Current.Request.Params["axn"];
        string orderBy = HttpContext.Current.Request.Params["orderBy"];
        string sfCode = HttpContext.Current.Request.Params["sfCode"];
        string divisionCode = HttpContext.Current.Request.Params["divisionCode"];
        string SF = HttpContext.Current.Request.Params["SF"];
        string Ho_Id = HttpContext.Current.Request.Params["Ho_Id"];
        string dtdate = HttpContext.Current.Request.Params["date"];

        ApiDetails dd = new ApiDetails();


        // List<EmpDetails> empDetailsList = new List<EmpDetails>();
        dd = JsonConvert.DeserializeObject<ApiDetails>(tst);
        //dd.tableName = "vwDoctor_Master_APP";
        //dd.coloumns = "doctor_code as id,doctor_name as name,hospital_code,hospital_name,town_code,town_name,lat,long,addrs,doctor_category,Doc_Cat_Code,doctor_speciality,Doc_Class_ShortName as dr_class,Doc_ClsCode,isnull(ListedDr_DOB,'')DOB,isnull(ListedDr_DOW,'')DOW,Tlvst,isnull(Drvst_month,3)Drvst_month,Product_Code,Product_Brd_Code,Doc_Special_Code,idsl,Gcount,Geototal,addr,img_name,cus_phone";
        //dd.divisionCode = divisionCode;
        //dd.orderBy = orderBy;
        //dd.where = "";
        //dd.wt = "";

        string objField = string.Empty;
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);


        if (axn == "get/quali")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("iOS_getDocQual_NEW", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", dd.SF);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (axn == "get/class")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("iOS_getDocClass_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", dd.SF);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (axn == "getdivision_ho_sf")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getDivision_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@HOID", Ho_Id);
            cmd.Parameters.AddWithValue("@SFCode", sfCode);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "brand_master")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetBrand_App", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Divs", divisionCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "quiz")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("quiz", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "Map_Competitor_Product")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("MapCompetitor_Product", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Divs", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwCIP_APP")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT * from vwCIP_APP_new where sf_code='" + sfCode + "'", con);
            //cmd.CommandType = CommandType.StoredProcedure;


            //cmd.Parameters.AddWithValue("@Divs", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "Mas_CIP_Designation")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("Mas_CIP_Designation_new", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "Mas_CIP_Qualification")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select cast(id as varchar)id, name from Mas_CIP_Qualification where Division_code='"+ divisionCode.Remove(divisionCode.Length - 1, 1) + "' and ActiveFlg=0", con);
           // cmd.CommandType = CommandType.StoredProcedure;


          //  cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwCIPDepartment")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select cast(cip_department_code as varchar) id,cip_department_name name from Mas_CIP_Department where Division_code='" + divisionCode.Remove(divisionCode.Length - 1, 1) + "' and Active_Flag=0", con);
            // cmd.CommandType = CommandType.StoredProcedure;


            //  cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwCIPClass")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select cast(Doc_ClsCode as varchar) id,Doc_ClsSName name from Mas_Doc_Class where Division_code='"+ divisionCode.Remove(divisionCode.Length - 1, 1) + "' and Doc_Cls_ActiveFlag=0 and charindex(',c,', ',' + type + ',') > 0", con);
            // cmd.CommandType = CommandType.StoredProcedure;


            //  cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwHosp_Master_App")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("wHosp_Master_App", con);
             cmd.CommandType = CommandType.StoredProcedure;


              cmd.Parameters.AddWithValue("@sfcode", sfCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwHosp_Master_App_withFencing")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwHosp_Master_App_Withfencing", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sfcode", sfCode);
           



            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "mas_superstockist")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select * from vwSuper_stockist_App_New where division_code='" + divisionCode.Remove(divisionCode.Length - 1, 1) + "'", con);
          



            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "prod_feedbk")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select cast(FeedBack_Id as varchar) id,FeedBack_Name name from Mas_Product_Feedback where Division_code='"+ divisionCode.Remove(divisionCode.Length - 1, 1) + "' and Active_flag=0", con);
            //cmd.CommandType = CommandType.StoredProcedure;


            //cmd.Parameters.AddWithValue("@sfcode", sfCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (axn == "get/last_checkindetails")
        {




            con.Open();

            SqlCommand cmd = new SqlCommand("day_checkin", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@sf_code", sfCode);
            cmd.Parameters.AddWithValue("@dt", dtdate);
            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();

            da.Fill(ds);

            List<day_checkin> checkinarylst = new List<day_checkin>();
            foreach (DataRow row in ds.Rows)
            {
                day_checkin day_checkin = new day_checkin();
                day_checkin.id = row["id"].ToString();
                day_checkin.name = row["name"].ToString();
                day_checkin.status = row["status"].ToString();

                day_checkin.Start_Time = row["Start_Time"].ToString();

                string inputDateStr = row["Activity_date"].ToString();
                DateTime inputDate = DateTime.ParseExact(inputDateStr, "dd/MM/yyyy HH:mm:ss", null);
                string outputDateStr = inputDate.ToString("yyyy-MM-dd HH:mm:ss");
                DateFmt date1 = new DateFmt();
                date1.date = outputDateStr.ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";


                day_checkin.Activity_date = date1;

                checkinarylst.Add(day_checkin);


            }

            ht3.Add("Day_Checkin", checkinarylst);




            SqlCommand cmd1 = new SqlCommand("cus_checkin", con);
            cmd1.CommandType = CommandType.StoredProcedure;
            cmd1.Parameters.AddWithValue("@sf_code", sfCode);
            cmd1.Parameters.AddWithValue("@dt", dtdate);
            cmd1.CommandTimeout = 8000;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            DataTable ds1 = new DataTable();

            da1.Fill(ds1);

            List<cus_checkin> checkinarylst1 = new List<cus_checkin>();
            foreach (DataRow row in ds1.Rows)
            {
                cus_checkin cus_checkin = new cus_checkin();
                cus_checkin.id = row["id"].ToString();
                cus_checkin.name = row["name"].ToString();
                cus_checkin.Status = row["status"].ToString();

                cus_checkin.Type = row["Type"].ToString();
                string inputDateStr = row["Activity_date"].ToString();
                DateTime inputDate = DateTime.ParseExact(inputDateStr, "dd/MM/yyyy HH:mm:ss", null);
                string outputDateStr = inputDate.ToString("yyyy-MM-dd HH:mm:ss");
                DateFmt date1 = new DateFmt();
                date1.date = outputDateStr.ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";


                cus_checkin.Activity_date = date1;

                string inputDateStr1 = row["Checkin_time"].ToString().Replace("01/01/1900 00:00:00", "");
                string outputDateStr1 = "";
                DateFmt date2 = new DateFmt();
                if (inputDateStr1 != "")
                {
                    DateTime inputDate1 = DateTime.ParseExact(inputDateStr1, "dd/MM/yyyy HH:mm:ss", null);
                    outputDateStr1 = inputDate1.ToString("yyyy-MM-dd HH:mm:ss");
                    date2.date = outputDateStr1.ToString();
                    date2.timezone_type = "3";
                    date2.timezone = "Asia/Kolkata";
                    cus_checkin.Checkin_time = date2;

                }
                else
                {
                    cus_checkin.Checkin_time = null;
                }

                

               
                
                string inputDateStr2 = row["Checkout_time"].ToString().Replace("01/01/1900 00:00:00", "");
                string outputDateStr2 = "";
                DateFmt date3 = new DateFmt();
                if (inputDateStr2 != "")
                {
                    DateTime inputDate2 = DateTime.ParseExact(inputDateStr2, "dd/MM/yyyy HH:mm:ss", null);
                    outputDateStr2 = inputDate2.ToString("yyyy-MM-dd HH:mm:ss");

                    date3.date = outputDateStr2.ToString();
                    date3.timezone_type = "3";
                    date3.timezone = "Asia/Kolkata";
                    cus_checkin.Checkout_time = date3;
                }
                else
                {
                    cus_checkin.Checkout_time = null;
                }
                


                

                checkinarylst1.Add(cus_checkin);


            }

            ht3.Add("Customer_Checkin", checkinarylst1);

            SqlCommand cmd2 = new SqlCommand("Chemist_checkin", con);
            cmd2.CommandType = CommandType.StoredProcedure;
            cmd2.Parameters.AddWithValue("@sf_code", sfCode);
            cmd2.Parameters.AddWithValue("@dt", dtdate);
            cmd2.CommandTimeout = 8000;
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

            DataTable ds2 = new DataTable();

            da2.Fill(ds2);

            List<Chemist_checkin> checkinarylst2 = new List<Chemist_checkin>();
            foreach (DataRow row in ds2.Rows)
            {
                Chemist_checkin Chemist_checkin = new Chemist_checkin();
                Chemist_checkin.id = row["id"].ToString();
                Chemist_checkin.name = row["name"].ToString();
                Chemist_checkin.Status = row["status"].ToString();

                Chemist_checkin.Type = row["Type"].ToString();

                string inputDateStr = row["Activity_date"].ToString();
                DateTime inputDate = DateTime.ParseExact(inputDateStr, "dd/MM/yyyy HH:mm:ss", null);
                string outputDateStr = inputDate.ToString("yyyy-MM-dd HH:mm:ss");
                DateFmt date1 = new DateFmt();
                date1.date = outputDateStr.ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";


                Chemist_checkin.Activity_date = date1;

                string inputDateStr1 = row["Checkin_time"].ToString().Replace("01/01/1900 00:00:00", "");
                string outputDateStr1 = "";
                DateFmt date2 = new DateFmt();
                if (inputDateStr1 != "")
                {
                    DateTime inputDate1 = DateTime.ParseExact(inputDateStr1, "dd/MM/yyyy HH:mm:ss", null);
                    outputDateStr1 = inputDate1.ToString("yyyy-MM-dd HH:mm:ss");
                    date2.date = outputDateStr1.ToString();
                    date2.timezone_type = "3";
                    date2.timezone = "Asia/Kolkata";
                    Chemist_checkin.Checkin_time = date2;
                }
                else
                {
                    Chemist_checkin.Checkin_time = null;
                }

                

                
                string inputDateStr2 = row["Checkout_time"].ToString().Replace("01/01/1900 00:00:00", "");
                string outputDateStr2 = "";
                DateFmt date3 = new DateFmt();
                if (inputDateStr2 != "")
                {
                    DateTime inputDate2 = DateTime.ParseExact(inputDateStr2, "dd/MM/yyyy HH:mm:ss", null);
                    outputDateStr2 = inputDate2.ToString("yyyy-MM-dd HH:mm:ss");
                    
                    date3.date = outputDateStr2.ToString();
                    date3.timezone_type = "3";
                    date3.timezone = "Asia/Kolkata";
                    Chemist_checkin.Checkout_time = date3;
                }
                else
                {
                    Chemist_checkin.Checkout_time = null;
                }
                



                

                checkinarylst2.Add(Chemist_checkin);


            }

            ht3.Add("Chemist_Checkin", checkinarylst2);

            SqlCommand cmd3 = new SqlCommand("unlisted_checkin", con);
            cmd3.CommandType = CommandType.StoredProcedure;
            cmd3.Parameters.AddWithValue("@sf_code", sfCode);
            cmd3.Parameters.AddWithValue("@dt", dtdate);
            cmd3.CommandTimeout = 8000;
            SqlDataAdapter da3 = new SqlDataAdapter(cmd3);

            DataTable ds3 = new DataTable();

            da3.Fill(ds3);

            List<unlisted_checkin> checkinarylst3 = new List<unlisted_checkin>();
            foreach (DataRow row in ds3.Rows)
            {
                unlisted_checkin unlisted_checkin = new unlisted_checkin();
                unlisted_checkin.id = row["id"].ToString();
                unlisted_checkin.name = row["name"].ToString();
                unlisted_checkin.Status = row["status"].ToString();

                unlisted_checkin.Type = row["Type"].ToString();


                string inputDateStr = row["Activity_date"].ToString();
                DateTime inputDate = DateTime.ParseExact(inputDateStr, "dd/MM/yyyy HH:mm:ss", null);
                string outputDateStr = inputDate.ToString("yyyy-MM-dd HH:mm:ss");
                DateFmt date1 = new DateFmt();
                date1.date = outputDateStr.ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";


                unlisted_checkin.Activity_date = date1;

                string inputDateStr1 = row["Checkin_time"].ToString().Replace("01/01/1900 00:00:00", "");
                string outputDateStr1 = "";
                DateFmt date2 = new DateFmt();
                if (inputDateStr1 != "")
                {
                    DateTime inputDate1 = DateTime.ParseExact(inputDateStr1, "dd/MM/yyyy HH:mm:ss", null);
                    outputDateStr1 = inputDate1.ToString("yyyy-MM-dd HH:mm:ss");
                    date2.date = outputDateStr1.ToString();
                    date2.timezone_type = "3";
                    date2.timezone = "Asia/Kolkata";
                    unlisted_checkin.Checkin_time = date2;
                }
                else
                {
                    unlisted_checkin.Checkin_time = null;
                }
              

               

                
                string inputDateStr2 = row["Checkout_time"].ToString().Replace("01/01/1900 00:00:00", "");
                string outputDateStr2 = "";
                DateFmt date3 = new DateFmt();
                if (inputDateStr2 != "")
                {
                    DateTime inputDate2 = DateTime.ParseExact(inputDateStr2, "dd/MM/yyyy HH:mm:ss", null);
                    outputDateStr2 = inputDate2.ToString("yyyy-MM-dd HH:mm:ss");
                    
                    date3.date = outputDateStr2.ToString();
                    date3.timezone_type = "3";
                    date3.timezone = "Asia/Kolkata";
                    unlisted_checkin.Checkout_time = date3;
                }
                else
                {
                    unlisted_checkin.Checkout_time = null;
                }
                

                checkinarylst3.Add(unlisted_checkin);


            }

            ht3.Add("unlisted_Checkin", checkinarylst3);

            List<unlisted_checkin> checkinarylst4 = new List<unlisted_checkin>();
            ht3.Add("cip_Checkin", checkinarylst4);
            con.Close();
            return ht3;

        }

        else if (axn == "get/visit_control")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select cast(CustCode as varchar)CustCode,CustType, convert(varchar, Vst_Date, 23)Dcr_dt,cast(month(Vst_Date) as varchar) Mnth,cast(year(Vst_Date) as varchar) Yr,CustName,isnull(SDP,'')town_code,isnull(SDP_Name,'')town_name,'1' Dcr_flag from tbVisit_Details where SF_Code='" + sfCode +"' and CustType=1 and  cast(CONVERT(varchar,Vst_Date,101)as datetime) >= DATEADD(mm, DATEDIFF(mm, 0, GETDATE()) - 1, 0) order by Vst_Date", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@sfcode", sfCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (axn == "get/Dcr_details")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select CustCode,CustType, convert(varchar, Vst_Date, 23)Dcr_dt,cast(month(Vst_Date) as varchar) Mnth,cast(year(Vst_Date) as varchar) Yr,CustName,isnull(SDP,'')town_code,isnull(SDP_Name,'')town_name,1 Dcr_flag from tbVisit_Details where SF_Code='" + sfCode + "' and CustType=1 and  cast(CONVERT(varchar,Vst_Date,101)as datetime) >= DATEADD(DAY, -1, GETDATE()) order by Vst_Date", con);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@sfcode", sfCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }


        else if (axn == "getsurvey")
        {


            con.Open();
            SqlCommand cmd = new SqlCommand("survey_head", con);
            cmd.CommandType = CommandType.StoredProcedure;


            // cmd.Parameters.AddWithValue("@div", rSF);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            SalesForce sf = new SalesForce();
            da.Fill(ds);
            con.Close();
            List<survey_head> surveyList = new List<survey_head>();
            List<survey_detail> surveydetailList = new List<survey_detail>();
            foreach (DataRow row in ds.Rows)
            {
                survey_head sh = new survey_head();

                sh.id = row["id"].ToString();
                sh.name = row["name"].ToString();
                sh.from_date = row["from_date"].ToString();
                sh.to_date = row["to_date"].ToString();




                con.Open();
                SqlCommand cmd1 = new SqlCommand("survey_head_detail", con);
                cmd1.CommandType = CommandType.StoredProcedure;


                cmd1.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
                cmd1.Parameters.AddWithValue("@sf", sfCode);
                cmd1.Parameters.AddWithValue("@surveyid", row["id"].ToString());


                cmd1.CommandTimeout = 8000;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

                DataTable ds1 = new DataTable();

                da1.Fill(ds1);
                con.Close();



                foreach (DataRow rw in ds1.Rows)
                {
                    survey_detail sd = new survey_detail();
                    sd.id = rw["id"].ToString();
                    sd.Survey = rw["Survey"].ToString();

                    sd.DrCat = rw["DrCat"].ToString();
                    sd.DrSpl = rw["DrSpl"].ToString();
                    sd.DrCls = rw["DrCls"].ToString();
                    sd.HosCls = rw["HosCls"].ToString();
                    sd.ChmCat = rw["ChmCat"].ToString();
                    sd.Stkstate = rw["Stkstate"].ToString();
                    sd.StkHQ = rw["StkHQ"].ToString();
                    sd.Stype = rw["Stype"].ToString();
                    sd.Qc_id = rw["Qc_id"].ToString();
                    sd.Qtype = rw["Qtype"].ToString();
                    sd.Qlength = rw["Qlength"].ToString();
                    sd.Mandatory = rw["Mandatory"].ToString();
                    sd.Qname = rw["Qname"].ToString();

                    sd.Qanswer = rw["Qanswer"].ToString();
                    sd.Active_Flag = rw["Active_Flag"].ToString();
                    surveydetailList.Add(sd);

                }
                sh.survey_for = surveydetailList;

                surveyList.Add(sh);
            }
            // ArrayList al = new ArrayList();
            return surveyList;
        }
        else if (dd.tableName == "vwDCR_MissedDates")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("Get_MissedDates_App_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwRmksTemplate")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("RemarksTemplate", con);
            cmd.CommandType = CommandType.StoredProcedure;


          // cmd.Parameters.AddWithValue("@sf", sfCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwFolders")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("MailFolder", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@Divs", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
      
        else if (axn == "get/categorys")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("iOS_getDocCats_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", dd.SF);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (axn == "get/speciality")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("iOS_getDocSpec_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", dd.SF);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "app_setup")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("AppSetup", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.Parameters.AddWithValue("@Div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns",dd.coloumns.Replace("\"","").Replace("[","").Replace("]","").Replace("''","''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            SalesForce sf = new SalesForce();
            da.Fill(ds);

            List<appSetup> tparylst2 = new List<appSetup>();
            List<appSetup_new> tparylst3 = new List<appSetup_new>();
            appSetup appstp = new appSetup();
            appSetup_new appstpnew = new appSetup_new();
            appstpnew.success = false;
            appstp.success = false;
            if (ds.Rows.Count > 0)
            {
                foreach (DataRow row in ds.Rows)
                {


                    appstp.success = true;
                    appstp.sfCode = row["sfCode"].ToString();
                    appstp.sfName = row["sfName"].ToString();
                    appstp.UserN = row["UserN"].ToString();
                    appstp.username = row["username"].ToString();
                    appstp.Pass = row["Pass"].ToString();
                    appstp.divisionCode = row["divisionCode"].ToString();
                    appstp.sftype = row["sftype"].ToString();
                    appstp.desigCode = row["desigCode"].ToString();
                    appstp.SFStat = row["SFStat"].ToString();
                    appstp.AppTyp = row["AppTyp"].ToString();
                    appstp.sample_validation = row["sample_validation"].ToString();
                    appstp.input_validation = row["input_validation"].ToString();
                    appstp.GeoChk = row["GeoChk"].ToString();
                    appstp.GEOTagNeed = row["GEOTagNeed"].ToString();
                    appstp.GEOTagNeedche = row["GEOTagNeedche"].ToString();
                    appstp.GEOTagNeedstock = row["GEOTagNeedstock"].ToString();
                    appstp.GEOTagNeedunlst = row["GEOTagNeedunlst"].ToString();
                    appstp.Android_App = row["Android_App"].ToString();
                    appstp.Tp_Start_Date = row["Tp_Start_Date"].ToString();
                    appstp.Tp_End_Date = row["Tp_End_Date"].ToString();
                    appstp.TBase = row["TBase"].ToString();
                    appstp.UNLNeed = row["UNLNeed"].ToString();
                    appstp.DrCap = row["DrCap"].ToString();
                    appstp.ChmCap = row["ChmCap"].ToString();
                    appstp.StkCap = row["StkCap"].ToString();
                    appstp.NLCap = row["NLCap"].ToString();
                    appstp.ChmNeed = row["ChmNeed"].ToString();
                    appstp.StkNeed = row["StkNeed"].ToString();
                    appstp.DPNeed = row["DPNeed"].ToString();
                    appstp.DINeed = row["DINeed"].ToString();
                    appstp.CPNeed = row["CPNeed"].ToString();
                    appstp.CINeed = row["CINeed"].ToString();
                    appstp.SPNeed = row["SPNeed"].ToString();
                    appstp.SINeed = row["SINeed"].ToString();
                    appstp.NPNeed = row["NPNeed"].ToString();
                    appstp.NINeed = row["NINeed"].ToString();
                    appstp.DRxCap = row["DRxCap"].ToString();
                    appstp.DSmpCap = row["DSmpCap"].ToString();
                    appstp.CQCap = row["CQCap"].ToString();
                    appstp.SQCap = row["SQCap"].ToString();
                    appstp.NRxCap = row["NRxCap"].ToString();
                    appstp.NSmpCap = row["NSmpCap"].ToString();
                    appstp.DisRad = row["DisRad"].ToString();
                    appstp.Attendance = row["Attendance"].ToString();
                    appstp.MCLDet = row["MCLDet"].ToString();
                    appstp.doctor_dobdow = row["doctor_dobdow"].ToString();
                    appstp.Doc_Pob_Mandatory_Need = row["Doc_Pob_Mandatory_Need"].ToString();
                    appstp.Chm_Pob_Mandatory_Need = row["Chm_Pob_Mandatory_Need"].ToString();
                    appstp.multiple_doc_need = row["multiple_doc_need"].ToString();
                    appstp.mailneed = row["mailneed"].ToString();
                    appstp.circular = row["circular"].ToString();
                    appstp.DrRxNd = row["DrRxNd"].ToString();
                    appstp.DrRxQMd = row["DrRxQMd"].ToString();
                    appstp.DrSmpQMd = row["DrSmpQMd"].ToString();
                    appstp.FeedNd = row["FeedNd"].ToString();
                    appstp.DrPrdMd = row["DrPrdMd"].ToString();
                    appstp.DrInpMd = row["DrInpMd"].ToString();
                    appstp.RcpaNd = row["RcpaNd"].ToString();
                    appstp.VstNd = row["VstNd"].ToString();
                    appstp.MsdEntry = row["MsdEntry"].ToString();
                    appstp.TPDCR_Deviation = row["TPDCR_Deviation"].ToString();
                    appstp.TPDCR_MGRAppr = row["TPDCR_MGRAppr"].ToString();
                    appstp.NextVst = row["NextVst"].ToString();
                    appstp.NextVst_Mandatory_Need = row["NextVst_Mandatory_Need"].ToString();
                    appstp.Appr_Mandatory_Need = row["Appr_Mandatory_Need"].ToString();
                    appstp.RCPAQty_Need = row["RCPAQty_Need"].ToString();
                    appstp.Prod_Stk_Need = row["Prod_Stk_Need"].ToString();
                    appstp.TP_Mandatory_Need = row["TP_Mandatory_Need"].ToString();
                    appstp.dayplan_tp_based = row["dayplan_tp_based"].ToString();
                    appstp.Sep_RcpaNd = row["Sep_RcpaNd"].ToString();
                    appstp.DlyCtrl = row["DlyCtrl"].ToString();
                    appstp.wrk_area_Name = row["wrk_area_Name"].ToString();
                    appstp.prod_det_need = row["prod_det_need"].ToString();
                    appstp.quiz_need = row["quiz_need"].ToString();
                    appstp.cip_need = row["cip_need"].ToString();
                    appstp.CIP_PNeed = row["CIP_PNeed"].ToString();
                    appstp.CIP_INeed = row["CIP_INeed"].ToString();
                    appstp.mediaTrans_Need = row["mediaTrans_Need"].ToString();
                    appstp.DrFeedMd = row["DrFeedMd"].ToString();
                    appstp.prdfdback = row["prdfdback"].ToString();
                    appstp.Doc_Pob_Need = row["Doc_Pob_Need"].ToString();
                    appstp.Chm_Pob_Need = row["Chm_Pob_Need"].ToString();
                    appstp.Stk_Pob_Need = row["Stk_Pob_Need"].ToString();
                    appstp.Ul_Pob_Need = row["Ul_Pob_Need"].ToString();
                    appstp.Stk_Pob_Mandatory_Need = row["Stk_Pob_Mandatory_Need"].ToString();
                    appstp.Ul_Pob_Mandatory_Need = row["Ul_Pob_Mandatory_Need"].ToString();
                    appstp.Doc_jointwork_Need = row["Doc_jointwork_Need"].ToString();
                    appstp.Chm_jointwork_Need = row["Chm_jointwork_Need"].ToString();
                    appstp.Stk_jointwork_Need = row["Stk_jointwork_Need"].ToString();
                    appstp.Ul_jointwork_Need = row["Ul_jointwork_Need"].ToString();
                    appstp.days = row["days"].ToString();
                    appstp.product_pob_need_msg = row["product_pob_need_msg"].ToString();
                    appstp.sfEmail = row["sfEmail"].ToString();
                    appstp.sfMobile = row["sfMobile"].ToString();
                    appstp.DS_name = row["DS_name"].ToString();
                    appstp.Doc_jointwork_Mandatory_Need = row["Doc_jointwork_Mandatory_Need"].ToString();
                    appstp.Chm_jointwork_Mandatory_Need = row["Chm_jointwork_Mandatory_Need"].ToString();
                    appstp.Stk_jointwork_Mandatory_Need = row["Stk_jointwork_Mandatory_Need"].ToString();
                    appstp.Ul_jointwork_Mandatory_Need = row["Ul_jointwork_Mandatory_Need"].ToString();
                    appstp.Doc_Product_caption = row["Doc_Product_caption"].ToString();
                    appstp.Chm_Product_caption = row["Chm_Product_caption"].ToString();
                    appstp.Stk_Product_caption = row["Stk_Product_caption"].ToString();
                    appstp.Ul_Product_caption = row["Ul_Product_caption"].ToString();
                    appstp.DFNeed = row["DFNeed"].ToString();
                    appstp.CFNeed = row["CFNeed"].ToString();
                    appstp.SFNeed = row["SFNeed"].ToString();
                    appstp.CIP_FNeed = row["CIP_FNeed"].ToString();
                    appstp.NFNeed = row["NFNeed"].ToString();
                    appstp.HFNeed = row["HFNeed"].ToString();
                    appstp.CIP_QNeed = row["CIP_QNeed"].ToString();
                    appstp.DENeed = row["DENeed"].ToString();
                    appstp.CENeed = row["CENeed"].ToString();
                    appstp.SENeed = row["SENeed"].ToString();
                    appstp.NENeed = row["NENeed"].ToString();
                    appstp.CIP_ENeed = row["CIP_ENeed"].ToString();
                    appstp.HENeed = row["HENeed"].ToString();
                    appstp.Expenseneed = row["Expenseneed"].ToString();
                    appstp.Catneed = row["Catneed"].ToString();
                    appstp.Campneed = row["Campneed"].ToString();
                    appstp.Approveneed = row["Approveneed"].ToString();
                    appstp.Doc_Input_caption = row["Doc_Input_caption"].ToString();
                    appstp.call_report = row["call_report"].ToString();
                    appstp.Chm_Input_caption = row["Chm_Input_caption"].ToString();
                    appstp.Stk_Input_caption = row["Stk_Input_caption"].ToString();
                    appstp.Ul_Input_caption = row["Ul_Input_caption"].ToString();
                    appstp.RmdrNeed = row["RmdrNeed"].ToString();
                    appstp.TempNd = row["TempNd"].ToString();
                    appstp.DrSampNd = row["DrSampNd"].ToString();
                    appstp.CmpgnNeed = row["CmpgnNeed"].ToString();
                    appstp.quote_Text = row["quote_Text"].ToString();
                    appstp.entryFormNeed = row["entryFormNeed"].ToString();
                    appstp.mydayplan_need = row["mydayplan_need"].ToString();
                    appstp.CHEBase = row["CHEBase"].ToString();
                    appstp.TPDCR_Deviation_Appr_Status = row["TPDCR_Deviation_Appr_Status"].ToString();
                    appstp.tp_new = row["tp_new"].ToString();
                    appstp.tp_need = row["tp_need"].ToString();
                    appstp.currentDay = row["currentDay"].ToString();
                    appstp.past_leave_post = row["past_leave_post"].ToString();
                    appstp.myplnRmrksMand = row["myplnRmrksMand"].ToString();
                    appstp.entryFormMgr = row["entryFormMgr"].ToString();
                    appstp.prod_remark = row["prod_remark"].ToString();
                    appstp.stp = row["stp"].ToString();
                    appstp.Remainder_geo = row["Remainder_geo"].ToString();
                    appstp.geoTagImg = row["geoTagImg"].ToString();
                    appstp.cntRemarks = row["cntRemarks"].ToString();
                    appstp.hosp_need = row["hosp_need"].ToString();
                    appstp.HPNeed = row["HPNeed"].ToString();
                    appstp.HINeed = row["HINeed"].ToString();
                    appstp.chmsamQty_need = row["chmsamQty_need"].ToString();
                    appstp.Pwdsetup = row["Pwdsetup"].ToString();
                    appstp.RcpaMd = row["RcpaMd"].ToString();
                    appstp.Rcpa_Competitor_extra = row["Rcpa_Competitor_extra"].ToString();
                    appstp.dashboard = row["dashboard"].ToString();
                    appstp.Order_management = row["Order_management"].ToString();
                    appstp.Order_caption = row["Order_caption"].ToString();
                    appstp.Primary_order_caption = row["Primary_order_caption"].ToString();
                    appstp.Secondary_order_caption = row["Secondary_order_caption"].ToString();
                    appstp.Primary_order = row["Primary_order"].ToString();
                    appstp.Secondary_order = row["Secondary_order"].ToString();
                    appstp.Gst_option = row["Gst_option"].ToString();
                    appstp.TPbasedDCR = row["TPbasedDCR"].ToString();
                    appstp.CIP_jointwork_Need = row["CIP_jointwork_Need"].ToString();
                    appstp.CIP_Caption = row["CIP_Caption"].ToString();
                    appstp.hosp_caption = row["hosp_caption"].ToString();
                    appstp.misc_expense_need = row["misc_expense_need"].ToString();
                    appstp.Location_track = row["Location_track"].ToString();
                    appstp.tracking_time = row["tracking_time"].ToString();
                    appstp.Taxname_caption = row["Taxname_caption"].ToString();
                    appstp.SurveyNd = row["SurveyNd"].ToString();
                    appstp.SrtNd = row["SrtNd"].ToString();
                    appstp.quiz_need_mandt = row["quiz_need_mandt"].ToString();
                    appstp.quiz_heading = row["quiz_heading"].ToString();
                    appstp.Product_Rate_Editable = row["Product_Rate_Editable"].ToString();
                    appstp.CustSrtNd = row["CustSrtNd"].ToString();
                    appstp.ActivityNd = row["ActivityNd"].ToString();
                    appstp.ChmSmpCap = row["ChmSmpCap"].ToString();
                    appstp.product_pob_need = row["product_pob_need"].ToString();
                    appstp.secondary_order_discount = row["secondary_order_discount"].ToString();
                    appstp.GeoTagNeedcip = row["GeoTagNeedcip"].ToString();
                    appstp.Target_report_md = row["Target_report_md"].ToString();
                    appstp.RCPA_unit_nd = row["RCPA_unit_nd"].ToString();
                    appstp.Chm_RCPA_Need = row["Chm_RCPA_Need"].ToString();
                    appstp.DrRCPA_competitor_Need = row["DrRCPA_competitor_Need"].ToString();
                    appstp.ChmRCPA_competitor_Need = row["ChmRCPA_competitor_Need"].ToString();
                    appstp.Currentday_TPplanned = row["Currentday_TPplanned"].ToString();
                    appstp.Doc_cluster_based = row["Doc_cluster_based"].ToString();
                    appstp.Chm_cluster_based = row["Chm_cluster_based"].ToString();
                    appstp.Stk_cluster_based = row["Stk_cluster_based"].ToString();
                    appstp.UlDoc_cluster_based = row["UlDoc_cluster_based"].ToString();
                    appstp.multi_cluster = row["multi_cluster"].ToString();
                    appstp.Terr_based_Tag = row["Terr_based_Tag"].ToString();
                    appstp.RcpaMd_Mgr = row["RcpaMd_Mgr"].ToString();
                    appstp.DrNeed = row["DrNeed"].ToString();
                    appstp.faq = row["faq"].ToString();
                    appstp.edit_holiday = row["edit_holiday"].ToString();
                    appstp.edit_weeklyoff = row["edit_weeklyoff"].ToString();
                    appstp.Target_report_Nd = row["Target_report_Nd"].ToString();
                    appstp.DcrLockDays = row["DcrLockDays"].ToString();
                    appstp.Doc_pob_caption = row["Doc_pob_caption"].ToString();
                    appstp.Stk_pob_caption = row["Stk_pob_caption"].ToString();
                    appstp.Chm_pob_caption = row["Chm_pob_caption"].ToString();
                    appstp.Uldoc_pob_caption = row["Uldoc_pob_caption"].ToString();
                    appstp.CIP_pob_caption = row["CIP_pob_caption"].ToString();
                    appstp.Hosp_pob_caption = row["Hosp_pob_caption"].ToString();
                    appstp.Remainder_call_cap = row["Remainder_call_cap"].ToString();
                    appstp.DrEvent_Md = row["DrEvent_Md"].ToString();
                    appstp.StkEvent_Md = row["StkEvent_Md"].ToString();
                    appstp.UlDrEvent_Md = row["UlDrEvent_Md"].ToString();
                    appstp.CipEvent_Md = row["CipEvent_Md"].ToString();
                    appstp.HospEvent_Md = row["HospEvent_Md"].ToString();
                    appstp.sequential_dcr = row["sequential_dcr"].ToString();
                    appstp.Leave_entitlement_need = row["Leave_entitlement_need"].ToString();
                    appstp.ChmEvent_Md = row["ChmEvent_Md"].ToString();
                    appstp.primarysec_need = row["primarysec_need"].ToString();
                    appstp.Territory_VstNd = row["Territory_VstNd"].ToString();
                    appstp.Dcr_firstselfie = row["Dcr_firstselfie"].ToString();
                    appstp.CipSrtNd = row["CipSrtNd"].ToString();
                    appstp.travelDistance_Need = row["travelDistance_Need"].ToString();
                    appstp.ChmRxQty = row["ChmRxQty"].ToString();
                    appstp.missedDateMand = row["missedDateMand"].ToString();
                    appstp.doc_business_product = row["doc_business_product"].ToString();
                    appstp.doc_business_value = row["doc_business_value"].ToString();
                    appstp.dcr_doc_business_product = row["dcr_doc_business_product"].ToString();
                    appstp.Dr_mappingproduct = row["Dr_mappingproduct"].ToString();
                    appstp.pro_det_need = row["pro_det_need"].ToString();
                    appstp.HosPOBNd = row["HosPOBNd"].ToString();
                    appstp.HosPOBMd = row["HosPOBMd"].ToString();
                    appstp.CIPPOBNd = row["CIPPOBNd"].ToString();
                    appstp.CIPPOBMd = row["CIPPOBMd"].ToString();
                    appstp.Remainder_prd_Md = row["Remainder_prd_Md"].ToString();
                    appstp.Dcr_summary_need = row["Dcr_summary_need"].ToString();
                    appstp.tracking_interval = row["tracking_interval"].ToString();
                    appstp.GeoTagging = row["GeoTagging"].ToString();
                    appstp.No_of_TP_View = row["No_of_TP_View"].ToString();
                    appstp.call_report_from_date = row["call_report_from_date"].ToString();
                    appstp.call_report_to_date = row["call_report_to_date"].ToString();
                    appstp.Sample_Val_Qty = row["Sample_Val_Qty"].ToString();
                    appstp.Input_Val_Qty = row["Input_Val_Qty"].ToString();
                    appstp.Authentication = row["Authentication"].ToString();
                    appstp.GeoTagApprovalNeed = row["GeoTagApprovalNeed"].ToString();
                    appstp.ChmSrtNd = row["ChmSrtNd"].ToString();
                    appstp.UnlistSrtNd = row["UnlistSrtNd"].ToString();
                    appstp.RCPA_competitor_add = row["RCPA_competitor_add"].ToString();
                    appstp.rcpaextra = row["rcpaextra"].ToString();
                    appstp.RCPA_unit_nd = row["RCPA_unit_nd"].ToString();


                    DateFmt date1 = new DateFmt();
                    date1.date = row["dt"].ToString();
                    date1.timezone_type = "3";
                    date1.timezone = "Asia/Kolkata";

                    appstp.SFTPDate = date1;

                    tparylst2.Add(appstp);

                    string val = row["dt"].ToString();
                    val = "{\"date\":\"" + val + "\"}";
                    //row["dt"] = val.Replace("\\","");
                }
                return tparylst2;
            }
            else
            {
                tparylst3.Add(appstpnew);
                return tparylst3;
            }

            //dt = ds.Tables[0];
            con.Close();

        }
        //doctor with fencing
        else if (dd.tableName == "vwDoctor_Master_APP_WithFencing")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("viewapp_doc_master_withfencing", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns",dd.coloumns.Replace("\"","").Replace("[","").Replace("]","").Replace("''","''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        //doctor
        else if (dd.tableName == "vwDoctor_Master_APP")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("viewapp_doc_master", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        //chemist
        else if (dd.tableName == "vwChemists_Master_APP")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwChemists_Master_APP_new", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        //chemist with fencing
        else if (dd.tableName == "vwChemists_Master_APP_WithFencing")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwChemists_Master_APP_WithFencing_new", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "category_master")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetProdcategory", con);
            cmd.CommandType = CommandType.StoredProcedure;


            // cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@Divs", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "vwTown_Master_APP")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwTown_Master_APP_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "DaySummCnt")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getCusVstDet_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            //  dt = ds.Tables[0];
            con.Close();
            //ht1.Add("First", ds);

            con.Open();
            SqlCommand cmd1 = new SqlCommand("getWTVstDet_New", con);
            cmd1.CommandType = CommandType.StoredProcedure;


            cmd1.Parameters.AddWithValue("@SF", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd1.CommandTimeout = 8000;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            DataTable ds4 = new DataTable();

            da1.Fill(ds4);




            con.Close();
            ArrayList aa = new ArrayList();
            aa.Add(ds);
            aa.Add(ds4);

            return aa;
        }
        else if (dd.tableName == "vwsample_input_stocks" || dd.tableName == "vwSample_Input_Stocks")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetSampleStockDetail", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.Parameters.AddWithValue("@Div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            SalesForce sf = new SalesForce();
            da.Fill(ds);


            List<sample> samplearylst = new List<sample>();
            foreach (DataRow row in ds.Rows)
            {
                sample sampledata = new sample();
                sampledata.SF = row["SF"].ToString();
                sampledata.DivisionCode = row["DivisionCode"].ToString();
                sampledata.Code = row["code"].ToString();
                sampledata.Name = row["name"].ToString();
                sampledata.Pack = row["pack"].ToString();
                sampledata.Balance_Stock = row["Balance_Stock"].ToString();
                samplearylst.Add(sampledata);

            }
            ht4.Add("Sample_Stock", samplearylst);
            // dt = ds.Tables[0];
            //con.Close();
            ////ht1.Add("First", ds);

            //con.Open();
            SqlCommand cmd1 = new SqlCommand("GetInputStockDetail", con);
            cmd1.CommandType = CommandType.StoredProcedure;


            cmd1.Parameters.AddWithValue("@SF", sfCode);
            cmd1.Parameters.AddWithValue("@Div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd1.CommandTimeout = 8000;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            DataTable ds4 = new DataTable();

            da1.Fill(ds4);


            List<input> inputarylst = new List<input>();
            foreach (DataRow row in ds4.Rows)
            {
                input inputdata = new input();
                inputdata.SF = row["SF"].ToString();
                inputdata.DivisionCode = row["DivisionCode"].ToString();
                inputdata.Code = row["code"].ToString();
                inputdata.Name = row["name"].ToString();
                //inputdata.Pack = row["Pack"].ToString();
                inputdata.Balance_Stock = row["Balance_Stock"].ToString();
                inputarylst.Add(inputdata);

            }
            ht4.Add("Input_Stock", inputarylst);


            con.Close();
            //ArrayList aa = new ArrayList();
            //aa.Add(ds);
            //aa.Add(ds4);

            return ht4;
        }
        else if (dd.tableName == "vwFeedTemplate")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwFeedTemplate_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            // cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "gift_master")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getAppGift_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF_Code", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (dd.tableName == "mas_worktype")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("GetWorkTypes_App_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SFCd", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "vwLeaveType")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwLeave_new", con);
            //SqlCommand cmd = new SqlCommand("getleavedates", con);
            cmd.CommandType = CommandType.StoredProcedure;
            //cmd.Parameters.AddWithValue("@Month", tp_month);
            //cmd.Parameters.AddWithValue("@year", tp_year);
            //cmd.Parameters.AddWithValue("@sf", sfCode);

            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "product_master")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getAppProd_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF_Code", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "Campaign")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getCampaign_approvelist", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            
            cmd.Parameters.AddWithValue("@effDt", dtdate);
            cmd.Parameters.AddWithValue("@Campaign_flag", "0");
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "mas_product")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getAppProd_new2", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF_Code", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }


        else if (dd.tableName == "vwstockiest_Master_APP")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwstockiest_Master_APP_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "vwstockiest_Master_APP_WithFencing")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwstockiest_Master_APP_New_withFencing", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }


        else if (dd.tableName == "vwunlisted_doctor_master_APP")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwunlisted_doctor_master_APP_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }


        else if (dd.tableName == "vwunlisted_doctor_master_APP_WithFencing")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("vwunlisted_doctor_master_APP_New_WithFencing", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@sf", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "subordinate_master")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getBaseLvlSFs_APP_new", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }



        else if (dd.tableName == "salesforce_master")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getJointWork_App_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@BSF", sfCode);
            cmd.Parameters.AddWithValue("@ESF", rSF);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "vwtp_plan")
        {


            //key = ToBase64Decode(key);

            con.Open();
            //SqlCommand cmd = new SqlCommand("GetTourPlanDetails", con);
            //cmd.CommandType = CommandType.StoredProcedure;


            //cmd.Parameters.AddWithValue("@SF", sfCode);
            //cmd.Parameters.AddWithValue("@Tour_Month", tp_month);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            ////cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //// cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            //cmd.CommandTimeout = 8000;
            //SqlDataAdapter da = new SqlDataAdapter(cmd);

            //DataSet ds = new DataSet();
            //SalesForce sf = new SalesForce();
            //da.Fill(ds);



            //dt = ds.Tables[0];




            SqlCommand cmd = new SqlCommand("GetTourPlanDetails_previous", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.Parameters.AddWithValue("@Tour_Month", tp_month);
            cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            SalesForce sf = new SalesForce();
            da.Fill(ds);

            List<TpData> tparylst = new List<TpData>();
            foreach (DataRow row in ds.Rows)
            {
                TpData tpdata = new TpData();
                tpdata.SFCode = row["SFCode"].ToString();
                tpdata.SFName = row["SFName"].ToString();
                tpdata.Div = row["Div"].ToString();
                tpdata.Mnth = row["Mnth"].ToString();
                tpdata.Yr = row["Yr"].ToString();
                tpdata.dayno = row["dayno"].ToString();
                tpdata.Change_Status = row["Change_Status"].ToString();
                tpdata.Rejection_Reason = row["Rejection_Reason"].ToString();
                tpdata.WTCode = row["WTCode"].ToString();
                tpdata.WTCode2 = row["WTCode2"].ToString();
                tpdata.WTCode3 = row["WTCode3"].ToString();
                tpdata.WTName = row["WTName"].ToString();
                tpdata.WTName2 = row["WTName2"].ToString();
                tpdata.WTName3 = row["WTName3"].ToString();
                tpdata.ClusterCode = row["ClusterCode"].ToString();
                tpdata.ClusterCode2 = row["ClusterCode2"].ToString();
                tpdata.ClusterCode3 = row["ClusterCode3"].ToString();
                tpdata.ClusterName = row["ClusterName"].ToString();
                tpdata.ClusterName2 = row["ClusterName2"].ToString();
                tpdata.ClusterName3 = row["ClusterName3"].ToString();
                tpdata.ClusterSFs = row["ClusterSFs"].ToString();
                tpdata.ClusterSFNms = row["ClusterSFNms"].ToString();
                tpdata.JWCodes = row["JWCodes"].ToString();
                tpdata.JWNames = row["JWNames"].ToString();
                tpdata.JWCodes2 = row["JWCodes2"].ToString();
                tpdata.JWNames2 = row["JWNames2"].ToString();
                tpdata.JWCodes3 = row["JWCodes3"].ToString();
                tpdata.JWNames3 = row["JWNames3"].ToString();
                tpdata.Dr_Code = row["Dr_Code"].ToString();
                tpdata.Dr_Name = row["Dr_Name"].ToString();
                tpdata.Dr_two_code = row["Dr_two_code"].ToString();
                tpdata.Dr_two_name = row["Dr_two_name"].ToString();
                tpdata.Dr_three_code = row["Dr_three_code"].ToString();
                tpdata.Dr_three_name = row["Dr_three_name"].ToString();
                tpdata.Chem_Code = row["Chem_Code"].ToString();
                tpdata.Chem_Name = row["Chem_Name"].ToString();
                tpdata.Chem_two_code = row["Chem_two_code"].ToString();
                tpdata.Chem_two_name = row["Chem_two_name"].ToString();
                tpdata.Chem_three_code = row["Chem_three_code"].ToString();
                tpdata.Chem_three_name = row["Chem_three_name"].ToString();
                tpdata.Stockist_Code = row["Stockist_Code"].ToString();
                tpdata.Stockist_Name = row["Stockist_Name"].ToString();
                tpdata.Stockist_two_code = row["Stockist_two_code"].ToString();
                tpdata.Stockist_two_name = row["Stockist_two_name"].ToString();
                tpdata.Stockist_three_code = row["Stockist_three_code"].ToString();
                tpdata.Stockist_three_name = row["Stockist_three_name"].ToString();
                tpdata.Day = row["day"].ToString();
                tpdata.Tour_Month = row["Tour_Month"].ToString();
                tpdata.Tour_Year = row["Tour_Year"].ToString();
                tpdata.tpmonth = row["tpmonth"].ToString();
                tpdata.tpday = row["tpday"].ToString();
                tpdata.DayRemarks = row["DayRemarks"].ToString();
                tpdata.DayRemarks2 = row["DayRemarks2"].ToString();
                tpdata.DayRemarks3 = row["DayRemarks3"].ToString();
                tpdata.access = row["access"].ToString();
                tpdata.EFlag = row["EFlag"].ToString();
                tpdata.FWFlg = row["FWFlg"].ToString();
                tpdata.FWFlg2 = row["FWFlg2"].ToString();
                tpdata.FWFlg3 = row["FWFlg3"].ToString();
                tpdata.HQCodes = row["HQCodes"].ToString();
                tpdata.HQNames = row["HQNames"].ToString();
                tpdata.HQCodes2 = row["HQCodes2"].ToString();
                tpdata.HQNames2 = row["HQNames2"].ToString();
                tpdata.HQCodes3 = row["HQCodes3"].ToString();
                tpdata.HQNames3 = row["HQNames3"].ToString();
                // tpdata.submitted_time = row["submitted_time"].ToString();
                tpdata.Entry_mode = row["Entry_mode"].ToString();



                DateFmt date1 = new DateFmt();
                date1.date = row["dt"].ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";
                DateFmt1 date2 = new DateFmt1();
                date2.date = row["submitted_time_dt"].ToString();
                date2.timezone_type = "3";
                date2.timezone = "Asia/Kolkata";

                tpdata.TPDt = date1;
                tpdata.submitted_time = date2;
                tparylst.Add(tpdata);

                string val = row["dt"].ToString();
                val = "{\"date\":\"" + val + "\"}";
                //row["dt"] = val.Replace("\\","");
            }

            ht.Add("previous", tparylst);



            SqlCommand cmd1 = new SqlCommand("GetTourPlanDetails_New", con);
            cmd1.CommandType = CommandType.StoredProcedure;


            cmd1.Parameters.AddWithValue("@SF", sfCode);
            cmd1.Parameters.AddWithValue("@Tour_Month", tp_month);
            cmd1.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd1.CommandTimeout = 8000;
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);

            DataTable ds1 = new DataTable();

            da1.Fill(ds1);

            List<TpData> tparylst2 = new List<TpData>();
            foreach (DataRow row in ds1.Rows)
            {
                TpData tpdata = new TpData();
                tpdata.SFCode = row["SFCode"].ToString();
                tpdata.SFName = row["SFName"].ToString();
                tpdata.Div = row["Div"].ToString();
                tpdata.Mnth = row["Mnth"].ToString();
                tpdata.Yr = row["Yr"].ToString();
                tpdata.dayno = row["dayno"].ToString();
                tpdata.Change_Status = row["Change_Status"].ToString();
                tpdata.Rejection_Reason = row["Rejection_Reason"].ToString();
                tpdata.WTCode = row["WTCode"].ToString();
                tpdata.WTCode2 = row["WTCode2"].ToString();
                tpdata.WTCode3 = row["WTCode3"].ToString();
                tpdata.WTName = row["WTName"].ToString();
                tpdata.WTName2 = row["WTName2"].ToString();
                tpdata.WTName3 = row["WTName3"].ToString();
                tpdata.ClusterCode = row["ClusterCode"].ToString();
                tpdata.ClusterCode2 = row["ClusterCode2"].ToString();
                tpdata.ClusterCode3 = row["ClusterCode3"].ToString();
                tpdata.ClusterName = row["ClusterName"].ToString();
                tpdata.ClusterName2 = row["ClusterName2"].ToString();
                tpdata.ClusterName3 = row["ClusterName3"].ToString();
                tpdata.ClusterSFs = row["ClusterSFs"].ToString();
                tpdata.ClusterSFNms = row["ClusterSFNms"].ToString();
                tpdata.JWCodes = row["JWCodes"].ToString();
                tpdata.JWNames = row["JWNames"].ToString();
                tpdata.JWCodes2 = row["JWCodes2"].ToString();
                tpdata.JWNames2 = row["JWNames2"].ToString();
                tpdata.JWCodes3 = row["JWCodes3"].ToString();
                tpdata.JWNames3 = row["JWNames3"].ToString();
                tpdata.Dr_Code = row["Dr_Code"].ToString();
                tpdata.Dr_Name = row["Dr_Name"].ToString();
                tpdata.Dr_two_code = row["Dr_two_code"].ToString();
                tpdata.Dr_two_name = row["Dr_two_name"].ToString();
                tpdata.Dr_three_code = row["Dr_three_code"].ToString();
                tpdata.Dr_three_name = row["Dr_three_name"].ToString();
                tpdata.Chem_Code = row["Chem_Code"].ToString();
                tpdata.Chem_Name = row["Chem_Name"].ToString();
                tpdata.Chem_two_code = row["Chem_two_code"].ToString();
                tpdata.Chem_two_name = row["Chem_two_name"].ToString();
                tpdata.Chem_three_code = row["Chem_three_code"].ToString();
                tpdata.Chem_three_name = row["Chem_three_name"].ToString();
                tpdata.Stockist_Code = row["Stockist_Code"].ToString();
                tpdata.Stockist_Name = row["Stockist_Name"].ToString();
                tpdata.Stockist_two_code = row["Stockist_two_code"].ToString();
                tpdata.Stockist_two_name = row["Stockist_two_name"].ToString();
                tpdata.Stockist_three_code = row["Stockist_three_code"].ToString();
                tpdata.Stockist_three_name = row["Stockist_three_name"].ToString();
                tpdata.Day = row["day"].ToString();
                tpdata.Tour_Month = row["Tour_Month"].ToString();
                tpdata.Tour_Year = row["Tour_Year"].ToString();
                tpdata.tpmonth = row["tpmonth"].ToString();
                tpdata.tpday = row["tpday"].ToString();
                tpdata.DayRemarks = row["DayRemarks"].ToString();
                tpdata.DayRemarks2 = row["DayRemarks2"].ToString();
                tpdata.DayRemarks3 = row["DayRemarks3"].ToString();
                tpdata.access = row["access"].ToString();
                tpdata.EFlag = row["EFlag"].ToString();
                tpdata.FWFlg = row["FWFlg"].ToString();
                tpdata.FWFlg2 = row["FWFlg2"].ToString();
                tpdata.FWFlg3 = row["FWFlg3"].ToString();
                tpdata.HQCodes = row["HQCodes"].ToString();
                tpdata.HQNames = row["HQNames"].ToString();
                tpdata.HQCodes2 = row["HQCodes2"].ToString();
                tpdata.HQNames2 = row["HQNames2"].ToString();
                tpdata.HQCodes3 = row["HQCodes3"].ToString();
                tpdata.HQNames3 = row["HQNames3"].ToString();
                //tpdata.submitted_time = row["submitted_time"].ToString();
                tpdata.Entry_mode = row["Entry_mode"].ToString();



                DateFmt date1 = new DateFmt();
                date1.date = row["dt"].ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";
                DateFmt1 date2 = new DateFmt1();
                date2.date = row["submitted_time_dt"].ToString();
                date2.timezone_type = "3";
                date2.timezone = "Asia/Kolkata";
                tpdata.TPDt = date1;
                tpdata.submitted_time = date2;
                tparylst2.Add(tpdata);

                string val = row["dt"].ToString();
                val = "{\"date\":\"" + val + "\"}";
                //row["dt"] = val.Replace("\\","");
            }

            ht.Add("current", tparylst2);

            SqlCommand cmd2 = new SqlCommand("GetTourPlanDetails_Next", con);
            cmd2.CommandType = CommandType.StoredProcedure;


            cmd2.Parameters.AddWithValue("@SF", sfCode);
            cmd2.Parameters.AddWithValue("@Tour_Month", tp_month);
            cmd2.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd2.CommandTimeout = 8000;
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

            DataTable ds2 = new DataTable();

            da2.Fill(ds2);

            List<TpData> tparylst1 = new List<TpData>();
            foreach (DataRow row in ds2.Rows)
            {
                TpData tpdata = new TpData();
                tpdata.SFCode = row["SFCode"].ToString();
                tpdata.SFName = row["SFName"].ToString();
                tpdata.Div = row["Div"].ToString();
                tpdata.Mnth = row["Mnth"].ToString();
                tpdata.Yr = row["Yr"].ToString();
                tpdata.dayno = row["dayno"].ToString();
                tpdata.Change_Status = row["Change_Status"].ToString();
                tpdata.Rejection_Reason = row["Rejection_Reason"].ToString();
                tpdata.WTCode = row["WTCode"].ToString();
                tpdata.WTCode2 = row["WTCode2"].ToString();
                tpdata.WTCode3 = row["WTCode3"].ToString();
                tpdata.WTName = row["WTName"].ToString();
                tpdata.WTName2 = row["WTName2"].ToString();
                tpdata.WTName3 = row["WTName3"].ToString();
                tpdata.ClusterCode = row["ClusterCode"].ToString();
                tpdata.ClusterCode2 = row["ClusterCode2"].ToString();
                tpdata.ClusterCode3 = row["ClusterCode3"].ToString();
                tpdata.ClusterName = row["ClusterName"].ToString();
                tpdata.ClusterName2 = row["ClusterName2"].ToString();
                tpdata.ClusterName3 = row["ClusterName3"].ToString();
                tpdata.ClusterSFs = row["ClusterSFs"].ToString();
                tpdata.ClusterSFNms = row["ClusterSFNms"].ToString();
                tpdata.JWCodes = row["JWCodes"].ToString();
                tpdata.JWNames = row["JWNames"].ToString();
                tpdata.JWCodes2 = row["JWCodes2"].ToString();
                tpdata.JWNames2 = row["JWNames2"].ToString();
                tpdata.JWCodes3 = row["JWCodes3"].ToString();
                tpdata.JWNames3 = row["JWNames3"].ToString();
                tpdata.Dr_Code = row["Dr_Code"].ToString();
                tpdata.Dr_Name = row["Dr_Name"].ToString();
                tpdata.Dr_two_code = row["Dr_two_code"].ToString();
                tpdata.Dr_two_name = row["Dr_two_name"].ToString();
                tpdata.Dr_three_code = row["Dr_three_code"].ToString();
                tpdata.Dr_three_name = row["Dr_three_name"].ToString();
                tpdata.Chem_Code = row["Chem_Code"].ToString();
                tpdata.Chem_Name = row["Chem_Name"].ToString();
                tpdata.Chem_two_code = row["Chem_two_code"].ToString();
                tpdata.Chem_two_name = row["Chem_two_name"].ToString();
                tpdata.Chem_three_code = row["Chem_three_code"].ToString();
                tpdata.Chem_three_name = row["Chem_three_name"].ToString();
                tpdata.Stockist_Code = row["Stockist_Code"].ToString();
                tpdata.Stockist_Name = row["Stockist_Name"].ToString();
                tpdata.Stockist_two_code = row["Stockist_two_code"].ToString();
                tpdata.Stockist_two_name = row["Stockist_two_name"].ToString();
                tpdata.Stockist_three_code = row["Stockist_three_code"].ToString();
                tpdata.Stockist_three_name = row["Stockist_three_name"].ToString();
                tpdata.Day = row["day"].ToString();
                tpdata.Tour_Month = row["Tour_Month"].ToString();
                tpdata.Tour_Year = row["Tour_Year"].ToString();
                tpdata.tpmonth = row["tpmonth"].ToString();
                tpdata.tpday = row["tpday"].ToString();
                tpdata.DayRemarks = row["DayRemarks"].ToString();
                tpdata.DayRemarks2 = row["DayRemarks2"].ToString();
                tpdata.DayRemarks3 = row["DayRemarks3"].ToString();
                tpdata.access = row["access"].ToString();
                tpdata.EFlag = row["EFlag"].ToString();
                tpdata.FWFlg = row["FWFlg"].ToString();
                tpdata.FWFlg2 = row["FWFlg2"].ToString();
                tpdata.FWFlg3 = row["FWFlg3"].ToString();
                tpdata.HQCodes = row["HQCodes"].ToString();
                tpdata.HQNames = row["HQNames"].ToString();
                tpdata.HQCodes2 = row["HQCodes2"].ToString();
                tpdata.HQNames2 = row["HQNames2"].ToString();
                tpdata.HQCodes3 = row["HQCodes3"].ToString();
                tpdata.HQNames3 = row["HQNames3"].ToString();
                //tpdata.submitted_time = row["submitted_time"].ToString();
                tpdata.Entry_mode = row["Entry_mode"].ToString();



                DateFmt date1 = new DateFmt();
                date1.date = row["dt"].ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";
                DateFmt1 date2 = new DateFmt1();
                date2.date = row["submitted_time_dt"].ToString();
                date2.timezone_type = "3";
                date2.timezone = "Asia/Kolkata";
                tpdata.TPDt = date1;
                tpdata.submitted_time = date2;
                tparylst1.Add(tpdata);

                string val = row["dt"].ToString();
                val = "{\"date\":\"" + val + "\"}";
                //row["dt"] = val.Replace("\\","");
            }



            ht.Add("next", tparylst1);
            con.Close();
            return ht;
        }

        else if (dd.tableName == "tp_objective")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("select cast(id as varchar) id,objective_name name from mas_tp_objective where division_code='" + divisionCode.Remove(divisionCode.Length - 1, 1) + "' and status=0", con);
            //cmd.CommandType = CommandType.StoredProcedure;


            //cmd.Parameters.AddWithValue("@SF", sfCode);
            //cmd.Parameters.AddWithValue("@Tour_Month", tp_month);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }

        else if (dd.tableName == "vwMyDayPlan")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getTodayTP_native_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", rSF);
            cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            SalesForce sf = new SalesForce();
            da.Fill(ds);


            List<vwMyDayPlan> tparylst2 = new List<vwMyDayPlan>();
            foreach (DataRow row in ds.Rows)
            {
                vwMyDayPlan tpdataplan = new vwMyDayPlan();
                tpdataplan.SFCode = row["SFCode"].ToString();
                tpdataplan.worktype = row["worktype"].ToString();
                tpdataplan.WTNm = row["WTNm"].ToString();
                tpdataplan.FWFlg = row["FWFlg"].ToString();
                tpdataplan.subordinateid = row["subordinateid"].ToString();
                tpdataplan.HQNm = row["HQNm"].ToString();

                tpdataplan.clusterid = row["clusterid"].ToString();
                tpdataplan.clstrName = row["clstrName"].ToString();
                tpdataplan.remarks = row["remarks"].ToString();
                tpdataplan.TpVwFlg = row["TpVwFlg"].ToString();
                tpdataplan.TP_Doctor = row["TP_Doctor"].ToString();
                tpdataplan.TP_cluster = row["TP_cluster"].ToString();
                tpdataplan.TP_worktype = row["TP_worktype"].ToString();
                tpdataplan.TP_HQCode = row["TP_HQCode"].ToString();



                DateFmt date1 = new DateFmt();
                date1.date = row["dt"].ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";

                tpdataplan.TPDt = date1;

                tparylst2.Add(tpdataplan);

                string val = row["dt"].ToString();
                val = "{\"date\":\"" + val + "\"}";
                //row["dt"] = val.Replace("\\","");
            }

            ht.Add("current", tparylst2);





            //  dt = ds.Tables[0];
            con.Close();
            return tparylst2;
        }




        else if (dd.tableName == "subordinate")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getHyrSF_APP_new", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", rSF);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable ds = new DataTable();
            SalesForce sf = new SalesForce();
            da.Fill(ds);
            con.Close();
            List<subordinate> subordinatelst = new List<subordinate>();
            foreach(DataRow row in ds.Rows)
            {
                subordinate sb = new subordinate();
                sb.id = row["id"].ToString();
                sb.name = row["name"].ToString();
                sb.SF_HQ = row["SF_HQ"].ToString();
                DateFmt date1 = new DateFmt();
                date1.date = row["SF_DOB1"].ToString();
                date1.timezone_type = "3";
                date1.timezone = "Asia/Kolkata";
                sb.SF_DOB = date1;
                DateFmt date2 = new DateFmt();
                date2.date = row["SF_DOW1"].ToString();
                date2.timezone_type = "3";
                date2.timezone = "Asia/Kolkata";
                sb.SF_DOW = date2;
                sb.steps = row["steps"].ToString();
                sb.Reporting_To_SF = row["Reporting_To_SF"].ToString();

                subordinatelst.Add(sb);
            }
            
            return subordinatelst;
        }

        else if (dd.tableName == "GetMailSF")
        {


            //key = ToBase64Decode(key);

            con.Open();
            SqlCommand cmd = new SqlCommand("getFullHryList_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            //cmd.Parameters.AddWithValue("@TPDt", cdate);
            //cmd.Parameters.AddWithValue("@Tour_Month", cd);
            //cmd.Parameters.AddWithValue("@Tour_Year", tp_year);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            // cmd.Parameters.AddWithValue("@columns", dd.coloumns.Replace("\"", "").Replace("[", "").Replace("]", "").Replace("''", "''''"));




            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);



            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else
        {
            //DataSet ds = new DataSet();
            

            //dt = ds.Tables[0];
            //con.Close();
            return dt;
        }

        //else if (dd.tableName == "vwLeaveType")
        //{


        //    //key = ToBase64Decode(key);

        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("select Leave_Code id,Leave_SName name,Leave_Name from vwLeaveType where Division_code='"+ divisionCode.Remove(divisionCode.Length - 1, 1) + "'", con);
        //    cmd.CommandTimeout = 600;
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da1.Fill(ds);



        //    dt = ds.Tables[0];
        //    con.Close();
        //}

        //if (dt.Rows.Count > 0)
        //{
        //    objField = JsonConvert.SerializeObject(dt);
        //}
        //else
        //{
        //    objField="[]";
        //}

        // return dt;

        return new Object();

    }


    [HttpGet]
    public DataTable getapis()
    {

        string tp_month = HttpContext.Current.Request.Params["tp_month"];
        string tp_year = HttpContext.Current.Request.Params["tp_year"];
        string cdate = HttpContext.Current.Request.Params["cdate"];
        string rSF = HttpContext.Current.Request.Params["rSF"];
        string axn = HttpContext.Current.Request.Params["axn"];
        string orderBy = HttpContext.Current.Request.Params["orderBy"];
        string sfCode = HttpContext.Current.Request.Params["sfCode"];
        string divisionCode = HttpContext.Current.Request.Params["divisionCode"];
        string SF = HttpContext.Current.Request.Params["SF"];
        string year = HttpContext.Current.Request.Params["year"];
        string stateCode = HttpContext.Current.Request.Params["stateCode"];


        string objField = string.Empty;
        string strConn = System.Configuration.ConfigurationManager.ConnectionStrings["Ereportcon"].ToString();
        SqlConnection con = new SqlConnection(strConn);
        if (axn == "get/tpsetup")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("select SF_code,isnull(AddsessionNeed,1)AddsessionNeed,isnull(AddsessionCount,1)AddsessionCount,isnull(DrNeed,1)DrNeed,isnull(ChmNeed,1)ChmNeed,isnull(JWNeed,1)JWNeed,isnull(ClusterNeed,1)ClusterNeed,isnull(clustertype,1)clustertype,div,isnull(StkNeed,1)StkNeed,isnull(Cip_Need,1)Cip_Need,isnull(HospNeed,1)HospNeed,isnull(FW_meetup_mandatory,1)FW_meetup_mandatory,isnull(max_doc,0)max_doc,isnull(tp_objective,1)tp_objective,isnull(Holiday_Editable,0)Holiday_Editable,isnull(Weeklyoff_Editable,0)Weeklyoff_Editable from tpSetup where div='" + divisionCode.Remove(divisionCode.Length - 1, 1) + "'", con);
            //cmd.CommandType = CommandType.StoredProcedure;


            //cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);


            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (axn == "FF_holiday")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("getHolidays_SF_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.Parameters.AddWithValue("@Div", divisionCode.Remove(divisionCode.Length - 1, 1));
            cmd.Parameters.AddWithValue("@year", year);
            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);


            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (axn == "FF_weekoff")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("getWeeklyoff_SF_New", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            cmd.Parameters.AddWithValue("@stateCode", stateCode);
            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);


            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else if (axn == "vwCheckLeave")
        {
            con.Open();
            SqlCommand cmd = new SqlCommand("vwcheckleave", con);
            cmd.CommandType = CommandType.StoredProcedure;


            cmd.Parameters.AddWithValue("@SF", sfCode);
            //cmd.Parameters.AddWithValue("@div", divisionCode.Remove(divisionCode.Length - 1, 1));
            //cmd.Parameters.AddWithValue("@stateCode", stateCode);
            cmd.CommandTimeout = 8000;
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();
            SalesForce sf = new SalesForce();
            da.Fill(ds);


            dt = ds.Tables[0];
            con.Close();
            return dt;
        }
        else
        {
            return dt;
        }


        //else if (dd.tableName == "vwLeaveType")
        //{


        //    //key = ToBase64Decode(key);

        //    con.Open();
        //    SqlCommand cmd = new SqlCommand("select Leave_Code id,Leave_SName name,Leave_Name from vwLeaveType where Division_code='"+ divisionCode.Remove(divisionCode.Length - 1, 1) + "'", con);
        //    cmd.CommandTimeout = 600;
        //    SqlDataAdapter da1 = new SqlDataAdapter(cmd);
        //    DataSet ds = new DataSet();
        //    da1.Fill(ds);



        //    dt = ds.Tables[0];
        //    con.Close();
        //}

        //if (dt.Rows.Count > 0)
        //{
        //    objField = JsonConvert.SerializeObject(dt);
        //}
        // else
        //{
        //    objField="[]";
        //}

       // return new object();
    }




    public static string ToBase64Decode(string base64EncodedText)
    {
        if (String.IsNullOrEmpty(base64EncodedText))
        {
            return base64EncodedText;
        }

        byte[] base64EncodedBytes = Convert.FromBase64String(base64EncodedText);
        return System.Text.Encoding.UTF8.GetString(base64EncodedBytes);
    }
}


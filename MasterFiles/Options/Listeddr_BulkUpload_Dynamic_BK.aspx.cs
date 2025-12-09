using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Data.OleDb;

using System.Data.Sql;
using System.Data.SqlClient;
using System.IO;
using System.Configuration;
using System.Data.Common;
using ClosedXML.Excel;
using System.Text;
using DBase_EReport;
using System.Data;
using System.Text.RegularExpressions;
using ClosedXML;
using System.Web.Script.Serialization;
using System.Drawing;
public partial class MasterFiles_Options_Listeddr_BulkUpload_Dynamic_Bk : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = new DataSet();
    string strSf_Code = string.Empty;
    DataTable dtListed = null;
    DataSet dsListedDR = null;
    string sfCode = string.Empty;

    SqlConnection conn = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    DataSet ds = new DataSet();
    DataTable Dt = new DataTable();
    DataTable dst = new DataTable();
    DataTable dbset = new DataTable();
    DataTable dtcopy = new DataTable();
    DataTable dtc = new DataTable();
    DataSet dsSpec = new DataSet();
    DataSet dslstCat = new DataSet();
    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sfCode = Session["sf_code"].ToString();
        if (!Page.IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            //FillReporting();

            getFile();
            foreach (ListItem item in CblDoctorCode.Items)
            {
                if ((item.Value) == "Sl_No" || (item.Value) == "ListedDr_Name" || (item.Value) == "Sf_Code" || (item.Value) == "Territory_Code" || (item.Value) == "Cluster_Name" || (item.Value) == "Doc_Special_Code" || (item.Value) == "Doc_Cat_Code")
                {
                    item.Attributes.Add("class", "border1");
                  
                }

                if ((item.Value) == "Sl_No")
                {
                    item.Attributes.Add("title", "Auto Generated Serial No.");
                    
                }
                if ((item.Value) == "ListedDr_Name")
                {
                    item.Attributes.Add("title", "* Should not be Empty.");
                }
                if ((item.Value) == "Territory_Code")
                {
                    item.Attributes.Add("title", " * While doing the DCR, Field Force " + "\n" + "  has to select the Territory for getting the Doctors... Should not be Empty.");
                
                }
                if ((item.Value) == "Sf_Code")
                {
                    item.Attributes.Add("title", "* Should be the Login User Name. "+"\n"+"  Should not be Empty.");
                }
                if ((item.Value) == "Cluster_Name")
                {
                    item.Attributes.Add("title", "For making Standard Fare Chart.");
                }
                if ((item.Value) == "Doc_Special_Code")
                {
                    item.Attributes.Add("title", " * Copy the Specialty Name " + "\n" + "  From the Master.... Should not be Empty.");
                }
                if ((item.Value) == "Doc_Cat_Code")
                {
                    item.Attributes.Add("title", " * Copy the Category Name " + "\n" + "  From the Master..... Should not be Empty.");
                }
                if ((item.Value) == "Doc_QuaCode")
                {
                    item.Attributes.Add("title", "Enter the Qualification Name.");
                }
                if ((item.Value) == "Doc_ClsCode")
                {
                    item.Attributes.Add("title", "Copy the Class Name from the Master.");
                }
                if ((item.Value) == "Territory_Cat")
                {
                    item.Attributes.Add("title", "Enter 1, 2, 3 & 4. " + "\n" + "  1 for HQ, 2 for EX, 3 for OS and 4 for OS-EX.");
                }
                if ((item.Value) == "ListedDR_Address1")
                {
                    item.Attributes.Add("title", "Listed Doctor Address with Street Name, Place, City and Pincode in One line.");
                }
                if ((item.Value) == "ListedDr_Hospital")
                {
                    item.Attributes.Add("title", "Enter the Name of the Hospital.");
                }
                if ((item.Value) == "Hospital_Address")
                {
                    item.Attributes.Add("title", "Hospital Address with Street Name,Place, City and Pincode.");
                }
                if ((item.Value) == "ListedDR_DOB")
                {
                    item.Attributes.Add("title", "Format should be DD/MM/YYYY or DD-MM-YYYY or DD.MM.YYYY");
                }
                if ((item.Value) == "ListedDR_DOW")
                {
                    item.Attributes.Add("title", "Format should be DD/MM/YYYY or DD-MM-YYYY or DD.MM.YYYY");
                }
                if ((item.Value) == "ListedDR_EMail")
                {
                    item.Attributes.Add("title", "Enter the Doctor Email ID.");
                }
                if ((item.Value) == "ListedDR_Phone")
                {
                    item.Attributes.Add("title", "Enter the Landline No. for the Doctor.");
                }
                if ((item.Value) == "ListedDR_Mobile")
                {
                    item.Attributes.Add("title", "Enter the Mobile No.for the Doctor.");
                }
                if ((item.Value) == "ListedDr_Sex")
                {
                    item.Attributes.Add("title", "Male or Female has to be typed.");
                }
                if ((item.Value) == "No_of_Visit")
                {
                    item.Attributes.Add("title", "Should be Numeric... Numbers only Allowed.");
                }
                if ((item.Value) == "State_Code")
                {
                    item.Attributes.Add("title", "Enter the State which Doctor belongs.");
                }
                if ((item.Value) == "ListedDr_Fax")
                {
                    item.Attributes.Add("title", "Enter the Listed Doctor FAX No.");
                }
                if ((item.Value) == "ListedDr_website")
                {
                    item.Attributes.Add("title", "Any Website if Doctor have.");
                }
                if ((item.Value) == "ListedDr_PinCode")
                {
                    item.Attributes.Add("title", "Enter the Pincode.");
                }
                if ((item.Value) == "Dr_Business_Value")
                {
                    item.Attributes.Add("title", "Enter Numeric... Numbers only Allowed. " + "\n" + " (Our Business)");
                }
                if ((item.Value) == "Dr_Potential")
                {
                    item.Attributes.Add("title", "Enter Numeric.... Numbers only Allowed. " + "\n" + " (Doctor Potential)");
                }
                if ((item.Value) == "visit_Session")
                {
                    item.Attributes.Add("title", "Product Code to be typed with the separation Symbol like '/' . " + "\n" + "Available at Product Detail Master -- Bulk Edit with the Name of MAP Code");
                }
                if ((item.Value) == "Hospital_Country")
                {
                    item.Attributes.Add("title", "Enter the Listed Doctor Country.");
                }
                if ((item.Value) == "Hospital_State")
                {
                    item.Attributes.Add("title", "Enter the Hospital State.");
                }
                if ((item.Value) == "Day_1")
                {
                    item.Attributes.Add("title", "Number only Allowed.");
                }
                if ((item.Value) == "Day_2")
                {
                    item.Attributes.Add("title", "Number only Allowed.");
                }
                if ((item.Value) == "Day_3")
                {
                    item.Attributes.Add("title", "Number only Allowed.");
                }
                if ((item.Value) == "Geo_Tag_Count")
                {
                    item.Attributes.Add("title", "Enter the Geo Tag Count for the Doctor should be Numeric. " + "\n" + " Numbers only Allowed.");
                }
                if ((item.Value) == "Unique_Dr_Code")
                {
                    item.Attributes.Add("title", "Enter the Listed Doctor Code if any.");
                }
                if ((item.Value) == "ListedDr_RegNo")
                {
                    item.Attributes.Add("title", "Enter the Listed Doctor Registration Number.");
                }
                if ((item.Value) == "ListedDr_Avg_Patients")
                {
                    item.Attributes.Add("title", "Enter the Average Patient per Day. " + "\n" + " Numbers only Allowed.");
                }
                if ((item.Value) == "ListedDr_Visit_Days")
                {
                    item.Attributes.Add("title", "Enter like SUNDAY/MONDAY or SUN/MON/TUE..etc.");
                }
                if ((item.Value) == "Other1")
                {
                    item.Attributes.Add("title", "Enter Any other Information like Hobbies, etc.");
                }
                if ((item.Value) == "Other2")
                {
                    item.Attributes.Add("title", "Enter Any other Information like PANCARD No, etc.");
                }
                if ((item.Value) == "Other3")
                {
                    item.Attributes.Add("title", "Enter Any other Information like Family Information, etc.");
                }
            }
        }
        hHeading.InnerText = Page.Title;
    }
    private void getFile()
    {
        DB_EReporting db = new DB_EReporting();
        string strQry = "SELECT Trans_Sl_No,Column_Name from Mas_Listeddoctor_Upload where Division_code='" + div_code + "' and Active_Flag=0";

        DataSet ds = db.Exec_DataSet(strQry);

        if (ds.Tables[0].Rows.Count > 0)
        {
            btnGen.Enabled = false;

            lnkDelete.Enabled = true;
            FlUploadcsv.Enabled = true;
            btnUpload.Enabled = true;
            lnkDownload.Enabled = true;


            string col_name = ds.Tables[0].Rows[0]["Column_Name"].ToString();
            string[] col;

            if (col_name != "")
            {
                int iIndex = -1;
                col = col_name.Split(',');
                foreach (string st in col)
                {
                    for (iIndex = 0; iIndex < CblDoctorCode.Items.Count; iIndex++)
                    {
                        if (st == CblDoctorCode.Items[iIndex].Text)
                        {
                            CblDoctorCode.Items[iIndex].Selected = true;
                            CblDoctorCode.Items[iIndex].Attributes.Add("style", "Color: Red; font-weight:Bold;");
                            CblDoctorCode.Enabled = false;
                            //      CblDoctorCode.CssClass = "curser";


                        }
                    }
                }

            }

        }
        else
        {
            FlUploadcsv.Enabled = false;
            btnUpload.Enabled = false;
            lnkDelete.Enabled = false;
            btnGen.Enabled = true;
            lnkDownload.Enabled = false;
            //   lnkDownload.Attributes.Add("class", "border1");
            lnkDownload.ForeColor = System.Drawing.Color.Gray;

            lnkDelete.ForeColor = System.Drawing.Color.Gray;
        }

    }
    private void ImporttoDatatable()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {

                string excelPath = Server.MapPath("~/Upload_Document/") + Path.GetFileName(FlUploadcsv.PostedFile.FileName);

                // if (!System.IO.File.Exists(excelPath))
                //  {
                FlUploadcsv.SaveAs(excelPath);
                //  }
                //  else
                //  {
                // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>createCustomAlert('Selected File is Already Exists')</script>");
                // }

                string conString = string.Empty;
                string extension = Path.GetExtension(FlUploadcsv.PostedFile.FileName);
                switch (extension)
                {
                    case ".xls": //Excel 97-03
                        conString = ConfigurationManager.ConnectionStrings["Excel03ConString"].ConnectionString;
                        break;
                    case ".xlsx": //Excel 07 or higher
                        conString = ConfigurationManager.ConnectionStrings["Excel07+ConString"].ConnectionString;
                        break;

                }
                conString = string.Format(conString, excelPath);

                //string connStr = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties=Excel 12.0;";

                string connStr = "";

                if (Path.GetExtension(excelPath) == ".xls")
                {
                    connStr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + excelPath + ";Extended Properties=\"Excel 8.0;HDR=Yes;IMEX=2\"";
                }
                else if (Path.GetExtension(excelPath) == ".xlsx")
                {
                    connStr = @"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + excelPath + ";Extended Properties='Excel 12.0;HDR=YES;IMEX=1;';";
                }

                DataTable dtCloned = new DataTable();
                using (OleDbConnection excel_con = new OleDbConnection(connStr))
                {
                    excel_con.Open();
                    string sheet1 = excel_con.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null).Rows[0]["TABLE_NAME"].ToString();
                    DataTable dtExcelData = new DataTable();

                    using (OleDbDataAdapter oda = new OleDbDataAdapter("SELECT * from [" + sheet1 + "]", excel_con))
                    {
                        //ds = new DataSet();
                        //oda.Fill(ds);
                        //Dt = ds.Tables[0];

                        ds = new DataSet();
                        oda.Fill(ds);
                        //  Dt = ds.Tables[0];
                        dtCloned = ds.Tables[0];

                        //  oda.Fill(dbset);
                        // Dt.AcceptChanges();

                        //objAdapter1.Fill(ds);
                        //Dt = ds.Tables[0];
                    }
                    //excel_con.Close();
                    excel_con.Close();

                    Dt = dtCloned.Clone();
                    Dt.Columns[1].DataType = typeof(string);
                    foreach (DataRow r in dtCloned.Rows)
                    {
                        Dt.ImportRow(r);
                    }
                }
            }
        }
        catch (Exception ex)
        {

        }
    }

    private void InsertData()
    {
        try
        {
            if (FlUploadcsv.HasFile)
            {
                string StrSpec_Code = string.Empty;
                string StrTerritory_Code = string.Empty;
                string StrCat_Code = string.Empty;
                string StrCls_Code = string.Empty;
                string strUsername = string.Empty;
                string Doc_Type = string.Empty;
                string StrQua_Code = string.Empty;
                string StrSpec_SName = string.Empty;
                string StrCat_SName = string.Empty;
                string StrCls_SName = string.Empty;
                string StrQua_SName = string.Empty;
                string strUploadMessage = string.Empty;
                int ListerDrCode;
                int selectedCount = CblDoctorCode.Items.Cast<ListItem>().Count(li => li.Selected);
                string Terr_Cat = string.Empty;
                dtc = Dt.Copy();
                dtc.Columns.Add("Excel_RowNo", typeof(String));
                dtc.Columns.Add("Remarks", typeof(String));
                dtcopy = dtc.Clone();
                //  dtcopy.Columns.Add("Remarks", typeof(String));
                if (Dt.Columns.Count != selectedCount)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Excel Format is Not Matched');window.location='Listeddr_BulkUpload_Dynamic_Bk.aspx'</script>");

                }
                else if (Dt.Rows.Count == 0)
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No rows available in the Excel');window.location='Listeddr_BulkUpload_Dynamic_Bk.aspx'</script>");

                }
                
                else
                {
                    conn.Open();
                    for (int i = 0; i < Dt.Rows.Count; i++)
                    {
                        DataRow row = Dt.Rows[i];
                        DataRow ro = dtc.Rows[i];


                        int columnCount = Dt.Columns.Count;
                        string[] columns = new string[columnCount];
                        DB_EReporting db = new DB_EReporting();
                        SalesForce sf = new SalesForce();
                        string emailPattern = @"^\w+@[a-zA-Z_]+?\.[a-zA-Z]{2,3}$"; // Email address pattern  
                        string zipCodePattern = @"^\d{3}\s?\d{3}$";
                        string phonePattern = @"^[2-9]\d{2}-\d{3}-\d{4}$"; // US Phone number pattern   
                        string number = @"^[0-9]+$";
                        string hq = @"^(?:[1-4]|0[1-4]|4)$";
                        //bool isEmailValid = Regex.IsMatch(txtEmail.Text, emailPattern);
                        //bool isZipValid = Regex.IsMatch(txtZipCode.Text, zipCodePattern);
                        //bool isPhoneValid = Regex.IsMatch(txtPhone.Text, phonePattern); 
                        string date = @"^(0[1-9]|[12][0-9]|3[01])[- /.](0[1-9]|1[012])[- /.](19|20)\d\d$";
                        //string date = @"^(0[1-9]|1[012])[- /.](0[1-9]|[12][0-9]|3[01])[- /.](19|20)\d\d$";

                        string user = string.Empty;
                        string doc_name = string.Empty;
                        string terr_name = string.Empty;
                        string cat = string.Empty;
                        string spe = string.Empty;
                        string remarks = string.Empty;
                        string row_no = string.Empty;
                        string dob_dt = string.Empty;
                        string dow_dt = string.Empty;
                        string num = string.Empty;
                        string num1 = string.Empty;
                        string num2 = string.Empty;
                        string num3 = string.Empty;
                        string num4 = string.Empty;
                        string num5 = string.Empty;
                        string num6 = string.Empty;
                        string desc = string.Empty;
                        string terrcat = string.Empty;
                        bool isValid = true;
                        bool isValid2 = true;

                        bool isnum = true;
                        bool isnum1 = true;
                        bool isnum2 = true;
                        bool isnum3 = true;
                        bool isnum4 = true;
                        bool isnum5 = true;
                        bool isnum6 = true;
                        bool tercat = true;
                        int rowno = i + 2;
                        int prd = 0;

                        for (int j = 0; j < columnCount; j++)
                        {
                            columns[j] = row[j].ToString().Trim();
                            DataColumn col = Dt.Columns[j];

                            string strQry = "select Column_Db from Upload_Excel_ColumnNames where Column_Name ='" + col.ColumnName.Trim() + "'";
                            DataSet dtd = db.Exec_DataSet(strQry);
                            if (dtd.Tables[0].Rows.Count > 0)
                            {
                                Dt.Columns[j].ColumnName = dtd.Tables[0].Rows[0]["Column_Db"].ToString();
                            }
                            else
                            {
                                Dt.Columns[j].ColumnName = Dt.Columns[j].ColumnName;
                            }


                            DataSet dsGetLstDocName = new DataSet();
                            Doctor doc = new Doctor();
                            ListedDR lstDR = new ListedDR();
                            if (Dt.Columns[j].ColumnName == "Sf_Code")
                            {
                                dsSalesForce = sf.getSfCode_Upload(columns[j].Replace("'", " ").Trim(), div_code);
                                if (dsSalesForce.Tables[0].Rows.Count > 0)
                                {
                                    strUsername = dsSalesForce.Tables[0].Rows[0][0].ToString();
                                    //  columns[j] =strUsername;
                                    Dt.Rows[i][j] = strUsername;
                                }
                                else
                                {

                                }

                            }
                            //else if (Dt.Columns[j].ColumnName == "Territory_Code")
                            //{
                            //    DataSet dslstDR = new DataSet();
                            //    dslstDR = lstDR.GetTerritory_Upload(columns[j].Trim(), strUsername, div_code);

                            //    if (dslstDR.Tables[0].Rows.Count > 0)
                            //    {
                            //        StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                            //       // columns[j] = StrTerritory_Code;
                            //    }

                            //}
                            else if (Dt.Columns[j].ColumnName == "Doc_Cat_Code")
                            {

                                dslstCat = lstDR.GetDoc_Cat_Code(columns[j].Replace("'", " ").Trim(), div_code);
                                if (dslstCat.Tables[0].Rows.Count > 0)
                                {
                                    StrCat_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                                    //Dt.Rows[i][j] = StrCat_Code;
                                }
                                else
                                {
                                    //foreach (DataRow dr in dtc.Rows)
                                    //{
                                    //    if (dtc.Rows[i] == dr)
                                    //    {

                                    //        dtcopy.Rows.Add(dr.ItemArray);
                                    //    }
                                    //}
                                    //   dtc.Rows[i][j] = "Category Name Not Exists";

                                }
                            }
                            else if (Dt.Columns[j].ColumnName == "Doc_Special_Code")
                            {

                                dsSpec = lstDR.GetCategory_Special_Code(columns[j].Trim(), div_code);
                                if (dsSpec.Tables[0].Rows.Count > 0)
                                {
                                    StrSpec_Code = dsSpec.Tables[0].Rows[0][0].ToString();
                                    // Dt.Rows[i][j] = StrSpec_Code;
                                }
                                else
                                {

                                    //   dtc.Rows[i][j] = "Speciality Name Not Exists";

                                }
                            }
                            else if (Dt.Columns[j].ColumnName == "Territory_Cat")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    tercat = Regex.IsMatch(columns[j].Trim(), hq);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "ListedDR_DOB")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    string dd = columns[j].Trim().Replace("00:00:00", "");
                                    isValid = Regex.IsMatch(dd.Trim(), date);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "ListedDR_DOW")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    string dd2 = columns[j].Trim().Replace("00:00:00", "");
                                    isValid2 = Regex.IsMatch(dd2.Trim(), date);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "No_of_Visit")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    isnum = Regex.IsMatch(columns[j].Trim(), number);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "Dr_Business_Value")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    isnum1 = Regex.IsMatch(columns[j].Trim(), number);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "Dr_Potential")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    isnum2 = Regex.IsMatch(columns[j].Trim(), number);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "visit_Session")
                            {
                                string prod = string.Empty;
                                DataSet dsprod = new DataSet();

                                if (columns[j].Trim() != "")
                                {
                                    string[] strProductSplit = columns[j].Trim().Split('/');

                                    foreach (string strprod in strProductSplit)
                                    {
                                        if (strprod != "")
                                        {


                                            string strQry2 = "SELECT Map_Product_Code from mas_product_detail where Division_code='" + div_code + "' and Map_Product_Code='" + strprod + "' and Product_Active_Flag=0 ";

                                            dsprod = db.Exec_DataSet(strQry2);
                                            if (dsprod.Tables[0].Rows.Count > 0)
                                            {
                                                prod += dsprod.Tables[0].Rows[0]["Map_Product_Code"].ToString() + "/";
                                            }

                                        }
                                    }
                                    string[] strdes = prod.Trim().Split('/');
                                    if (strProductSplit.Length != strdes.Length)
                                    {
                                        prd = 1;
                                    }

                                }
                            }
                            else if (Dt.Columns[j].ColumnName == "Day_1")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    isnum3 = Regex.IsMatch(columns[j].Trim(), number);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "Day_2")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    isnum4 = Regex.IsMatch(columns[j].Trim(), number);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "Day_3")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    isnum5 = Regex.IsMatch(columns[j].Trim(), number);
                                }

                            }
                            else if (Dt.Columns[j].ColumnName == "Geo_Tag_Count")
                            {
                                if (columns[j].Trim() != "")
                                {
                                    isnum6 = Regex.IsMatch(columns[j].Trim(), number);
                                }

                            }
                        }
                        if (columns[3].Trim() == "" ||  columns[2].Trim() == "" || dsSalesForce.Tables[0].Rows.Count == 0 || dslstCat.Tables[0].Rows.Count == 0 || dsSpec.Tables[0].Rows.Count == 0 || dsSalesForce.Tables[0].Rows.Count == 0 || (isValid == false) || (isValid2 == false) || (isnum == false) || (isnum1 == false) || (isnum2 == false) || (isnum3 == false) || (isnum4 == false) || (isnum5 == false) || (isnum6 == false) || (tercat == false) || (prd == 1))
                        {
                            if (columns[2].Trim() == "")
                            {
                                doc_name = "Doctor Name Empty" + '/';
                            }
                            if (columns[3].Trim() == "")
                            {
                                terr_name = "Territory Name Empty" + '/';
                            }

                            if (dsSalesForce.Tables[0].Rows.Count == 0)
                            {
                                user = "User Name not Exists" + '/';
                            }
                            if (dslstCat.Tables[0].Rows.Count == 0)
                            {
                                cat = "Category Name not Exists" + '/';
                            }
                            if (dsSpec.Tables[0].Rows.Count == 0)
                            {
                                spe = "Speciality Name not Exists" + '/';
                            }
                            if (isValid == false)
                            {
                                dob_dt = "DOB Format Wrong" + '/';
                            }
                            if (isValid2 == false)
                            {
                                dow_dt = "DOW Format Wrong" + '/';
                            }
                            if (tercat == false)
                            {
                                terrcat = "Territory Type allowed Only Numbers (1 to 4)" + '/';
                            }
                            if (prd == 1)
                            {
                                desc = "Product Map Code Not Matched" + '/';
                            }
                            if (isnum == false)
                            {
                                num = "No of Visit allowed Only Numbers" + '/';
                            }
                            if (isnum1 == false)
                            {
                                num1 = "Doctor Business Value allowed Only Numbers" + '/';
                            }
                            if (isnum2 == false)
                            {
                                num2 = "Expected Business Value allowed Only Numbers" + '/';
                            }

                            if (isnum3 == false)
                            {
                                num3 = "DAY1 allowed Only Numbers" + '/';
                            }
                            if (isnum4 == false)
                            {
                                num4 = "DAY2 allowed Only Numbers" + '/';
                            }
                            if (isnum5 == false)
                            {
                                num5 = "DAY3 allowed Only Numbers" + '/';
                            }
                            if (isnum6 == false)
                            {
                                num6 = "Geo Tag Count allowed Only Numbers" + '/';
                            }
                            if (columns[1].Trim() == "" && columns[2].Trim() == "" && columns[3].Trim() == "" && columns[4].Trim() == "" && columns[5].Trim() == "")
                            {

                            }
                            else
                            {
                                remarks = doc_name + terr_name + user + cat + spe + terrcat + dob_dt + dow_dt + num + num1 + num2 + num3 + num4 + num5 + num6 + desc;
                                ro[dtc.Columns.Count - 2] = rowno;
                                ro[dtc.Columns.Count - 1] = remarks.Remove(remarks.Length - 1);
                                foreach (DataRow dr in dtc.Rows)
                                {

                                    if (dtc.Rows[i] == dr)
                                    {

                                        dtcopy.Rows.Add(dr.ItemArray);

                                    }
                                }
                            }

                        }


                    }
                    conn.Close();
                }
                if (dtcopy.Rows.Count > 0)
                {
                    pnlDr.Visible = true;
                    lnlnot.Focus();
                    // dtcopy.Columns.Remove("ListedDR_DOB");
                    List<string> listtoRemove = new List<string> { "Sl No", "User Name", "Listed Doctor Name", "Territory/Cluster(For DCR)", "Speciality", "Category", "Territory Type", "DOB(DD/MM/YY)", "DOW(DD/MM/YY)", "No of Visit", "Doctor Business Value", "Expected Business Value", "DAY1", "DAY2", "DAY3", "Geo Tag Count", "Product Code(p1/p2/p3)", "Remarks", "Excel_RowNo" };
                    for (int i = dtcopy.Columns.Count - 1; i >= 0; i--)
                    {
                        DataColumn dc = dtcopy.Columns[i];
                        if (listtoRemove.Contains(dc.ColumnName.Trim()))
                        {

                        }
                        else
                        {
                            dtcopy.Columns.Remove(dc);
                        }
                    }
                    GrdFixation.DataSource = dtcopy;
                    GrdFixation.DataBind();
                    System.IO.StringWriter sw = new System.IO.StringWriter();
                    System.Web.UI.HtmlTextWriter htw = new System.Web.UI.HtmlTextWriter(sw);

                    // Render grid view control.
                    GrdFixation.RenderControl(htw);
                    //User objUser = new User();
                    string filePath = Server.MapPath("~/Not_Uploaded_Doctor/");
                    string fileName = ("Not_Upl_Listeddr_Bulk_" + div_code + ".xls");

                    // Write the rendered content to a file.
                    string renderedGridView = sw.ToString();
                    System.IO.File.WriteAllText(filePath + fileName, renderedGridView);
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Upload Not Successful. Kindly download the below link for Error Records.');</script>");
                    int countCol = Dt.Columns.Count;

                }
                else
                {
                    pnlDr.Visible = false;
                    bool product = false;
                    //    DB_EReporting db = new DB_EReporting();
                    // SalesForce sf = new SalesForce();
                    ListedDR lstDR = new ListedDR();
                    //List<string> listtoRemove = new List<string> { "Sl_No" };
                    //for (int i = Dt.Columns.Count - 1; i >= 0; i--)
                    //{
                    //    DataColumn dc = Dt.Columns[i];
                    //    if (listtoRemove.Contains(dc.ColumnName.Trim()))
                    //    {
                    //        Dt.Columns.Remove(dc);
                    //    }
                    //    else
                    //    {

                    //    }
                    //}
                    using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
                    {
                        connection.Open();

                        SqlCommand command = connection.CreateCommand();
                        SqlTransaction transaction;

                        // Start a local transaction.
                        transaction = connection.BeginTransaction("SampleTransaction");

                        // Must assign both transaction object and connection
                        // to Command object for a pending local transaction
                        command.Connection = connection;
                        command.Transaction = transaction;

                        try
                        {
                            conn.Open();
                            string[] columnNames = Dt.Columns.Cast<DataColumn>()
                          .Select(x => x.ColumnName)
                          .ToArray();

                            var ValuetoReturn = (from Rows in Dt.AsEnumerable()
                                                 select Rows[columnNames[1]]).Distinct().ToList();
                            if (chkDeact.Checked == true)
                            {
                                foreach (var sf_code in ValuetoReturn)
                                {
                                    command.CommandText = "update Mas_ListedDr set ListedDr_Active_Flag = 1,listeddr_deactivate_date = getdate() where ListedDr_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + sf_code + "' ";
                                    command.ExecuteNonQuery();
                                }
                            }
                            for (int i = Dt.Rows.Count - 1; i >= 0; i--)
                            {
                                if (Dt.Rows[i][1] == DBNull.Value)
                                {
                                    Dt.Rows[i].Delete();
                                }
                            }
                            Dt.AcceptChanges();
                            foreach (DataRow dsrc in Dt.Rows)
                            {

                                //  string insertcommand = "insert into Mas_ListedDr ";
                                string cols = "";
                                string vals = "";
                                string Product_map = "";
                                string sf = string.Empty;
                                DateTime dtDob = new DateTime();
                                DateTime dtDow = new DateTime();
                                string dtDocDow = "";
                                string dtDocDob = "";
                                DataRow dr = dtc.NewRow();
                                foreach (DataColumn clm in Dt.Columns)
                                {
                                    //   dr[clm.ColumnName] = dsrc[clm.ColumnName].ToString();
                                    string terr_cat = string.Empty;
                                    string alias_name = string.Empty;

                                    if (clm.ColumnName == "Territory_Code")
                                    {
                                        if (Dt.Columns.Contains("Territory_Cat"))
                                        {

                                            terr_cat = dsrc["Territory_Cat"].ToString().Replace("'", " ").Trim();
                                        }
                                        else
                                        {
                                            terr_cat = "1";
                                        }
                                        if (dsrc["Cluster_Name"].ToString() != "")
                                        {
                                            alias_name = dsrc["Cluster_Name"].ToString().Replace("'", " ").Trim();
                                        }
                                        else
                                        {
                                            alias_name = dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim();
                                        }
                                        DataSet dslstDR = new DataSet();
                                        dslstDR = lstDR.GetTerritory_Upload_Deact(dsrc["Territory_Code"].ToString().Replace("'", " ").Trim(), dsrc["Sf_Code"].ToString(), div_code);

                                        if (dslstDR.Tables[0].Rows.Count > 0)
                                        {
                                            StrTerritory_Code = dslstDR.Tables[0].Rows[0][0].ToString();
                                            cols += "," + "Territory_Code";
                                            vals += "," + "'" + StrTerritory_Code.Trim() + "'";

                                            string strQry = "update Mas_Territory_Creation set Territory_Active_Flag=0,Territory_Deactive_Date=NULL where Territory_Code='" + StrTerritory_Code + "' and sf_code='" + dsrc["Sf_Code"].ToString() + "' ";
                                            SqlCommand cmd1 = new SqlCommand(strQry, conn);
                                            cmd1.ExecuteNonQuery();
                                        }
                                        if (dslstDR.Tables[0].Rows.Count == 0)
                                        {
                                            Territory terr = new Territory();
                                            int terrcode = terr.GetterrCode();
                                            string strQry = "insert into Mas_Territory_Creation (Territory_Code,Territory_Name,Territory_Cat,Territory_Sname,division_code, " +
                                                " SF_Code,Territory_Active_Flag,Created_date,Alias_Name) " +
                                                " VALUES('" + terrcode + "', '" + dsrc["Territory_Code"].ToString().Replace("'", " ").Trim() + "' , '" + terr_cat + "' , null, " +
                                                " '" + div_code + "', '" + dsrc["Sf_Code"].ToString() + "', 0, getdate(),'" + alias_name.Replace("'", " ").Trim() + "')";
                                            SqlCommand cmd1 = new SqlCommand(strQry, conn);
                                            cmd1.ExecuteNonQuery();

                                            DataSet dsTerritory = new DataSet();
                                            string strTerritoryName = "";
                                            dsTerritory = lstDR.getListedDr_TerritoryName(dsrc["Territory_Code"].ToString().Replace("'", " ").Trim(), dsrc["Sf_Code"].ToString());
                                            if (dsTerritory.Tables[0].Rows.Count > 0)
                                            {
                                                strTerritoryName = dsTerritory.Tables[0].Rows[0][0].ToString();
                                                cols += "," + "Territory_Code";
                                                vals += "," + "'" + strTerritoryName.Trim() + "'";
                                                //cols += "," + "Doc_Special_Code";
                                                //vals += "," + StrSpec_Code;
                                            }

                                        }
                                    }
                                    else if (clm.ColumnName == "Doc_Special_Code")
                                    {
                                        DataSet dslstSpec = new DataSet();
                                        dslstSpec = lstDR.GetCategory_Special_Code(dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim(), div_code);
                                        if (dslstSpec.Tables[0].Rows.Count > 0)
                                        {
                                            StrSpec_Code = dslstSpec.Tables[0].Rows[0][0].ToString();
                                            StrSpec_SName = dslstSpec.Tables[0].Rows[0][1].ToString();
                                            cols += "," + "Doc_Spec_ShortName";
                                            vals += "," + "'" + StrSpec_SName.Trim() + "'";
                                            cols += "," + "Doc_Special_Code";
                                            vals += "," + "'" + StrSpec_Code + "'";
                                        }
                                    }
                                    else if (clm.ColumnName == "Doc_Cat_Code")
                                    {
                                        DataSet dslstCat = new DataSet();
                                        dslstCat = lstDR.GetDoc_Cat_Code(dsrc[clm.ColumnName].ToString().Trim().Replace("'", " ").Trim(), div_code);
                                        if (dslstCat.Tables[0].Rows.Count > 0)
                                        {
                                            StrCat_Code = dslstCat.Tables[0].Rows[0][0].ToString();
                                            StrCat_SName = dslstCat.Tables[0].Rows[0][1].ToString();
                                            cols += "," + "Doc_Cat_ShortName";
                                            vals += "," + "'" + StrCat_SName.Trim() + "'";
                                            cols += "," + "Doc_Cat_Code";
                                            vals += "," + "'" + StrCat_Code + "'";
                                        }


                                    }
                                    else if (clm.ColumnName == "Doc_QuaCode")
                                    {
                                        if (dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim() != "")
                                        {
                                            DataSet dslstQua = new DataSet();
                                            dslstQua = lstDR.GetQua_Upload(dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim(), div_code);
                                            if (dslstQua.Tables[0].Rows.Count > 0)
                                            {
                                                StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                                                StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                                                cols += "," + "Doc_Qua_Name";
                                                vals += "," + "'" + StrQua_SName.Trim().Replace("'", " ") + "'";
                                                cols += "," + "Doc_QuaCode";
                                                vals += "," + "'" + StrQua_Code + "'";
                                            }
                                            else
                                            {
                                                ListedDR objQua = new ListedDR();
                                                int Doc_QuaCode = objQua.GetQua_code();



                                                string strQry1 = "INSERT INTO Mas_Doc_Qualification(Doc_QuaCode,Division_Code,Doc_QuaSName,Doc_QuaName,Doc_Qua_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                                         "values('" + Doc_QuaCode + "','" + div_code + "', '" + dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim() + "' , '" + dsrc[clm.ColumnName].ToString() + "' ,0,getdate(),getdate())";
                                                SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                                cmd2.ExecuteNonQuery();



                                                dslstQua = lstDR.GetQua_Upload(dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim(), div_code);
                                                if (dslstQua.Tables[0].Rows.Count > 0)
                                                {
                                                    StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                                                    StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                                                    cols += "," + "Doc_Qua_Name";
                                                    vals += "," + "'" + StrQua_SName.Trim().Replace("'", " ") + "'";
                                                    cols += "," + "Doc_QuaCode";
                                                    vals += "," + "'" + StrQua_Code + "'";
                                                }
                                            }
                                        }
                                        else
                                        {
                                            DataSet dslstQua = new DataSet();
                                            dslstQua = lstDR.GetQua_Upload("MBBS", div_code);
                                            if (dslstQua.Tables[0].Rows.Count > 0)
                                            {
                                                StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                                                StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                                                cols += "," + "Doc_Qua_Name";
                                                vals += "," + "'" + StrQua_SName.Trim().Replace("'", " ") + "'";
                                                cols += "," + "Doc_QuaCode";
                                                vals += "," + "'" + StrQua_Code + "'";
                                            }
                                            else
                                            {
                                                ListedDR objQua = new ListedDR();
                                                int Doc_QuaCode = objQua.GetQua_code();



                                                string strQry1 = "INSERT INTO Mas_Doc_Qualification(Doc_QuaCode,Division_Code,Doc_QuaSName,Doc_QuaName,Doc_Qua_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                                         "values('" + Doc_QuaCode + "','" + div_code + "', 'MBBS' , 'MBBS' ,0,getdate(),getdate())";
                                                SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                                cmd2.ExecuteNonQuery();



                                                dslstQua = lstDR.GetQua_Upload("MBBS", div_code);
                                                if (dslstQua.Tables[0].Rows.Count > 0)
                                                {
                                                    StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                                                    StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                                                    cols += "," + "Doc_Qua_Name";
                                                    vals += "," + "'" + StrQua_SName.Trim().Replace("'", " ") + "'";
                                                    cols += "," + "Doc_QuaCode";
                                                    vals += "," + "'" + StrQua_Code + "'";
                                                }
                                            }
                                        }
                                    }
                                    else if (clm.ColumnName == "Doc_ClsCode")
                                    {
                                        DataSet dslstCls = new DataSet();
                                        dslstCls = lstDR.GetClass_Code(dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim(), div_code);
                                        if (dslstCls.Tables[0].Rows.Count > 0)
                                        {
                                            StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
                                            StrCls_SName = dslstCls.Tables[0].Rows[0][1].ToString();
                                            cols += "," + "Doc_Class_ShortName";
                                            vals += "," + "'" + StrCls_SName.Trim() + "'";
                                            cols += "," + "Doc_ClsCode";
                                            vals += "," + "'" + StrCls_Code + "'";
                                        }
                                        else
                                        {
                                            dslstCls = lstDR.GetClass_Code("Nil", div_code);
                                            if (dslstCls.Tables[0].Rows.Count > 0)
                                            {
                                                StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
                                                StrCls_SName = dslstCls.Tables[0].Rows[0][1].ToString();
                                                cols += "," + "Doc_Class_ShortName";
                                                vals += "," + "'" + StrCls_SName.Trim() + "'";
                                                cols += "," + "Doc_ClsCode";
                                                vals += "," + "'" + StrCls_Code + "'";
                                            }
                                            else
                                            {
                                                ListedDR objcls = new ListedDR();
                                                int Doc_clsCode = objcls.GetCls_code();
                                                string strQry2 = "INSERT INTO Mas_Doc_Class(Doc_ClsCode,Division_Code,Doc_ClsSName,Doc_ClsName,Doc_Cls_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                   "values('" + Doc_clsCode + "','" + div_code + "','Nil', 'Nil',0,getdate(),getdate())";
                                                SqlCommand cmd2 = new SqlCommand(strQry2, conn);
                                                cmd2.ExecuteNonQuery();
                                                dslstCls = lstDR.GetClass_Code("Nil", div_code);
                                                if (dslstCls.Tables[0].Rows.Count > 0)
                                                {
                                                    StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
                                                    StrCls_SName = dslstCls.Tables[0].Rows[0][1].ToString();
                                                    cols += "," + "Doc_Class_ShortName";
                                                    vals += "," + "'" + StrCls_SName.Trim() + "'";
                                                    cols += "," + "Doc_ClsCode";
                                                    vals += "," + "'" + StrCls_Code + "'";
                                                }
                                            }
                                        }


                                    }
                                    else if (clm.ColumnName == "ListedDR_DOB")
                                    {

                                        if (dsrc["ListedDR_DOB"].ToString() != "")
                                        {
                                            dtDob = Convert.ToDateTime(dsrc["ListedDR_DOB"].ToString().Replace("00:00:00", ""));
                                            dtDocDob = dtDob.Month + "-" + dtDob.Day + "-" + dtDob.Year;
                                            cols += "," + "ListedDR_DOB";
                                            vals += "," + "'" + dtDocDob + "'";
                                        }
                                    }
                                    else if (clm.ColumnName == "ListedDR_DOW")
                                    {
                                        if (dsrc["ListedDR_DOW"].ToString() != "")
                                        {
                                            dtDow = Convert.ToDateTime(dsrc["ListedDR_DOW"].ToString().Replace("00:00:00", ""));
                                            dtDocDow = dtDow.Month + "-" + dtDow.Day + "-" + dtDow.Year;
                                            cols += "," + "ListedDR_DOW";
                                            vals += "," + "'" + dtDocDow + "'";
                                        }
                                    }
                                    else if (clm.ColumnName == "visit_Session")
                                    {
                                        product = true;
                                        Product_map = dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim();
                                        sf = dsrc["sf_code"].ToString().Trim();
                                    }
                                    if (clm.ColumnName != "Sl_No" && clm.ColumnName != "Doc_Special_Code" && clm.ColumnName != "Doc_Cat_Code" && clm.ColumnName != "Territory_Code" && clm.ColumnName != "Territory_Cat" && clm.ColumnName != "Doc_QuaCode" && clm.ColumnName != "Doc_ClsCode" && clm.ColumnName != "ListedDR_DOB" && clm.ColumnName != "ListedDR_DOW")
                                    {
                                        if (cols.Length > 0)
                                        {
                                            cols += ",[" + clm.ColumnName + "]";
                                        }
                                        else
                                        {
                                            cols = "[" + clm.ColumnName + "]";
                                        }
                                        if (vals.Length > 0)
                                        {

                                            vals += "," + "'" + dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim() + "'";

                                            // vals += "," + "'" + dsrc[clm.ColumnName].ToString() + "'";
                                        }
                                        else
                                        {
                                            vals = "'" + dsrc[clm.ColumnName].ToString().Replace("'", " ").Trim() + "'";

                                            //  vals += "," + "'" + Value + "'";

                                            //vals = "'" + dsrc[clm.ColumnName].ToString() + "'";
                                        }
                                    }

                                }
                               

                                DataColumnCollection col = dtcopy.Columns;
                                if (!col.Contains("Qualification"))
                                {
                                    DataSet dslstQua = new DataSet();
                                    dslstQua = lstDR.GetQua_Upload("MBBS", div_code);
                                    if (dslstQua.Tables[0].Rows.Count > 0)
                                    {
                                        StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                                        StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                                        cols += "," + "Doc_Qua_Name";
                                        vals += "," + "'" + StrQua_SName.Trim().Replace("'", " ") + "'";
                                        cols += "," + "Doc_QuaCode";
                                        vals += "," + "'" + StrQua_Code + "'";
                                    }
                                    else
                                    {
                                        ListedDR objQua = new ListedDR();
                                        int Doc_QuaCode = objQua.GetQua_code();



                                        string strQry1 = "INSERT INTO Mas_Doc_Qualification(Doc_QuaCode,Division_Code,Doc_QuaSName,Doc_QuaName,Doc_Qua_ActiveFlag,Created_Date,LastUpdt_Date)" +
                                                 "values('" + Doc_QuaCode + "','" + div_code + "', 'MBBS' , 'MBBS' ,0,getdate(),getdate())";
                                        SqlCommand cmd2 = new SqlCommand(strQry1, conn);
                                        cmd2.ExecuteNonQuery();



                                        dslstQua = lstDR.GetQua_Upload("MBBS", div_code);
                                        if (dslstQua.Tables[0].Rows.Count > 0)
                                        {
                                            StrQua_Code = dslstQua.Tables[0].Rows[0][0].ToString();
                                            StrQua_SName = dslstQua.Tables[0].Rows[0][1].ToString();
                                            cols += "," + "Doc_Qua_Name";
                                            vals += "," + "'" + StrQua_SName.Trim().Replace("'", " ") + "'";
                                            cols += "," + "Doc_QuaCode";
                                            vals += "," + "'" + StrQua_Code + "'";
                                        }
                                    }
                                }


                                if (!col.Contains("Class"))
                                {
                                    DataSet dslstCls = new DataSet();


                                        dslstCls = lstDR.GetClass_Code("Nil", div_code);
                                        if (dslstCls.Tables[0].Rows.Count > 0)
                                        {
                                            StrCls_Code = dslstCls.Tables[0].Rows[0][0].ToString();
                                            StrCls_SName = dslstCls.Tables[0].Rows[0][1].ToString();
                                            cols += "," + "Doc_Class_ShortName";
                                            vals += "," + "'" + StrCls_SName.Trim() + "'";
                                            cols += "," + "Doc_ClsCode";
                                            vals += "," + "'" + StrCls_Code + "'";
                                        }
                                        else
                                        {
                                            ListedDR objcls = new ListedDR();
                                            int Doc_clsCode = objcls.GetCls_code();
                                            string strQry2 = "INSERT INTO Mas_Doc_Class(Doc_ClsCode,Division_Code,Doc_ClsSName,Doc_ClsName,Doc_Cls_ActiveFlag,Created_Date,LastUpdt_Date)" +
                               "values('" + Doc_clsCode + "','" + div_code + "','Nil', 'Nil',0,getdate(),getdate())";
                                            SqlCommand cmd2 = new SqlCommand(strQry2, conn);
                                            cmd2.ExecuteNonQuery();
                                            dslstCls = lstDR.GetClass_Code("Nil", div_code);
                                            if (dslstCls.Tables[0].Rows.Count > 0)
                                            {
                                                cols += "," + "Doc_Class_ShortName";
                                                vals += "," + "'" + StrCls_SName.Trim() + "'";
                                                cols += "," + "Doc_ClsCode";
                                                vals += "," + "'" + StrCls_Code + "'";
                                            }
                                        }




                                    }
                               
                                ListedDR objListedDR = new ListedDR();
                                //  ListerDrCode = objListedDR.GetListedDrCode();
                                DataSet dsdr = new DataSet();
                                command.CommandText = "SELECT isnull(Max(ListedDrCode)+1,'1') ListedDrCode from Mas_ListedDr";

                                SqlDataAdapter daEx = new SqlDataAdapter(command);

                                daEx.Fill(dsdr);
                                string DrCode = dsdr.Tables[0].Rows[0]["ListedDrCode"].ToString();
                                command.CommandText = "insert into Mas_ListedDr (ListedDrCode,ListedDr_Sl_No,SLVNo,Division_Code,ListedDr_Active_Flag, ListedDr_Created_Date," + cols + ") values('" + DrCode + "','" + DrCode + "','" + DrCode + "'," + div_code + ",'0',getdate()," + vals + ")";
                                // SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                                command.ExecuteNonQuery();

                                if(product ==true)
                                {
                                    if(Product_map != "")
                                    {
                                        command.CommandText = " declare @Prod_Sl_No int " +
                                                              " SELECT @Prod_Sl_No = ((ISNULL(MAX(cast((Sl_No) as int)), 0)) + 1)   FROM map_lstdrs_product "+
                                                              " insert into map_lstdrs_product " +
                                                              " select @Prod_Sl_No - 1 + row_number() over (order by (select Uniq_slNo)) as Prod_Sl_No,ListedDrCode,Product_Code_SlNo, " +
                                                              " dense_rank()  OVER(PARTITION BY sf_code, ListedDrCode ORDER BY Map_Product_Code) as priorty,created_date,division_code,sf_code,Product_Detail_Name,ListedDr_Name " +
                                                              " from (select Sl_No, ROW_NUMBER() OVER(ORDER BY sf_code, ListedDrCode, Sl_No) Uniq_slNo, Sl_No_prirt, ListedDrCode, Product_Code_SlNo, "+
                                                              " ROW_NUMBER() OVER(PARTITION BY sf_code, ListedDrCode ORDER BY Sl_No) as priorty, " +
                                                              " created_date, division_code, sf_code, Product_Detail_Name, ListedDr_Name, Map_Product_Code " +
                                                              " from(select  ROW_NUMBER() OVER(ORDER BY a.sf_code, a.ListedDrCode, b.Map_Product_Code) Uniq_slNo, " +
                                                              " ROW_NUMBER() OVER(PARTITION BY sf_code, ListedDrCode, Map_Product_Code ORDER BY Map_Product_Code) as Sl_No_prirt, " +
                                                              " ROW_NUMBER() OVER(PARTITION BY a.sf_code, a.ListedDrCode, b.Map_Product_Code ORDER BY ListedDr_Name) as Sl_No, " +
                                                              " a.ListedDrCode, b.Product_Code_SlNo, getdate() as created_date, a.division_code, a.sf_code, b.Product_Detail_Name, a.ListedDr_Name, " +
                                                              " b.Map_Product_Code from mas_listeddr a, Mas_Product_Detail b where " +
                                                              " sf_code ='"+sf+"' and  ListedDr_Active_Flag = 0 and ListedDrCode ='"+DrCode+"' and " +
                                                              " b.division_code = a.division_code and  b.division_code = '"+div_code+"' and b.Product_Active_Flag = 0 " +
                                                              " and   charindex('/' + cast(b.Map_Product_Code as varchar) + '/', '/' + replace((replace(('" + Product_map+"'), '/ ', '/')), ' /', '/') + '/') > 0 " +
                                                              " )tt "+
                                                              " )ST order by sf_code, ListedDrCode, priorty";

                                        command.ExecuteNonQuery();

                                        command.CommandText = " declare @div_code varchar(20) " +
                                                              " ; with cte as( select * from( SELECT  listeddr_code, STUFF((SELECT distinct ',' + CAST(C.Product_code AS VARCHAR(500))[text()] "+
                                                              " FROM Map_LstDrs_Product C inner join mas_listeddr M on C.listeddr_code = M.listeddrcode and M.listeddrcode ='" + DrCode + "'  " +
                                                              " WHERE C.listeddr_code = t.listeddr_code and t.listeddr_code ='" + DrCode + "' and c.Division_Code = '" + div_code + "' and M.Division_Code = '" + div_code + "' " +
                                                              " FOR XML PATH(''), TYPE).value('.', 'NVARCHAR(MAX)'), 1, 1, '') Product "+
                                                              " FROM Map_LstDrs_Product t GROUP BY listeddr_code) as dd) "+
                                                              " UPDATE t2 SET t2.Map_ListedDr_Products = t1.Product  FROM cte AS t1  INNER JOIN mas_listeddr AS t2 "+
                                                              " ON t1.listeddr_code = t2.listeddrcode and t2.listeddrcode ='" + DrCode + "'  and   t2.Division_Code = '" + div_code + "'";
                                        command.ExecuteNonQuery();
                                    }
                                }

                            }
                            int user = ValuetoReturn.Count;
                            command.CommandText = "insert into Audit_Listeddr_Upload(Division_Code,Division_Name,Uploaded_Date,No_Of_Users,IP_Address) values ('" + div_code+"','"+Session["div_Name"].ToString()+"',getdate(),'"+ user + "', '"+Request.ServerVariables["REMOTE_ADDR"].ToString()+"')";
                            command.ExecuteNonQuery();

                            transaction.Commit();
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Successfully');</script>");
                            conn.Close();

                        }

                        catch (Exception ex)
                        {
                            Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                            Console.WriteLine("  Message: {0}", ex.Message);

                            // Attempt to roll back the transaction.
                            try
                            {
                                transaction.Rollback();
                            }
                            catch (Exception ex2)
                            {
                                Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                                Console.WriteLine("  Message: {0}", ex2.Message);
                            }
                            var message = new JavaScriptSerializer().Serialize(ex.Message.ToString());
                            var script = string.Format("{0}", message);
                            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('" + script + "');</script>");
                        }
                        connection.Close();
                       // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");

                    }



                    // ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Doctor Uploaded Sucessfully');</script>");

                }
            }


        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    protected void GrdFixation_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.Header)
        {
            //e.Row.Cells[0].BackColor = Color.Yellow;
            //e.Row.Cells[0].ForeColor = Color.Black;
            e.Row.Cells[1].BackColor = Color.Yellow;
            e.Row.Cells[1].ForeColor = Color.Black;
            e.Row.Cells[2].BackColor = Color.Yellow;
            e.Row.Cells[2].ForeColor = Color.Black;
            e.Row.Cells[3].BackColor = Color.Yellow;
            e.Row.Cells[3].ForeColor = Color.Black;
            e.Row.Cells[4].BackColor = Color.Yellow;
            e.Row.Cells[4].ForeColor = Color.Black;
            e.Row.Cells[5].BackColor = Color.Yellow;
            e.Row.Cells[5].ForeColor = Color.Black;
        }
    }


    protected void chkDeact_CheckedChanged(object sender, EventArgs e)
    {

    }
    protected void btnUpload_Click(object sender, EventArgs e)
    {
        ImporttoDatatable();

        InsertData();


    }
    protected void lnlnot_Click(object sender, EventArgs e)
    {
        try
        {
            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~/Not_Uploaded_Doctor/Not_Upl_Listeddr_Bulk_" + div_code + ".xls");
            if (!File.Exists(fileName))
            {
                Console.WriteLine("File Not exists.");
            }
            else
            {
                Response.AppendHeader("Content-Disposition", "attachment; filename=" + "Not_Upl_Listeddr_Bulk_" + div_code + ".xls");
                Response.TransmitFile(fileName);
                Response.End();
            }
        }
        catch (Exception ex)
        {
            // lblMessage.Text = ex.Message;
        }
    }
    //private void Deactivate()
    //{
    //    if (chkDeact.Checked == true)
    //    {
    //        conn.Open();
    //        string sql = "update Mas_ListedDr set ListedDr_Active_Flag = 1,listeddr_deactivate_date = getdate() where ListedDr_Active_Flag =0 and Division_Code = '" + div_code + "' and SF_Code = '" + strSf_Code + "' ";
    //        SqlCommand cmd = new SqlCommand(sql, conn);
    //        cmd.ExecuteNonQuery();
    //        conn.Close();
    //    }
    //}
    public override void VerifyRenderingInServerForm(Control control)
    {

    }
    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        try
        {


            Response.ContentType = "application/vnd.ms-excel";
            string fileName = Server.MapPath("~\\Document\\Listeddr_Bulk_" + div_code + ".xlsx");
            Response.AppendHeader("Content-Disposition", "attachment; filename=Listeddr_Bulk_" + div_code + ".xlsx");
            Response.TransmitFile(fileName);
            Response.End();

        }

        catch (Exception ex)
        {

            // lblMessage.Text = ex.Message;

        }
    }



    protected void btnGen_Click(object sender, EventArgs e)
    {
        btnGen.Enabled = false;
        CblDoctorCode.Enabled = false;
        int number = CblDoctorCode.Items.Count;
        int iReturn = -1;
        StringBuilder sw = new StringBuilder();
        string col_names = string.Empty;
        string col_db = string.Empty;
        string slno = string.Empty;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        for (int i = 0; i < number; i++)
        {
            if (CblDoctorCode.Items[i].Selected == true)
            {
                dst.Columns.Add(CblDoctorCode.Items[i].ToString());
                col_names += CblDoctorCode.Items[i].ToString() + ',';
                col_db += CblDoctorCode.Items[i].Value + ',';
            }

        }
        DB_EReporting db = new DB_EReporting();

        string strQry = "SELECT isnull(Max(Trans_Sl_No)+1,'1') Trans_Sl_No from Mas_Listeddoctor_Upload";

        DataSet ds = db.Exec_DataSet(strQry);

        slno = ds.Tables[0].Rows[0][0].ToString();

        string file = " Listeddr_Bulk_" + div_code + ".xlsx";
        string strQry2 = "insert into Mas_Listeddoctor_Upload (Trans_Sl_No,Division_Code,Upload_File_Name,Created_Date,Active_Flag,Column_Name,Column_Db) " +
                         " VALUES('" + slno + "', '" + div_code + "' , '" + file + "', getdate(),0, " +
                         " '" + col_names + "','" + col_db + "')";
        SqlCommand cmd1 = new SqlCommand(strQry2, con);
        cmd1.ExecuteNonQuery();

        con.Close();
        System.IO.StringWriter tw = new System.IO.StringWriter();
        System.Web.UI.HtmlTextWriter hw = new System.Web.UI.HtmlTextWriter(tw);
        DataGrid dgGrid = new DataGrid();

        dgGrid.BorderWidth = Unit.Pixel(1);
        dgGrid.BorderColor = System.Drawing.Color.Black;
        dgGrid.BackColor = System.Drawing.Color.LightBlue;
        dgGrid.GridLines = GridLines.Both;
        dgGrid.CellPadding = 1;
       //dgGrid.Columns[1].HeaderStyle.BackColor = Color.Red;
        //ds.Columns.Add("PayS_Month");
        //ds.Columns.Add("PayS_Year");

        dgGrid.DataSource = dst;

        dgGrid.DataBind();


        hw.Write("<html xmlns:o=\"urn:schemas-microsoft-com:office:office\" ");
        hw.Write("xmlns:x=\"urn:schemas-microsoft-com:office:excel\" ");
        hw.Write("xmlns=\"http://www.w3.org/TR/REC-html40\"> ");
        hw.Write("<head> ");
        hw.Write("<!--[if gte mso 9]><xml> ");
        hw.Write("<x:ExcelWorkbook> ");
        hw.Write("<x:ExcelWorksheets> ");
        hw.Write("<x:ExcelWorksheet> ");
        hw.Write("<x:Name>Sheet1</x:Name> ");
        hw.Write("<x:WorksheetOptions> ");
        hw.Write("<x:Selected/> ");
        hw.Write("<x:ProtectContents>False</x:ProtectContents> ");
        hw.Write("<x:ProtectObjects>False</x:ProtectObjects> ");
        hw.Write("<x:ProtectScenarios>False</x:ProtectScenarios> ");
        hw.Write("</x:WorksheetOptions> ");
        hw.Write("</x:ExcelWorksheet> ");
        hw.Write("</x:ExcelWorksheets> ");
        hw.Write("</x:ExcelWorkbook> ");
        hw.Write("</xml><![endif]--> ");
        hw.Write("</head>");
        hw.WriteLine("");

        StringBuilder sb = new StringBuilder();
        dgGrid.RenderControl(hw);
        //string FileName = Server.MapPath("~/Upload_Document/PaySlip_" + div_code + ".xlsx");
        //Request.Files["xlsFile"].SaveAs(FileName);
        // string path = Server.MapPath("~/Document/");
        // System.IO.FileInfo objFileInfo = new System.IO.FileInfo(path);
        //  objFileInfo.IsReadOnly = false;
        //if (!Directory.Exists(path))

        // {           
        //    Directory.CreateDirectory(path);       
        //}
        //if (File.Exists(@"~/Document/PaySlip_" + div_code + ".xlsx"))
        //{
        //    string file_Path = @"~/Document/PaySlip_" + div_code + ".xlsx";
        //    File.Delete(file_Path);
        //}
        //using (StringWriter sw1 = new StringWriter(sb))       
        //{           
        //    using (HtmlTextWriter hw1 = new HtmlTextWriter(sw1))           
        //    {
        //        StreamWriter writer = File.AppendText(path + "PaySlip_" + div_code + ".xlsx");

        //        hw1.BeginRender();                             
        //        string html = sb.ToString();           
        //        writer.WriteLine(html);
        //    }     
        //}


        //  Response.ContentType = "application/vnd.ms-excel";
        //Response.ContentType = "application/application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";

        //  Response.AppendHeader("Content-Disposition",
        //                        "attachment; filename= PaySlip_" + div_code + ".xls");
        //  System.IO.FileInfo objFileInfo1 = new System.IO.FileInfo(path);
        //  objFileInfo1.IsReadOnly = false;

        using (XLWorkbook wb = new XLWorkbook())
        {
            wb.Worksheets.Add(dst, "Sheet1");
            var ws = wb.Worksheet(1);
            var rngHeaders = ws.Range("A1:G1");
            rngHeaders.Style.Fill.BackgroundColor = XLColor.LightSalmon;
        

            Response.Clear();
            Response.Buffer = true;
            Response.Charset = "";
            Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
            Response.AddHeader("content-disposition", "attachment;filename=" + "Listeddr_Bulk_" + div_code + ".xlsx");
            string filePath = "Listeddr_Bulk_" + div_code + ".xlsx";

            //add your destination folder

            using (MemoryStream MyMemoryStream = new MemoryStream())
            {
                wb.SaveAs(MyMemoryStream);
                FileStream fileStream = new FileStream(Server.MapPath("~/Document/Listeddr_Bulk_" + div_code + ".xlsx"), FileMode.Create, FileAccess.Write, FileShare.Write);

                //MyMemoryStream.WriteTo(Response.OutputStream);
                //Response.Flush();
                //Response.End();

                MyMemoryStream.WriteTo(fileStream);
                fileStream.Close();
                MyMemoryStream.WriteTo(Response.OutputStream);
                MyMemoryStream.Close();

                Response.Flush();
                Response.End();
            }
            //System.IO.FileInfo objFileInfo1 = new System.IO.FileInfo(path);
            //objFileInfo1.IsReadOnly = false;
        }


        //      this.EnableViewState = false;

        Response.Write(tw.ToString());
        Response.End();
        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Excel Genereated Successfully');window.location='Listeddr_BulkUpload_Dynamic.aspx'</script>");
        //  getFile();
    }

    protected void lnkDelete_Click(object sender, EventArgs e)
    {

        string path = Server.MapPath("~/Document/Listeddr_Bulk_" + div_code + ".xlsx");
        FileInfo file = new FileInfo(path);
        if (file.Exists)//check file exsit or not  
        {
            file.Delete();

        }
        else
        {

        }
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
        con.Open();
        string strQry2 = "Update Mas_Listeddoctor_Upload set Active_Flag=1,Updated_Date =getdate()  where Division_code='" + div_code + "' and Active_Flag=0";
        SqlCommand cmd1 = new SqlCommand(strQry2, con);
        cmd1.ExecuteNonQuery();


        //CblDoctorCode.ClearSelection();
        //foreach (ListItem item in CblDoctorCode.Items)
        //{
        //    if ((item.Value) == "ListedDr_Name" || (item.Value) == "Sf_Code" || (item.Value) == "Territory_Code" || (item.Value) == "Cluster_Name" || (item.Value) == "Doc_Special_Code" || (item.Value) == "Doc_Cat_Code")
        //    {
        //        item.Selected = true;
        //        item.Enabled = false;
        //        item.Attributes.Add("class", "border1");
        //    }
        //    else
        //    {
        //        item.Enabled = true;
        //    }
        //}
        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Excel Deleted Successfully');window.location='Listeddr_BulkUpload_Dynamic_Bk.aspx'</script>");
        con.Close();
        //getFile();
    }
}
using Bus_Objects;
using DBase_EReport;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;
using System.Transactions;
using System.Web;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_Doctor_Business_Entry : System.Web.UI.Page
{

    string div_code = string.Empty;
    DataSet dsTPYear = new DataSet();



    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            string SFCode = Session["sf_code"].ToString();
            div_code = Session["div_code"].ToString();


            BusinessEntryDL BEL = new BusinessEntryDL();
            BusinessEntry.SFDetails SFDet = BEL.get_SFDetails(SFCode);
            hSF_Code.Value = SFDet.SFCode;
            hSFTyp.Value = SFDet.SFtype.ToString();
            SFDet.Div = Convert.ToInt32(div_code);
            hDiv.Value = SFDet.Div.ToString();
            SFInf.InnerHtml = SFDet.SFName;


            SFDet.SysIP = Request.ServerVariables["REMOTE_ADDR"];
            if (Request.Browser.IsMobileDevice) SFDet.ETyp = "Mobile"; else SFDet.ETyp = "Desktop";

            //DCREntry.DCRSetup Setup = BL.getSetups(SFDet);

            ddlYear.Items.Clear();
            int currentYear = DateTime.Now.Year;
            ddlYear.Items.Add((currentYear - 1).ToString());
            ddlYear.Items.Add(currentYear.ToString());
            ddlYear.Items.Add((currentYear + 1).ToString());
            ddlYear.SelectedValue = currentYear.ToString();
            ddlMonth.SelectedValue = (DateTime.Now.Month - 1).ToString();


            DB_EReporting db_ER = new DB_EReporting();


            if (SFDet.SFtype == 2)
            {
                aHome.HRef = "../MGR_Home.aspx";
            }
            else if (SFDet.SFtype == 3)
            {
                aHome.HRef = "../BasicMaster.aspx";
            }
            else
            {
                aHome.HRef = "../default_MR.aspx";
            }

            Path.GetFileName(Request.Url.AbsolutePath);

            string scrpt = "clearMasterData();showHideBlockMsg();setData('SFDet'," + JsonConvert.SerializeObject(SFDet) + ");";

            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);


            //Plchold_HQ.Visible = false;


            ListItem DfItm = new ListItem("-- Select The Work Type --", "");
            string ScriptValues = "";



            List<BusinessEntry.SFDetails> BSFs = BEL.get_BaseSFs(SFDet.SFCode, SFDet.Div.ToString());
            //List<BusinessEntry.SFDetails> BSFs = BEL.get_BaseSFs(SFDet.SFCode);
            ddl_HQ.DataSource = BSFs;
            ddl_HQ.DataValueField = "SFCode";
            ddl_HQ.DataTextField = "SFName";
            ddl_HQ.DataBind();
            for (int il = 0; il < ddl_HQ.Items.Count; il++)
            {
                ListItem item = ddl_HQ.Items[il];
                item.Attributes["data-sfdiv"] = BSFs[il].Div.ToString();
            }
            DfItm = new ListItem("-- Select The Headquater --", "");
            ddl_HQ.Items.Insert(0, DfItm);
            DfItm = new ListItem("-- Select The Clusters Name --", "");
            ddl_SDP.Items.Insert(0, DfItm);

            lbSDP.InnerHtml = "Territory Name";
            string prodJSON = BEL.getBusinessProdsJSON(SFDet.Div.ToString(), SFDet.State.ToString());

            ScriptValues += "d_Prod = " + prodJSON + ";setData('d_Prod',d_Prod);";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DatasLoad", ScriptValues, true);
        }
    }
    [WebMethod]
    public static string GetClusterList(string SF)
    {
        BusinessEntryDL BL = new BusinessEntryDL();
        string TwnJSON = BL.getClustersJSON(SF);
        return TwnJSON;
    }
    [WebMethod]
    public static string GetDoctorList(string SF)
    {
        BusinessEntryDL BEL = new BusinessEntryDL();
        string DocJSON = BEL.getDoctor_JSON(SF);
        return DocJSON;
    }


    public static void Log(string logMessage)
    {
        using (StreamWriter w = File.AppendText(HttpContext.Current.Server.MapPath("/DCR") + "/log/log_DrBusiness" + DateTime.Today.ToString("yyy_MM_dd") + ".txt"))
        {
            w.WriteLine("Log Entry :{0} {1} {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), logMessage);
            w.WriteLine("----------------------------------------------------------------------------------------------------------");
        }
    }




    [WebMethod(EnableSession = true)]
    public static string GetBusinessEntry_TerritoryWise(string sf_Code, string Month, string Year, string Terri_code)
    {

        BusinessEntryDL BEL = new BusinessEntryDL();
        string DCRJSON = BEL.GetBusinessEntry_TerritoryWise(sf_Code, Month, Year, Terri_code);
        return DCRJSON;

    }

    [WebMethod(EnableSession = true)]
    public static string Get_AllReport(string sf_Code, string Month, string Year)
    {
        BusinessEntryDL BEL = new BusinessEntryDL();
        string SFCJSON = BEL.Get_AllReport(sf_Code, Month, Year);
        return SFCJSON;

    }




    [WebMethod]
    public static string Save_Business_Entry_Single_Data(string SFData, string DCRData)
    {

        BusinessEntryDL BEL = new BusinessEntryDL();
        BusinessEntry.DCRDatas Data = JsonConvert.DeserializeObject<BusinessEntry.DCRDatas>(DCRData);
        BusinessEntry.SFDetails SFDet = JsonConvert.DeserializeObject<BusinessEntry.SFDetails>(SFData);
        Data.Div = (byte)SFDet.Div;
        string DataID = "";
        using (var scope = new TransactionScope())
        {
            try
            {
                string Detstr = "[";

                //if (Data.ETabs.IndexOf("D") > -1)
                //{
                for (int il = 0; il < Data.MslData.Count; il++)
                {
                    if (Detstr != "[") Detstr += ",";
                    Detstr += BEL.SaveBusinessEntry(SFDet.SFCode, 1, SFDet.Div, Data.MslData[il], Data.Month, Data.Year);
                    DataID = "Msl:" + Data.MslData[il].cus.val;
                }
                //}


                Detstr += "]";


                scope.Complete();
                scope.Dispose();
                string JWJSON = "{\"success\":true}";

                return JWJSON;

            }
            catch (Exception ex)
            {
                scope.Dispose();
                var errmsg = "SF :" + SFDet.SFCode + " Error on:" + DataID + "\r\nmsg:" + ex.Message + "\r\nData:" + DCRData;
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();

                foreach (StackFrame frame in frames)
                {
                    if (frame.GetFileName() != "")
                        errmsg += "\n" + frame.GetFileName() + ":" + frame.GetMethod().Name + "(" + frame.GetFileLineNumber() + "," + frame.GetFileColumnNumber() + ")";
                }

                Log(errmsg);
                throw ex;
            }
            finally { BEL = null; }
        }
    }

    [WebMethod(EnableSession = true)]
    public static string Delete_Business_Details(string sf_codee, string div_codee, string Dr_Code, string hndMonth, string hndYear)
    {
        try
        {
            DB_EReporting db = new DB_EReporting();
            int iReturn = -1;

            string strQry = "EXEC Sp_Business_Entry_Delete '" + sf_codee + "','" + div_codee + "','" + Dr_Code + "','" + hndMonth + "','" + hndYear + "'";
            iReturn = db.ExecQry(strQry);

            string svJSON = "{\"success\":true}";
            return svJSON;

        }
        catch (Exception ex)
        {
            return "{\"success\":false, \"message\":\"" + ex.Message.Replace("\"", "\\\"") + "\"}";
        }
    }



}
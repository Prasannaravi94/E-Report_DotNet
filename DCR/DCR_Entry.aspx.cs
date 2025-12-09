using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Diagnostics;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Text;
using System.Text.RegularExpressions;
using System.Data.SqlClient;
using System.Configuration;
using System.Data;
using System.Transactions;
using Newtonsoft.Json;
using Bus_Objects;
using Bus_EReport;
using DBase_EReport;

public partial class DCR_DCR_Entry : System.Web.UI.Page
{
    static string Sf_Code = string.Empty;
    static string Activity_Date = string.Empty;
    static string Div_Code = string.Empty;
    static string Dev_Reason = string.Empty;
    static string TP_Area = string.Empty;
    static string TP_Worktype = string.Empty;
    static string SF_Name = string.Empty;
    static string TPDCR_MGRAppr = string.Empty;
    static string sTerritory_Code = string.Empty;

    DataSet dsTPC = null;
    string Terr_Cat = string.Empty;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();

    protected void Page_Load(object sender, EventArgs e)
    {

        if (!Page.IsPostBack)
        {
            string SFCode = Session["sf_code"].ToString();
            DCREntryBL BL = new DCREntryBL();
            DCREntry.SFDetails SFDet = BL.get_SFDetails(SFCode);
            hSF_Code.Value = SFDet.SFCode;
            hSFTyp.Value = SFDet.SFtype.ToString();
            hDiv.Value = SFDet.Div.ToString();
            SFInf.InnerHtml = SFDet.SFName;

            SFDet.SysIP = Request.ServerVariables["REMOTE_ADDR"];
            if (Request.Browser.IsMobileDevice) SFDet.ETyp = "Mobile"; else SFDet.ETyp = "Desktop";

            DCREntry.DCRSetup Setup = BL.getSetups(SFDet);

            var DtDet = BL.GetDCRDtDet(SFDet, Setup);
            Path.GetFileName(Request.Url.AbsolutePath);
            DtInf.InnerHtml = DtDet.DCR_Date.ToString("dd/MM/yyyy") + " - " + DtDet.DCR_Date.DayOfWeek.ToString() + " " + ((DtDet.DTRem != "") ? " - <span class='stat" + DtDet.Type + "'>" + DtDet.DTRem + "</span>" : "");

            hSRem.Value = DtDet.DTRem.ToString();

            string month = DtDet.DCR_Date.ToString("MM");
            string year = DtDet.DCR_Date.Year.ToString();
            DataSet dsTP = null;
            string stQry = string.Empty;
            DB_EReporting db_ER = new DB_EReporting();
            //if (SFDet.SFtype == 1 && (SFDet.Div == 2 || SFDet.Div == 4))
            if (SFDet.Div == 2 || SFDet.Div == 4 || SFDet.Div == 3)
            {
                stQry = "SELECT * FROM trans_tp where sf_code= '" + SFDet.SFCode + "' and tour_month='" + Convert.ToInt32(month) + "' and tour_year='" + Convert.ToInt32(year) + "' ";
                dsTP = db_ER.Exec_DataSet(stQry);
                if (dsTP.Tables[0].Rows.Count > 0)
                {

                }
                else
                {
                    if (SFDet.SFtype == 1)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                             "alert",
                                             "alert('Tour plan is not submitted or Approved');window.location ='../Default_MR.aspx';",
                                             true);
                    }
                    else
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(),
                                            "alert",
                                            "alert('Tour plan is not submitted or Approved');window.location ='../MasterFiles/MGR/MGR_Index.aspx';",
                                            true);
                    }


                }
            }





            //string scrpt = "clearMasterData();lckMsg='" + DtDet.DtMsg + "';showHideBlockMsg();setData('SFDet'," + JsonConvert.SerializeObject(SFDet) + ");setData('Setup'," + JsonConvert.SerializeObject(Setup) + ");";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);

            string scrpt = "clearMasterData();lckMsg='" + DtDet.DtMsg + "';showHideBlockMsg();setData('SFDet'," + JsonConvert.SerializeObject(SFDet) + ");setData('Setup'," + JsonConvert.SerializeObject(Setup) + ");";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "SetupData", scrpt, true);

            hDCRDt.Value = DtDet.DCR_Date.ToString("MM/dd/yyyy");
            hDtTyp.Value = DtDet.Type.ToString();
            hSTime.Value = DtDet.STime.ToString("yyyy-MM-dd HH:mm:ss");
            hCurrDt.Value = DtDet.CurrDt.ToString("MM/dd/yyyy");
            Plchold_HQ.Visible = false;
            if (SFDet.SFtype != 1)
            {
                Plchold_HQ.Visible = true;
                aHome.HRef = "../MGR_Home.aspx";
            }
            else
                aHome.HRef = "../default_MR.aspx";

            aSS.HRef = "javascript:showModalPopUp('" + SFCode + "', '" + System.DateTime.Now.Month.ToString() + "', '" + System.DateTime.Now.Year.ToString() + "', '" + System.DateTime.Now.Month.ToString() + "', '" + System.DateTime.Now.Year.ToString() + "','" + SFInf.InnerHtml + "')";
            aIS.HRef = "javascript:showModalPopUp_Input('" + SFCode + "', '" + System.DateTime.Now.Month.ToString() + "', '" + System.DateTime.Now.Year.ToString() + "', '" + System.DateTime.Now.Month.ToString() + "', '" + System.DateTime.Now.Year.ToString() + "','" + SFInf.InnerHtml + "')";

            List<DCREntry.Worktypes> WTs = BL.get_WorkTypes(SFDet.SFCode);
            ddl_WorkType.DataSource = WTs;
            ddl_WorkType.DataValueField = "Code";
            ddl_WorkType.DataTextField = "Name";
            ddl_WorkType.DataBind();

            //DataSet dsSalesforce = new DataSet();
            //SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);

            //SqlCommand cmd = new SqlCommand("gethalf_Daywrk", conn);
            //cmd.CommandType = CommandType.StoredProcedure;
            //cmd.CommandTimeout = 240;
            //cmd.Parameters.Add("@divcode", SqlDbType.VarChar);
            //cmd.Parameters[0].Value = SFDet.Div.ToString();
            //SqlDataAdapter da = new SqlDataAdapter();
            //da.SelectCommand = cmd;
            //conn.Open();
            //da.Fill(dsSalesforce);
            //conn.Close();
            //List<string> RR_ID = new List<string>();
            //if (dsSalesforce.Tables[0].Rows.Count > 0)
            //{
            //    chkhaf_work.DataSource = dsSalesforce;
            //    chkhaf_work.DataTextField = "Worktype_Name_B";
            //    chkhaf_work.DataValueField = "WorkType_Code_B";
            //    chkhaf_work.DataBind();
            //}

            List<DCREntry.Clusters> WTs1 = BL.gethalf_Daywrk(SFDet.Div.ToString());
            string ajson = JsonConvert.SerializeObject(WTs1);
            string ScriptValues = "";
            //ScriptValues += "check = " + ajson + ";setData('check',check);";
            //ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DatasLoad", ScriptValues, true);

            for (int il = 0; il < ddl_WorkType.Items.Count; il++)
            {
                ListItem item = ddl_WorkType.Items[il];
                item.Attributes["data-eTabs"] = WTs[il].ETabs;
                item.Attributes["data-FWFlg"] = WTs[il].FWFlg;
            }

            ListItem DfItm = new ListItem("-- Select The Work Type --", "");
            ddl_WorkType.Items.Insert(0, DfItm);

            if (DtDet.Type > 0 && DtDet.Type < 3)
            {
                string DCRJSON = BL.getDCRDetails(SFDet.SFCode, DtDet.DCR_Date);
                ScriptValues += "$(window).bind('load', function () {setExistData('" + Regex.Replace(DCRJSON, "[\n\r\t]", " ") + "');});";
            }
            else if (SFDet.SFCode.Contains("MGR") && Setup.DCRJointWrk == "E")
            {
                string DCRJSON = BL.getDCRDetailsTrans(SFDet.SFCode, DtDet.DCR_Date);
                //DCRJSON = DCRJSON.Remove(DCRJSON.Length - 1);
                //DCRJSON += "}";

                if (DCRJSON.Length > 2)
                {
                    ScriptValues += "$(window).bind('load', function () {setExistData('" + Regex.Replace(DCRJSON, "[\n\r\t]", " ") + "');});";
                }


            }
            if (SFDet.SFtype == 1)
            {
                ddl_HQ.Items.Clear();
                DfItm = new ListItem("-- Select The Headquater --", "");
                ddl_HQ.Items.Insert(0, DfItm);
                DfItm = new ListItem(SFDet.SFName, SFDet.SFCode);
                DfItm.Attributes["data-sfdiv"] = SFDet.Div.ToString();
                ddl_HQ.Items.Insert(0, DfItm);
                ddl_HQ.SelectedValue = SFDet.SFCode;

                List<DCREntry.Clusters> TwnsCont = BL.getClustFilter(SFDet.SFCode, DtDet.DCR_Date);

                if (Setup.No_of_TP_View > 1 && !DtDet.DTRem.Contains("TP") && Setup.TPBaseDCRDr == 1 && DtDet.DCR_Date < System.DateTime.Now.Date.AddDays(1) && TwnsCont.Count > 0)
                {

                    //hTeCnt.Value = "1";
                    ddl_SDP.DataSource = TwnsCont;
                    ddl_SDP.DataValueField = "Code";
                    ddl_SDP.DataTextField = "Name";

                    for (int i = 0; i < ddl_SDP.Items.Count; i++)
                    {
                        var a = TwnsCont[i].PM_Visit;
                        ddl_SDP.Items[i].Attributes.Add("name", TwnsCont[i].Name.ToString());
                        ddl_SDP.Items[i].Attributes.Add("data-pmvisit", TwnsCont[i].PM_Visit.ToString());
                        ddl_SDP.Items[i].Attributes.Add("data-cmvisit", TwnsCont[i].CM_Visit.ToString());
                        ddl_SDP.Items[i].Attributes.Add("data-Territory_Cat", TwnsCont[i].Territory_Cat.ToString());
                    }

                    //drp.Items(i).Attributes.Add("data-siteId", dataSrc(i));
                    ddl_SDP.DataBind();
                    string sTwns = JsonConvert.SerializeObject(TwnsCont);
                    ScriptValues += "d_twns=" + sTwns.Replace("\"Code\"", "\"id\"").Replace("\"Name\"", "\"name\"") + ";setData('d_twn_" + SFCode + "',d_twns);";
                }
                else
                {
                    List<DCREntry.Clusters> Twns = BL.getClusters(SFDet.SFCode);
                    //hTeCnt.Value = "2";
                    ddl_SDP.DataSource = Twns;
                    ddl_SDP.DataValueField = "Code";
                    ddl_SDP.DataTextField = "Name";
                    ddl_SDP.DataBind();

                    for (int i = 0; i < ddl_SDP.Items.Count; i++)
                    {
                        var a = Twns[i].PM_Visit;
                        ddl_SDP.Items[i].Attributes.Add("name", Twns[i].Name.ToString());
                        ddl_SDP.Items[i].Attributes.Add("data-pmvisit", Twns[i].PM_Visit.ToString());
                        ddl_SDP.Items[i].Attributes.Add("data-cmvisit", Twns[i].CM_Visit.ToString());
                        ddl_SDP.Items[i].Attributes.Add("data-Territory_Cat", Twns[i].Territory_Cat.ToString());
                    }

                    string sTwns = JsonConvert.SerializeObject(Twns);
                    ScriptValues += "d_twns=" + sTwns.Replace("\"Code\"", "\"id\"").Replace("\"Name\"", "\"name\"") + ";setData('d_twn_" + SFCode + "',d_twns);";


                }
            }
            else
            {
                List<DCREntry.SFDetails> BSFs = BL.get_BaseSFs(SFDet.SFCode);
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

            }

            DfItm = new ListItem("-- Select The " + Setup.SDPCap + " Name --", "");
            ddl_SDP.Items.Insert(0, DfItm);

            DateTime dt = System.DateTime.Now.Date.AddDays(1);

            if (Setup.TPBaseDCRDr == 1 && DtDet.DCR_Date < System.DateTime.Now.Date.AddDays(1))
            {
                DCREntry.TPBaseDCR TPDCR = BL.get_TPBaseDCR(SFCode, DtDet.DCR_Date);
                if (TPDCR.WorkType != null && TPDCR.WorkType != "Leave")
                {
                    if (SFDet.SFCode.Contains("MR"))
                    {
                        if (TPDCR.Territory_Code != null)
                        {
                            //if (TPDCR.Territory_Code.Contains(","))
                            //{
                            hTerrType.Value = "1";
                            if (!(DtDet.DTRem.Contains("TP Deviation Released")))
                            {
                                for (int ihq = ddl_SDP.Items.Count - 1; ihq > 0; ihq--)
                                {
                                    if (!(TPDCR.Territory_Code.Contains(ddl_SDP.Items[ihq].Value)))
                                    {
                                        //ddl_SDP.Items.FindByValue(ddl_SDP.Items[ihq].Value).Attributes.Add("style", "display:none");
                                        ddl_SDP.Items.RemoveAt(ihq);
                                    }
                                }
                            }
                            //}
                            //else
                            //{
                            //    this.ddl_SDP.SelectedValue = TPDCR.Territory_Code;
                            //}


                            //this.ddl_SDP.SelectedValue = TPDCR.Territory_Code;
                            for (int il = 0; il < ddl_WorkType.Items.Count; il++)
                            {
                                if (ddl_WorkType.Items[il].Text == TPDCR.WorkType)
                                {
                                    ddl_WorkType.Items[il].Selected = true;
                                }
                            }

                            if (Setup.TPDCR_MGRAppr == 1 && DtDet.DCR_Date < System.DateTime.Now.Date.AddDays(1))
                            {
                                string[] Territory_Code = TPDCR.Territory_Code.Split(',');
                                bool count = false;
                                foreach (string Terri_Code in Territory_Code)
                                {
                                    strQry = "select Territory_Cat from Mas_Territory_Creation where Territory_Active_Flag=0 and Territory_Code='" + Terri_Code + "'";
                                    dsTPC = db_ER.Exec_DataSet(strQry);

                                    Terr_Cat = dsTPC.Tables[0].Rows[0]["Territory_Cat"].ToString();

                                    if (Terr_Cat == "1")
                                    {
                                        count = true;
                                    }
                                }

                                if (count == true)
                                {
                                    hTerrType.Value = "1";
                                    if (!(DtDet.DTRem.Contains("TP Deviation Released")))
                                    {
                                        for (int ihq = ddl_SDP.Items.Count - 1; ihq > 0; ihq--)
                                        {
                                            if (ddl_SDP.Items[ihq].Attributes["data-Territory_Cat"] != "1")
                                            {
                                                ddl_SDP.Items.RemoveAt(ihq);
                                            }
                                        }
                                    }
                                }

                                //strQry = "select Territory_Cat from Mas_Territory_Creation where Territory_Active_Flag=0 and Territory_Code='" + TPDCR.Territory_Code + "'";
                                //dsTPC = db_ER.Exec_DataSet(strQry);

                                //Terr_Cat = dsTPC.Tables[0].Rows[0]["Territory_Cat"].ToString();

                                //if (Terr_Cat == "1")
                                //{
                                //    hTerrType.Value = "1";
                                //    for (int ihq = ddl_SDP.Items.Count - 1; ihq > 0; ihq--)
                                //    {
                                //        if (ddl_SDP.Items[ihq].Attributes["data-Territory_Cat"] != "1")
                                //        {
                                //            ddl_SDP.Items.RemoveAt(ihq);
                                //        }
                                //    }
                                //}
                            }


                        }
                    }
                    else
                    {
                        //for (int ihq = 0; ihq < ddl_HQ.Items.Count; ihq++)
                        //{

                        //    if ((TPDCR.SF_Code != null) && (TPDCR.SF_Code.Contains(",MR")))
                        //    {
                        //        hTerrType.Value = "1";

                        //        if (!(TPDCR.SF_Code.Contains(ddl_HQ.Items[ihq].Value)) && !(DtDet.DTRem.Contains("TP Deviation Released")))
                        //        {
                        //            ddl_HQ.Items.FindByValue(ddl_HQ.Items[ihq].Value).Attributes.Add("style", "display:none");


                        //        }
                        //    }

                        //    else
                        //    {
                        //        if (ddl_HQ.Items[ihq].Value == TPDCR.SF_Code)
                        //        {
                        //            //DfItm = new ListItem(ddl_HQ.Items[ihq].Text, ddl_HQ.Items[ihq].Value);
                        //            //ddl_HQ.Items.Clear();
                        //            //ddl_HQ.Items.Insert(0, DfItm);
                        //            //ddl_HQ.SelectedValue = TPDCR.SF_Code;
                        //            //commented
                        //            ddl_HQ.Items[ihq].Selected = true;
                        //            break;
                        //        }
                        //    }
                        //}



                        for (int ihq = ddl_HQ.Items.Count - 1; ihq > 0; ihq--)
                        {
                           
                            if (TPDCR.SF_Code != null)
                            {
                                hTerrType.Value = "1";
                                if (!(TPDCR.SF_Code.Contains(ddl_HQ.Items[ihq].Value)) && !(DtDet.DTRem.Contains("TP Deviation Released")))
                                {
                                    ddl_HQ.Items.RemoveAt(ihq);

                                }
                            }
                        }

                        string scrpt1 = "sTPDCR=JSON.parse('" + JsonConvert.SerializeObject(TPDCR) + "');";
                        ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "TPDCR", scrpt1, true);

                        for (int il = 0; il < ddl_WorkType.Items.Count; il++)
                        {
                            if (ddl_WorkType.Items[il].Text == TPDCR.WorkType)
                            {
                                ddl_WorkType.Items[il].Selected = true;
                            }
                        }
                    }
                }
                else
                {
                    Setup.TPDCRDeviation = 0;
                }


            }

            lbSDP.InnerHtml = Setup.SDPCap + " Name";

            string prodJSON = BL.getProdsJSON(SFDet.SFCode, Setup.ProdSampleQtyValid);
            string inputJSON = BL.getInputJSON(SFDet.SFCode, DtDet.DCR_Date, Setup.InputQtyValid);

            string FedBkJSON = BL.getFeedBkJSON(SFDet.Div);
            string PFedBkJSON = BL.getProdFeedBkJSON(SFDet.Div);
            ScriptValues += "d_Prod = " + prodJSON + ";check = " + ajson + ";d_Input = " + inputJSON + ";d_fedbk=" + FedBkJSON + ";d_Rxs=" + PFedBkJSON + ";setData('d_Prod',d_Prod);setData('check',check);setData('d_Input',d_Input);setData('d_fedbk',d_fedbk);setData('d_Rxs',d_Rxs);";
            ScriptManager.RegisterClientScriptBlock(this, this.GetType(), "DatasLoad", ScriptValues, true);

            DataSet dsFlash = null;
            AdminSetup adm1 = new AdminSetup();
            string Flash = string.Empty;
            string FlashNews = string.Empty;
            string strDivSplit1 = SFDet.Div.ToString();

            if (strDivSplit1 != "")
            {
                dsFlash = adm1.Get_Flash_News_adm(strDivSplit1);

                if (dsFlash.Tables[0].Rows.Count > 0)
                {
                    Flash = dsFlash.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    FlashNews = Flash;
                    lblFlash.Text += FlashNews + "&nbsp;&nbsp;| &nbsp; &nbsp;";
                }

            }
        }
    }
    [WebMethod]
    public static string GetClusterList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string TwnJSON = BL.getClustersJSON(SF);
        return TwnJSON;
    }
    [WebMethod]
    public static string GetDoctorList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string DocJSON = BL.getDoctorJSON(SF);
        return DocJSON;
    }
    [WebMethod]
    public static string GetChemistList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string ChmJSON = BL.getChemistJSON(SF);
        return ChmJSON;
    }
    [WebMethod]
    public static string GetStockistList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string StkJSON = BL.getStockistJSON(SF);
        return StkJSON;
    }
    [WebMethod]
    public static string GetUnlistedDrList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string StkJSON = BL.getUnlistedDrJSON(SF);
        return StkJSON;
    }
    [WebMethod]
    public static string GetJntWrkList(string BSF, string ESF)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getJntWrkJSON(BSF, ESF);
        return JWJSON;
    }
    [WebMethod]
    public static string GetHospitalList(string SF)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getHospJSON(SF);
        return JWJSON;
    }


    [WebMethod]
    public static string GetCateList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getCateJSON(Div);
        return JWJSON;
    }
    [WebMethod]
    public static string GetSpecList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getSpecJSON(Div);
        return JWJSON;
    }
    [WebMethod]
    public static string GetClaList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getClaJSON(Div);
        return JWJSON;
    }
    [WebMethod]
    public static string GetQualList(int Div)
    {
        DCREntryBL BL = new DCREntryBL();
        string JWJSON = BL.getQualJSON(Div);
        return JWJSON;
    }

    [WebMethod(EnableSession = true, CacheDuration = 300)] //Added Cache duration for Server Method failed.

    public static string SaveDCR(string SFData, string DCRData, string sSetup, string selValues, IList<DCREntry.NewCus> nData)
    {

        DCREntryBL BL = new DCREntryBL();
        DCREntry.DCRDatas Data = JsonConvert.DeserializeObject<DCREntry.DCRDatas>(DCRData);
        DCREntry.DCRSetup Setup = JsonConvert.DeserializeObject<DCREntry.DCRSetup>(sSetup);
        DCREntry.SFDetails SFDet = JsonConvert.DeserializeObject<DCREntry.SFDetails>(SFData);
        IList<DCREntry.NewCus> nDet = nData;
        Data.Div = (byte)SFDet.Div;
        string DataID = "";
        string Sample = "";
        string Input = "";

        using (var scope = new TransactionScope())
        {
            try
            {

                BL.deleteEntryTemp(Data.SFCode, Data.EDate);
                string Detstr = "[";
                string ARCd = BL.SaveDCRTemp1(Data, selValues);
                if (Data.FWFlg == "F")
                {
                    if (Data.ETabs.IndexOf("D") > -1)
                    {
                        for (int il = 0; il < Data.MslData.Count; il++)
                        {
                            DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.MslData[il].cus.val);
                            if (Detstr != "[") Detstr += ",";
                            Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 1, Data.Div, Data.ETyp, Data.EDate, Data.MslData[il], nDta);
                            DataID = "Msl:" + Data.MslData[il].cus.val;
                            Sample += Data.MslData[il].prd.val;
                            Input += Data.MslData[il].inp.val;
                        }
                    }
                    if (Data.ETabs.IndexOf("C") > -1)
                    {
                        for (int il = 0; il < Data.ChmData.Count; il++)
                        {
                            DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.ChmData[il].cus.val);
                            if (Detstr != "[") Detstr += ",";
                            Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 2, Data.Div, Data.ETyp, Data.EDate, Data.ChmData[il], nDta);
                            DataID = "Chm:" + Data.ChmData[il].cus.val;
                            Sample += Data.ChmData[il].prd.val;
                            Input += Data.ChmData[il].inp.val;
                        }
                    }
                    if (Data.ETabs.IndexOf("S") > -1)
                    {
                        for (int il = 0; il < Data.StkData.Count; il++)
                        {
                            if (Detstr != "[") Detstr += ",";
                            Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 3, Data.Div, Data.ETyp, Data.EDate, Data.StkData[il]);
                            DataID = "Stk:" + Data.StkData[il].cus.val;
                            Sample += Data.StkData[il].prd.val;
                            Input += Data.StkData[il].inp.val;
                        }
                    }
                    if (Data.ETabs.IndexOf("U") > -1)
                    {
                        for (int il = 0; il < Data.UdrData.Count; il++)
                        {
                            DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.UdrData[il].cus.val);
                            if (Detstr != "[") Detstr += ",";
                            Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 4, Data.Div, Data.ETyp, Data.EDate, Data.UdrData[il], nDta);
                            DataID = "Udr:" + Data.UdrData[il].cus.val;
                            Sample += Data.UdrData[il].prd.val;
                            Input += Data.UdrData[il].inp.val;
                        }
                    }
                    if (Data.ETabs.IndexOf("H") > -1)
                    {
                        for (int il = 0; il < Data.HosData.Count; il++)
                        {
                            DCREntry.NewCus nDta = nDet.FirstOrDefault(p => p.id == Data.HosData[il].cus.val);
                            if (Detstr != "[") Detstr += ",";
                            Detstr += BL.SaveDCRDetTemp(ARCd, Data.SFCode, 5, Data.Div, Data.ETyp, Data.EDate, Data.HosData[il], nDta);
                            DataID = "Hos:" + Data.HosData[il].cus.val;
                            Sample += Data.HosData[il].prd.val;
                            Input += Data.HosData[il].inp.val;
                        }
                    }
                }
                Detstr += "]";
                if (Data.DCRType == 0)
                {
                    BL.udpDCRDt(Data.SFCode, Data.EDate.AddDays(1));
                }
                else if (Data.DCRType == 1 || Data.DCRType == 2)//Added on 02-Feb-25
                {
                    DB_EReporting db_ER = new DB_EReporting();

                    string Str_Last_Date = string.Empty;
                    DataSet dsDCR_Last_Date = new DataSet();

                    Str_Last_Date = "select CAST(Last_DCR_Date AS DATE) as Last_DCR_Date from Mas_Salesforce_DCRTPdate  where sf_Code= '" + Data.SFCode + "'";
                    dsDCR_Last_Date = db_ER.Exec_DataSet(Str_Last_Date);

                    if (dsDCR_Last_Date.Tables[0].Rows.Count > 0)
                    {
                        var lastDCRDate = dsDCR_Last_Date.Tables[0].Rows[0]["Last_DCR_Date"];

                        if (lastDCRDate != DBNull.Value)
                        {
                            DateTime lastDCRDateValue = Convert.ToDateTime(lastDCRDate);
                            if (lastDCRDateValue.Date == Data.EDate.Date)
                            {
                                BL.udpDCRDt(Data.SFCode, Data.EDate.AddDays(1));
                            }
                        }
                    }

                }
                BL.updateType(Data.SFCode, Data.EDate, Data.DCRType);

                if (Setup.DCRAppr == 0)
                {
                    BL.ApproveDCR(Data.SFCode, Data.EDate);
                }

                if (Setup.ProdSampleQtyValid == 1)
                {
                    BL.Sample_Input_Update(Data.SFCode, Sample, 1);
                }
                if (Setup.InputQtyValid == 1)
                {
                    BL.Sample_Input_Update(Data.SFCode, Input, 2);
                }

                scope.Complete();
                scope.Dispose();


               

                var DtDet = BL.GetDCRDtDet(SFDet, Setup);
                string DCRJSON = "{}";
                if (DtDet.Type > 0 && DtDet.Type < 3) DCRJSON = BL.getDCRDetails(SFDet.SFCode, DtDet.DCR_Date);

                string JWJSON = "{\"success\":true,";
                JWJSON = JWJSON + "\"NextDat\":" + JsonConvert.SerializeObject(DtDet) + ",";
                JWJSON = JWJSON + "\"newCus\":" + Detstr + ",";
                JWJSON = JWJSON + "\"exDta\":" + DCRJSON + "}"; //BL.getJntWrkJSON(BSF, ESF);              
                return JWJSON;

            }
            catch (Exception ex)
            {
                scope.Dispose();
                var errmsg = "SF :" + SFDet.SFCode + " Call Date : " + Data.EDate + " Error on:" + DataID + "\r\nmsg:" + ex.Message + "\r\nData:" + DCRData;
                StackTrace st = new StackTrace(ex, true);
                StackFrame[] frames = st.GetFrames();

                foreach (StackFrame frame in frames)
                {
                    if (frame.GetFileName() != "")
                        errmsg += "\n" + frame.GetFileName() + ":" + frame.GetMethod().Name + "(" + frame.GetFileLineNumber() + "," + frame.GetFileColumnNumber() + ")";
                }

                Log(errmsg);
                if (ex.Message.Contains("deadlocked on lock"))
                {
                    ex = new Exception("Internet Inconsistency...Try again.");

                }
                throw ex;
            }
            finally { BL = null; }
        }
    }

    public static void Log(string logMessage)
    {
        using (StreamWriter w = File.AppendText(HttpContext.Current.Server.MapPath("/DCR") + "/log/log" + DateTime.Today.ToString("yyy_MM_dd") + ".txt"))
        {
            w.WriteLine("Log Entry :{0} {1} {2}", DateTime.Now.ToLongDateString(), DateTime.Now.ToLongTimeString(), logMessage);
            w.WriteLine("----------------------------------------------------------------------------------------------------------");
            w.Close();
        }
    }
    [WebMethod(EnableSession = true)]
    public static string SaveDeviate(string objData)
    {
        string[] arr = objData.Split('^');
        Sf_Code = arr[0];
        Activity_Date = arr[1];
        Div_Code = arr[2];
        Dev_Reason = arr[3];
        TP_Area = arr[4];
        TP_Worktype = arr[5];
        SF_Name = arr[6];
        TPDCR_MGRAppr = arr[7];
        sTerritory_Code = arr[8];

        DCREntryBL BL = new DCREntryBL();
        try
        {


            BL.SaveDeviate(Sf_Code, Activity_Date, Div_Code, Dev_Reason, TP_Area, TP_Worktype, SF_Name, Convert.ToInt32(TPDCR_MGRAppr), sTerritory_Code);


        }
        catch (Exception ex)
        {

        }
        return "0";
    }

}
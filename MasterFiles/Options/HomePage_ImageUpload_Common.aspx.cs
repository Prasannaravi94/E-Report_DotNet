using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using Bus_EReport;
using System.Web.Configuration;
using DBase_EReport;
using Newtonsoft.Json;
using System.Web.Services;
using System.Web.Script.Services;


public partial class MasterFiles_Options_HomePage_ImageUpload_Common : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    string div_code = string.Empty;
    string sf_code = string.Empty;
    DataSet ds = new DataSet();
    DataSet dsDesig = null;
    SqlDataAdapter da;
    public SqlConnection conn;
    DataSet dsSubDivision = new DataSet();
    DataSet dsSalesForce = null;
    public SqlCommand com;
    SqlCommand cmd;
    int Id;
    DataSet dsMail = null;
    DataSet dsFrom = null;
    DataSet dsDivision = new DataSet();
    DataSet dsState = new DataSet();
   
    string sf_Type = string.Empty;
    string HO_ID = string.Empty;
    string sNew_Sf_Code = string.Empty;
    DataSet dsUserList = new DataSet();
    string sLevel = string.Empty;
    string temp_code = string.Empty;
    string mail_to_sf_code = string.Empty;
    string temp_Name = string.Empty;
    string mail_to_sf_Name = string.Empty;
    string mail_cc_sf_code = string.Empty;
    string strSF_Name = string.Empty;
    string mail_bcc_sf_code = string.Empty;
    SalesForce sf = new SalesForce();
    
    string strMail_CC = string.Empty;
    string sf_Name = string.Empty;
    string strMail_To = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strFileDateTime = string.Empty;
    string div_Name = string.Empty;
    string[] statecd;
    string state_cd = string.Empty;
    string sState = string.Empty;
    
    protected void Page_Load(object sender, EventArgs e)
    {
        this.Page.Form.Enctype = "multipart/form-data";
        div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        HO_ID = Session["HO_ID"].ToString();
        if (!IsPostBack)
        {
            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            pnlpopup.Visible = false;
            BindGridviewData();
            txtSub.Focus();
            // FillCheckDesig();
            pnlCompose.Visible = true;

            FillDesignation();
            FillSubdiv();
            FillSubdiv();
            FillStatemulti();


            //FillAddressBook();
        }
        hHeading.InnerText = Page.Title;
        txtEffFrom.Attributes.Add("autocomplete", "off");
        txtEffTo.Attributes.Add("autocomplete", "off");

    }
    private void BindGridviewData()
    {
        ds.Clear();
        SqlCommand cmd = new SqlCommand("select * from Mas_HomePage_Image where Division_Code = '" + div_code + "'", con);
        SqlDataAdapter da = new SqlDataAdapter(cmd);

        da.Fill(ds);
        con.Close();
        gvDetails.DataSource = ds.Tables[0];
        gvDetails.DataBind();
    }

    //private void FillCheckDesig()
    //{

    //    Designation dv = new Designation();
    //    dsDesig = dv.getDesig_Check(div_code);
    //    chkDesig.DataTextField = "Designation_Short_Name";
    //    chkDesig.DataValueField = "Designation_Code";
    //    chkDesig.DataSource = dsDesig;
    //    chkDesig.DataBind();
    //}

    // Save files to Folder and files path in database
    //protected void btnUpload_Click(object sender, EventArgs e)
    //{
    //    string sChkLocation = string.Empty;
    //    string sChkLocationname = string.Empty;
    //    if (fileUpload1.HasFile)
    //    {
    //        BindGridviewData();
    //        string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
    //        fileUpload1.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + filename));
    //        con.Open();
    //        SqlCommand cmd = new SqlCommand();


    //        cmd = new SqlCommand("SELECT isnull(max(Id)+1,'1') Id from Mas_HomePage_Image", con);

    //        SqlDataAdapter daimage = new SqlDataAdapter(cmd);
    //        DataSet dsimage = new DataSet();
    //        daimage.Fill(dsimage);
    //        cmd = new SqlCommand("insert into Mas_HomePage_Image(Id,FileName,FilePath,subject,Upload_Date,Division_Code,Designation_Code,Designation_Short_Name) values('" + dsimage.Tables[0].Rows[0][0].ToString() + "',@Name,@Path,@Subject,@Upload_Date,@Division_Code,@Designation_Code,@Designation_Short_Name)", con);
    //        // cmd.Parameters.Add(new SqlParameter("@Id", ds.Tables[0].Rows[0][0].ToString()));
    //        cmd.Parameters.AddWithValue("@Name", filename);
    //        cmd.Parameters.AddWithValue("@Path", "~/MasterFiles/Options/Files/" + filename);
    //        cmd.Parameters.Add(new SqlParameter("@Subject", txtSub.Text));
    //        cmd.Parameters.Add(new SqlParameter("@Upload_Date", DateTime.Now));
    //        cmd.Parameters.Add(new SqlParameter("@Division_Code", div_code));

    //        for (int i = 0; i < chkDesgn.Items.Count; i++)
    //        {
    //            if (chkDesgn.Items[i].Selected)
    //            {
    //                sChkLocation = sChkLocation + chkDesgn.Items[i].Value + ",";
    //                sChkLocationname = sChkLocationname + chkDesgn.Items[i].Text + ",";
    //            }
    //        }
    //        // sChkLocation = sChkLocation.TrimEnd(',');
    //        // sChkLocationname = sChkLocationname.TrimEnd(',');
    //        cmd.Parameters.AddWithValue("@Designation_Code", sChkLocation);
    //        cmd.Parameters.AddWithValue("@Designation_Short_Name", sChkLocationname);
    //        //}


    //        cmd.ExecuteNonQuery();
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Uploaded Sucessfully');</script>");
    //        txtSub.Text = "";
    //        con.Close();
    //        BindGridviewData();

    //    }
    //    else
    //    {
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select a File');</script>");
    //    }
    //}
    // This button click event is used to download files from gridview



    #region Button Send
    protected void btnUpload_Click(object sender, EventArgs e)
    {
      //  string DivCode = string.Empty;
      ////  string to_sf_code = string.Empty;
        DataSet dsMailCompose = null;
      //  string cc_sf_code = string.Empty;
      //  string bcc_sf_code = string.Empty;
      //  string tobcc_sf_name = string.Empty;
      //  string toCC_sf_name = string.Empty;
      //  string Attachpath = string.Empty;
        //
     //   DataTable dt = (DataTable)ViewState["CurrentTable"];
        //  DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
        //DataTable dtAll = new DataTable();
        //if (dtFrwd != null)
        //    dtAll.Merge(dtFrwd);
        //if (dt == null)
        //    SetInitialRow(false);
        //dt = (DataTable)ViewState["CurrentTable"];
        //dtAll.Merge(dt);
        ////
        //foreach (DataRow dtRow in dtAll.Rows)
        //{
        //    if (dtRow["New_File_Name"].ToString() != "" && dtRow["New_File_Name"].ToString() != null)
        //        Attachpath += dtRow["New_File_Name"].ToString() + ",";
        //}
        //for (int i = 0; i < grdAttchmentFiles.Rows.Count; i++)
        //{
        //    FileUpload attachFile = (FileUpload)grdAttchmentFiles.Rows[i].Cells[0].FindControl("upldFiles");
        //    Label lblFileAttach = (Label)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lblFileAttach");
        //    HiddenField hdnAttachFile = (HiddenField)grdAttchmentFiles.Rows[i].Cells[0].FindControl("hdnAttachFile");
        //    if (attachFile.HasFile)
        //    {
        //        string sNewFileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + attachFile.FileName;
        //        attachFile.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Mails/Attachment/" + sNewFileName));
        //        lblFileAttach.Text = attachFile.FileName;
        //        hdnAttachFile.Value = sNewFileName;
        //        Attachpath += sNewFileName + ",";
        //    }
        //}
        //if (Attachpath != "")
        //    Attachpath = Attachpath.Remove(Attachpath.LastIndexOf(","));
        ////
        //if (div_code.Contains(','))
        //    div_code = div_code.Remove(div_code.Length - 1);

        if (fileUpload1.HasFile)
        {
            BindGridviewData();
            string dateval = DateTime.Now.ToString();
            string dateval1 = dateval.Replace(" ", "_").Replace("/", "-").Replace(":", ".").ToString();
            string filename = dateval1 + '_' + fileUpload1.FileName.ToString();
         //   string filename = Path.GetFileName(fileUpload1.PostedFile.FileName);
            fileUpload1.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + filename));
            con.Open();

            string filepath = "~/MasterFiles/Options/Files/" + filename;
            //   SqlCommand cmd = new SqlCommand();


            //      cmd = new SqlCommand("SELECT isnull(max(Id)+1,'1') Id from Mas_HomePage_Image", con);

            //     SqlDataAdapter daimage = new SqlDataAdapter(cmd);
            //    DataSet dsimage = new DataSet();


            string cur_sf_code = string.Empty;
            string cur_sf_level = string.Empty;
            Boolean blnMail = false;

            if (ViewState["mail_to_sf_code"] != null)
            {
                blnMail = true;
                //
                string sMail_To_Sf_Codes = ViewState["mail_to_sf_code"].ToString();


                string strSF_Name = "";
                if (sf_Type == "3")
                    strSF_Name = Session["Corporate"].ToString();
                else
                    strSF_Name = Session["sf_name"].ToString() + "-" + Session["Sf_HQ"] + "-" + Session["Designation_Short_Name"];

                AdminSetup adm = new AdminSetup();

                dsMailCompose = adm.UploadImgae(filename, filepath, txtSub.Text.Trim(), div_code, ViewState["mail_to_sf_code"].ToString(),
                                               ViewState["mail_to_sf_Name"].ToString(), Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text));





            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select Fieldforce');</script>");
                pan_fld.Visible = true;
            }


            if (blnMail)
            {
                txtAddr.Text = "";
                txtSub.Text = "";

                foreach (ListItem item in chkFF.Items)
                {
                    item.Selected = false;
                }


                ViewState["CurrentTable"] = null;

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image Upload Successfully');</script>");
                pan_fld.Visible = false;
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please Select a File');</script>");
            pan_fld.Visible = false;
        }
    }
    #endregion


    //protected void grdAttchmentFiles_RowCreated(object sender, GridViewRowEventArgs e)
    //{
    //    if (e.Row.RowType == DataControlRowType.DataRow)
    //    {
    //        DataTable dt = (DataTable)ViewState["CurrentTable"];
    //        DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
    //        LinkButton lb = (LinkButton)e.Row.FindControl("lnkRemoveAttach");
    //        DataTable dtAll = new DataTable();
    //        if (dtFrwd != null)
    //            dtAll.Merge(dtFrwd);
    //        if (dt == null)
    //            SetInitialRow(false);
    //        dt = (DataTable)ViewState["CurrentTable"];
    //        dtAll.Merge(dt);
    //        if (lb != null)
    //        {
    //            if (dtAll.Rows.Count > 1)
    //            {
    //                if (dtFrwd != null)
    //                    if (dtFrwd.Rows.Count > 0)
    //                        if (e.Row.RowIndex < dtFrwd.Rows.Count)
    //                            lb.Visible = false;
    //            }
    //            if (e.Row.RowIndex == dtAll.Rows.Count - 1)
    //                lb.Visible = false;
    //            else
    //                lb.Visible = false;
    //        }
    //    }
    //}

    //#region Button Send Mail Click
    //protected void btnUpload_Click1(object sender, EventArgs e)
    //{
    //    string DivCode = string.Empty;
    //    string to_sf_code = string.Empty;
    //    DataSet dsMailCompose = null;
    //    string cc_sf_code = string.Empty;
    //    string bcc_sf_code = string.Empty;
    //    string tobcc_sf_name = string.Empty;
    //    string toCC_sf_name = string.Empty;
    //    string Attachpath = string.Empty;
    //    //
    //    DataTable dt = (DataTable)ViewState["CurrentTable"];
    //    DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
    //    DataTable dtAll = new DataTable();
    //    if (dtFrwd != null)
    //        dtAll.Merge(dtFrwd);
    //    if (dt == null)
    //        SetInitialRow(false);
    //    dt = (DataTable)ViewState["CurrentTable"];
    //    dtAll.Merge(dt);
    //    //
    //    foreach (DataRow dtRow in dtAll.Rows)
    //    {
    //        if (dtRow["New_File_Name"].ToString() != "" && dtRow["New_File_Name"].ToString() != null)
    //            Attachpath += dtRow["New_File_Name"].ToString() + ",";
    //    }
    //    for (int i = 0; i < grdAttchmentFiles.Rows.Count; i++)
    //    {
    //        FileUpload attachFile = (FileUpload)grdAttchmentFiles.Rows[i].Cells[0].FindControl("upldFiles");
    //        Label lblFileAttach = (Label)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lblFileAttach");
    //        HiddenField hdnAttachFile = (HiddenField)grdAttchmentFiles.Rows[i].Cells[0].FindControl("hdnAttachFile");
    //        if (attachFile.HasFile)
    //        {
    //            string sNewFileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + attachFile.FileName;
    //            attachFile.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + sNewFileName));
    //            lblFileAttach.Text = attachFile.FileName;
    //            hdnAttachFile.Value = sNewFileName;
    //            Attachpath += sNewFileName + ",";
    //        }
    //    }
    //    if (Attachpath != "")
    //        Attachpath = Attachpath.Remove(Attachpath.LastIndexOf(","));
    //    //
    //    if (div_code.Contains(','))
    //        div_code = div_code.Remove(div_code.Length - 1);
    //    //
    //    string cur_sf_code = string.Empty;
    //    string cur_sf_level = string.Empty;
    //    Boolean blnMail = false;
    //    //
    //    if (ViewState["mail_to_sf_code"] != null)
    //    {
    //        blnMail = true;
    //        //
    //        string sMail_To_Sf_Codes = ViewState["mail_to_sf_code"].ToString();
         
    //        //
    //        string strSF_Name = "";
    //        if (sf_Type == "3")
    //            strSF_Name = Session["Corporate"].ToString();
    //        else
    //            strSF_Name = Session["sf_name"].ToString() + "-" + Session["Sf_HQ"] + "-" + Session["Designation_Short_Name"];
    //        //
    //        AdminSetup adm = new AdminSetup();


    //        dsMailCompose = adm.UploadImgae(Attachpath, Attachpath, txtSub.Text.Trim(), div_code, ViewState["mail_to_sf_code"].ToString(),
    //                                      ViewState["mail_to_sf_Name"].ToString(), Convert.ToDateTime(txtEffFrom.Text), Convert.ToDateTime(txtEffTo.Text));
    //        //
    //        ////dsMailCompose = adm.ComposeMail(sNew_Sf_Code, ViewState["mail_to_sf_code"].ToString(), txtSub.Text.Trim(), txtMsg.Text.Trim(),
    //        ////                                Attachpath, cc_sf_code, bcc_sf_code, div_code, Request.ServerVariables["REMOTE_ADDR"].ToString(),
    //        ////                                toCC_sf_name, tobcc_sf_name, strSF_Name, ViewState["mail_to_sf_Name"].ToString());

    //        //
    //    }
    //    //
    //    if (blnMail)
    //    {
    //        txtAddr.Text = "";
    //        txtSub.Text = "";
           
    //        foreach (ListItem item in chkFF.Items)
    //        {
    //            item.Selected = false;
    //        }
    //        //
          
    //        //
    //        ViewState["Attachment_Forward"] = null;
    //        ViewState["CurrentTable"] = null;
    //        ViewState["mail_cc_sf_code"] = null;
    //        ViewState["mail_bcc_sf_code"] = null;
    //        ViewState["mail_to_sf_NameBCC"] = null;
    //        ViewState["mail_to_sf_NameCC"] = null;
    //        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image has been sent successfully');</script>");
    //    }
    //}
    //#endregion
    //private void SetPreviousData(DataTable dtTbl, bool blTyp)
    //{
    //    int rowIndex = 0;
    //    DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
    //    if (dtTbl != null)
    //    {
    //        if (dtTbl.Rows.Count > 0)
    //        {
    //            for (int i = 0; i < dtTbl.Rows.Count - 1; i++)
    //            {
    //                FileUpload upldFile = (FileUpload)grdAttchmentFiles.Rows[i].Cells[0].FindControl("upldFiles");
    //                Label lblFileAttach = (Label)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lblFileAttach");
    //                HiddenField hdnAttachFile = (HiddenField)grdAttchmentFiles.Rows[i].Cells[0].FindControl("hdnAttachFile");
    //                LinkButton lnkBtnRmv = (LinkButton)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lnkRemoveAttach");
    //                if (i < dtTbl.Rows.Count - 1)
    //                {
    //                    //Assign the value from DataTable to the TextBox   
    //                    lblFileAttach.Text = dtTbl.Rows[i]["Original_Name"].ToString();
    //                    hdnAttachFile.Value = dtTbl.Rows[i]["New_File_Name"].ToString();
    //                    lblFileAttach.Visible = true;
    //                    upldFile.Visible = false;
    //                    if (blTyp)
    //                        lnkBtnRmv.Visible = false;
    //                    if (dtFrwd == null)
    //                        lnkBtnRmv.Visible = true;
    //                    else
    //                    {
    //                        bool blRmvBtnVsbl = true;
    //                        foreach (DataRow dtRow in dtFrwd.Rows)
    //                        {
    //                            if (hdnAttachFile.Value == dtRow["New_File_Name"].ToString() && hdnAttachFile.Value != "" && hdnAttachFile.Value != null)
    //                            {
    //                                lnkBtnRmv.Visible = false;
    //                                blRmvBtnVsbl = false;
    //                                break;
    //                            }
    //                        }
    //                        if (blRmvBtnVsbl)
    //                            lnkBtnRmv.Visible = true;
    //                    }
    //                }
    //                rowIndex++;
    //            }
    //        }
    //    }
    //}
    ////
    //protected void lnkRemoveAttach_Click(object sender, EventArgs e)
    //{
    //    LinkButton lb = (LinkButton)sender;
    //    GridViewRow gvRow = (GridViewRow)lb.NamingContainer;
    //    //
    //    DataTable dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
    //    DataTable dtCrntTbl = (DataTable)ViewState["CurrentTable"];
    //    DataTable dtAllAttach = new DataTable();
    //    if (dtFrwdAttach != null)
    //        dtAllAttach.Merge(dtFrwdAttach);
    //    dtAllAttach.Merge(dtCrntTbl);
    //    //
    //    int rowID = gvRow.RowIndex, iDtFrwdRowsCnt = 0;
    //    if (dtFrwdAttach != null)
    //        iDtFrwdRowsCnt = dtFrwdAttach.Rows.Count;
    //    //
    //    if (dtCrntTbl != null)
    //    {
    //        if (dtCrntTbl.Rows.Count > 1)
    //        {
    //            if (gvRow.RowIndex < dtAllAttach.Rows.Count - 1)
    //            {
    //                //Remove the Selected Row data and reset row number  
    //                string fileName = Server.MapPath("~/MasterFiles/Options/Files/" + dtAllAttach.Rows[rowID]["New_File_Name"].ToString());
    //                if (System.IO.File.Exists(fileName))
    //                    System.IO.File.Delete(fileName);
    //                dtCrntTbl.Rows.Remove(dtCrntTbl.Rows[rowID - iDtFrwdRowsCnt]);
    //                ResetRowID(dtCrntTbl);
    //            }
    //        }
    //        //Store the current data in ViewState for future reference  
    //        ViewState["CurrentTable"] = dtCrntTbl;
    //    }
    //    dtAllAttach = new DataTable();
    //    if (dtFrwdAttach != null)
    //        dtAllAttach.Merge(dtFrwdAttach);
    //    dtAllAttach.Merge(dtCrntTbl);
    //    //Set Previous Data on Postbacks  
    //    grdAttchmentFiles.DataSource = dtAllAttach;
    //    grdAttchmentFiles.DataBind();
    //    SetPreviousData(dtAllAttach, false);
    //}
    ////

    //private void ResetRowID(DataTable dt)
    //{
    //    int rowNumber = 1;
    //    if (dt.Rows.Count > 0)
    //    {
    //        foreach (DataRow row in dt.Rows)
    //        {
    //            row[0] = rowNumber;
    //            rowNumber++;
    //        }
    //    }
    //}
    //protected void btnNewFile_Click(object sender, EventArgs e)
    //{
    //    DataTable dt = (DataTable)ViewState["CurrentTable"];
    //    DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
    //    DataTable dtAll = new DataTable();
    //    if (dtFrwd != null)
    //        dtAll.Merge(dtFrwd);
    //    if (dt == null)
    //        SetInitialRow(false);
    //    dt = (DataTable)ViewState["CurrentTable"];
    //    dtAll.Merge(dt);
    //    AddNewRowToGrid(dtAll);
    //}
    //private void AddNewRowToGrid(DataTable dtAttach)
    //{
    //    DataTable dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
    //    DataTable dtCrntTbl = (DataTable)ViewState["CurrentTable"];
    //    DataTable dtAllAttach = new DataTable();
    //    //
    //    if (dtAttach != null)
    //    {
    //        DataTable dtCurrentTable = (DataTable)ViewState["CurrentTable"];
    //        DataTable dtFrwd = (DataTable)ViewState["Attachment_Forward"];
    //        DataRow drCurrentRow = null;
    //        //
    //        if (dtCurrentTable.Rows.Count > 0)
    //        {
    //            drCurrentRow = dtCurrentTable.NewRow();
    //            drCurrentRow["RowNumber"] = dtCurrentTable.Rows.Count + 1;

    //            int iEmptyRowCnt = 0;
    //            //add new row to DataTable 
    //            foreach (DataRow dtRows in dtCurrentTable.Rows)
    //            {
    //                if (dtRows["New_File_Name"].ToString() == null || dtRows["New_File_Name"].ToString() == "")
    //                    iEmptyRowCnt++;
    //            }

    //            if (iEmptyRowCnt == 1 || iEmptyRowCnt == 0)
    //                dtCurrentTable.Rows.Add(drCurrentRow);

    //            //Store the current data to ViewState for future reference   
    //            //dtAttach.Rows.Add(drCurrentRow);
    //            //
    //            ViewState["CurrentTable"] = dtCurrentTable;
    //            //                
    //            dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
    //            dtCrntTbl = (DataTable)ViewState["CurrentTable"];
    //            dtAllAttach = new DataTable();
    //            if (dtFrwdAttach != null)
    //                dtAllAttach.Merge(dtFrwdAttach);
    //            dtAllAttach.Merge(dtCrntTbl);
    //            grdAttchmentFiles.DataSource = dtAllAttach;
    //            grdAttchmentFiles.DataBind();
    //            //
    //            for (int i = 0, k = 0; i < dtAllAttach.Rows.Count - 1; i++)
    //            {
    //                FileUpload attachFile = (FileUpload)grdAttchmentFiles.Rows[i].Cells[0].FindControl("upldFiles");
    //                Label lblFileAttach = (Label)grdAttchmentFiles.Rows[i].Cells[0].FindControl("lblFileAttach");
    //                HiddenField hdnAttachFile = (HiddenField)grdAttchmentFiles.Rows[i].Cells[0].FindControl("hdnAttachFile");
    //                if (attachFile.HasFile)
    //                {
    //                    string sNewFileName = DateTime.Now.ToString("dd_MM_yyyy_HH_mm_ss") + attachFile.FileName;
    //                    attachFile.PostedFile.SaveAs(Server.MapPath("~/MasterFiles/Options/Files/" + sNewFileName));
    //                    lblFileAttach.Text = attachFile.FileName;
    //                    hdnAttachFile.Value = sNewFileName;
    //                }
    //                if (k < dtCurrentTable.Rows.Count)
    //                {
    //                    if (dtFrwd != null)
    //                    {
    //                        if (i >= dtFrwd.Rows.Count)
    //                        {
    //                            dtCurrentTable.Rows[k]["Original_Name"] = lblFileAttach.Text;
    //                            dtCurrentTable.Rows[k]["New_File_Name"] = hdnAttachFile.Value;
    //                            k++;
    //                        }
    //                    }
    //                    else
    //                    {
    //                        dtCurrentTable.Rows[k]["Original_Name"] = lblFileAttach.Text;
    //                        dtCurrentTable.Rows[k]["New_File_Name"] = hdnAttachFile.Value;
    //                        k++;
    //                    }
    //                }
    //                dtAttach.Rows[i]["Original_Name"] = lblFileAttach.Text;
    //                dtAttach.Rows[i]["New_File_Name"] = hdnAttachFile.Value;
    //            }

    //            //Rebind the Grid with the current data to reflect changes  
    //            dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
    //            dtCrntTbl = (DataTable)ViewState["CurrentTable"];
    //            dtAllAttach = new DataTable();
    //            if (dtFrwdAttach != null)
    //                dtAllAttach.Merge(dtFrwdAttach);
    //            dtAllAttach.Merge(dtCrntTbl);
    //            //
    //            grdAttchmentFiles.DataSource = dtAllAttach;
    //            grdAttchmentFiles.DataBind();
    //            ViewState["CurrentTable"] = dtCurrentTable;
    //        }
    //    }
    //    else
    //    {
    //        Response.Write("ViewState is null");
    //    }
    //    //Set Previous Data on Postbacks   
    //    dtFrwdAttach = (DataTable)ViewState["Attachment_Forward"];
    //    dtCrntTbl = (DataTable)ViewState["CurrentTable"];
    //    dtAllAttach = new DataTable();
    //    if (dtFrwdAttach != null)
    //        dtAllAttach.Merge(dtFrwdAttach);
    //    dtAllAttach.Merge(dtCrntTbl);
    //    SetPreviousData(dtAllAttach, false);
    //}

    //private void SetInitialRow(bool blType)
    //{
    //    DataTable dt = new DataTable();
    //    DataRow dr = null;
    //    //
    //    dt.Columns.Add(new DataColumn("RowNumber", typeof(string)));
    //    dt.Columns.Add(new DataColumn("Original_Name", typeof(string)));//for TextBox value   
    //    dt.Columns.Add(new DataColumn("New_File_Name", typeof(string)));
    //    //
    //    dr = dt.NewRow();
    //    dr["RowNumber"] = 1;
    //    //dr["Column1"] = string.Empty;
    //    dt.Rows.Add(dr);

    //    //Store the DataTable in ViewState for future reference   
    //    ViewState["CurrentTable"] = dt;

    //    //Bind the Gridview   
    //    if (blType)
    //    {
    //        grdAttchmentFiles.DataSource = dt;
    //        grdAttchmentFiles.DataBind();
    //    }
    //}


    protected void lnkDownload_Click(object sender, EventArgs e)
    {
        // BindGridviewData();
       



            SqlCommand cmd = new SqlCommand("select * from Mas_HomePage_Image where Division_Code = '" + div_code + "'  ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            con.Close();
            gvDetails.DataSource = ds.Tables[0];
            gvDetails.DataBind();

            LinkButton lnkbtn = sender as LinkButton;
            GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            string filePath = ds.Tables[0].Rows[0][2].ToString();
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
        
    }

    //public void lstst_SelectedIndexChanged(object sender, System.EventArgs e)
    //{
    //    string st = string.Empty;
    //    string sub = string.Empty;
    //    foreach (System.Web.UI.WebControls.ListItem item in lstst.Items)
    //    {
    //        if (item.Selected)
    //        {
    //            st += item.Value + ',';
    //        }
    //    }
    //    if (st != "")
    //    {
    //        st = st.Remove(st.Length - 1);
    //        conn = new SqlConnection(WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);


    //            cmd = new SqlCommand("select sf_code,sf_name + ' - ' + sf_Designation_Short_Name + ' - ' + Sf_HQ as sf_name from mas_salesforce where Reporting_To_SF='admin' and sf_type='2' and sf_status !=2 and state_code in (" + st + ") order by state_code ", conn);

    //        da = new SqlDataAdapter(cmd);
    //        ds = new DataSet();
    //        da.Fill(ds);
    //        //if (ds.Tables[0].Rows.Count > 0)
    //        //{
    //        //    lstSf.DataSource = ds;
    //        //    lstSf.DataValueField = ds.Tables[0].Columns["sf_code"].ColumnName;
    //        //    lstSf.DataTextField = ds.Tables[0].Columns["sf_name"].ColumnName;
    //        //    lstSf.DataBind();
    //        //}
    //    }
    //}

    protected void ddlState_SelectedIndexChanged(object sender, EventArgs e)
    {

        if (ddlState.SelectedValue.ToString().Trim().Length > 0)
        {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillDllSalesForce();
        }

        //
        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked)
                i++;
        }
        //
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;
        //
        FillgridColor();
        FillColor();
        //
    }
    #region FillDesignation
    private void FillDesignation()
    {
        DataTable dt = new DataTable();
        if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
        {
            AdminSetup adm = new AdminSetup();
            if (Session["sf_type"].ToString() == "3")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
            }
            //
            dt = sf.getAddressBookDesign(div_code, "admin", 0);
        }
        else if (Session["sf_type"].ToString() == "1")
        {
            DCR dc = new DCR();
            dt = dc.LoadMailWorkwithDes(sNew_Sf_Code);
        }
        else if (Session["sf_type"].ToString() == "2")
        {
            SalesForce sf = new SalesForce();
            DataSet dsmgrsf = new DataSet();
            DataSet DsAudit = sf.SF_Hierarchy(div_code, sNew_Sf_Code);
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {
                dt = sf.getAddressBookDesign(div_code, sf_code, 0);
            }
            else
            {
                // Fetch Managers Audit Team
                dt = sf.getAuditManagerTeam_mail(div_code, sNew_Sf_Code, 0);
                DataView view = new DataView(dt);
                dt = view.ToTable(true, "Designation_Short_Name", "Designation_Code");
            }
        }
        if (dt.Rows.Count > 0)
        {
            chkDesgn.DataTextField = "Designation_Short_Name";
            chkDesgn.DataValueField = "Designation_Code";
            chkDesgn.DataSource = dt;
            chkDesgn.DataBind();
        }
    }
    #endregion


    
    protected void chkLevelAll_CheckedChanged(object sender, EventArgs e)
    {
        bool blChkLvlAll_Chkd = chkLevelAll.Checked;
        foreach (ListItem item in chkDesgn.Items)
        {
            item.Selected = blChkLvlAll_Chkd;
            chkMR.Checked = blChkLvlAll_Chkd;
        }
        //
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = blChkLvlAll_Chkd;
            int j = 0;
            if (blChkLvlAll_Chkd)
            {
                j = gvFF.Rows.Count;
            }
            lblSelectedCount.Text = "No.of Field Force Selected : " + j.ToString();
        }
        FillgridColor();
        //
    }

    protected void ChkAllState_CheckedChanged(object sender, EventArgs e)
    {
        bool blChkStAll_Chkd = ChkAllState.Checked;
        foreach (ListItem item in chkstate.Items)
        {
            item.Selected = blChkStAll_Chkd;
            chkMR_State.Checked = blChkStAll_Chkd;
        }
        //
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = blChkStAll_Chkd;
            int j = 0;
            if (blChkStAll_Chkd)
            {
                j = gvFF.Rows.Count;
            }
            lblSelectedCount.Text = "No.of Field Force Selected : " + j.ToString();
        }
        FillgridColor();
        //
    }


    #region FillgridColor
    private void FillgridColor()
    {
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblBackColor = (Label)grid_row.FindControl("lblBackColor");
            string bcolor = "#" + lblBackColor.Text;
            grid_row.BackColor = System.Drawing.Color.FromName(bcolor);
        }
    }
    #endregion
    #region chkMR_OnCheckChanged
    protected void chkMR_OnCheckChanged(object sender, EventArgs e)
    {
        if (chkMR.Checked == false)
        {
            chkLevelAll.Checked = false;
        }
    }
    #endregion

    #region chkMR_State_OnCheckChanged
    protected void chkMR_State_OnCheckChanged(object sender, EventArgs e)
    {
        if (chkMR_State.Checked == false)
        {
            ChkAllState.Checked = false;
        }
    }
    #endregion
    //    
    #region chkDesgn_OnSelectedIndexChanged
    protected void chkDesgn_OnSelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            Label lblSf_Name = (Label)grid_row.FindControl("lblSf_Name");
            //
            if (lblSf_Name.Text.Contains("admin"))
            {
                if (chkLevelAll.Checked)
                    chkSf.Checked = true;
            }
        }
        //
        int i = 0;
        foreach (ListItem item in chkDesgn.Items)
        {
            bool blChckd = item.Selected;
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                Label lblDesignation_Code = (Label)grid_row.FindControl("lblDesignation_Code");
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                if (item.Value == lblDesignation_Code.Text)
                {
                    chkSf.Checked = blChckd;
                    if (chkSf.Checked)
                        i++;
                }
            }
        }
        //
        //int i = 0;
        //foreach (GridViewRow row in gvFF.Rows)
        //{
        //    CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
        //    if (chkSf.Checked)
        //        i++;
        //}
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;
        //
        //FillgridColor();
        //
    }
    protected void chkstate_OnSelectedIndexChanged(object sender, EventArgs e)
    {

        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            Label lblSf_Name = (Label)grid_row.FindControl("lblSf_Name");
            //
            if (lblSf_Name.Text.Contains("admin"))
            {
                if (ChkAllState.Checked)
                    chkSf.Checked = true;
            }
        }
        //
        int i = 0;
        foreach (ListItem item in chkstate.Items)
        {
            bool blChckd = item.Selected;
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                Label lblState = (Label)grid_row.FindControl("lblState");
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");

                if (item.Value == lblState.Text)
                {
                    chkSf.Checked = blChckd;
                    if (chkSf.Checked)
                        i++;
                }
            }
        }

      
        //foreach (GridViewRow row in gvFF.Rows)
        //{
        //    CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
        //    if (chkSf.Checked)
        //        i++;
        //}
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;
        //
        //FillgridColor();
        //


    }
    #endregion
    //
    #region gvFF_OnCheckedChanged
    protected void gvFF_OnCheckedChanged(object sender, EventArgs e)
    {
        CheckBox chkSf = (CheckBox)sender;
        GridViewRow row1 = (GridViewRow)chkSf.Parent.Parent;
        row1.Focus();
        int count = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox ChkBoxRows = (CheckBox)row.FindControl("chkSf");
            if (ChkBoxRows.Checked)
                count++;
        }
        lblSelectedCount.Text = "No.of Filed Force Selected : " + count;
        FillgridColor();
    }
    #endregion

    #region rdoadr_SelectedIndexChanged
    protected void rdoadr_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (rdoadr.SelectedValue.ToString() == "0")
        {
            ddlFFType.SelectedValue = "0";
            chkMR.Checked = false;
            ddlAlpha.Visible = false;
            Enable_Disable_Control_For_AddressBook(false, true,false);
            lblsubname.Visible = false;
            ddlsub.Visible = false;
            lblstate.Visible = false;
            chkstate.Visible = false;
            lblStateName.Visible = false;
            ddlState.Visible = false;
          //  lstst.Visible = false;

            //
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                item.Selected = false;
            }
            //
        }
        else if (rdoadr.SelectedValue.ToString() == "1")
        {
            Enable_Disable_Control_For_AddressBook(true, false,false);
            lblsubname.Visible = false;
            ddlsub.Visible = false;
            lblstate.Visible = false;
            chkstate.Visible = false;
            // lstState.Visible = false;
            lblStateName.Visible = false;
            //lstst.Visible = false;
            ddlState.Visible = false;

        }
        else if (rdoadr.SelectedValue.ToString() == "2")
        {
            Enable_Disable_Control_For_AddressBook(false, false,true);
            lblsubname.Visible = true;
            ddlsub.Visible = true;
            lblStateName.Visible = false;
            ddlState.Visible = false;
            chkMR_State.Checked = false;
            Get_State();
            FillSubdiv();
            FillStatemulti();
            lblstate.Visible = true;
            chkstate.Visible = true;
            chkMR_State.Visible = true;
            foreach (ListItem item in chkstate.Items)
            {
                ChkAllState.Checked = false;
                item.Selected = false;
            }

        }
        else if (rdoadr.SelectedValue.ToString() == "3")
        {
            Enable_Disable_Control_For_AddressBook(false, false,false);
            lblStateName.Visible = true;
            ddlState.Visible = true;
            //  lstst.Visible = true;
            //  lstState.Visible = false;
            FillState(div_code);
           // Get_State();
            lblsubname.Visible = false;
            ddlsub.Visible = false;
            lblstate.Visible = false;
            chkstate.Visible = false;
            

        }
        //
        //
        bool blCnt = false;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = false;
            blCnt = true;
        }
        if (blCnt)
            lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
        //
        ddlFFType.SelectedValue = "2";
        //ddlFieldForce.SelectedValue = "0";
        FillManagers();
        FillgridColor();
        //
    }
    #endregion

    private void Get_State()
    {
        Division dv = new Division();
        DataSet dsDivision;
        //dsDivision = dv.getStatePerDivision(div_code);
        //if (dsDivision.Tables[0].Rows.Count > 0)
        //{
        //    int i = 0;
        //    state_cd = string.Empty;
        //    sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
        //    statecd = sState.Split(',');
        //    foreach (string st_cd in statecd)
        //    {
        //        if (i == 0)
        //        {
        //            state_cd = state_cd + st_cd;
        //        }
        //        else
        //        {
        //            if (st_cd.Trim().Length > 0)
        //            {
        //                state_cd = state_cd + "," + st_cd;
        //            }
        //        }
        //        i++;
        //    }

        State st = new State();
        DataSet dsState;
        
        
            dsState = st.getState_Division();
        


        chkstate.DataTextField = "statename";
        chkstate.DataValueField = "state_code";
        chkstate.DataSource = dsState;
        chkstate.DataBind();
       
    }

    #region Enable_Disable_Control_For_AddressBook
    private void Enable_Disable_Control_For_AddressBook(bool ddl_FFType_FldFrc_lblFF, bool Chk_LvlAll_Desg_Lbl13, bool Chk_LvlAll_State_Lbl13)
    {
        ddlFFType.Visible = ddl_FFType_FldFrc_lblFF;
        ddlFieldForce.Visible = ddl_FFType_FldFrc_lblFF;
        lblFF.Visible = ddl_FFType_FldFrc_lblFF;
        //
        chkLevelAll.Visible = Chk_LvlAll_Desg_Lbl13;
        chkDesgn.Visible = Chk_LvlAll_Desg_Lbl13;
        Label3.Visible = Chk_LvlAll_Desg_Lbl13;

        ChkAllState.Visible=Chk_LvlAll_State_Lbl13;
        chkstate.Visible = Chk_LvlAll_State_Lbl13;
        lblstate.Visible = Chk_LvlAll_State_Lbl13;
        //
    }
    #endregion

    private void FillState(string div_code)
    {
        Division dv = new Division();
        DataSet dsDivision = new DataSet();
        string state_cd = string.Empty;
        string sState = string.Empty;
        string[] statecd;

        dsDivision = dv.getStateMailDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            DataSet dsSf = new DataSet();
            dsSf = st.getSt(state_cd);
            //  ddlState.DataTextField = "statename";
            // ddlState.DataValueField = "state_code";
            //  ddlState.DataSource = dsSf;
            //  ddlState.DataBind();


             ddlState.DataTextField = "statename";
            ddlState.DataValueField = "state_code";
            ddlState.DataSource = dsSf;
            ddlState.DataBind();
        }
    }

    #region FillAddressBook
    private void FillStatemulti()
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(div_code);
        if (dsDivision.Tables[0].Rows.Count > 0)
        {
            int i = 0;
            state_cd = string.Empty;
            sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
            statecd = sState.Split(',');
            foreach (string st_cd in statecd)
            {
                if (i == 0)
                {
                    state_cd = state_cd + st_cd;
                }
                else
                {
                    if (st_cd.Trim().Length > 0)
                    {
                        state_cd = state_cd + "," + st_cd;
                    }
                }
                i++;
            }

            State st = new State();
            dsState = st.getStmulti(state_cd);

            chkstate.DataTextField = "statename";
            chkstate.DataValueField = "state_code";
            chkstate.DataSource = dsState;
            chkstate.DataBind();

        }
    }
    #endregion
    #region FillManagers
    private void FillManagers()
    {
        SalesForce sf = new SalesForce();
        //
        string sCrnt_Sf_Code = sf_code;
        if (ddlFFType.SelectedValue.ToString() == "2")
        {
            ddlAlpha.Visible = false;
            if (Session["sf_type"].ToString() == "3")
            {
                if (div_code.Contains(','))
                    div_code = div_code.Substring(0, div_code.Length - 1);
            }
            dsSalesForce = sf.sp_UserList_Hierarchy_Upload(div_code, sCrnt_Sf_Code);
        }
        else if (ddlFFType.SelectedValue.ToString() == "1")
        {
            //FillSF_Alpha();
            ddlAlpha.Visible = true;
            dsSalesForce = sf.UserList_Alpha(div_code, sCrnt_Sf_Code);
        }
        //
        if (ddlFFType.SelectedValue.ToString() == "2")
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                ddlFieldForce.DataTextField = "sf_name";
                ddlFieldForce.DataValueField = "sf_code";
                ddlFieldForce.DataSource = dsSalesForce;
                ddlFieldForce.DataBind();
                //
                ddlSF.DataTextField = "des_color";
                ddlSF.DataValueField = "SF_Code";
                ddlSF.DataSource = dsSalesForce;
                ddlSF.DataBind();
                //
                FillColor();
                //
            }
        }
        //
        if (Session["sf_type"].ToString() == "1")
        {
            ddlFieldForce.Visible = false;
            ddlFFType.Visible = false;
            lblFF.Visible = false;
        }
    }
    #endregion
    //
    #region FillColor (for Dropdown-Manager)
    private void FillColor()
    {
        int j = 0;
        //
        foreach (ListItem ColorItems in ddlSF.Items)
        {
            string bcolor = "";
            try
            {
                bcolor = "#" + ColorItems.Text;
            }
            catch
            {
                bcolor = "#FFFFFF";
            }
            //
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);
            j++;
        }
    }
    #endregion

    #region FillSubDiv
    private void FillSubdiv()
    {
        SubDivision dv = new SubDivision();
        dsSubDivision = dv.getSubdivision(div_code);
        if (dsSubDivision.Tables[0].Rows.Count > 0)
        {
            ddlsub.DataTextField = "subdivision_name";
            ddlsub.DataValueField = "subdivision_code";
            ddlsub.DataSource = dsSubDivision;
            ddlsub.DataBind();
        }
        //FillStatemulti();
        //lblstate.Visible = true;
        //chkstate.Visible = true;
    }
    #endregion


    #region FillAddressBook
    private void FillAddressBook()
    {
        try
        {
            //FillDesignation();
            //
            DataTable dt1 = new DataTable();
            //
            DataTable dtMR = new DataTable();
            DataSet dsmgrsf = new DataSet();
            SalesForce sf = new SalesForce();
            string sCrnt_Sf_Code = sf_code;

            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);

            DataSet DsAudit = sf.SF_Hierarchy(div_code, sCrnt_Sf_Code);
            //
            DataTable dtHO = new DataTable();
            if (DsAudit.Tables[0].Rows.Count > 0 || Session["sf_type"].ToString() == "1")
            {

                if (Session["sf_type"].ToString() == "1")
                {
                    DCR dc = new DCR();
                    dtMR = sf.getMail_MRJointWork_New(div_code, sCrnt_Sf_Code, 0);

                    //dtHO = sf.sp_UserList_HOID(div_code);

                    dtHO = sf.sp_UserList_HOID(div_code);
                    if (dtHO.Rows.Count > 0)
                    {
                        dtMR.Merge(dtHO);
                    }
                    //dtMR.Merge(dtHO);
                    //
                    gvFF.DataSource = dtMR;
                    gvFF.DataBind();
                    //
                    if (dtMR.Rows.Count < 1 || dtMR.Rows.Count == null)
                        lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
                    //
                }
                else if (Session["sf_code"].ToString() == "admin")
                {
                    if (div_code.Contains(','))
                        div_code = div_code.Substring(0, div_code.Length - 1);
                    //
                    dtMR.Clear();



                    dsSalesForce = sf.SalesForceListMgrGet_Mail(div_code, "admin", HO_ID);
                    //dtMR = sf.getAddressBookWithoutAdmin(div_code, sCrnt_Sf_Code, 0);

                    dtHO = sf.sp_UserList_HOID(div_code);
                    dtHO.Merge(dsSalesForce.Tables[0]);
                    //dsSalesForce.Merge(dtHO);

                    dtMR.Merge(dtHO);
                    ViewState["dsSalesForce"] = dtMR;


                    //
                }
                else if (Session["sf_type"].ToString() == "2")
                {
                    if (div_code.Contains(','))
                        div_code = div_code.Substring(0, div_code.Length - 1);
                    //
                    dtMR = sf.getAddressBookMgr_New(div_code, sCrnt_Sf_Code, 0);
                    //dtHO = sf.sp_UserList_HOID(div_code);
                    //dtMR.Merge(dtHO);
                    //

                }
            }
            else
            {
                dtMR = sf.getAuditManagerTeam_mail_New(div_code, sCrnt_Sf_Code, 0);
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            }
            //
            if (ViewState["dsSalesForce"] != null)
            {
                dtMR = (DataTable)ViewState["dsSalesForce"];
            }
            gvFF.DataSource = dtMR;
            gvFF.DataBind();
            //
            string[] sDesg_Col = { "Designation_Code", "Designation_Short_Name" };
            DataTable dtDes = dtMR.DefaultView.ToTable(true, sDesg_Col);
            chkDesgn.DataTextField = "Designation_Short_Name";
            chkDesgn.DataValueField = "Designation_Code";
            chkDesgn.DataSource = dtDes;
            chkDesgn.DataBind();
            //
            if (dtMR.Rows.Count < 1 || dtMR == null)
                lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
            //
            FillgridColor();
            //
        }
        catch (Exception ex)
        {
            Response.Write(ex.Message);
        }
    }
    #endregion

    #region imgAddressClose_Click
    protected void imgAddressClose_Click(object sender, EventArgs e)
    {
        string sel_ff = string.Empty;
        if (ViewState["from"] != null)
        {
            if (ViewState["from"].ToString() == "To")
            {
                txtAddr.Text = "";
            }
         
            //
            foreach (GridViewRow row in gvFF.Rows)
            {
                CheckBox ChkboxRows = (CheckBox)row.FindControl("chkSf");
                Label lblSf_Name = (Label)row.FindControl("lblSf_Name");
                Label lblsf_mail = (Label)row.FindControl("lblsf_mail");
                //
                if (ChkboxRows.Checked)
                {
                    sel_ff = sel_ff + lblSf_Name.Text;
                    sel_ff = sel_ff.Replace("&nbsp;", "");
                    sel_ff = sel_ff + " , ";
                    //
                    temp_code = lblsf_mail.Text.ToString().Substring(0, lblsf_mail.Text.ToString().IndexOf('-'));
                    temp_Name = lblSf_Name.Text.ToString().Replace("&nbsp;", "");
                    mail_to_sf_code = mail_to_sf_code + temp_code + ",";
                    mail_to_sf_Name = mail_to_sf_Name + temp_Name + ",";
                }
            }
            pnlpopup.Visible = false;
        }



        if (ViewState["from"].ToString() == "To")
        {
            txtAddr.Text = sel_ff;
            ViewState["mail_to_sf_code"] = mail_to_sf_code;
            ViewState["mail_to_sf_Name"] = mail_to_sf_Name;
        }


        ViewState["pnlpopup"] = "";
        ViewState["from"] = "";
    }
    #endregion


    #region OnLoadComplete
    protected override void OnLoadComplete(EventArgs e)
    {
        //ServerEndTime = DateTime.Now;
        //TrackPageTime();//It will give you page load time  
    }
    #endregion

    #region imgAddressBook_Click
    protected void imgAddressBook_Click(object sender, EventArgs e)
    {
        if (ViewState["dsSalesForce"] == null)
        {
            FillAddressBook();
        }
   
        rdoadr.SelectedValue = "0";
        ddlFFType_SelectedIndexChanged(sender, e);
        //
        ViewState["from"] = "To";
        ViewState["pnlCompose"] = "true";
        ViewState["pnlpopup"] = "true";
        //
        if (rdoadr.SelectedValue.ToString() == "0")
            Enable_Disable_Control_For_AddressBook(false, true,false);
        else
        {
            Enable_Disable_Control_For_AddressBook(true, false,false);
            //
            ddlAlpha.Visible = true;
            //
            foreach (ListItem item in chkDesgn.Items)
            {
                chkLevelAll.Checked = false;
                chkMR.Checked = false;
                item.Selected = false;
            }
            foreach (ListItem item in chkstate.Items)
            {
                ChkAllState.Checked = false;
                chkMR_State.Checked = false;
                item.Selected = false;
            }
        }
        //
        foreach (ListItem item in chkDesgn.Items)
        {
            chkLevelAll.Checked = false;
            chkMR.Checked = false;
            item.Selected = false;
        }

        foreach (ListItem item in chkstate.Items)
        {
            ChkAllState.Checked = false;
            chkMR_State.Checked = false;
            item.Selected = false;
        }
        //FillgridColor();
        pnlpopup.Visible = true;
    }
    #endregion

    #region ddlFFType_SelectedIndexChanged
    protected void ddlFFType_SelectedIndexChanged(object sender, EventArgs e)
    {
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            chkSf.Checked = false;
        }
        //
        ddlFieldForce.Items.Clear();
        lblSelectedCount.Text = "No.of Filed Force Selected : " + 0;
        lblFF.Text = "Field Force";
        FillManagers();
        //FillgridColor();
        //
    }
    #endregion
    //
    #region ddlFieldForce_SelectedIndexChanged
    protected void ddlFieldForce_SelectedIndexChanged(object sender, EventArgs e)
    {
        chkLevelAll.Checked = false;
        foreach (ListItem item in chkDesgn.Items)
        {
            item.Selected = false;
        }
        if (ddlFieldForce.SelectedValue.ToString().Trim().Length > 0)
        {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillSalesForce();
        }
        else if (ddlFFType.SelectedItem.Text == "Division")
        {
            dsUserList = sf.UserList_Self(ddlFieldForce.SelectedValue, "admin");
            //
            if (dsUserList.Tables[0].Rows.Count > 0)
            {
                gvFF.Visible = true;
                gvFF.DataSource = dsUserList;
                gvFF.DataBind();
            }
        }
        //
        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked)
                i++;
        }
        //
        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;
        //
        FillgridColor();
        FillColor();
        //
    }
    //
    #endregion
    #region FillSalesForce
    private void FillSalesForce()
    {
        DataTable dt = new DataTable();
        //
        if (ddlFieldForce.SelectedValue == "")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        else if (ddlFieldForce.SelectedValue == "0")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        //
        if (Session["sf_type"].ToString() == "3")
        {
            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
        }
        dsUserList = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue);
        //
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblsf_Code = (Label)grid_row.FindControl("lblsf_Code");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            for (int i = 0; i < dsUserList.Tables[0].Rows.Count; i++)
            {
                if (dsUserList.Tables[0].Rows[i]["sf_code"].ToString() == lblsf_Code.Text)
                {
                    chkSf.Checked = true;
                    break;
                }
                //
            }
        }
    }
    #endregion
    private void FillDllSalesForce()
    {
        DataTable dt = new DataTable();
        //
        if (ddlState.SelectedValue == "0")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }

        


        //
        if (Session["sf_type"].ToString() == "3")
        {
            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
        }
        //dsUserList = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue);

        Division dv = new Division();

        //dsUserList = dv.getStatePerDivision(div_code);      

        //
        foreach (GridViewRow grid_row in gvFF.Rows)
        {
            Label lblState = (Label)grid_row.FindControl("lblState");
            CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
            //for (int i = 0; i < dsUserList.Tables[0].Rows.Count; i++)
            //{
            if (ddlState.SelectedValue == lblState.Text)
            {
                chkSf.Checked = true;
                //break;
            }
            //
            //}
        }
    }

    public void gvDetails_RowDeleting(object sender, GridViewDeleteEventArgs e)
    {
    }

    #region ddlAlpha_SelectedIndexChanged
    protected void ddlAlpha_SelectedIndexChanged(object sender, EventArgs e)
    {/*
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.UserList_Alphasearch(div_code, "admin", ddlAlpha.SelectedValue);
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();
            //
            ddlSF.DataTextField = "sf_color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }*/
    }
    #endregion
    #region ddlSub_SelectedIndexChanged
    protected void ddlSub_SelectedIndexChanged(object sender, EventArgs e)
    {
        if (ddlsub.SelectedValue.ToString().Trim().Length > 0)
        {
            ViewState["dsSalesForce"] = null;
            ViewState["dsDoctor"] = null;
            FillDdlSubsalesforce();
        }


        int i = 0;
        foreach (GridViewRow row in gvFF.Rows)
        {
            CheckBox chkSf = (CheckBox)row.FindControl("chkSf");
            if (chkSf.Checked)
                i++;
        }

        lblSelectedCount.Text = "No.of Filed Force Selected : " + i;

        FillgridColor();
        FillColor();
        FillStatemulti();
        lblstate.Visible = true;
        chkstate.Visible = true;

    }
    #endregion

    #region FillDdlSubsalesforce
    private void FillDdlSubsalesforce()
    {
        DataTable dt = new DataTable();
        //
        if (ddlsub.SelectedValue == "0")
        {
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                chkSf.Checked = false;
            }
        }
        //
        if (Session["sf_type"].ToString() == "3")
        {
            if (div_code.Contains(','))
                div_code = div_code.Substring(0, div_code.Length - 1);
        }
        //dsUserList = sf.UserList_get_SelfMail(div_code, ddlFieldForce.SelectedValue);

        Division dv = new Division();

        //dsUserList = dv.getStatePerDivision(div_code);  

        int i = 0;
        foreach (ListItem item in chkstate.Items)
        {
            bool blChckd = item.Selected;
            foreach (GridViewRow grid_row in gvFF.Rows)
            {
                Label lblsubdivision_code = (Label)grid_row.FindControl("lblsubdivision_code");
                Label lblState = (Label)grid_row.FindControl("lblState");
                CheckBox chkSf = (CheckBox)grid_row.FindControl("chkSf");
                if (item.Value == lblState.Text || ddlsub.SelectedValue == lblsubdivision_code.Text)
                {
                    chkSf.Checked = blChckd;
                    if (chkSf.Checked)
                        i++;
                }

            }
        }


    }
    #endregion

    #region grdFF_RowDataBound
    protected void grdFF_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        try
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                Label lblBackColor = (Label)e.Row.FindControl("lblsf_color");
                Label lblSFType = (Label)e.Row.FindControl("lblsf_Type");

                if (lblBackColor != null)
                {
                    string sClrCode = "#FFFFFF";
                    if (lblBackColor.Text == "-Level1")/*level 1*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#37C8FF";
                        else
                            sClrCode = "#BADCF7";
                    }
                    else if (lblBackColor.Text == "-Level2")/*level 2*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#718FC7";
                        else
                            sClrCode = "#ccffcc";
                    }
                    else if (lblBackColor.Text == "-Level3")/*level 3*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#e0ffff";
                        else
                            sClrCode = "#ffffcc";
                    }
                    else if (lblBackColor.Text == "-Level4")/*level 4*/
                    {
                        if (lblSFType.Text == "1")
                            sClrCode = "#fff0f5";
                        else
                            sClrCode = "e0ffff";
                    }
                    e.Row.BackColor = System.Drawing.Color.FromName(sClrCode);
                }
            }
        }
        catch { }
    }
    #endregion
   
    public void gvDetails_RowCommand(object sender, GridViewCommandEventArgs e)
    {


        if (e.CommandName == "Delete")
        {

            try
            {
                int Id = Convert.ToInt32(e.CommandArgument);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandText = "delete from Mas_HomePage_Image where Id=@Id";
                cmd.Connection = con;
                cmd.Parameters.Add(new SqlParameter("@Id", Id));
                con.Open();
                cmd.ExecuteNonQuery();
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
                BindGridviewData();
                con.Close();
            }
            catch (Exception ex)
            {

            }
        }
        if (e.CommandName == "Edit")
        {
            int Id = Convert.ToInt32(e.CommandArgument);





            SqlCommand cmd = new SqlCommand("select * from Mas_HomePage_Image where Division_Code = '" + div_code + "' and id='" + Id + "' ", con);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            da.Fill(ds);
            con.Close();
            gvDetails.DataSource = ds.Tables[0];
            gvDetails.DataBind();
            LinkButton lnkbtn = sender as LinkButton;
          //  GridViewRow gvrow = lnkbtn.NamingContainer as GridViewRow;
            string filePath = ds.Tables[0].Rows[0][2].ToString();
            Response.ContentType = "image/jpg";
            Response.AddHeader("Content-Disposition", "attachment;filename=\"" + filePath + "\"");
            Response.TransmitFile(Server.MapPath(filePath));
            Response.End();
        }


    }
}
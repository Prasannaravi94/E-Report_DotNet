using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Hospital_HospitalCreation : System.Web.UI.Page
{
    DataSet dsHospital = null;
    DataSet dsDoc = null;
    string sf_code = string.Empty;
    string Hospital_Name = string.Empty;
    string Hospital_Address1 = string.Empty;
    string Hospital_Contact = string.Empty;
    string Hospital_Phone = string.Empty;
    string Hospital_Terr = string.Empty;
    string div_code = string.Empty;
    int iCnt = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        heading.InnerText = this.Page.Title;
        lblSelect.Visible = true;
        if (Session["sf_type"].ToString() == "1")
        {
            DataList1.BackColor = System.Drawing.Color.White;
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            //btnBack.Visible = false;

        }
        else
        {
            DataList1.BackColor = System.Drawing.Color.White;
            sf_code = Session["sf_code"].ToString();
            if (Session["sf_code_Temp"].ToString() != "")
            {
              sf_code = Session["sf_code_Temp"].ToString();
            }
            UserControl_MenuUserControl c1 =
                (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            //btnBack.Visible = false;
            //menu1.Visible = false;
            Session["backurl"] = "HospitalList.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:#696D6E;'>For " + Session["sfName"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:#696D6E;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:#696D6E;'>  " + Session["sf_HQ"] + "</span>" + " )";
        }
        if (!Page.IsPostBack)
        {
            Session["backurl"] = "HospitalList.aspx";
            //menu1.Title = this.Page.Title;
            FillHospital();
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
        }
    }
    protected override void OnLoadComplete(EventArgs e)
    {
        ServerEndTime = DateTime.Now;
        TrackPageTime();//It will give you page load time  
    }
    public void TrackPageTime()
    {
        TimeSpan serverTimeDiff = ServerEndTime.Subtract(ServerStartTime);
        time = serverTimeDiff.Minutes;
    }
    protected DataSet FillTerritory()
    {
        ListedDR lstDR = new ListedDR();
        dsHospital = lstDR.FetchTerritory(sf_code);
        return dsHospital;
    }
    //protected void btnBack_Click(object sender, EventArgs e)
    //{
    //    System.Threading.Thread.Sleep(time);
    //    try
    //    {
    //        Server.Transfer("HospitalList.aspx");
    //    }
    //    catch (Exception ex)
    //    {

    //    }
    //}
    protected void btnGo_Click(object sender, EventArgs e)
    {
        DataList1.Visible = true;
        FillDoctors();
        FillddlHosColor();
    }

    private void FillddlHosColor()
    {
        Hospital hosp = new Hospital();

        dsHospital = hosp.getHospitalTerr(sf_code);
        foreach (DataRow row in dsHospital.Tables[0].Rows)
        {
            if (row != null)
            {
                dsDoc = hosp.Map_Hos_ListedDr(sf_code, div_code, row["Hospital_Code"].ToString());

                if (dsDoc.Tables[0].Rows.Count > 0)
                {
                    string Hos_Code = row["Hospital_Code"].ToString();
                    string Hos_Text = row["Hospital_Name"].ToString() + " " + "(" + dsDoc.Tables[0].Rows.Count + ")";
                    row["Hospital_Name"] = Hos_Text;

                    ListItem ddl = ddlHospitalName.Items.FindByValue(Hos_Code);
                    if (ddl != null)
                    {
                        ddl.Attributes.Add("style", "background-color: #ffff00 !important");
                    }
                }
            }
        }
    }
    protected void btnclr_Click(object sender, EventArgs e)
    {
        Response.Redirect(Request.Url.AbsoluteUri);
    }
    private void FillDoctors()
    {
        btnSubmit.Visible = true;

        ListedDR LstDoc = new ListedDR();
        dsDoc = LstDoc.getListedDr_new(sf_code);
        if (dsDoc.Tables[0].Rows.Count > 0)
        {
            lblSelect.Visible = false;
            DataList1.Visible = true;
            DataList1.DataSource = dsDoc;
            DataList1.DataBind();
        }
        else
        {
            lblSelect.Visible = false;
            DataList1.DataSource = dsDoc;
            DataList1.DataBind();
        }

        string str_DoctCode = "";
        Hospital hosp = new Hospital();
        dsHospital = hosp.getDoctorsfor_MapHos(ddlHospitalName.SelectedValue);

        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            for (int i = 0; i < dsHospital.Tables[0].Rows.Count; i++)
            {
                str_DoctCode = dsHospital.Tables[0].Rows[i].ItemArray.GetValue(0).ToString();
                foreach (DataListItem grid in DataList1.Items)
                {
                    Label chk = (Label)grid.FindControl("lblDoctorsCode");
                    string[] Salesforce;
                    if (str_DoctCode != "")
                    {
                        iCnt = -1;
                        Salesforce = str_DoctCode.Split(',');
                        foreach (string sf in Salesforce)
                        {
                            CheckBox chkCatName = (CheckBox)grid.FindControl("chkDocName");
                            Label hf = (Label)grid.FindControl("lblDoctorsCode");

                            if (sf == hf.Text)
                            {
                                chkCatName.Checked = true;
                                chkCatName.Attributes.Add("style", "Color: Red; font-weight:Bold; font-size:16px; ");
                            }
                        }
                    }
                }
            }
        }
    }
    protected void FillHospital()
    {
        Hospital hosp = new Hospital();

        dsHospital = hosp.getHospitalTerr(sf_code);
        if (dsHospital.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsHospital.Tables[0].Rows)
            {
                if (row != null)
                {
                    dsDoc = hosp.Map_Hos_ListedDr(sf_code, div_code, row["Hospital_Code"].ToString());

                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        string Hos_Code = row["Hospital_Code"].ToString();
                        string Hos_Text = row["Hospital_Name"].ToString() + " " + "(" + dsDoc.Tables[0].Rows.Count + ")";
                        row["Hospital_Name"] = Hos_Text;
                    }
                }
            }

            ddlHospitalName.DataTextField = "Hospital_Name";
            ddlHospitalName.DataValueField = "Hospital_Code";
            ddlHospitalName.DataSource = dsHospital;
            ddlHospitalName.DataBind();
            ddlHospitalName.Items.Insert(0, new ListItem("---Select---", "0", true));
        }
        else
        {
            foreach (DataRow row in dsHospital.Tables[0].Rows)
            {
                if (row != null)
                {
                    dsDoc = hosp.Map_Hos_ListedDr(sf_code, div_code, row["Hospital_Code"].ToString());

                    if (dsDoc.Tables[0].Rows.Count > 0)
                    {
                        string Hos_Code = row["Hospital_Code"].ToString();
                        string Hos_Text = row["Hospital_Name"].ToString() + " " + "(" + dsDoc.Tables[0].Rows.Count + ")";
                        row["Hospital_Name"] = Hos_Text;
                    }
                }
            }

            ddlHospitalName.DataTextField = "Hospital_Name";
            ddlHospitalName.DataValueField = "Hospital_Code";
            ddlHospitalName.DataSource = dsHospital;
            ddlHospitalName.DataBind();
            ddlHospitalName.Items.Insert(0, new ListItem("---Select---", "0", true));
        }

        FillddlHosColor();
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        string strPrd = "";
        int iReturn = -1;
        Hospital lst = new Hospital();

        foreach (DataListItem grid in DataList1.Items)
        {
            Label chk = (Label)grid.FindControl("lblDoctorsCode");
            CheckBox chkDocName = (CheckBox)grid.FindControl("chkDocName");
            if (chkDocName.Checked == true)
            {
                strPrd += chk.Text + ",";
            }
        }
        if (strPrd == "")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Select atleast single Doctor');</script>");
        }
        else
        {
            strPrd = strPrd.Remove(strPrd.Length - 1);
            string[] ListedDrCode = strPrd.Split(',');

            bool s = lst.HosDoctors_RecordExist(sf_code, ddlHospitalName.SelectedValue);

            if (s == false)
            {
                foreach (string ListedDrCod in ListedDrCode)
                {
                    iReturn = lst.RecordAdd_DoctorsMap(ddlHospitalName.SelectedValue, ListedDrCod, sf_code, div_code);
                }

                if (iReturn > 0)
                {
                    DataList1.Visible = false;
                    btnSubmit.Visible = false;
                    lblSelect.Visible = true;
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                }
            }
            else
            {
                iReturn = lst.RecordUpdateHosLstdDrMap(sf_code, ddlHospitalName.SelectedValue, div_code);

                foreach (string ListedDrCod in ListedDrCode)
                {
                    iReturn = lst.RecordAdd_DoctorsMap(ddlHospitalName.SelectedValue, ListedDrCod, sf_code, div_code);
                }
                if (iReturn > 0)
                {
                    DataList1.Visible = false;
                    btnSubmit.Visible = false;
                    lblSelect.Visible = true;
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Mapped Successfully');</script>");
                }
            }
        }

        FillHospital();
        FillddlHosColor();
    }
    protected void btnback1_Click(object sender, EventArgs e)
    {
        Response.Redirect("HospitalList.aspx");
    }
}
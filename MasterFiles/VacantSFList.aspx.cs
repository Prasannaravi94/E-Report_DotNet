using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using System.Xml;
using System.Web.Services;
using System.Web.Script.Services;

public partial class MasterFiles_VacantSFList : System.Web.UI.Page
{

    DataSet dsSalesForce = null;
    string div_code = string.Empty;
    string sCmd = string.Empty;
    string search = string.Empty;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    DataSet dsDivision = null;
    DataSet dsState = null;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["backurl"] = "SalesForceList.aspx";
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            //FillSalesForce();
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            ddlFieldForceType.Focus();
            ddlFields.SelectedValue = "Sf_Name";
            btnSearch.Visible = true;
            btnGo.Visible = true;
            FillSF_Alpha();
            fill_sales2();
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
    private void FillSalesForce()
    {
        SalesForce sf = new SalesForce();
        //if (ddlFieldForceType.SelectedValue == "0")
        //{
           // dsSalesForce = sf.getSalesForceVaclist(div_code);

        //    dsSalesForce = sf.GetSalesforce_vaclist(div_code);
        //}
        //else
        //{
            dsSalesForce = sf.getSalesForceVaclist(div_code,ddlFieldForceType.SelectedValue);
        //}
            if (ddlFieldForceType.SelectedValue == "R")
            {
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    grdSalesForce.Columns[7].Visible = true;
                    grdSalesForce.Columns[9].Visible = true;
                    grdSalesForce.Columns[10].Visible = false;
                    grdSalesForce.Columns[12].Visible = false;
                    grdSalesForce.Visible = true;
                    grdSalesForce.DataSource = dsSalesForce;
                    grdSalesForce.DataBind();
                }
                else
                {
                    grdSalesForce.DataSource = dsSalesForce;
                    grdSalesForce.DataBind();
                }
            }
            else
            {
                if (dsSalesForce.Tables[0].Rows.Count > 0)
                {
                    grdSalesForce.Columns[7].Visible = false;
                    grdSalesForce.Columns[9].Visible = false;
                    grdSalesForce.Columns[10].Visible = true;
                    grdSalesForce.Columns[12].Visible = true;
                    grdSalesForce.Visible = true;
                    grdSalesForce.DataSource = dsSalesForce;
                    grdSalesForce.DataBind();
                }
                else
                {
                    grdSalesForce.DataSource = dsSalesForce;
                    grdSalesForce.DataBind();
                }
            }
    }
    protected void btnSearch_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
            FillSalesForce();
            GridView1.Visible = false;
     
    }

    protected void grdSalesForce_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Activate")
        {
            string sf_code = Convert.ToString(e.CommandArgument);

            //Deactivate
            SalesForce sf = new SalesForce();
            int iReturn = sf.VacActivate(sf_code,"");
            if (iReturn > 0)
            {
              //  menu1.Status = "SalesForce has been Activated Successfully";
                //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Activated Successfully');</script>");
                //Response.Redirect("~/MasterFiles/ReMapReportingStructure.aspx?reporting_to=" + sf_code);
                Response.Write("<script>alert('Activated Successfully') ; location.href='~/MasterFiles/ReMapReportingStructure.aspx?reporting_to=" + sf_code  + "'</script>");
              
            }
            else
            {
               // menu1.Status = "Unable to Activate";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Activate');</script>");
            }
            FillSalesForce();
        }
        if (e.CommandName == "Deactivate")
        {
            string sf_code = Convert.ToString(e.CommandArgument);

            //Deactivate

            SalesForce sf = new SalesForce();
            int iReturn = sf.DeActivate(sf_code, ddlFieldForceType.SelectedValue);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Unable to Deactivate');</script>");
            }
            FillSalesForce();
        }
    }

    protected void grdSalesForce_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        grdSalesForce.PageIndex = e.NewPageIndex;
        sCmd = Session["GetCmdArgChar"].ToString();

        if (sCmd == "All")
        {
            FillSalesForce();
        }
        else if (sCmd != "")
        {
            FillSalesForce(sCmd);
        }
        else if (txtsearch.Text != "")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }

        else if (hdnProduct.Value != "")
        {
            if (Convert.ToInt16(hdnProduct.Value) > 0)
            {
                Search();
            }
        }
        else if (ddlSrc.SelectedIndex > 0)
        {
            Search();
        }
    }

    protected void ddlFields_SelectedIndexChanged(object sender, EventArgs e)
    {
        search = ddlFields.SelectedValue.ToString();
        txtsearch.Text = string.Empty;
        grdSalesForce.PageIndex = 0;

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
        {
            txtsearch.Visible = true;
            btnGo.Visible = true;
            ddlSrc.Visible = false;
            txtsearch.Focus();
        }
        else
        {
            txtsearch.Visible = false;
            ddlSrc.Visible = true;
            btnGo.Visible = true;
        }

        if (search == "StateName")
        {
            FillState(div_code);
        }

        if (search == "Designation_Name")
        {
            FillDesignation();
            ddlSrc.Focus();
        }
    }

    private void FillDesignation()
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getDesignation_SN(div_code);
        ddlSrc.DataTextField = "Designation_Name";
        ddlSrc.DataValueField = "Designation_Code";
        ddlSrc.DataSource = dsSalesForce;
        ddlSrc.DataBind();
    }
    protected void ddlSrc_SelectedIndexChanged(object sender, EventArgs e)
    {

    }

    protected void btnGo_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Session["GetCmdArgChar"] = string.Empty;
        grdSalesForce.PageIndex = 0;
        Search();
        GridView1.Visible = false;
        if (ddlFieldForceType.SelectedValue == "R")
        {
            grdSalesForce.Columns[7].Visible = true;
            grdSalesForce.Columns[9].Visible = true;
            grdSalesForce.Columns[10].Visible = false;
            grdSalesForce.Columns[12].Visible = false;
        }
        else
        {
            grdSalesForce.Columns[7].Visible = false;
            grdSalesForce.Columns[9].Visible = false;
            grdSalesForce.Columns[10].Visible = true;
            grdSalesForce.Columns[12].Visible = true;
        }
    }

    private void Search()
    {
        search = ddlFields.SelectedValue.ToString();

        if (search == "UsrDfd_UserName" || search == "Sf_Name" || search == "Sf_HQ" || search == "sf_emp_id")
        {
            FindSalesForce(ddlFields.SelectedValue, txtsearch.Text, Session["div_code"].ToString());
        }

        else if (search == "StateName")
        {
            txtsearch.Text = string.Empty;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_st_vacant(div_code, hdnProduct.Value, ddlFieldForceType.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
        else if (search == "Designation_Name")
        {
            txtsearch.Text = string.Empty;
            SalesForce sf = new SalesForce();
            dsSalesForce = sf.getSalesForce_desVacant(div_code, hdnProduct.Value, ddlFieldForceType.SelectedValue);
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
    }

    private void FindSalesForce(string sSearchBy, string sSearchText, string div_code)
    {
        string sFind = string.Empty;
        sFind = " AND a." + sSearchBy + " like '" + sSearchText + "%' AND a.Division_Code = '" + div_code + ",' ";

        SalesForce sf = new SalesForce();
        dsSalesForce = sf.FindSalesForceVacant(sFind, ddlFieldForceType.SelectedValue);

        if (ddlFieldForceType.SelectedValue == "R")
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Columns[7].Visible = true;
                grdSalesForce.Columns[9].Visible = true;
                grdSalesForce.Columns[10].Visible = false;
                grdSalesForce.Columns[12].Visible = false;
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
        else
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Columns[7].Visible = false;
                grdSalesForce.Columns[9].Visible = false;
                grdSalesForce.Columns[10].Visible = true;
                grdSalesForce.Columns[12].Visible = true;
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
    }

    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)//done by resh
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            grdSalesForce.PageIndex = 0;
            FillSalesForce();
        }
        else
        {
            grdSalesForce.PageIndex = 0;
            FillSalesForce(sCmd);
        }
        GridView1.Visible = false;
        if (ddlFieldForceType.SelectedValue == "R")
        {
            grdSalesForce.Columns[7].Visible = true;
            grdSalesForce.Columns[9].Visible = true;
            grdSalesForce.Columns[10].Visible = false;
            grdSalesForce.Columns[12].Visible = false;
        }
        else
        {
            grdSalesForce.Columns[7].Visible = false;
            grdSalesForce.Columns[9].Visible = false;
            grdSalesForce.Columns[10].Visible = true;
            grdSalesForce.Columns[12].Visible = true;
        }

    }

    private void FillSalesForce(string sAlpha)
    {
        SalesForce sf = new SalesForce();
        dsSalesForce = sf.getSalesForceAlpha_vacant(div_code, sAlpha, ddlFieldForceType.SelectedValue);

        if (ddlFieldForceType.SelectedValue == "R")
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Columns[7].Visible = true;
                grdSalesForce.Columns[9].Visible = true;
                grdSalesForce.Columns[10].Visible = false;
                grdSalesForce.Columns[12].Visible = false;
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
        else
        {
            if (dsSalesForce.Tables[0].Rows.Count > 0)
            {
                grdSalesForce.Columns[7].Visible = false;
                grdSalesForce.Columns[9].Visible = false;
                grdSalesForce.Columns[10].Visible = true;
                grdSalesForce.Columns[12].Visible = true;
                grdSalesForce.Visible = true;
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
            else
            {
                grdSalesForce.DataSource = dsSalesForce;
                grdSalesForce.DataBind();
            }
        }
    }

    private void FillSF_Alpha()
    {
        //SalesForce sf = new SalesForce();
        //dsSalesForce = sf.getSalesForcelist_Alphabet_ForVacant(div_code);
        //if (dsSalesForce.Tables[0].Rows.Count > 0)
        //{
        //    dlAlpha.DataSource = dsSalesForce;
        //    dlAlpha.DataBind();
        //}

        DataTable dt = new DataTable();

        string[] letters = { "All", "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K",
                     "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V",
                     "W", "X", "Y", "Z"};
        dt.Columns.Add(new DataColumn("Letter",
       typeof(string)));

        for (int i = 0; i < letters.Length; i++)
        {
            DataRow dr = dt.NewRow();
            dr[0] = letters[i];
            dt.Rows.Add(dr);
        }
        dlAlpha.DataSource = dt.DefaultView;
        dlAlpha.DataBind();
    }

    private void FillState(string div_code)
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
            dsState = st.getState(state_cd);
            ddlSrc.DataTextField = "statename";
            ddlSrc.DataValueField = "state_code";
            ddlSrc.DataSource = dsState;
            ddlSrc.DataBind();
        }
    }

    private void fill_sales2()
    {
        DataSet dsSales = new DataSet();
        SalesForce sf = new SalesForce();
        dsSales = sf.getSales_SampleNames();

        if (dsSales.Tables[0].Rows.Count > 0)
        {
            GridView1.Visible = true;
            GridView1.DataSource = dsSales;
            GridView1.DataBind();
        }
    }
    protected void GridView1_Rowdatabound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblactivate = (Label)e.Row.FindControl("lblactivate");
            Label lbledit = (Label)e.Row.FindControl("lbledit");
            Label lblrej = (Label)e.Row.FindControl("lblrej");
            Label lbldeact = (Label)e.Row.FindControl("lbldeact");

            lblactivate.Text = "ACTIVATE";
            lbledit.Text = "EDIT";
            lblrej.Text = "REJOIN";
            lbldeact.Text = "DEACTIVATE";
        }


    }


    [WebMethod(EnableSession = true)]
    [ScriptMethod]

    public static List<StateDetail2> GetDropDown(StateDetail2 objDDL)
    {
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        List<StateDetail2> objDel = new List<StateDetail2>();

        if (objDDL.Type == "StateName")
        {
            Division dv = new Division();
            DataSet dsDivision = dv.getStatePerDivision(div_code);
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                int i = 0;
                string[] statecd;
                string state_cd = string.Empty;
                string sState = dsDivision.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
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
                DataSet dsState = st.getState(state_cd);

                foreach (DataRow dr in dsState.Tables[0].Rows)
                {
                    StateDetail2 objStateDet = new StateDetail2();
                    objStateDet.StateName = dr["statename"].ToString();
                    objStateDet.State_Code = dr["state_code"].ToString();
                    objDel.Add(objStateDet);
                }

            }
        }
        else if (objDDL.Type == "Designation_Name")
        {
            SalesForce sf = new SalesForce();
            DataSet dsSalesForce = sf.getDesignation_SN(div_code);
            foreach (DataRow Des in dsSalesForce.Tables[0].Rows)
            {
                StateDetail2 objDes = new StateDetail2();
                objDes.Designation_Name = Des["Designation_Name"].ToString();
                objDes.Designation_Code = Des["Designation_Code"].ToString();
                objDel.Add(objDes);
            }
        }

        return objDel;
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Response.Redirect("SalesForceList.aspx");
    }
}
public class StateDetail2
{
    public string StateName { get; set; }
    public string State_Code { get; set; }
    public string Designation_Name { get; set; }
    public string Designation_Code { get; set; }
    public string Type { get; set; }

}
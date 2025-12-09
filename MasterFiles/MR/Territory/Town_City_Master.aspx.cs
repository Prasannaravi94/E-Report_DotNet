using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Data;
using Bus_EReport;

public partial class MasterFiles_MR_Town_City_Master : System.Web.UI.Page
{
       string div_code = string.Empty;
      DataSet dsTerritory = null;
     string sf_code = string.Empty;
     DataSet dsDivision = new DataSet();
     DataSet dsState = new DataSet();
     string[] statecd;
     string state_cd = string.Empty;
     string sState = string.Empty;
    DataSet dsListedDR = new DataSet();
   SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    protected void Page_Load(object sender, EventArgs e)
    {
      
             div_code = Session["div_code"].ToString();
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
           (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            // usc_MR.FindControl("btnBack").Visible = false;
          
           
        }
        else
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MenuUserControl c1 =
             (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;

            
        }

            txtCity.Focus();

            if (!IsPostBack)
            {
               
                FillState(div_code);
           FillDoc_Alpha();
             //   FillGrid();

            }

    

       
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
            if (dsState.Tables[0].Rows.Count > 0)
            {
                ddlState.DataTextField = "statename";
                ddlState.DataValueField = "state_code";
                ddlState.DataSource = dsState;
                ddlState.DataBind();
            }
        }
    }
     void FillGrid()

    {

        try

        {

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "select Sl_No, State_code,State_Name,Town_Name,Town_Type,case when Town_Type='ME' then 'Metro'  else case when Town_Type='NM' then 'Non-Metro' end  end as Town , " +
                              "(select count(Town_City) from mas_territory_creation d where d.Town_City = a.Town_Name and  Territory_Active_Flag=0 and division_code='" + div_code + "' ) as Active " +
                              "from Mas_State_TownCity a where division_code='" + div_code + "' and active_flag=0 and State_code='" + ddlState.SelectedValue + "' order by Town_Name";

            cmd.Connection = con;

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataSet ds = new DataSet();

            da.Fill(ds);

            gvCity.DataSource = ds;

            gvCity.DataBind();
            foreach (GridViewRow row in gvCity.Rows)
            {
                LinkButton lnkdeact = (LinkButton)row.FindControl("btnDelete");
                Label lblimg = (Label)row.FindControl("lblimg");
                Label lblCount = (Label)row.FindControl("lblDr");
                //if (Convert.ToInt32(dsDocCat.Tables[0].Rows[row.RowIndex][4].ToString()) > 0)
                if (lblCount.Text != "0")
                {
                    // grdProCat.Rows[row.RowIndex].Cells[7].Enabled = false;
                    lnkdeact.Visible = false;
                    lblimg.Visible = true;
                }
            }

        }

        catch

        {

 

        }

    }

 

    void ClearControls()

    {

        try

        {

            txtCity.Text = "";

            hidCityID.Value = "";
            Territory_Type.SelectedIndex =0;

            btnSave.Visible = true;

            btnUpdate.Visible = false;

        }

        catch

        {

 

            throw;

        }

    }

 

    protected void btnSave_Click(object sender, EventArgs e)

    {

        try

        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();

            cmd.CommandText = " SELECT ISNULL(MAX(Sl_No),0)+1 FROM Mas_State_TownCity";

            SqlDataAdapter das = new SqlDataAdapter(cmd);
            DataSet dscity = new DataSet();

            das.Fill(dscity);
            int city_id = Convert.ToInt32(dscity.Tables[0].Rows[0][0].ToString());

            cmd.CommandText = "SELECT Town_Name FROM Mas_State_TownCity WHERE Town_Name='" + txtCity.Text.Trim() + "' and State_code='"+ddlState.SelectedValue+"' and Division_Code = '" + div_code + "' and Active_Flag=0";

            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataSet dsTown = new DataSet();

            dat.Fill(dsTown);
            cmd.Connection = con;

            if (dsTown.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
            }
            else
            {
                cmd.CommandText = "insert into Mas_State_TownCity (Sl_No,State_code,State_Name,Town_Name,Town_Type,Division_Code,Creation_Date,Active_Flag) values (@city_id,@st_code,@st_name,@City_Name,@City_Type,@div_code,getdate(),0)";


                cmd.Parameters.AddWithValue("@city_id", city_id);
                cmd.Parameters.AddWithValue("@st_code", ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@st_name", ddlState.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@City_Name", txtCity.Text.Trim());

                cmd.Parameters.AddWithValue("@City_Type", Territory_Type.SelectedValue);

                cmd.Parameters.AddWithValue("@div_code", div_code);

                cmd.Connection = con;

                //  con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

                FillGrid();

                ClearControls();

                // lblMessage.Text = "Saved Successfully.";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            }
        }

        catch

        {

 

        }

        finally

        {

            if (con.State == ConnectionState.Open)

                con.Close();

        }

    }

 

    protected void btnClear_Click(object sender, EventArgs e)

    {

        try

        {

            ClearControls();

        }

        catch

        {

 

        }

    }

 

    protected void btnEdit_Click(object sender, EventArgs e)

    {

        try

        {

            ClearControls();

            LinkButton btn = sender as LinkButton;

            GridViewRow grow = btn.NamingContainer as GridViewRow;

            hidCityID.Value = (grow.FindControl("lblCityID") as Label).Text;

            txtCity.Text = (grow.FindControl("lblCity") as Label).Text;

            Territory_Type.SelectedValue = (grow.FindControl("type") as Label).Text;

         //   txtAddress.Text = (grow.FindControl("lblAddress") as Label).Text;

            btnSave.Visible = false;

            btnUpdate.Visible = true;

        }

        catch

        {

 

        }

    }

 

    protected void btnUpdate_Click(object sender, EventArgs e)

    {

        try

        {
            con.Open();

            SqlCommand cmd = con.CreateCommand();


            cmd.CommandText = "SELECT Town_Name FROM Mas_State_TownCity WHERE Town_Name='" + txtCity.Text.Trim() + "' and State_code='" + ddlState.SelectedValue + "' and Division_Code = '" + div_code + "' and Active_Flag=0  AND Sl_No!='" + hidCityID.Value + "'";

            SqlDataAdapter dat = new SqlDataAdapter(cmd);
            DataSet dsTown = new DataSet();

            dat.Fill(dsTown);
            cmd.Connection = con;
            con.Close();
            if (dsTown.Tables[0].Rows.Count > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Already Exist');</script>");
                ClearControls();
            }
            else
            {

                cmd.CommandText = "update Mas_State_TownCity set Town_Name=@City,Town_Type=@Type,State_code=@st_code,State_Name=@st_name where Sl_No=@City_Code and Division_code='" + div_code + "'";

                cmd.Parameters.AddWithValue("@City", txtCity.Text);

                cmd.Parameters.AddWithValue("@Type", Territory_Type.SelectedValue);
                cmd.Parameters.AddWithValue("@st_code", ddlState.SelectedValue);
                cmd.Parameters.AddWithValue("@st_name", ddlState.SelectedItem.Text.Trim());
                cmd.Parameters.AddWithValue("@City_Code", hidCityID.Value);

                cmd.Connection = con;

                con.Open();

                cmd.ExecuteNonQuery();

                con.Close();

                FillGrid();

                ClearControls();

                //lblMessage.Text = "Updated Successfully.";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
            }

        }

        catch

        {

 

        }

        finally

        {

            if (con.State == ConnectionState.Open)

                con.Close();

        }

    }

 

    protected void btnDelete_Click(object sender, EventArgs e)

    {

        try

        {

            ClearControls();

            LinkButton btn = sender as LinkButton;

            GridViewRow grow = btn.NamingContainer as GridViewRow;

            SqlCommand cmd = new SqlCommand();

            cmd.CommandText = "update Mas_State_TownCity set active_flag=1,Deactivation_Date=getdate() where Sl_No=@City_Code and Division_code='"+div_code+"'";

            cmd.Parameters.AddWithValue("@City_Code", (grow.FindControl("lblCityID") as Label).Text);

            cmd.Connection = con;

            con.Open();

            cmd.ExecuteNonQuery();

            con.Close();

            FillGrid();
            ClearControls();

          //  lblMessage.Text = "Deleted Successfully.";
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deactivated Successfully');</script>");

        }

        catch

        {

 

        }

        finally

        {

            if (con.State == ConnectionState.Open)

                con.Close();

        }

    }
    protected void gvCity_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
           
           
        }
    }


 
    protected void gvCity_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        gvCity.PageIndex = e.NewPageIndex;

        FillGrid();

    }
    protected void btnGo_Click(object sender, EventArgs e)
    {
        ClearControls();
        FillDoc_Alpha();
        FillGrid();
    }
    private void FillDoc_Alpha()
    {
        ListedDR Lstdr = new ListedDR();
        dsListedDR = Lstdr.getCity_Alphabet(div_code,ddlState.SelectedValue);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            dlAlpha.DataSource = dsListedDR;
            dlAlpha.DataBind();
        }
    }
    private void FillGrid(string sAlpha)
    {
        ListedDR Lstdr = new ListedDR();
        dsListedDR = Lstdr.getCity_Alphabet(div_code, ddlState.SelectedValue, sAlpha);
        if (dsListedDR.Tables[0].Rows.Count > 0)
        {
            gvCity.Visible = true;
            gvCity.DataSource = dsListedDR;
            gvCity.DataBind();
        }
        else
        {
            gvCity.DataSource = dsListedDR;
            gvCity.DataBind();
        }
    }
    protected void dlAlpha_ItemCommand(object source, DataListCommandEventArgs e)
    {
        LinkButton lnk = (LinkButton)e.CommandSource;
        string sCmd = e.CommandArgument.ToString();
        Session["GetCmdArgChar"] = sCmd;

        if (sCmd == "All")
        {
            gvCity.PageIndex = 0;
            FillGrid();
        }
        else
        {
            gvCity.PageIndex = 0;
            FillGrid(sCmd);
        }

    }
}
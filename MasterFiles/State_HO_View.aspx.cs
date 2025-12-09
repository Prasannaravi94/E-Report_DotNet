using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Bus_EReport;
using System.Drawing;

public partial class MasterFiles_State_HO_View : System.Web.UI.Page
{
    SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
    #region "Declaration"
    DataSet dsHO = null;
    string a;
    DataSet dsDivision = null;
    DataSet dsDesignation = null;
    DataSet dsdiv = null;
    int Ho_id = 0;
    string state_cd = string.Empty;
    string sState = string.Empty;
    string[] statecd;
    DataSet dsState = null;
    string divcode = string.Empty;
    string Name = string.Empty;
    string User_Name = string.Empty;
    string Rep_to = string.Empty;
    string Password = string.Empty;
    string sf_type = string.Empty;
    string div_code = string.Empty;
    string div_code1 = string.Empty;
    string division_code = string.Empty;
    string HO_ID = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    string State_Code = string.Empty;
    string Division_Code = string.Empty;
    string weekoff = string.Empty;
    string lb1 = string.Empty;
    int time;
    string state_co;
    int iLength = -1;

    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        sf_type = Session["sf_type"].ToString();
        HO_ID = Session["HO_ID"].ToString();
        if (sf_type == "3")
        {
            division_code = Session["division_code"].ToString();
        }
        else
        {
            division_code = Session["div_code"].ToString();
        }
        if (!Page.IsPostBack)
        {
            Filldiv();

            menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);


        }
        if (!IsPostBack)
        {
            FillStateList();
        }
    }
    private void Filldiv()
    {
        Division dv = new Division();
        if (sf_type == "3")
        {
            string[] strDivSplit = division_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }

    }


    private void FillStateList()
    {
        Division dv = new Division();
        dsDivision = dv.getStatePerDivision(ddlDivision.SelectedValue);
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
            dsState = st.getStateChkBox_WeekOff(state_cd, ddlDivision.SelectedValue);
            grdSubHoID.Visible = true;
            grdSubHoID.DataSource = dsState;
            grdSubHoID.DataBind();

            string[] wf;
            foreach (GridViewRow row in grdSubHoID.Rows)
            {
                if (row.RowType == DataControlRowType.DataRow)
                {
                    Label lblwkof = (Label)row.Cells[4].FindControl("lblwkof");
                    var listboxes = row.FindControl("ListBox1") as ListBox;

                    string lblWk = lblwkof.Text;

                    wf = (lblWk).Split(',');
                    foreach (string wof in wf)
                    {
                        if (wof != "")
                        {
                            listboxes.Items[Convert.ToInt32(wof)].Selected = true;
                        }
                    }

                }
            }
        }
        else
        {

        }
    }
    public void update()
    {

        con.Open();
        SqlCommand cmd = new SqlCommand("select State_Code,Division_Code from Mas_Statewise_Holiday_Fixation_Weekdays where State_Code='" + State_Code + "' and Division_Code='" + div_code + "'", con);
        SqlDataReader dr = cmd.ExecuteReader();
        if (dr.Read())
        {
            state_co = dr[0].ToString();
            div_code1 = dr[1].ToString();
            con.Close();
        }
        else
        {
            con.Close();
            insert();
        }

    }
    public void insert()
    {
        con.Open();

        SqlCommand cmd = new SqlCommand("insert into Mas_Statewise_Holiday_Fixation_Weekdays values('" + State_Code + "','" + div_code + "','" + lb1 + "')", con);
        int i = cmd.ExecuteNonQuery();
        if (i > 0)
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "validation", "<Script>alert('Updated Successfully')</Script>", false);
        }
        else
        {
            ScriptManager.RegisterStartupScript(this, this.GetType(), "validation", "<Script>alert('Update Failed')</Script>", false);
        }
        con.Close();
    }
    protected void But_update_Click(object sender, EventArgs e)
    {
        foreach (GridViewRow row in grdSubHoID.Rows)
        {
            if (row.RowType == DataControlRowType.DataRow)
            {
                Label lblstate = (Label)row.Cells[1].FindControl("lblSNo");
                State_Code = lblstate.Text.ToString();
                string queryname = ((Label)row.FindControl("lblSNo")).Text;
                var listboxes = row.FindControl("ListBox1") as ListBox;

                div_code = ddlDivision.SelectedValue;
                //weekoff = lblProductBrdCode.Text.ToString();
                lb1 = "";
                foreach (ListItem item in listboxes.Items)
                {
                    if (item.Selected)
                    {
                        lb1 += item.Value + ",";
                    }
                }

                if (lb1 == "")
                {
                    con.Open();
                    SqlCommand cmd = new SqlCommand("delete from Mas_Statewise_Holiday_Fixation_Weekdays where State_Code='" + State_Code + "' and Division_Code='" + div_code + "'", con);

                    int i = cmd.ExecuteNonQuery();
                    if (i > 0)
                    {
                        ScriptManager.RegisterStartupScript(this, this.GetType(), "validation", "<Script>alert('Updated Successfully')</Script>", false);
                    }
                    else
                    {
                        //lblProductBrdCode.Enabled = true;

                    }
                    con.Close();
                }
                else
                {
                    update();
                    if (State_Code == state_co && div_code == div_code1)
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("update Mas_Statewise_Holiday_Fixation_Weekdays set State_Code='" + State_Code + "',Division_Code='" + div_code + "',Holiday_Mode='" + lb1 + "' where State_Code='" + state_co + "'and Division_Code='" + div_code1 + "'", con);

                        int i = cmd.ExecuteNonQuery();
                        if (i > 0)
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "validation", "<Script>alert('Updated Successfully')</Script>", false);
                        }
                        else
                        {
                            ScriptManager.RegisterStartupScript(this, this.GetType(), "validation", "<Script>alert('Update Failed')</Script>", false);
                        }
                        con.Close();
                    }


                }
            }


        }


    }



    protected void grdSubHoID_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {

            //RequiredFieldValidator rfv = e.Row.FindControl("rfv") as RequiredFieldValidator;
            DropDownList Territory_Type = (DropDownList)e.Row.FindControl("DropDownList");
            if (Territory_Type != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;

                Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Holiday_Mode"].ToString()));
                //Territory_Type.Attributes.Add("disabled", "disabled");
            }
        }





    }



    protected void ddlselectindex_1(object sender, EventArgs e)
    {
        FillStateList();
    }
    protected void grdSubHoID_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
    protected void grdSubHoID_SelectedIndexChanged1(object sender, EventArgs e)
    {

    }
}
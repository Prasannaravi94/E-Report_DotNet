using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;

using System.Data.SqlClient;
using DBase_EReport;
public partial class MasterFiles_MR_Territory_BulkEdit : System.Web.UI.Page
{
    #region "Declaration"
    DataSet dsTerritory = null;
    string sf_code = string.Empty;
    string Territory_Code = string.Empty;
    string Territory_Type = string.Empty;
    string Territory_Name = string.Empty;
    string Territory_SName = string.Empty;
    string Territory_Ali_Name = string.Empty;
    string Territory_Allowance = string.Empty;
    string div_code = string.Empty;
    int i;
    int iReturn = -1;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string Town_Name = string.Empty;
    string Territory_Visit = string.Empty;
    
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //GetWorkName();
            sf_code = Session["sf_code"].ToString();
            if (Session["sf_code_Temp"] != null)
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            ViewTerritory();
            Session["backurl"] = "Territory.aspx";
            //ViewTerritory();
        }
        if (Session["sf_type"].ToString() == "1")
        {
            sf_code = Session["sf_code"].ToString();
            UserControl_MR_Menu Usc_MR =
       (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(Usc_MR);
            Usc_MR.Title = this.Page.Title;
            //btnBack.Visible = false;
            //menu1.Visible = true;
            //menu1.FindControl("btnBack").Visible = false; 
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:Maroon;'>For " + Session["sfName"] + " </span>" + " - " +
                                "<span style='font-weight: bold;color:Maroon;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                                 "<span style='font-weight: bold;color:Maroon;'>  " + Session["sf_HQ"] + "</span>" + " )";
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                Usc_MR.Title = "Edit all" + " - " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            }

        }
        else
        {
            //menu1.Visible = false;
            UserControl_MenuUserControl c1 =
        (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
            Divid.Controls.Add(c1);
            c1.Title = this.Page.Title;
            Session["backurl"] = "Territory.aspx";
            lblTerrritory.Text = "( " + "<span style='font-weight: bold;color:#696D6E;'>For " + Session["sfName"] + " </span>" + " - " +
                              "<span style='font-weight: bold;color:#696D6E;'> " + Session["sf_Designation_Short_Name"] + " </span>" + " - " +
                               "<span style='font-weight: bold;color:#696D6E;'>  " + Session["sf_HQ"] + "</span>" + " )";
            //btnBack.Visible = false;
            Territory terr = new Territory();
            dsTerritory = terr.getWorkAreaName(div_code);
            if (dsTerritory.Tables[0].Rows.Count > 0)
            {
                c1.Title = "Edit all" + " - " + dsTerritory.Tables[0].Rows[0]["wrk_area_Name"];
            }
        }
        if (div_code.Trim() == "17")
        {
            grdTerritory.Columns[4].Visible = false;
            grdTerritory.Columns[5].Visible = true;

            FillCity();
        }
        else
        {
            grdTerritory.Columns[4].Visible = true;
            grdTerritory.Columns[5].Visible = false;

        }


    }
    protected DataSet FillCity()
    {
        ListedDR lstDR = new ListedDR();
        SalesForce sf = new SalesForce();
        DataSet dscity = new DataSet();
        DataSet dsst = new DataSet();
        dsst = sf.CheckStatecode(sf_code);
        if (dsst.Tables[0].Rows.Count > 0)
        {
            dscity = lstDR.FetchTownCity(div_code, dsst.Tables[0].Rows[0]["State_Code"].ToString());

        }
        return dscity;
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


    private void ViewTerritory()
    {
        Territory terr = new Territory();
        dsTerritory = terr.getTerritory_New(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {
            grdTerritory.Visible = true;
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
        }
        else
        {
            grdTerritory.DataSource = dsTerritory;
            grdTerritory.DataBind();
            btnSubmit.Visible = false;
        }
    }

    protected void grdTerritory_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            DropDownList Territory_Type = (DropDownList)e.Row.FindControl("Territory_Type");
            DropDownList Territory_Visit = (DropDownList)e.Row.FindControl("Territory_Visit");

            if (Territory_Type != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Territory_Type.SelectedIndex = Territory_Type.Items.IndexOf(Territory_Type.Items.FindByText(row["Territory_Cat"].ToString()));
            }

            if (Territory_Visit != null)
            {
                DataRowView row = (DataRowView)e.Row.DataItem;
                Territory_Visit.SelectedIndex = Territory_Visit.Items.IndexOf(Territory_Visit.Items.FindByText(row["Territory_Visit"].ToString()));
            }

            if (div_code.Trim() == "17")
            {
                DropDownList City_Code = (DropDownList)e.Row.FindControl("City_Code");
                DataRowView row = (DataRowView)e.Row.DataItem;
                City_Code.SelectedIndex = City_Code.Items.IndexOf(City_Code.Items.FindByText(row["Town_City"].ToString()));
            }
        }
    }

    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        bool terr = false;
        //Doctor dv = new Doctor();
        System.Threading.Thread.Sleep(time);
        foreach (GridViewRow gridRow in grdTerritory.Rows)
        {
            Label lbl_Territory_Code = (Label)gridRow.Cells[1].FindControl("lblTerritory_Code");
            Territory_Code = lbl_Territory_Code.Text.ToString();
            TextBox txt_Territory_Name = (TextBox)gridRow.Cells[1].FindControl("txtTerritory_Name");
            Territory_Name = txt_Territory_Name.Text.Replace(",", "/").ToString();
            TextBox txt_Territory_Sname = (TextBox)gridRow.Cells[1].FindControl("txtTerritory_Sname");
            Territory_SName = txt_Territory_Sname.Text.ToString();
            TextBox txtTerritory_Allowance = (TextBox)gridRow.Cells[1].FindControl("txtTerritory_Allowance");
            Territory_Allowance = txtTerritory_Allowance.Text.ToString();


            DropDownList ddl_Territory_Type = (DropDownList)gridRow.Cells[1].FindControl("Territory_Type");
            Territory_Type = ddl_Territory_Type.SelectedValue.ToString();

            DropDownList ddl_Territory_Visit = (DropDownList)gridRow.Cells[2].FindControl("Territory_Visit");

            Territory_Visit = ddl_Territory_Visit.SelectedValue.ToString();

            
            if (Session["sf_code_Temp"] != null)
            {
                sf_code = Session["sf_code_Temp"].ToString();
            }
            else
            {
                sf_code = Session["sf_code"].ToString();
            }
            if (Territory_Visit.Trim() == "")
            {
                Territory_Visit = "0";
            }
            DataSet dster = new DataSet();
            Territory ter = new Territory();
            string visit = string.Empty;
            dster = ter.getTerritory_Det(sf_code, Territory_Code);
            if(dster.Tables[0].Rows.Count >0)
            {
                visit = dster.Tables[0].Rows[0]["Territory_Visit"].ToString();
                if(visit != Territory_Visit)
                {
                    terr = true;
                }
            }

            // Update Territory
            Territory Terr = new Territory();
            //  iReturn = Terr.RecordUpdate_aliasName(Territory_Code, Territory_Name, Territory_SName, Territory_Type, Territory_Ali_Name,sf_code);
            using (SqlConnection connection = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString))
            {
                connection.Open();
                SqlCommand command = connection.CreateCommand();
                SqlTransaction transaction;

                transaction = connection.BeginTransaction();
                command.Connection = connection;
                command.Transaction = transaction;

                try
                {

                    SqlConnection con1 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString);
                    if(terr == true)
                    {
                        //command.CommandText = " delete from Mas_Territory_Push_Msg where Sf_Code='"+sf_code+"' )";
                        //command.ExecuteNonQuery();
                       string strQry = "select Sf_Code from Mas_Territory_Push_Msg  where Sf_Code='" + sf_code + "' and Flag=0 ";
                        DB_EReporting db_ER = new DB_EReporting();
                        DataSet  dspush = db_ER.Exec_DataSet(strQry);
                        if (dspush.Tables[0].Rows.Count > 0)
                        {

                        }
                        else
                        {
                            command.CommandText = " INSERT INTO [dbo].[Mas_Territory_Push_Msg] (Sf_Code,Division_Code,Flag,Created_Date) " +
                                              " VALUES ('" + sf_code + "'  ,'" + div_code + "' ,'0',getdate())";
                            command.ExecuteNonQuery();                                                 
                          

                            int iReturn = -1;
                            DB_EReporting db = new DB_EReporting();
                            string strQry2 = "EXEC Territory_Push_Msg '" + sf_code + "'";

                            iReturn = db.ExecQry(strQry2);
                           
                        }

                    }
                    if (div_code.Trim() == "17")
                    {
                        DropDownList ddltown = (DropDownList)gridRow.Cells[1].FindControl("City_Code");
                        Town_Name = ddltown.SelectedItem.Text.Trim();
                        if (Town_Name.Trim() == "---Select---")
                        {
                            Town_Name = "";
                        }
                        if (string.IsNullOrEmpty(Territory_Allowance) || string.IsNullOrWhiteSpace(Territory_Allowance))
                        {
                            Territory_Allowance = "0";
                        }
                        else
                        {
                            Territory_Allowance = txtTerritory_Allowance.Text.ToString().Trim();
                        }
                        if (Territory_Visit.Trim() == "")
                        {
                            Territory_Visit = "0";
                        }

                        command.CommandText = "UPDATE Mas_Territory_Creation " +
                            " SET Territory_Name = '" + Territory_Name + "', " +
                            " Territory_Cat = '" + Territory_Type + "', Territory_Visit = '" + Territory_Visit + "',Territory_Allowance='"+ Territory_Allowance + "' " +
                            " Territory_SName = '" + Territory_SName + "', LastUpdt_Date= getdate(),Town_City='" + Town_Name + "' " +
                            " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' ";
                        command.ExecuteNonQuery();
                    }
                    else
                    {
                        //if (Territory_Allowance == "")
                        //{
                        //    Territory_Allowance = "0";
                        //}
                        if (string.IsNullOrEmpty(Territory_Allowance) || string.IsNullOrWhiteSpace(Territory_Allowance))
                        {
                            Territory_Allowance = "0";
                        }
                        else
                        {
                            Territory_Allowance = txtTerritory_Allowance.Text.ToString().Trim();
                        }
                        if (Territory_Visit.Trim() == "")
                        {
                            Territory_Visit = "0";
                            
                        }

                        TextBox txtAlias_Name = (TextBox)gridRow.Cells[1].FindControl("txtAlias_Name");
                        Territory_Ali_Name = txtAlias_Name.Text.Replace(",", "/").ToString();

                        command.CommandText = "UPDATE Mas_Territory_Creation " +
                            " SET Territory_Name = '" + Territory_Name + "', " +
                            " Territory_Cat = '" + Territory_Type + "',Territory_Visit = '" + Territory_Visit + "', " +
                            " Territory_SName = '" + Territory_SName + "', LastUpdt_Date= getdate(),Alias_Name='" + Territory_Ali_Name + "',Territory_Allowance='" + Territory_Allowance + "' " +
                            " WHERE Territory_Code = '" + Territory_Code + "' and sf_code='" + sf_code + "' ";
                        command.ExecuteNonQuery();
                    }

                    command.CommandText = " update mas_distance_Fixation set  " +
                             " To_Code_Code= STUFF(To_Code_Code, LEN(To_Code_Code), 1, '" + Territory_Type + "'), Town_Cat='" + Territory_Type + "' " +
                             " where to_code='" + Territory_Code + "' ";
                    command.ExecuteNonQuery();

                    command.CommandText = "delete from mas_distance_fixation where from_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                    command.ExecuteNonQuery();

                    command.CommandText = "delete from mas_distance_fixation where to_code in(select cast(territory_code as varchar) from mas_territory_creation where territory_cat='1')";
                    command.ExecuteNonQuery();
                    transaction.Commit();
                    connection.Close();
                    //  ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully'); { self.close() };</script>");
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
                }

                catch (Exception ex)
                {
                    Console.WriteLine("Commit Exception Type: {0}", ex.GetType());
                    Console.WriteLine("Message: {0}", ex.Message);
                    // Attempt to roll back the transaction.
                    try
                    {
                        transaction.Rollback();
                    }

                    catch (Exception ex2)
                    {
                        // This catch block will handle any errors that may have occurred
                        // on the server that would cause the rollback to fail, such as
                        // a closed connection.
                        Console.WriteLine("Rollback Exception Type: {0}", ex2.GetType());
                        Console.WriteLine("  Message: {0}", ex2.Message);
                    }
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error Exists!');</script>");
                }
            }
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {
        System.Threading.Thread.Sleep(time);
        Server.Transfer("Territory.aspx");
    }
}
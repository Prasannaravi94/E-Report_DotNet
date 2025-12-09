using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Bus_EReport;
using DBase_EReport;

public partial class MIS_Reports_Personal_Details_Status : System.Web.UI.Page
{
    string div_code = string.Empty;
    DataSet dsSalesForce = null;
    string sf_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    DataSet dsSalesforce = null;
    DataSet dsCheck = null;

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (sf_type == "3")
        {
            div_code = Session["div_code"].ToString();
        }
        else
        {
            div_code = Session["div_code"].ToString();
        }
        if (div_code.Contains(','))
        {
            div_code = div_code.Remove(div_code.Length - 1);
        }

        if (!Page.IsPostBack)
        {
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            //menu1.Title = this.Page.Title;
            //// menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            FillMRManagers();

        }

        FillColor();


    }

    private void FillMRManagers()
    {
        string strQry = string.Empty;

       // SalesForce sf = new SalesForce();
       // dsSalesForce = sf.SalesForceListMgrGet(div_code, sf_code);
     //   dsSalesForce = sf.UserListTP_Hierarchy(div_code, sf_code);

        DB_EReporting db_ER = new DB_EReporting();

        strQry = "EXEC AllFieldforce_Novacant '" + div_code + "', '" + sf_code + "' ";
        dsSalesForce = db_ER.Exec_DataSet(strQry);

        if (sf_type == "3")
        {
            //dsSalesForce.Tables[0].Rows[0].Delete();
            //dsSalesForce.Tables[0].Rows[1].Delete();

        }
        if (dsSalesForce.Tables[0].Rows.Count > 0)
        {
            ddlFieldForce.DataTextField = "sf_name";
            ddlFieldForce.DataValueField = "sf_code";
            ddlFieldForce.DataSource = dsSalesForce;
            ddlFieldForce.DataBind();

            ddlSF.DataTextField = "Desig_Color";
            ddlSF.DataValueField = "sf_code";
            ddlSF.DataSource = dsSalesForce;
            ddlSF.DataBind();
        }


        FillColor();
    }

    private void FillColor()
    {
        int j = 0;

        foreach (ListItem ColorItems in ddlSF.Items)
        {
            //ddlFieldForce.Items[j].Selected = true;
            string bcolor = "#" + ColorItems.Text;
            ddlFieldForce.Items[j].Attributes.Add("style", "background-color:" + bcolor);

            j = j + 1;

        }
    }

}
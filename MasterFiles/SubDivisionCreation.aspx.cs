using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_SubDivisionCreation : System.Web.UI.Page
{
#region "Declaration"
    DataSet dsSubDiv = null;
    string Subdivision_Code = string.Empty;
    string divcode = string.Empty;
    string subdiv_sname = string.Empty;
    string subdiv_name = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
#endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        Session["backurl"] = "SubDivisionList.aspx";
        Subdivision_Code = Request.QueryString["Subdivision_Code"];
      
        if (!Page.IsPostBack)
        {
           
            menu1.Title = this.Page.Title;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            txtSubDivision_Sname.Focus();
            if (Subdivision_Code != "" && Subdivision_Code != null)
            {
                SubDivision sd = new SubDivision();
                dsSubDiv = sd.getSubDiv(divcode, Subdivision_Code);                
                if (dsSubDiv.Tables[0].Rows.Count > 0)
                {
                    txtSubDivision_Sname.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();                  
                    txtSubDivision_Name.Text = dsSubDiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                }
            }
          
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
     protected void btnSubmit_Click(object sender, EventArgs e)
    {
         System.Threading.Thread.Sleep(time);
        subdiv_sname = txtSubDivision_Sname.Text;
        subdiv_name = txtSubDivision_Name.Text;
        if (Subdivision_Code == null)
        {
            // Add New Sub Division
            SubDivision dv = new SubDivision();
            int iReturn = dv.RecordAdd(divcode, subdiv_sname, subdiv_name);

             if (iReturn > 0 )
            {           
           
                // menu1.Status = "Sub Division created Successfully ";
               ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");           
               Resetall();
            }
             else if (iReturn == -2)
             {
                
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sub Division Name Already Exist');</script>");
                 txtSubDivision_Name.Focus();
             }
             else if (iReturn == -3)
             {
                 ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sub Division Short Name Already Exist');</script>");
                 txtSubDivision_Sname.Focus();
             }
        }
        else
        {
            // Update Sub Division
            SubDivision dv = new SubDivision();
            int subdivcode = Convert.ToInt16(Subdivision_Code);
            int iReturn = dv.RecordUpdate(subdivcode, subdiv_sname, subdiv_name, divcode);
            if (iReturn > 0 )
            {
               // menu1.Status = "Sub Division Updated Successfully ";
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');window.location='SubDivisionList.aspx';</script>");
            }
            else if (iReturn == -2)
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sub Division Name Already Exist');</script>");
                txtSubDivision_Name.Focus();
            }
            else if (iReturn == -3)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Sub Division Short Name Already Exist');</script>");
                txtSubDivision_Sname.Focus();
            }
        }
    }
     private void Resetall()
     {
         txtSubDivision_Sname.Text = "";
         txtSubDivision_Name.Text = "";
     }

    protected void btnBack_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("SubDivisionList.aspx");
    }
}
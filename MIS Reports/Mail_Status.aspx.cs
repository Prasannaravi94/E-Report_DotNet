using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;

public partial class MasterFiles_Mail_Status : System.Web.UI.Page
{
    #region "Declaration"
    string divcode = string.Empty;
    string sfcode = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    DataSet mail = null;
    #endregion
    protected void Page_Load(object sender, EventArgs e)
    {
        divcode = Convert.ToString(Session["div_code"]);
        sfcode = Convert.ToString(Session["Sf_Code"]);

       
        if (!Page.IsPostBack)
        {
            //menu1.Title = this.Page.Title;
            //menu1.FindControl("btnBack").Visible = false;
          
            
        }
        
 

    }
  protected void btnSF_Click(object sender, EventArgs e)
    {
        FillMail();
        }
  protected void grdMail_PageIndexChanging(object sender, GridViewPageEventArgs e)
  {
      grdMail.PageIndex = e.NewPageIndex;
      FillMail();
  }

  private void FillMail()
  {

      AdminSetup ast = new AdminSetup();
      mail = ast.Mail_Status(txtStartDate.Text.Substring(3, 2) + "/" + txtStartDate.Text.Substring(0, 2) + "/" + txtStartDate.Text.Substring(6, 4), txtEndDate.Text.Substring(3, 2) + "/" + txtEndDate.Text.Substring(0, 2) + "/" + txtEndDate.Text.Substring(6, 4), divcode);
     // mail = ast.Mail_Status(txtStartDate.Text, txtEndDate.Text, divcode);

        if (mail.Tables[0].Rows.Count > 0)
        {
            grdMail.Visible = true;
            grdMail.DataSource = mail;
            grdMail.DataBind();


            foreach (GridViewRow gridRow in grdMail.Rows)
            {

                
                HyperLink link = new HyperLink();
				HyperLink link1 = new HyperLink();
                Label lbl = new Label();
				Label lbl1 = new Label();
                HiddenField cnt = (HiddenField)gridRow.FindControl("lblMailHidden");
                HiddenField sfCodeHidden = (HiddenField)gridRow.FindControl("sfCodeHidden");
                //Label subj = (Label)gridRow.FindControl("lblSubject");
				HiddenField subj = (HiddenField)gridRow.FindControl("lblSubject");
                Label sentime = (Label)gridRow.FindControl("lbltime");
                Label name = (Label)gridRow.FindControl("sfNameHidden");

                HiddenField slno = (HiddenField)gridRow.FindControl("slnoHidden");

                link.Text = "<span>" + cnt.Value + "</span>";
                lbl.Text = cnt.Value;
                string sURL = "Mail_Status_Zoom.aspx?code=" + sfCodeHidden.Value + "&subj=" + subj.Value + "&sentime=" + sentime.Text + "&name=" + name.Text + "&slno=" + slno.Value+ "";
                link.Attributes["onclick"] = "javascript:window.open('" + sURL + "',null,'');";
                link.NavigateUrl = "#";
                gridRow.Cells[2].Controls.Add(link);

				link1.Text = "<span>" + subj.Value + "</span>";
                lbl1.Text = subj.Value;
                string sURL1 = "Mail_Zoom.aspx?slno=" + slno.Value + "";
                link1.Attributes["onclick"] = "javascript:window.open('" + sURL1 + "',null,'');";
                link1.NavigateUrl = "#";
                gridRow.Cells[3].Controls.Add(link1);

                

            }



        }
        else
        {
            grdMail.DataSource = mail;
            grdMail.DataBind();
            
        }     
      }
      
  

}
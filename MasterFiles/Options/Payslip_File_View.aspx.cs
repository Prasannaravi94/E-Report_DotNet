using System;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using Ionic.Zip;
using System.Collections.Generic;
using System.Data;
using Bus_EReport;
public partial class Payslip_File_View : System.Web.UI.Page
{
    DataSet ds = new DataSet();
    string div_code = string.Empty;
    string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
          div_code = Session["div_code"].ToString();
        sf_code = Session["sf_code"].ToString();
        if (!IsPostBack)
        {
            //string[] filePaths = Directory.GetFiles(Server.MapPath("~//MasterFiles//Options/PaySlip_Files/../"));

            //List<ListItem> files = new List<ListItem>();
            //foreach (string filePath in filePaths)
            //{
            //    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
            //}
            
        }
        if (Session["sf_type"].ToString() == "1")
        {

            UserControl_MR_Menu c1 =
                (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            FillPayslip();

        }
        else if (Session["sf_type"].ToString() == "2")
        {
            UserControl_MGR_Menu c1 =
            (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
            Divid.Controls.Add(c1);
            // c1.FindControl("btnBack").Visible = false;
            c1.Title = this.Page.Title;
            FillPayslip();


        }
    }
    private void FillPayslip()
    {
        SalesForce sf = new SalesForce();
        ds = sf.getEmployee_code(sf_code, div_code);
        if (ds.Tables[0].Rows.Count > 0)
        {
            string strPath = Server.MapPath("~\\MasterFiles\\Options\\PaySlip_Files\\");
            string[] filess = Directory.GetFiles(strPath, "*.*", SearchOption.AllDirectories);
            List<ListItem> files = new List<ListItem>();
            foreach (string filePath in filess)
            {
                if (filePath.Contains(ds.Tables[0].Rows[0].ItemArray.GetValue(0).ToString().ToUpper()))
                {
                    files.Add(new ListItem(Path.GetFileName(filePath), filePath));
                }
            }
            if (files.Count > 0)
            {
                GridView1.DataSource = files;
                GridView1.DataBind();
            }
            else
            {
                GridView1.DataSource = files;
                GridView1.DataBind();
                btnDownload.Visible = false;
            }
        }
    }
    protected void DownloadFiles(object sender, EventArgs e)
    {
        using (ZipFile zip = new ZipFile())
        {
            zip.AlternateEncodingUsage = ZipOption.AsNecessary;
            zip.AddDirectoryByName("Files");
            foreach (GridViewRow row in GridView1.Rows)
            {
                if ((row.FindControl("chkSelect") as CheckBox).Checked)
                {
                    string filePath = (row.FindControl("lblFilePath") as Label).Text;
                    zip.AddFile(filePath, "Files");
                }
            }
            Response.Clear();
            Response.BufferOutput = false;
            string zipName = String.Format("Zip_{0}.zip", DateTime.Now.ToString("yyyy-MMM-dd-HHmmss"));
            Response.ContentType = "application/zip";
            Response.AddHeader("content-disposition", "attachment; filename=" + zipName);  
            zip.Save(Response.OutputStream);
            Response.End();
        }
    }
    protected void DownloadFile(object sender, EventArgs e)
    {
        string filePath = (sender as LinkButton).CommandArgument;
        Response.ContentType = ContentType;
        Response.AppendHeader("Content-Disposition", "attachment; filename=" + Path.GetFileName(filePath));
        Response.WriteFile(filePath);
        Response.End();
    }

}
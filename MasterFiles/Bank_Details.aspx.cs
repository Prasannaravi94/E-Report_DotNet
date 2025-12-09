using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

public partial class MasterFiles_Bank_Details : System.Web.UI.Page
{
    string screen_name = string.Empty;
    DataSet dsGridShowHideColumn = new DataSet();
    DataSet dsGridShowHideColumn1 = new DataSet();
    DataSet dsGrid = null;
    string sf_code = string.Empty;
    protected void Page_Load(object sender, EventArgs e)
    {
        screen_name = "Bank_Details";
        sf_code = Session["Sf_Code"].ToString();
        if (!Page.IsPostBack)
        {
            fillgrid();
            menu1.Title = this.Page.Title;
           
        }

    }
    private void fillgrid()
    {

        SalesForce ds = new SalesForce();
      
            dsGrid = ds.getBank_Details();
            if (dsGrid.Tables[0].Rows.Count > 0)
            {
            divid.Visible = false;
            grdBankDetails.DataSource = dsGrid;
            grdBankDetails.DataBind();

            }
            else
            {
            divid.Visible = true;
            grdBankDetails.DataSource = dsGrid;
            grdBankDetails.DataBind();
            }
        DataTable dt = dsGrid.Tables[0];
        int countCol = dt.Columns.Count;
        Chemist chem = new Chemist();
        dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);
        for (int iCol = 0; iCol < countCol; iCol++)
        {
            DataColumn col = dt.Columns[iCol];
            if (col.ColumnName == "Bank_Name" || col.ColumnName == "Bank_AcNo" || col.ColumnName == "IFS_Code" || col.ColumnName == "Category" ||
                col.ColumnName == "SF_Address" || col.ColumnName == "SF_Mobile" || col.ColumnName == "SF_Email")
            {

                Chemist lst = new Chemist();
                int iReturn = -1;

                iReturn = lst.GridColumnShowHideInsert_New(screen_name, col.ColumnName, sf_code, true, 7);
            }
        }
        Chemist chem1 = new Chemist();

        dsGridShowHideColumn = chem1.GridColumnShowHideGet(screen_name, sf_code);
        if (dsGridShowHideColumn.Tables[0].Rows.Count > 0)
        {
            var result = from data in dsGridShowHideColumn.Tables[0].AsEnumerable()
                         select new
                         {
                             Ch_Name = data.Field<string>("column_name"),
                             Ch_Code = data.Field<string>("column_name")
                         };
            var listOfGrades = result.ToList();
            cblGridColumnList.Visible = true;
            cblGridColumnList.DataSource = listOfGrades;
            cblGridColumnList.DataTextField = "Ch_Name";
            cblGridColumnList.DataValueField = "Ch_Code";
            cblGridColumnList.DataBind();

            string headerText = string.Empty;

            for (int i = 0; i < dsGridShowHideColumn.Tables[0].Rows.Count; i++)
            {
                headerText = dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString();
                //headerText = cblGridColumnList.SelectedValue.ToString();

                ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

                if (ddl != null)
                {
                    if (Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = true;
                    }
                    else
                    {
                        cblGridColumnList.Items.FindByValue(headerText).Selected = false;
                    }
                }
            }
        }
        foreach (ListItem item in cblGridColumnList.Items)
        {
            if (!item.Selected)
            {
                if (item.Text == "Bank_Name")
                {
                    grdBankDetails.Columns[9].Visible = false;
                    grdBankDetails.HeaderRow.Cells[9].Visible = false;
                }
                else if (item.Text == "Bank_AcNo")
                {
                    grdBankDetails.Columns[10].Visible = false;
                    grdBankDetails.HeaderRow.Cells[10].Visible = false;
                }
                else if (item.Text == "IFS_Code")
                {
                    grdBankDetails.Columns[11].Visible = false;
                    grdBankDetails.HeaderRow.Cells[11].Visible = false;
                }
                else if (item.Text == "Category")
                {
                    grdBankDetails.Columns[12].Visible = false;
                    grdBankDetails.HeaderRow.Cells[12].Visible = false;
                }
                else if (item.Text == "SF_Address")
                {
                    grdBankDetails.Columns[13].Visible = false;
                    grdBankDetails.HeaderRow.Cells[13].Visible = false;
                }
                else if (item.Text == "SF_Mobile")
                {
                    grdBankDetails.Columns[14].Visible = false;
                    grdBankDetails.HeaderRow.Cells[14].Visible = false;
                }
                else if (item.Text == "SF_Email")
                {
                    grdBankDetails.Columns[15].Visible = false;
                    grdBankDetails.HeaderRow.Cells[15].Visible = false;
                }
            }
        }
    }
    protected void GrdDoctor_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        //Chemist chem = new Chemist();

        //dsGridShowHideColumn = chem.GridColumnShowHideGet(screen_name, sf_code);
        //if (dsGridShowHideColumn.Tables[0].Rows.Count > 0)
        //{
        //    var result = from data in dsGridShowHideColumn.Tables[0].AsEnumerable()
        //                 select new
        //                 {
        //                     Ch_Name = data.Field<string>("column_name"),
        //                     Ch_Code = data.Field<string>("column_name")
        //                 };
        //    var listOfGrades = result.ToList();
        //    cblGridColumnList.Visible = true;
        //    cblGridColumnList.DataSource = listOfGrades;
        //    cblGridColumnList.DataTextField = "Ch_Name";
        //    cblGridColumnList.DataValueField = "Ch_Code";
        //    cblGridColumnList.DataBind();

        //    string headerText = string.Empty;

        //    for (int i = 0; i < dsGridShowHideColumn.Tables[0].Rows.Count; i++)
        //    {
        //        headerText = dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString();

        //        ListItem ddl = cblGridColumnList.Items.FindByValue(dsGridShowHideColumn.Tables[0].Rows[i]["column_name"].ToString());

        //        if (ddl != null)
        //        {
        //            if (Convert.ToBoolean(dsGridShowHideColumn.Tables[0].Rows[i]["visible"]))
        //            {
        //                cblGridColumnList.Items.FindByValue(headerText).Selected = true;
        //            }
        //            else
        //            {
        //                cblGridColumnList.Items.FindByValue(headerText).Selected = false;
        //            }
        //        }
        //    }
        //}
    }
    protected void GVMissedCall_RowCreated(object sender, GridViewRowEventArgs e)
    {
        
    }







    
   protected void btnExcel_Click(object sender, EventArgs e)
    {
        string attachment = "attachment; filename=BankDetails.xls";
        Response.ClearContent();
        Response.AddHeader("content-disposition", attachment);
        Response.ContentType = "application/ms-excel";
        StringWriter sw = new StringWriter();
        HtmlTextWriter htw = new HtmlTextWriter(sw);
        HtmlForm frm = new HtmlForm();
        pnlContents.Parent.Controls.Add(frm);
        frm.Attributes["runat"] = "server";
        frm.Controls.Add(pnlContents);
        frm.RenderControl(htw);
        Response.Write(sw.ToString());
        Response.End();
    }
    [Serializable]
    public class CheckboxItem
    {
        public string Text { get; set; }
        public string Value { get; set; }

        public CheckboxItem(string value, string text)
        {
            Value = value;
            Text = text;
        }

        public override string ToString()
        {
            return Text;
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string show_columns = string.Empty;
        string hide_columns = string.Empty;
        foreach (ListItem item in cblGridColumnList.Items)
        {
            if (!item.Selected)
            {
                if (hide_columns == "")
                {
                    hide_columns = "'" + item.Text + "'";
                }
                else
                {
                    hide_columns = hide_columns + ",'" + item.Text + "'";
                }
            }
            else
            {
                if (show_columns == "")
                {
                    show_columns = "'" + item.Text + "'";
                }
                else
                {
                    show_columns = show_columns + ",'" + item.Text + "'";
                }
            }
        }

        if (screen_name != "" && sf_code != "")
        {
            Chemist lst = new Chemist();
            int iReturn = -1;

            iReturn = lst.GridColumnShowHideUpdate(screen_name, hide_columns, show_columns, sf_code);
        }
        Response.Redirect(Request.RawUrl);
    }
}
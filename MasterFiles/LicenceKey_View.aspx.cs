using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class MasterFiles_LicenceKey_View : System.Web.UI.Page
{
    #region "Declaration"
    int Division_Add2;
    string divcode = string.Empty;
    string sf_type = string.Empty;
    string division_code = string.Empty;
    DataSet dsdiv = null;
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {
       
        sf_type = Session["sf_type"] != null ? Session["sf_type"].ToString() : string.Empty;
        division_code = Session["division_code"] != null ? Session["division_code"].ToString() : string.Empty;

        if (!Page.IsPostBack)
        {
          
            BindGridView();
        }
    }

   
    private void BindGridView()
    {
        Division dv = new Division();
        DataTable dt = new DataTable(); 

       
        dt.Columns.Add("Division_Code");
        dt.Columns.Add("Division_Name");
        dt.Columns.Add("Division_SName");
        dt.Columns.Add("Division_Add2");

        if (sf_type == "3")
        {
            
            string[] strDivSplit = division_code.Split(',');

            
            foreach (string strdiv in strDivSplit)
            {
                if (!string.IsNullOrEmpty(strdiv))
                {
                  
                    DataSet dsdiv = dv.getDivisionHO_New(strdiv);

                   
                    if (dsdiv.Tables[0].Rows.Count > 0)
                    {
                        foreach (DataRow row in dsdiv.Tables[0].Rows)
                        {
                          
                            DataRow newRow = dt.NewRow();
                            newRow["Division_Code"] = row["Division_Code"];
                            newRow["Division_Name"] = row["Division_Name"];
                            newRow["Division_SName"] = row["Division_SName"];
                            newRow["Division_Add2"] = row["Division_Add2"];
                            dt.Rows.Add(newRow); 
                        }
                    }
                }
            }
        }

       
        grdlicense.DataSource = dt;
        grdlicense.DataBind();
    }




}
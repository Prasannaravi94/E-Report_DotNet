using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Xml;
using System.Xml.XPath;
using Bus_EReport;

public partial class MasterFiles_Geo_ShowMap : System.Web.UI.Page
{
    DataSet dsdoc = new DataSet();
    string sf_code = string.Empty;
    string strDate = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {
        DCR dc = new DCR();

        sf_code = Request.QueryString["sfcode"].ToString();

        dsdoc = dc.get_Geo_details_Maps(sf_code);
        if (dsdoc.Tables[0].Rows.Count > 0)
        {
            GVMap.DataSource = dsdoc;
            GVMap.DataBind();
        }
        
            }


    
   
}
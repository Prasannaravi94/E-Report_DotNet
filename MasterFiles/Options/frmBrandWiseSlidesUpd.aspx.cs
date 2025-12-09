using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.Services;
using System.Web.UI.WebControls;
using System.Web.Script.Services;
using System.Data;
using Newtonsoft.Json;
using Bus_EReport;
using System.IO;


public partial class MasterFiles_Options_frmBrandWiseSlidesUpd : System.Web.UI.Page
{
    string div_code=string.Empty;
    string sf_type = string.Empty;

    DataSet dsdiv=new DataSet();
    DataSet dsImg = new DataSet();
    DataSet dsDivision = new DataSet();

    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();
       
        if (!Page.IsPostBack)
        {
            Filldiv();
            FillSubdiv();
            FillBrand();
            FillProduct();
            FillImage();
            FillSpec();
            FillSINO();
            FillHer();
            //ddlBrand.SelectedIndex = 1;


        }

        //UserControl_MenuUserControl_TP c1 =
        //    (UserControl_MenuUserControl_TP)LoadControl("~/UserControl/MenuUserControl_TP.ascx");
        //Divid.Controls.Add(c1);       
        
        //// c1.FindControl("btnBack").Visible = false;
        //c1.Title = Page.Title;
    }

    protected void ddlDivision_SelectedIndexChanged(object sender, EventArgs e)
    {
        FillImage();

    }

    protected void btnUpload_Click(object sender, EventArgs e)
    {
        Product Prd = new Product();
        int iReturn = -1;

        string Division_SName = Session["Division_SName"].ToString();

        if (fileUpload.HasFile == true)
       {
            for (int i = 0; i < Request.Files.Count; i++)
            {
   
                HttpPostedFile PostedFile = Request.Files[i];
 
                if (PostedFile.ContentLength > 0)
                {
        
                    fileUpload.PostedFile.SaveAs(Server.MapPath("~/Edetailing_files/"+ Division_SName +"/download/" + PostedFile.FileName));

                   // iReturn = Prd.Insert_Image(PostedFile.FileName, ddlBrand.SelectedItem.Text, ddlBrand.SelectedValue, ddlDivision.SelectedValue, ddlSubdivision.SelectedValue);
                }

            }
        }
        
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Inserted Successfully');</script>");
        }
        //FillImage();
    }
    //[WebMethod]
    //public static string InsertImage(HttpContext context)
    //{
    //    int iReturn = -1;
    //    Product prd = new Product();
    //    DataSet dsdiv = new DataSet();
    //    //HttpFileCollection uploadedFiles = Request.Files;
    //    int i = context.Request.Files.Count;
    //    if (i > 0)
    //    {
    //        for (int j = 0; j < i; j++)
    //        {
    //            HttpFileCollection files = context.Request.Files;
    //            if (files != "")
    //            {
    //                DataTable dt = new DataTable();
    //                dsdiv = (DataSet)ViewState["dsSalesForce"];
    //                dsdiv.Tables[0].DefaultView.RowFilter = " division_code= '" + ddlDivision.SelectedValue + "'";
    //                dt = dsdiv.Tables[0].DefaultView.ToTable("0");
    //                userPostedFile.SaveAs(Server.MapPath("~/" + dt.Rows[0]["Division_SName"] + "/Download" + "\\" + Path.GetFileName(userPostedFile.FileName)));
    //                //fileUpload.Visible = true;
    //                //fileUpload.Text = "File(s) uploaded Successfully";

    //                iReturn = prd.Insert_Image(userPostedFile.FileName, ddlBrand.SelectedItem.Text, ddlBrand.SelectedValue, ddlDivision.SelectedValue, ddlSubdivision.SelectedValue);
    //            }
    //        }
    //    }
    //    if (iReturn > 0)
    //    {
    //        //ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Image Uploaded Successfully');</script>");
    //    }
    //}

    private void FillBrand()
    {
        DataSet dsBrand = new DataSet();
        Product Prd=new Product();

        dsBrand = Prd.GetProductBrand(ddlDivision.SelectedValue);
        ddlBrand.DataTextField = "Product_Brd_Name";
        ddlBrand.DataValueField = "Product_Brd_Code";
        ddlBrand.DataSource = dsBrand;
        ddlBrand.DataBind();
      
    }

    private void Filldiv()
    {
        Division dv = new Division();
        DataSet dsMerge = new DataSet();
        if (sf_type == "3")
        {
            string[] strDivSplit = div_code.Split(',');
            foreach (string strdiv in strDivSplit)
            {
                if (strdiv != "")
                {
                    dsdiv = dv.getDivisionHO(strdiv);
                    dsMerge.Merge(dsdiv);
                    ListItem liTerr = new ListItem();
                    liTerr.Value = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(0).ToString();
                    liTerr.Text = dsdiv.Tables[0].Rows[0].ItemArray.GetValue(1).ToString();
                    ddlDivision.Items.Add(liTerr);
                }
            }
        }
        else
        {
            dsDivision = dv.getDivision_Name();
            if (dsDivision.Tables[0].Rows.Count > 0)
            {
                ddlDivision.DataTextField = "Division_Name";
                ddlDivision.DataValueField = "Division_Code";
                ddlDivision.DataSource = dsDivision;
                ddlDivision.DataBind();
            }

            
        }

        ddlDivision.SelectedValue = div_code;
        ViewState["dsSalesForce"] = dsMerge;
    }

    protected DataSet FillProduct()
    {
        Product Prd=new Product();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Prd.getProd(ddlDivision.SelectedValue);
        //ddlProd.DataSource = dsFillProduct;
        //ddlProd.DataTextField = "Product_Detail_Code";
        //ddlProd.DataValueField = "Product_Detail_Name";
        //ddlProd.DataBind();

        return dsFillProduct;
    }

    protected DataSet FillSpec()
    {
        Doctor Doc = new Doctor();
        DataSet dsFillQul = new DataSet();

        dsFillQul = Doc.getDocSpec(ddlDivision.SelectedValue);
        //ddlSpeciality.DataSource = dsFillQul;
        //ddlSpeciality.DataTextField = "Doc_Cat_Name";
        //ddlSpeciality.DataValueField = "Doc_Cat_Code";
        //ddlSpeciality.DataBind();

        return dsFillQul;
    }

    protected DataSet FillHer()
    {
        Product Prd = new Product();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Prd.getProductGroup(ddlDivision.SelectedValue);
        //ddlHera.DataSource = dsFillProduct;
        //ddlHera.DataTextField = "Product_Grp_Name";
        //ddlHera.DataValueField = "Product_Grp_Code";
        //ddlHera.DataBind();

        return dsFillProduct;
    }

    //protected DataSet FillProductGroup()
    //{
    //    Product Prd = new Product();
    //    DataSet DsGrp = new DataSet();

    //    DsGrp = Prd.FillImg_ProductGroup(ddlDivision.SelectedValue);

    //    return DsGrp;

    //}

    protected DataSet FillSINO()
    {
        DataSet dsSINO = new DataSet();
        dsImg.Tables[0].Columns.Add("SL_NO1");
        for (int i = 1; i < dsImg.Tables[0].Rows.Count; i++)
        {
            dsImg.Tables[0].Rows[i]["SL_NO1"] = i;
        }

        return dsImg;
    }

    private void FillImage()
    {
        
        Product Prd = new Product();

        dsImg = Prd.GetImageFile(ddlDivision.SelectedValue,ddlBrand.SelectedValue,ddlSubdivision.SelectedValue);

        if (dsImg.Tables[0].Rows.Count > 0)
        {
            GrdUpload.DataSource = dsImg;
            GrdUpload.DataBind();
            btnUpdate.Visible = true;
        }
        else
        {
            GrdUpload.DataSource = null;
            GrdUpload.DataBind();
            btnUpdate.Visible = false;
        }

       

    }

    private void FillSubdiv()
    {
        SubDivision subDiv = new SubDivision();
        DataSet dsSub = new DataSet();

        dsSub = subDiv.getSubDiv(ddlDivision.SelectedValue);

        ddlSubdivision.DataSource = dsSub;
        ddlSubdivision.DataTextField = "subdivision_name";
        ddlSubdivision.DataValueField = "subdivision_code";
        ddlSubdivision.DataBind();


    }

    [WebMethod]
    public static string GetExistValueCheck(string division)
    {
        SalesForce sf = new SalesForce();
        DataSet dsFileName = new DataSet();
        //dsFileName = sf.GetFileName(division);
        return JsonConvert.SerializeObject(dsFileName);
    }

    public class userData
    {
        public string id_;
        public string name_;
        
    }    

    protected void btnUpdate_OnClick(object sender, EventArgs e)
    {
        int iReturn = -1;
        foreach (GridViewRow row in GrdUpload.Rows)
        {
            string Prod_Name = "";
            string Prod_Code="";
            string Spec_Name = "";
            string Spec_Code="";
            string Prod_Grp_Name = "";
            string Prod_Grp_Code ="";

            Product Prd = new Product();

            Label lblSINo = (Label)row.FindControl("imgSI_NO");
            Saplin.Controls.DropDownCheckBoxes ddlProd = (Saplin.Controls.DropDownCheckBoxes)row.FindControl("ddlProd");
            Saplin.Controls.DropDownCheckBoxes ddlSpeciality = (Saplin.Controls.DropDownCheckBoxes)row.FindControl("ddlSpeciality");
            Saplin.Controls.DropDownCheckBoxes ddlHer = (Saplin.Controls.DropDownCheckBoxes)row.FindControl("ddlHer");

            //Saplin.Controls.DropDownCheckBoxes ddlProd = (Saplin.Controls.DropDownCheckBoxes)row.FindControl("ddlProd");

            for (int i = 0; i < ddlProd.Items.Count; i++)
            {
                if (ddlProd.Items[i].Selected)
                {
                    Prod_Name += ddlProd.Items[i].Text + ",";
                    Prod_Code += ddlProd.Items[i].Value + ",";
                }
            }

            for (int i = 0; i < ddlSpeciality.Items.Count; i++)
            {
                if (ddlSpeciality.Items[i].Selected)
                {
                    Spec_Name += ddlSpeciality.Items[i].Text + ",";
                    Spec_Code += ddlSpeciality.Items[i].Value + ",";
                }
            }

            for (int i = 0; i < ddlHer.Items.Count; i++)
            {
                if (ddlHer.Items[i].Selected)
                {
                    Prod_Grp_Name += ddlHer.Items[i].Text + ",";
                    Prod_Grp_Code += ddlHer.Items[i].Value + ",";
                }
            }

            if (Prod_Name.Contains(","))
            {
                Prod_Name.Remove(Prod_Name.Length - 1);
                Prod_Code.Remove(Prod_Code.Length - 1);
            }

            if (Spec_Name.Contains(","))
            {

                Spec_Name.Remove(Spec_Name.Length - 1);
                Spec_Code.Remove(Spec_Code.Length - 1);
            }

            if (Prod_Grp_Name.Contains(","))
            {

                Prod_Grp_Name.Remove(Prod_Grp_Name.Length - 1);
                Prod_Grp_Code.Remove(Prod_Grp_Code.Length - 1);
            }

            iReturn = Prd.Upd_Img(lblSINo.Text, Prod_Name, Prod_Code, Spec_Name, Spec_Code, Prod_Grp_Code, Prod_Grp_Name);             

        }

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");
        }

    }

    protected void GrdUpload_Deleting(object sender, GridViewDeleteEventArgs e)
    {
        int iReturn = -1;
        //Determine the RowIndex of the Row whose Button was clicked.
        //int rowIndex = Convert.ToInt32(e.CommandArgument);

        //Reference the GridView Row.
        GridViewRow row = GrdUpload.Rows[e.RowIndex];

        Product Prd = new Product();
        string imgSI_NO = (row.FindControl("imgSI_NO") as Label).Text;
        iReturn = Prd.Delete_Img(imgSI_NO);

        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
        }
        FillImage();
    }

    protected void GrdUpload_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        int iReturn = -1;
        Product Prd=new Product();
        if (e.CommandName == "Delete")
        {
            //Determine the RowIndex of the Row whose Button was clicked.
            int rowIndex = Convert.ToInt32(e.CommandArgument);

            //Reference the GridView Row.
            GridViewRow row = GrdUpload.Rows[rowIndex];

            //Fetch value of Name.
            string imgSI_NO = (row.FindControl("imgSI_NO") as Label).Text;

            //Fetch value of Country

            iReturn = Prd.Delete_Img(imgSI_NO);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Deleted Successfully');</script>");
            }
            //FillImage();
        }
    }



    [WebMethod]
    public static string ConvertDataTabletoString(string division, string Brand,string SubDiv)
    {
        SalesForce sf = new SalesForce();
        DataTable dt = new DataTable();
        //ListItem lst = new ListItem();
        DataSet ds = new DataSet();
        ds = sf.GetFillCheckDropDownValue(division, Brand, SubDiv);

        System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
        List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
        Dictionary<string, object> row;
        dt = ds.Tables[0];
        foreach (DataRow dr in dt.Rows)
        {
            row = new Dictionary<string, object>();
            foreach (DataColumn col in dt.Columns)
            {
                row.Add(col.ColumnName, dr[col]);
            }
            rows.Add(row);
        }
        return serializer.Serialize(rows);

    }

    protected void GrdUpload_DataBound(object sender, GridViewEditEventArgs e)
    {

    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<userData> FillProductGroup(string BrandText, string Sub_Div)
    {
        List<userData> userData = new List<userData>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        Product Prd = new Product();
        DataSet dsFillProduct = new DataSet();
        dsFillProduct.Clear();
        dsFillProduct = Prd.GetBrandProduct(div_code, BrandText, Sub_Div);

        foreach (DataRow column in dsFillProduct.Tables[0].Rows)
        {
            userData data = new userData();
            data.id_ = column["Product_Code_SlNo"].ToString();
            data.name_ = column["Product_Detail_Name"].ToString();
            userData.Add(data);
        }
        return userData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<userData> FillSpecGroup()
    {
        List<userData> userData = new List<userData>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        Doctor Doc = new Doctor();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Doc.getDocSpec(div_code);
        foreach (DataRow column in dsFillProduct.Tables[0].Rows)
        {
            userData data = new userData();
            data.id_ = column["Doc_Cat_Code"].ToString();
            data.name_ = column["Doc_Cat_Name"].ToString();
            userData.Add(data);

        }
        return userData;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public static List<userData> FillTheroGroup()
    {
        List<userData> userData = new List<userData>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();

        Product Prd = new Product();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Prd.getProductGroup(div_code);
        foreach (DataRow column in dsFillProduct.Tables[0].Rows)
        {
            userData data = new userData();
            data.id_ = column["Product_Grp_Code"].ToString();
            data.name_ = column["Product_Grp_Name"].ToString();
            userData.Add(data);

        }
        return userData;
    }

    [WebMethod(EnableSession = true)]
    public static string ImgUpload(List<string> PrddataValue, List<string> PrddataText, List<string> SpecdataValue, List<string> SpecdataText, List<string> TheradataValue, List<string> TheradataText, List<string> vFileDate,string Div_Code,string Sub_Div,string BrandName,string BrandCode)
    {
        try
        {
            Product Prd = new Product();
            int iReturn = -1;
            string strprdValue = string.Join(",", PrddataValue.ToArray());
            string strPrdText = string.Join(",", PrddataText.ToArray());
            string strSpecValue = string.Join(",", SpecdataValue.ToArray());
            string strSpecText = string.Join(",", SpecdataText.ToArray());
            string strTheraValue = string.Join(",", TheradataValue.ToArray());
            string strTheraText = string.Join(",", TheradataText.ToArray());
            string strAlrFile = ""; 
            DataSet ds = new DataSet();
            string Division_SName = HttpContext.Current.Session["Division_SName"].ToString();

            var folder = HttpContext.Current.Server.MapPath("~/Edetailing_files/"+ Division_SName+"/download");
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }

            ds = Prd.GetFileFolderPath(Div_Code);
                if (vFileDate.Count >0)
                {
                    string x = vFileDate[0].ToString();
                    foreach (string file in x.Split(','))
                    {

                        string filePath = HttpContext.Current.Server.MapPath("~/Edetailing_files/"+Division_SName+"/download/" + file);
                        if (File.Exists(filePath))
                        {
                            strAlrFile += file + ",";
                        }
                        else
                        {
                            if (file != "")
                            {
                                iReturn = Prd.Insert_Image(file.ToString(), BrandName, BrandCode, Div_Code, Sub_Div, strprdValue, strPrdText, strSpecValue, strSpecText, strTheraValue, strTheraText);
                            }
                        }

                    }
                }          
            
                  
        }
        catch (Exception ex)
        {
           
        }
        return "0";
    }

    protected void btnView_Click(object sender, EventArgs e)
    {
        FillImage();
        //pnlPrd.Visible = true;
        Page.ClientScript.RegisterStartupScript(this.GetType(), "reset", " BrandWisePrd();", true);
    }
}
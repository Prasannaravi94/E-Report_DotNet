using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data;
using Bus_EReport;
using System.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.Xml;
using System.IO;
using System.Web.Services;
using System.Web.Script.Services;
using System.Xml.Linq;
using System.Collections;
using Newtonsoft.Json;

/// <summary>
/// Summary description for EDetailingWebService
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
// To allow this Web Service to be called from script, using ASP.NET AJAX, uncomment the following line. 
[System.Web.Script.Services.ScriptService]
public class EDetailingWebService : System.Web.Services.WebService
{
    #region EDetailing
    public EDetailingWebService()
    {
        //Uncomment the following line if using designed components 
        //InitializeComponent(); 
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Division_Detail> GetDivision()
    {
        List<Division_Detail> objField = new List<Division_Detail>();
        string div_code = HttpContext.Current.Session["div_code"].ToString();
        string sf_type = HttpContext.Current.Session["sf_type"].ToString();

        try
        {
            Division dv = new Division();
            DataSet dsDivision = new DataSet();
            if (sf_type == "3")
            {
                string[] strDivSplit = div_code.Split(',');
                foreach (string strdiv in strDivSplit)
                {
                    if (strdiv != "")
                    {
                        dsDivision = dv.getDivisionHO(strdiv);
                        foreach (DataRow dr in dsDivision.Tables[0].Rows)
                        {
                            Division_Detail objFFDet = new Division_Detail();
                            objFFDet.Division_Code = dr["Division_Code"].ToString();
                            objFFDet.Division_Name = dr["Division_Name"].ToString();
                            objField.Add(objFFDet);
                        }
                    }
                }
            }
            else
            {
                dsDivision = dv.getDivision_Name();
                if (dsDivision.Tables[0].Rows.Count > 0)
                {
                    foreach (DataRow dr in dsDivision.Tables[0].Rows)
                    {
                        Division_Detail objFFDet = new Division_Detail();
                        objFFDet.Division_Code = dr["Division_Code"].ToString();
                        objFFDet.Division_Name = dr["Division_Name"].ToString();
                        objField.Add(objFFDet);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<SubDivision_Detail> GetSubDivision()
    {
        List<SubDivision_Detail> objField = new List<SubDivision_Detail>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        try
        {
            SubDivision subDiv = new SubDivision();
            DataSet dsSub = new DataSet();

            dsSub = subDiv.getSubDiv(objDivCode);
            if (dsSub.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsSub.Tables[0].Rows)
                {
                    SubDivision_Detail objFFDet = new SubDivision_Detail();
                    objFFDet.SubDivision_Code = dr["subdivision_code"].ToString();
                    objFFDet.SubDivision_Name = dr["subdivision_name"].ToString();
                    objField.Add(objFFDet);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Brand_Detail> GetBrand(string objSubBrnd)
    {
        List<Brand_Detail> objField = new List<Brand_Detail>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        string objSubDiv = objSubBrnd.Split('^')[0];
        try
        {
            DataSet dsBrand = new DataSet();
            Product Prd = new Product();

            dsBrand = Prd.GetProductBrandSlide(objDivCode, objSubDiv);
            //dsBrand = Prd.GetProductBrand(objDivCode);
            if (dsBrand.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsBrand.Tables[0].Rows)
                {
                    Brand_Detail objFFDet = new Brand_Detail();
                    objFFDet.Brand_Code = dr["Product_Brd_Code"].ToString();
                    objFFDet.Brand_Name = dr["Product_Brd_Name"].ToString();
                    objField.Add(objFFDet);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Product_Detail1> GetProductGroup(string objSubBrnd)
    {
        List<Product_Detail1> objField = new List<Product_Detail1>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        string objSubDiv = objSubBrnd.Split('^')[0];
        string objDDBrand = objSubBrnd.Split('^')[1];

        try
        {
            DataSet dsProduct = new DataSet();
            Product Prd = new Product();

            dsProduct = Prd.GetMultiBrandProduct(objDivCode, objDDBrand, objSubDiv);
            if (dsProduct.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsProduct.Tables[0].Rows)
                {
                    Product_Detail1 objFFDet = new Product_Detail1();
                    objFFDet.Product_Code = dr["Product_Code_SlNo"].ToString();
                    objFFDet.Product_Name = dr["Product_Detail_Name"].ToString();
                    objField.Add(objFFDet);
                }
            }
        }
        catch (Exception ex)
        {
            throw ex;
        }

        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Spec_Detail> GetSpecGroup()
    {
        List<Spec_Detail> objField = new List<Spec_Detail>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        Doctor Doc = new Doctor();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Doc.getDocSpec(objDivCode);
        foreach (DataRow dr in dsFillProduct.Tables[0].Rows)
        {
            Spec_Detail objFFDet = new Spec_Detail();
            objFFDet.Spec_Code = dr["Doc_Cat_Code"].ToString();
            objFFDet.Spec_Name = dr["Doc_Cat_Name"].ToString();
            objField.Add(objFFDet);

        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Therapy_Detail> GetTherapyGroup()
    {
        List<Therapy_Detail> objField = new List<Therapy_Detail>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        Product Prd = new Product();
        DataSet dsFillProduct = new DataSet();

        dsFillProduct = Prd.getProductGroup(objDivCode);
        foreach (DataRow column in dsFillProduct.Tables[0].Rows)
        {
            Therapy_Detail objFFDet = new Therapy_Detail();
            objFFDet.Therapy_Code = column["Product_Grp_Code"].ToString();
            objFFDet.Therapy_Name = column["Product_Grp_Name"].ToString();
            objField.Add(objFFDet);
        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Upload_Files(string objUpload_Files)
    {
        string objField = "false";
        string objPrdCode = objUpload_Files.Split('^')[0];
        string objPrdName = objUpload_Files.Split('^')[1];
        string objSpecCode = objUpload_Files.Split('^')[2];
        string objSpecName = objUpload_Files.Split('^')[3];
        string objThrCode = objUpload_Files.Split('^')[4];
        string objThrName = objUpload_Files.Split('^')[5];
        string objFileName = objUpload_Files.Split('^')[6];
        string objDivCode = objUpload_Files.Split('^')[7];
        string objSubDivCode = objUpload_Files.Split('^')[8];
        string objBrdCode = objUpload_Files.Split('^')[9];
        string objBrdName = objUpload_Files.Split('^')[10];
        try
        {
            Product Prd = new Product();
            int iReturn = -1;
            int brdNameidx = 0;
            foreach (string brndCode in objBrdCode.Split(','))
            {
                string brdName = objBrdName.Split(',')[brdNameidx];

                foreach (string subCode in objSubDivCode.Split(','))
                {
                    DataSet dsImg = new DataSet();

                    dsImg = Prd.RecordExists(objDivCode, brndCode, subCode, objFileName.Replace(" ", "_"));

                    if (dsImg.Tables[0].Rows.Count > 0)
                    {
                        string obj_OPrdCode = string.Empty;
                        string obj_OPrdName = string.Empty;
                        string obj_OSpecCode = string.Empty;
                        string obj_OSpecName = string.Empty;
                        string obj_OThrCode = string.Empty;
                        string obj_OThrName = string.Empty;
                        string obj_OSubDivCode = string.Empty;

                        if (dsImg.Tables[0].Rows[0][0] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(objPrdCode))
                            {
                                obj_OPrdCode = dsImg.Tables[0].Rows[0][0].ToString() + objPrdCode;
                                List<string> u_PrdCode = obj_OPrdCode.Split(',').Distinct().ToList();
                                objPrdCode = string.Join(",", u_PrdCode);
                            }
                            else
                            {
                                objPrdCode = dsImg.Tables[0].Rows[0][0].ToString();
                            }
                        }

                        if (dsImg.Tables[0].Rows[0][1] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(objPrdName))
                            {
                                obj_OPrdName = dsImg.Tables[0].Rows[0][1].ToString() + objPrdName;
                                List<string> u_PrdName = obj_OPrdName.Split(',').Distinct().ToList();
                                objPrdName = string.Join(",", u_PrdName);
                            }
                            else
                            {
                                objPrdName = dsImg.Tables[0].Rows[0][1].ToString();
                            }
                        }

                        if (dsImg.Tables[0].Rows[0][2] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(objSpecCode))
                            {
                                obj_OSpecCode = dsImg.Tables[0].Rows[0][2].ToString() + objSpecCode;
                                List<string> u_SpecCode = obj_OSpecCode.Split(',').Distinct().ToList();
                                objSpecCode = string.Join(",", u_SpecCode);
                            }
                            else
                            {
                                objSpecCode = dsImg.Tables[0].Rows[0][2].ToString();
                            }
                        }

                        if (dsImg.Tables[0].Rows[0][3] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(objSpecName))
                            {
                                obj_OSpecName = dsImg.Tables[0].Rows[0][3].ToString() + objSpecName;
                                List<string> u_SpecName = obj_OSpecName.Split(',').Distinct().ToList();
                                objSpecName = string.Join(",", u_SpecName);
                            }
                            else
                            {
                                objSpecName = dsImg.Tables[0].Rows[0][3].ToString();
                            }
                        }

                        if (dsImg.Tables[0].Rows[0][4] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(objThrCode))
                            {
                                obj_OThrCode = dsImg.Tables[0].Rows[0][4].ToString() + objThrCode;
                                List<string> u_ThrCode = obj_OThrCode.Split(',').Distinct().ToList();
                                objThrCode = string.Join(",", u_ThrCode);
                            }
                            else
                            {
                                objThrCode = dsImg.Tables[0].Rows[0][4].ToString();
                            }
                        }

                        if (dsImg.Tables[0].Rows[0][5] != DBNull.Value)
                        {
                            if (!string.IsNullOrEmpty(objThrName))
                            {
                                obj_OThrName = dsImg.Tables[0].Rows[0][5].ToString() + objThrName;
                                List<string> u_ThrName = obj_OThrName.Split(',').Distinct().ToList();
                                objThrName = string.Join(",", u_ThrName);
                            }
                            else
                            {
                                objThrName = dsImg.Tables[0].Rows[0][5].ToString();
                            }
                        }
                    }

                    iReturn = Prd.Insert_Image(objFileName.Replace(" ", "_"), brdName, brndCode, objDivCode, subCode + ","
                        , string.IsNullOrEmpty(objPrdCode) ? null : objPrdCode + ","
                        , string.IsNullOrEmpty(objPrdName) ? null : objPrdName + ","
                        , string.IsNullOrEmpty(objSpecCode) ? null : objSpecCode + ","
                        , string.IsNullOrEmpty(objSpecName) ? null : objSpecName + ","
                        , string.IsNullOrEmpty(objThrCode) ? null : objThrCode + ","
                        , string.IsNullOrEmpty(objThrName) ? null : objThrName + ",");
                    brdNameidx++;
                }
            }
            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {

        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Upload_View> GetUploadView(string objSubBrnd)
    {
        List<Upload_View> objField = new List<Upload_View>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        string objSubDiv = objSubBrnd.Split('^')[0];
        string objDDBrand = objSubBrnd.Split('^')[1];
        string objDDProduct = string.IsNullOrEmpty(objSubBrnd.Split('^')[2]) ? "NULL" : objSubBrnd.Split('^')[2];
        string objDDSpec = string.IsNullOrEmpty(objSubBrnd.Split('^')[3]) ? "NULL" : objSubBrnd.Split('^')[3];
        string objDDTherapy = string.IsNullOrEmpty(objSubBrnd.Split('^')[4]) ? "NULL" : objSubBrnd.Split('^')[4];

        Product Prd = new Product();
        DataSet dsImg = new DataSet();

        dsImg = Prd.GetImageFile(objDivCode, objDDBrand, objSubDiv, objDDProduct, objDDSpec, objDDTherapy);
        string Division_SName = HttpContext.Current.Session["Division_SName"].ToString();
        if (dsImg.Tables[0].Rows.Count == 0)
        {
            return null;
        }
        else
        {
            foreach (DataRow column in dsImg.Tables[0].Rows)
            {
                Upload_View objFFDet = new Upload_View();
                objFFDet.SI_NO = column["SI_NO"].ToString();
                //objFFDet.Img_Src = column["Img_Src"].ToString();
                if (column["Img_Name"].ToString().Contains(".pdf"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/pdf.png"; }
                else if (column["Img_Name"].ToString().Contains(".zip"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/html.png"; }
                else if (column["Img_Name"].ToString().Contains(".html"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/html.png"; }
                else if (column["Img_Name"].ToString().Contains(".mp4") ||
                    column["Img_Name"].ToString().Contains(".ogv") ||
                    column["Img_Name"].ToString().Contains(".avi") ||
                    column["Img_Name"].ToString().Contains(".mov") ||
                    column["Img_Name"].ToString().Contains(".flv") ||
                    column["Img_Name"].ToString().Contains(".3gp"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/video.png"; }
                else { objFFDet.Img_Src = "../../EDetailing_Files/" + Division_SName + "/download/" + column["Img_Name"].ToString(); }
                objFFDet.Img_Name = column["Img_Name"].ToString();
                objFFDet.Product_Brand_Code = column["Product_Brand_Code"].ToString();
                objFFDet.Product_Brand = column["Product_Brand"].ToString();
                objFFDet.Product_Detail_Code = column["Product_Detail_Code"].ToString();
                objFFDet.Product_Detail_Name = column["Product_Detail_Name"].ToString();
                objFFDet.Doc_Special_Code = column["Doc_Special_Code"].ToString();
                objFFDet.Doc_Special_Name = column["Doc_Special_Name"].ToString();
                objFFDet.Product_Grp_Code = column["Product_Grp_Code"].ToString();
                objFFDet.Product_Grp_Name = column["Product_Grp_Name"].ToString();
                objFFDet.Priority = column["Priority"].ToString();
                objFFDet.File_type = column["File_type"].ToString();
                objField.Add(objFFDet);
            }
        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Upload_View> GetSlidePriority(string objSlidePriority)
    {
        List<Upload_View> objField = new List<Upload_View>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        string objDDBrand = objSlidePriority.Split('^')[1];
        string objSubDiv = string.IsNullOrEmpty(objSlidePriority.Split('^')[0]) ? "NULL" : objSlidePriority.Split('^')[0];
        string objModeVal = string.IsNullOrEmpty(objSlidePriority.Split('^')[2]) ? "NULL" : objSlidePriority.Split('^')[2];
        string objMode = objSlidePriority.Split('^')[3];

        Product Prd = new Product();
        DataSet dsImg = new DataSet();

        dsImg = Prd.GetSlidePriority(objDivCode, objDDBrand, objSubDiv, objModeVal, objMode);
        string Division_SName = HttpContext.Current.Session["Division_SName"].ToString();
        if (dsImg.Tables[0].Rows.Count == 0)
        {
            return null;
        }
        else
        {
            foreach (DataRow column in dsImg.Tables[0].Rows)
            {
                Upload_View objFFDet = new Upload_View();
                objFFDet.SI_NO = column["SI_NO"].ToString();
                //objFFDet.Img_Src = column["Img_Src"].ToString();
                if (column["Img_Name"].ToString().Contains(".pdf"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/pdf.png"; }
                else if (column["Img_Name"].ToString().Contains(".zip"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/html.png"; }
                else if (column["Img_Name"].ToString().Contains(".html"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/html.png"; }
                else if (column["Img_Name"].ToString().Contains(".mp4") ||
                    column["Img_Name"].ToString().Contains(".ogv") ||
                    column["Img_Name"].ToString().Contains(".avi") ||
                    column["Img_Name"].ToString().Contains(".mov") ||
                    column["Img_Name"].ToString().Contains(".flv") ||
                    column["Img_Name"].ToString().Contains(".3gp"))
                { objFFDet.Img_Src = "../../EDetailing_Files/Multi-Image/video.png"; }
                else { objFFDet.Img_Src = "../../EDetailing_Files/" + Division_SName + "/download/" + column["Img_Name"].ToString(); }
                objFFDet.Img_Name = column["Img_Name"].ToString();
                objFFDet.Priority = column["Priority"].ToString();
                objFFDet.File_type = column["File_type"].ToString();
                objField.Add(objFFDet);
            }
        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Update_Files(string objUpload_Files)
    {
        int iReturn = -1;

        string objField = "false";
        string objPrdCode = objUpload_Files.Split('^')[0];
        string objPrdName = objUpload_Files.Split('^')[1];
        string objSpecCode = objUpload_Files.Split('^')[2];
        string objSpecName = objUpload_Files.Split('^')[3];
        string objThrCode = objUpload_Files.Split('^')[4];
        string objThrName = objUpload_Files.Split('^')[5];
        string objFileName = objUpload_Files.Split('^')[6];
        string objDivCode = objUpload_Files.Split('^')[7];
        string objSubDivCode = objUpload_Files.Split('^')[8];
        string objBrdCode = objUpload_Files.Split('^')[9];
        string objBrdName = objUpload_Files.Split('^')[10];
        string objSL_No = objUpload_Files.Split('^')[11];
        string objComn = objUpload_Files.Split('^')[12];
        //string objAlt_Brand = objUpload_Files.Split('^')[12];
        if (objComn == "false")
        {
            objComn = "";
        }
        try
        {
            Product Prd = new Product();

            iReturn = Prd.Update_Image(objSL_No, objFileName.Replace(" ", "_"), objBrdName, objBrdCode, objDivCode, objSubDivCode 
                , string.IsNullOrEmpty(objPrdCode) ? null : objPrdCode + ","
                , string.IsNullOrEmpty(objPrdName) ? null : objPrdName + ","
                , string.IsNullOrEmpty(objSpecCode) ? null : objSpecCode + ","
                , string.IsNullOrEmpty(objSpecName) ? null : objSpecName + ","
                , string.IsNullOrEmpty(objThrCode) ? null : objThrCode + ","
                , string.IsNullOrEmpty(objThrName) ? null : objThrName + ",", objComn);

            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {

        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Delete_Files(string objSl_No)
    {
        string objField = "false";
        try
        {
            Product Prd = new Product();
            int iReturn = -1;

            foreach (string Sl_No in objSl_No.Split(','))
            {
                iReturn = Prd.Delete_Img(Sl_No);
            }
            //iReturn = Prd.Delete_Img(objSl_No);
            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {

        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Update_Common(string objImg_Common)
    {
        string objField = "false";
        try
        {
            string[] objImg_Cmn = new string[] { };
            int iReturn = -1;
            objImg_Cmn = objImg_Common.Split(',');
            foreach (string strPr in objImg_Cmn)
            {
                string imgSlNO = strPr.Split('^')[0].Trim();
                string Cmn = strPr.Split('^')[1].Trim();

                if (Cmn == "false")
                {
                    Cmn = "";
                }

                Product Prd = new Product();
                iReturn = Prd.Update_Common(imgSlNO, Cmn);
            }
            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {

        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Update_Priority(string objPriority)
    {
        string objField = "false";
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        try
        {
            int iReturn = -1;
            string[] objPriorityArr = new string[] { };
            objPriorityArr = objPriority.Split(',');
            foreach (string strPr in objPriorityArr)
            {
                string code = strPr.Split('^')[0].Trim();
                int prt = Convert.ToInt32(strPr.Split('^')[1].Trim());
                string mode = strPr.Split('^')[2].Trim();
                string subDiv = strPr.Split('^')[3].Trim();
                string modeVal = strPr.Split('^')[4].Trim();

                Product Prd = new Product();
                iReturn = Prd.Update_Mas_Priority(objDivCode, code, prt, mode, subDiv, modeVal);
            }
            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {

        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public string Update_Slide_Priority(string objPriority)
    {
        string objField = "false";
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        try
        {
            int iReturn = -1;
            string[] objPriorityArr = new string[] { };
            objPriorityArr = objPriority.Split(',');
            foreach (string strPr in objPriorityArr)
            {
                string slno = strPr.Split('^')[0].Trim();
                int prt = Convert.ToInt32(strPr.Split('^')[1].Trim());
                string mode = strPr.Split('^')[2].Trim();
                string subDiv = strPr.Split('^')[3].Trim();
                string modeVal = strPr.Split('^')[4].Trim();
                string brndVal = strPr.Split('^')[5].Trim();

                Product Prd = new Product();
                iReturn = Prd.Update_Slide_Priority(objDivCode, slno, prt, mode, subDiv, modeVal, brndVal);
            }
            if (iReturn > 0)
            {
                objField = "true";
            }
        }
        catch (Exception ex)
        {

        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Priority_View> GetPriority(string objmodePriority)
    {
        List<Priority_View> objField = new List<Priority_View>();
        string objDivCode = HttpContext.Current.Session["div_code"].ToString();
        string objSubDivCode = string.Empty;
        string objModeVal = string.Empty;

        Product Prd = new Product();
        DataSet dsPrt = new DataSet();

        if (objmodePriority.Contains('^'))
        {
            objSubDivCode = objmodePriority.Split('^')[0].Trim();
            objModeVal = objmodePriority.Split('^')[1].Trim();
            objmodePriority = objmodePriority.Split('^')[2].Trim();
        }

        dsPrt = Prd.GetPriorityView(objDivCode, objmodePriority, objModeVal, objSubDivCode);

        if (dsPrt.Tables[0].Rows.Count == 0)
        {
            return null;
        }
        else
        {
            foreach (DataRow column in dsPrt.Tables[0].Rows)
            {
                Priority_View objFFDet = new Priority_View();
                objFFDet.Product_Brd_Code = column["Product_Brd_Code"].ToString();
                objFFDet.Product_Brd_Name = column["Product_Brd_Name"].ToString();
                objFFDet.Priority = column["Priority"].ToString();
                objField.Add(objFFDet);
            }
        }
        return objField;
    }

    [WebMethod(EnableSession = true)]
    [ScriptMethod]
    public List<Upload_View> Upload_FilesExist(string objUpload_Files)
    {
        List<Upload_View> objField = new List<Upload_View>();
        string objFileName = objUpload_Files.Split('^')[0];
        string objDivCode = objUpload_Files.Split('^')[1];

        try
        {
            Product Prd = new Product();


            DataSet dsImg = new DataSet();

            dsImg = Prd.RecordFileNameExists(objDivCode, objFileName.Replace(" ", "_"));
            if (dsImg.Tables[0].Rows.Count == 0)
            {
                return null;
            }
            else
            {
                foreach (DataRow column in dsImg.Tables[0].Rows)
                {
                    Upload_View objFFDet = new Upload_View();
                    objFFDet.Img_Name = column["Img_Name"].ToString();
                    objFFDet.Flag = column["Flag"].ToString();
                    objField.Add(objFFDet);
                }
            }
        }
        catch (Exception ex)
        {

        }
        return objField;
    }
    //public string TimeStamp
    //{
    //    get
    //    {
    //        return (HttpContext.Current.Session["TimeStamp"] !=
    //        null ? HttpContext.Current.Session["TimeStamp"].ToString() : "0");
    //    }
    //    set
    //    {
    //        HttpContext.Current.Session["TimeStamp"] = value;
    //    }
    //}

    //public string TimeStamp_Product
    //{
    //    get
    //    {
    //        return (HttpContext.Current.Session["TimpStamp_Product"] !=
    //        null ? HttpContext.Current.Session["TimpStamp_Product"].ToString() : "0");
    //    }
    //    set
    //    {
    //        HttpContext.Current.Session["TimpStamp_Product"] = value;
    //    }
    //}

    //public string TimeStamp_Value
    //{
    //    get
    //    {
    //        return (HttpContext.Current.Session["TimeStamp_Value"] !=
    //        null ? HttpContext.Current.Session["TimeStamp_Value"].ToString() : "0");
    //    }
    //    set
    //    {
    //        HttpContext.Current.Session["TimeStamp_Value"] = value;
    //    }
    //}

    //public void DisplayContactUI(string Month, string Year, string StockistCode)
    //{
    //    //Contact Display code here

    //    string div_code = HttpContext.Current.Session["div_code"].ToString();

    //    SecSale ss = new SecSale();
    //    DataSet ds = ss.Get_TransHead_TimeStamp(div_code, Month, Year, StockistCode);

    //    if (ds.Tables[0].Rows.Count > 0)
    //    {
    //        TimeStamp = ds.Tables[0].Rows[0]["TimeStamp"].ToString();
    //    }
    //    else
    //    {
    //        TimeStamp = null;
    //    }

    //}
    #endregion
}

#region EDetailingParam
public class Division_Detail
{
    public string Division_Code { get; set; }
    public string Division_Name { get; set; }
}
public class SubDivision_Detail
{
    public string SubDivision_Code { get; set; }
    public string SubDivision_Name { get; set; }
}
public class Brand_Detail
{
    public string Brand_Code { get; set; }
    public string Brand_Name { get; set; }
}
public class Product_Detail1
{
    public string Product_Code { get; set; }
    public string Product_Name { get; set; }
}
public class Spec_Detail
{
    public string Spec_Code { get; set; }
    public string Spec_Name { get; set; }
}
public class Therapy_Detail
{
    public string Therapy_Code { get; set; }
    public string Therapy_Name { get; set; }
}
public class Upload_View
{
    public string SI_NO { get; set; }
    public string Img_Src { get; set; }
    public string Img_Name { get; set; }
    public string Product_Brand_Code { get; set; }
    public string Product_Brand { get; set; }
    public string Product_Detail_Code { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Doc_Special_Code { get; set; }
    public string Doc_Special_Name { get; set; }
    public string Product_Grp_Code { get; set; }
    public string Product_Grp_Name { get; set; }
    public string Priority { get; set; }
    public string File_type { get; set; }
    public string Flag { get; set; }
}

public class Priority_View
{
    public string Product_Brd_Code { get; set; }
    public string Product_Brd_Name { get; set; }
    public string Product_Detail_Code { get; set; }
    public string Product_Detail_Name { get; set; }
    public string Doc_Cat_Code { get; set; }
    public string Doc_Cat_Name { get; set; }
    public string Product_Grp_Code { get; set; }
    public string Product_Grp_Name { get; set; }
    public string Priority { get; set; }
}
#endregion
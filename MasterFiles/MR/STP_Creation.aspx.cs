using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.IO;
using System.Web;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using Bus_EReport;
using DBase_EReport;
using System.Web.Services;
using System.Web.Script.Services;
using DBase_EReport;
using System.Data.SqlClient;
using System.Configuration;
using System.Web.Script.Serialization;
using Newtonsoft.Json;


public partial class MasterFiles_MR_STP_Creation : System.Web.UI.Page
{

    string sf_code = string.Empty;
    string div_code = string.Empty;
    string sf_type = string.Empty;
    DataSet dsTP = new DataSet();
    TP_New tp = new TP_New();
    DataSet dsTerritory = new DataSet();
    DataSet dsDr = new DataSet();
    DataSet LDA = new DataSet();
    DataSet dsCat = new DataSet();
    string Edit = string.Empty;

    protected void Page_Load(object sender, EventArgs e)
    {

        sf_code = Session["sf_code"].ToString();
        div_code = Session["div_code"].ToString();
        sf_type = Session["sf_type"].ToString();

        if (!Page.IsPostBack)
        {
            // menu.FindControl("btnBack").Visible = false;
            menu.Title = this.Page.Title;
            fill_stp();
            filltot_dr();
            fillcategory();
        }

    }

    private void fill_stp()
    {

        dsTP = tp.STP_EDIT(div_code,sf_code);

        string Flag = string.Empty;

        if (dsTP.Tables[0].Rows.Count > 0)
        {
            Flag = dsTP.Tables[0].Rows[0]["Active_Flag"].ToString();

           if (Flag == "0")
           {
               btnSave.Visible = false;
               lblHead.Text = "STP EDIT";

               Edit = "Edit";
           }
           else if (Flag == "2")
           {
               lblHead.Text = "STP DRAFT";
               Edit = "draft";
           }
        }
        else
        {
            dsTP = tp.STP_Creation();
        }

        if (dsTP.Tables[0].Rows.Count > 0)
        {

            grdSTP.Visible = true;
            grdSTP.DataSource = dsTP;
            grdSTP.DataBind();
        }
        else
        {
            grdSTP.DataSource = dsTP;
            grdSTP.DataBind();
        }
        
    }

    

    protected DataSet FillTerritory()
    {


        dsTerritory = tp.FetchTerritory_STP(sf_code);
        if (dsTerritory.Tables[0].Rows.Count > 0)
        {

            totterrcode.Value = dsTerritory.Tables[0].Rows.Count.ToString();
            totterr.Text = totterrcode.Value;
        }
        else
        {
            totterrcode.Value = "0";
        }
        return dsTerritory;
    }

    private void filltot_dr()
    {
        Doctor dr = new Doctor();      
        dsDr = dr.filldr_terr_count(sf_code);
        

        if (dsDr.Tables[0].Rows.Count > 0)
        {
            totdoctorcode.Value = dsDr.Tables[0].Rows[0]["totdrr"].ToString();
            totchemcode.Value = dsDr.Tables[0].Rows[0]["totchemm"].ToString();

            totdoctorr.Text = totdoctorcode.Value;
            totchemistt.Text = totchemcode.Value;
        }
        else
        {
            totdoctorcode.Value = "0";
            totchemcode.Value = "0";

        }
    }
    protected void grdSTP_RowDataBound(object sender, GridViewRowEventArgs e)
    {

        if (e.Row.RowType == System.Web.UI.WebControls.DataControlRowType.DataRow)
        {

            // when mouse is over the row, save original color to new attribute, and change it to highlight color
            e.Row.Attributes.Add("onmouseover", "this.originalstyle=this.style.backgroundColor;this.style.backgroundColor='#EEFFAA'");

            // when mouse leaves the row, change the bg color to its original value   
            e.Row.Attributes.Add("onmouseout", "this.style.backgroundColor=this.originalstyle;");

        }
        if (e.Row.RowType == DataControlRowType.DataRow)
        {
            Label lblday_plan_name = (Label)e.Row.FindControl("lblday_plan_name");

            if (lblday_plan_name.Text == "Monday 1" || lblday_plan_name.Text == "Monday 2" || lblday_plan_name.Text == "Monday 3" || lblday_plan_name.Text == "Monday 4")
            {
                e.Row.BackColor = System.Drawing.Color.Lavender;
            }

            else if (lblday_plan_name.Text == "Tuesday 1" || lblday_plan_name.Text == "Tuesday 2" || lblday_plan_name.Text == "Tuesday 3" || lblday_plan_name.Text == "Tuesday 4")
            {
                e.Row.BackColor = System.Drawing.Color.LightCyan;
            }

            else if (lblday_plan_name.Text == "Wednesday 1" || lblday_plan_name.Text == "Wednesday 2" || lblday_plan_name.Text == "Wednesday 3" || lblday_plan_name.Text == "Wednesday 4")
            {
                e.Row.BackColor = System.Drawing.Color.MistyRose;
            }

            else if (lblday_plan_name.Text == "Thursday 1" || lblday_plan_name.Text == "Thursday 2" || lblday_plan_name.Text == "Thursday 3" || lblday_plan_name.Text == "Thursday 4")
            {
                e.Row.BackColor = System.Drawing.Color.Beige;
            }

            else if (lblday_plan_name.Text == "Friday 1" || lblday_plan_name.Text == "Friday 2" || lblday_plan_name.Text == "Friday 3" || lblday_plan_name.Text == "Friday 4")
            {
                e.Row.BackColor = System.Drawing.Color.Moccasin;
            }

            else if (lblday_plan_name.Text == "Saturday 1" || lblday_plan_name.Text == "Saturday 2" || lblday_plan_name.Text == "Saturday 3" || lblday_plan_name.Text == "Saturday 4")
            {
                e.Row.BackColor = System.Drawing.Color.Lavender;
            }


        }

        if (Edit != null && Edit == "Edit" || Edit == "draft")
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                CheckBoxList chkterr = (CheckBoxList)e.Row.FindControl("chkterr");
                TextBox txtterr = (TextBox)e.Row.FindControl("txtterr");

                HiddenField hdndr = (HiddenField)e.Row.FindControl("hdndr");
                HiddenField hdndr_name = (HiddenField)e.Row.FindControl("hdndr_name");

                HiddenField hdnchem = (HiddenField)e.Row.FindControl("hdnchem");
                HiddenField hdnchem_name = (HiddenField)e.Row.FindControl("hdnchem_name");
                Label lbldrchem = (Label)e.Row.FindControl("lbldrchem");


                string patch_codee = dsTP.Tables[0].Rows[e.Row.RowIndex]["Patch_Code"].ToString();
               
                string Patch_name = string.Empty;

                for (int i = 0; i < chkterr.Items.Count; i++)
                {
                    if (patch_codee.Contains(chkterr.Items[i].Value))
                    {
                        chkterr.Items[i].Selected = true;
                        Patch_name += chkterr.Items[i].Text + ",";
                    }
                    
                }

                txtterr.Text = Patch_name.TrimEnd(',');

                string dr_code = dsTP.Tables[0].Rows[e.Row.RowIndex]["dr_code"].ToString();

                string dr_name = dsTP.Tables[0].Rows[e.Row.RowIndex]["dr_name"].ToString();


                string chem_code = dsTP.Tables[0].Rows[e.Row.RowIndex]["chem_code"].ToString();
                string chem_name = dsTP.Tables[0].Rows[e.Row.RowIndex]["chem_name"].ToString();

                hdndr.Value = dr_code;
                hdndr_name.Value = dr_name;

                hdnchem.Value = chem_code;
                hdnchem_name.Value = chem_name;


                int count = 0;
                int count2 = 0;
                
                string[] values = dr_code.Split(',');
                for (int i = 0; i < values.Length; i++)
                {
                    if (values[i] != "")
                    {

                        count += 1;
                    }
                }

                string[] values2 = chem_code.Split(',');
                for (int i = 0; i < values2.Length; i++)
                {
                    if (values2[i] != "")
                    {

                        count2 += 1;
                    }
                }

                lbldrchem.Text = count.ToString() + " / " + count2.ToString();

            }
        }
    }

    [WebMethod]
    public static string GetCustomersdr(string teritoryname)
    {
       

        string sf_code = string.Empty;


       

        string valss = string.Empty;

        string div_code = HttpContext.Current.Session["div_code"].ToString();




            sf_code = HttpContext.Current.Session["sf_code"].ToString();
        

        string[] cust = teritoryname.Split(new char[] { ',' });
        foreach (string teri in cust)
        {
            // string s = string.Format("'{0}'", string.Join("','", teri));

            valss += "'" + teri.TrimStart(' ') + "'" + ",";
        }


        string query = "select id, ListedDrCode," +
                      " case id when 1 then '<span style=color:MEDIUMVIOLETRED;font-weight:bold;width:5000px>'+ListedDr_Name +  '</span>' +' - '+'<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+ '</span>'  " +
                      " when 2 then '<span style=color:MAROON;font-weight:bold>'+ListedDr_Name+ '</span>' +' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+'</span>' " +
                      " when 3 then '<span style=color:MEDIUMSLATEBLUE;font-weight:bold>'+ListedDr_Name +'</span>' + ' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name +' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )' +  '</span>'  " +
                      " when 4 then '<span style=color:TEAL;font-weight:bold>'+ListedDr_Name + '</span>' + ' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+  '</span>' " +
                      " when 5 then '<span style=color:LightCoral;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name +' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )' + '</span>' " +
                      " when 6 then '<span style=color:MEDIUMVIOLETRED;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+ '</span>' " +
                      " when 7 then '<span style=color:MAROON;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+ '</span>' " +
                      " when 8 then '<span style=color:MEDIUMSLATEBLUE;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+  '</span>' " +
                      " when 9 then '<span style=color:TEAL;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+  '</span>' " +
                      " when 10 then '<span style=color:LightCoral;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+ '</span>' " +
                      " when 11 then '<span style=color:MEDIUMVIOLETRED;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+ '</span>' " +
                      " when 12 then '<span style=color:MAROON;font-weight:bold>'+ListedDr_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>' + Territory_Name + ' - ' +Doc_Cat_ShortName +'( '+cast(No_of_visit as varchar)+' )'+ '</span>' " +
                      " end as ListedDr_Name,Territory_Name from " +
                      "  ( select  dense_rank() over ( order by Territory_Name) as id, c.ListedDrCode, ListedDr_Name,Territory_Name,Doc_Cat_ShortName,d.No_of_visit " +
                     " from Mas_Territory_Creation p , Mas_ListedDr c , Mas_Doctor_Category d where d.Doc_Cat_Code=c.Doc_Cat_Code and Doc_Cat_Active_Flag=0 and cast(p.Territory_Code as varchar)=CAST( c.Territory_Code  as varchar) " +
                      " and p.Territory_Name in (" + valss.TrimEnd(',') + ") and  p.SF_Code='" + sf_code + "' and c.division_code='" + div_code + "' and ListedDr_Active_Flag=0 union all " +
                       " select '99' as id, '888888' ListedDrCode,'' ListedDr_Name,'z' Territory_Name,'' Doc_Cat_ShortName,'' No_of_visit ) yyy order by Territory_Name,ListedDr_Name ";
                      // " CASE listeddr_visit_days " +
                      //" WHEN '" + dayshort + "' THEN '" + day + "' else 5 END";

        SqlCommand cmd = new SqlCommand(query);
        return GetData(cmd).GetXml();
    }

    [WebMethod]
    public static string GetCustomerschem(string teritoryname)
    {
        //string query = "SELECT Designation_Code,Designation_Short_Name  " +
        //             " FROM Mas_SF_Designation where Division_Code ='7'";

        string sf_code = string.Empty;

        string valss = string.Empty;

        string div_code = HttpContext.Current.Session["div_code"].ToString();

 
        
            sf_code = HttpContext.Current.Session["sf_code"].ToString();
        

        string[] cust = teritoryname.Split(new char[] { ',' });
        foreach (string teri in cust)
        {
            // string s = string.Format("'{0}'", string.Join("','", teri));

            valss += "'" + teri.TrimStart(' ') + "'" + ",";
        }

        string query = "select id, Chem_Code," +
                    " case id when 1 then '<span style=color:olive;font-weight:bold>'+Chem_Name + '</span>' +' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name + '</span>' " +
                    " when 2 then '<span style=color:orange-red;font-weight:bold>'+Chem_Name+'</span>' + ' - '+ '<span style=color:red;font-weight:bold>'+Territory_Name + '</span>' " +
                    " when 3 then '<span style=color:indigo;font-weight:bold>'+Chem_Name +'</span>' + ' - '+'<span style=color:red;font-weight:bold>'+ Territory_Name + '</span>' " +
                    " when 4 then '<span style=color:Brown;font-weight:bold>'+Chem_Name + '</span>' +' - '+ '<span style=color:red;font-weight:bold>'+Territory_Name + '</span>'" +
                    " when 5 then '<span style=color:Amaranth;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 6 then '<span style=color:olive;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 7 then '<span style=color:orange;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 8 then '<span style=color:indigo;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 9 then '<span style=color:Brown;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 10 then '<span style=color:Amaranth;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 11 then '<span style=color:olive;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " when 12 then '<span style=color:orange;font-weight:bold>'+Chem_Name + ' - '+ '<span style=color:red;font-weight:bold>'+ Territory_Name +  '</span>'" +
                    " end as Chem_Name,Territory_Name from " +
                    "( select dense_rank() over ( order by Territory_Name) as id, c.Chemists_Code as Chem_Code, c.Chemists_Name  as Chem_Name,Territory_Name from Mas_Territory_Creation p inner join mas_chemists c on cast(p.Territory_Code as varchar)=CAST( c.Territory_Code  as varchar) " +
                    " where p.Territory_Name in (" + valss.TrimEnd(',') + ") and  p.SF_Code='" + sf_code + "' and c.division_code='" + div_code + "' and Chemists_Active_Flag=0  union all " +
                    "  select '99' as id, '99' Chem_Code,'' Chem_Name,'z' Territory_Name ) yyy order by Territory_Name   ";


        SqlCommand cmd = new SqlCommand(query);
        return GetData(cmd).GetXml();
    }


    private static DataSet GetData(SqlCommand cmd)
    {
        string strConnString = ConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;
        using (SqlConnection con = new SqlConnection(strConnString))
        {
            using (SqlDataAdapter sda = new SqlDataAdapter())
            {
                cmd.Connection = con;
                sda.SelectCommand = cmd;
                using (DataSet ds = new DataSet())
                {
                    sda.Fill(ds);
                    return ds;

                }
            }
        }
    }


    protected void btnSave_Click(object sender, EventArgs e)
    {
        Create_STP("draft");
    }
    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        Create_STP("submit");
    }

    private void fillcategory()
    {
        dsCat = tp.Fillcateg_STP(div_code, sf_code);
        grdcat.DataSource = dsCat;
        grdcat.DataBind();

        dsCat = tp.Fillcateg_STP_Vst(div_code, sf_code);
        grdvst.DataSource = dsCat;
        grdvst.DataBind();
    }

    private void Create_STP(string mode)
    {
         TP_New tp = new TP_New();
         string Active_Flag = string.Empty;

         if (mode == "submit")
         {
             Active_Flag = "0";
         }
         else if (mode == "draft")
         {
             Active_Flag = "2";
         }
         foreach (GridViewRow row in grdSTP.Rows)
         {
             Label lblday_plan_name = (Label)row.FindControl("lblday_plan_name");
             HiddenField hdnplan_shortname =(HiddenField)row.FindControl("hdnplan_shortname");
             HiddenField hdnplan_code = (HiddenField)row.FindControl("hdnplan_code");

             CheckBoxList chkterr = (CheckBoxList)row.FindControl("chkterr");


          

             string Patch_Code = string.Empty;
             string Patch_Name = string.Empty;

             for (int i = 0; i < chkterr.Items.Count; i++)
             {
                 if (chkterr.Items[i].Selected)
                 {
                     Patch_Code += chkterr.Items[i].Value + ",";
                     Patch_Name += chkterr.Items[i].Text + ",";
                 }
             }


             HiddenField hdndr = (HiddenField)row.FindControl("hdndr");
             HiddenField hdndr_name = (HiddenField)row.FindControl("hdndr_name");

             HiddenField hdnchem = (HiddenField)row.FindControl("hdnchem");
             HiddenField hdnchem_name = (HiddenField)row.FindControl("hdnchem_name");




             int iReturn = tp.STP_Creation_Insert(sf_code, div_code, lblday_plan_name.Text, hdnplan_shortname.Value, hdnplan_code.Value, Patch_Code.TrimEnd(','), Patch_Name.TrimEnd(','), hdndr.Value, hdndr_name.Value, hdnchem.Value, hdnchem_name.Value, Active_Flag);

             if (iReturn > 0)
             {

                 if (mode == "submit")
                 {
                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('STP Submitted Successfully');window.location='STP_Creation.aspx'</script>");
                 }
                 else if (mode == "draft")
                 {

                     ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('STP Saved Successfully');window.location='STP_Creation.aspx'</script>");
                 }
             }


         }
  
    }

    
}
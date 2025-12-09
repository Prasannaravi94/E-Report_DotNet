using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Configuration;
using Bus_EReport;
using DBase_EReport;

public partial class MasterFiles_Designation_level : System.Web.UI.Page
{

    #region "Declaration"
    DataSet dsDesignation = null;
    DataSet dsDes = null;
    DataSet dsdiv = null;
    DataSet dsDivision = null;
    int Designation_Code = 0;
    string Designation_Short_Name = string.Empty;
    string Designation_Name = string.Empty;
    string Desig_Color = string.Empty;
    string type = string.Empty;
    string division_code = string.Empty;
    string sf_type = string.Empty;
    DateTime ServerStartTime;
    DateTime ServerEndTime;
    int time;
    string strQry = string.Empty;
    DB_EReporting db_ER = new DB_EReporting();
    #endregion

    protected void Page_Load(object sender, EventArgs e)
    {

        division_code = Request.QueryString["div_code"];
        if (!Page.IsPostBack)
        {
            
            //menu1.Title = this.Page.Title;
            ////// menu1.FindControl("btnBack").Visible = false;
            ServerStartTime = DateTime.Now;
            base.OnPreInit(e);
            Filldesg();

            strQry = "select Designation_Code,Report_Level from Mas_SF_Designation where division_code='" + division_code + "' " +
                " and type =2 and Designation_Active_Flag=0 and Report_Level !='' order by Manager_SNo";

            dsDes = db_ER.Exec_DataSet(strQry);

            string oneee = "";
            string twoo = "";
            string threee = "";
            string fourrr = "";
            string fivee = "";
            string sixxx = "";
            string seveenn = "";
            string eightt = "";
            string ninee = "";
            string tenn = "";



            if (dsDes.Tables[0].Rows.Count > 0)
            {
                foreach (DataRow dr in dsDes.Tables[0].Rows)
                {

                    if (dr["Report_Level"].ToString() == "1")
                    {
                        foreach (ListItem item in Chkfirst.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                             {
                                 item.Selected = true;
                                 oneee += item + ",";

                                 txtfirst.Text = oneee;
                                

                             }
                        }
                    }
                    else if(dr["Report_Level"].ToString() == "2")
                    {
                        foreach (ListItem item in chksecond.Items)
                        {
                            if(item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected=true;
                                twoo += item + ",";
                                txtsecond.Text = twoo;
                            }
                        }
                    }

                    else if (dr["Report_Level"].ToString() == "3")
                    {
                        foreach (ListItem item in chkthird.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                threee += item + ",";
                                txtthird.Text = threee;
                            }
                        }
                    }
                    else if (dr["Report_Level"].ToString() == "4")
                    {
                        foreach (ListItem item in chkfourth.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                fourrr += item + ",";
                                txtfourth.Text = fourrr;
                            }
                        }
                    }

                    else if (dr["Report_Level"].ToString() == "5")
                    {
                        foreach (ListItem item in chkfifth.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                fivee += item + ",";
                                txtfifth.Text = fivee;
                            }
                        }
                    }

                    else if (dr["Report_Level"].ToString() == "6")
                    {
                        foreach (ListItem item in Chksixth.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                sixxx += item + ",";
                                txtsixth.Text = sixxx;
                            }
                        }
                    }

                    else if (dr["Report_Level"].ToString() == "7")
                    {
                        foreach (ListItem item in chkseventh.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                seveenn += item + ",";
                                txtseventh.Text = seveenn;
                            }
                        }
                    }
                    else if (dr["Report_Level"].ToString() == "8")
                    {
                        foreach (ListItem item in chkeighth.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                eightt += item + ",";
                                txteighth.Text = eightt;
                            }
                        }
                    }

                    else if (dr["Report_Level"].ToString() == "9")
                    {
                        foreach (ListItem item in chkninth.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                ninee += item + ",";
                                txtninth.Text = ninee;
                            }
                        }
                    }

                    else if (dr["Report_Level"].ToString() == "10")
                    {
                        foreach (ListItem item in chktenth.Items)
                        {
                            if (item.Value == dr["Designation_Code"].ToString())
                            {
                                item.Selected = true;
                                tenn += item + ",";
                               txttenth.Text = tenn;
                            }
                        }
                    }

                }
            }

            //txtfirst.Text = oneee;

        }
      //  setValueToChkBoxList();

    }

    protected void Chkfirst_SelectedIndexChanged(object sender, EventArgs e)
    {
        string first_name = "";
        string first_value = "";

        for (int i = 0; i < Chkfirst.Items.Count; i++)
        {
            if (Chkfirst.Items[i].Selected)
            {
                first_name += Chkfirst.Items[i].Text + ",";
                first_value += Chkfirst.Items[i].Value + ",";
            }
        }

        txtfirst.Text = first_name;

        if (first_value != "")
        {
            Session["first_value"] = first_value.Remove(first_value.Length - 1);
        }
        else
        {
            Session["first_value"] = null;
        }
    }

    protected void chksecond_SelectedIndexChanged(object sender, EventArgs e)
    {
        string second_name = "";
        string second_value = "";

        for (int i = 0; i < chksecond.Items.Count; i++)
        {
            if (chksecond.Items[i].Selected)
            {
                second_name += chksecond.Items[i].Text + ",";
                second_value += chksecond.Items[i].Value + ",";
            }
        }

       txtsecond.Text = second_name;

       if (second_value != "")
       {
           Session["second_value"] = second_value.Remove(second_value.Length - 1);
       }
       else
       {
           Session["second_value"] = null;
       }
    }

    protected void chkthird_SelectedIndexChanged(object sender, EventArgs e)
    {
        string third_name = "";
        string third_value = "";

        for (int i = 0; i < chkthird.Items.Count; i++)
        {
            if (chkthird.Items[i].Selected)
            {
                third_name += chkthird.Items[i].Text + ",";
                third_value += chkthird.Items[i].Value + ",";
            }
        }

        txtthird.Text = third_name;

        if (third_value != "")
        {
            Session["third_value"] = third_value.Remove(third_value.Length - 1);
        }
        else
        {
            Session["third_value"] = null;
        }
    }

    protected void chkfourth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string fourth_name = "";
        string fourth_value = "";

        for (int i = 0; i < chkfourth.Items.Count; i++)
        {
            if (chkfourth.Items[i].Selected)
            {
                fourth_name += chkfourth.Items[i].Text + ",";
                fourth_value += chkfourth.Items[i].Value + ",";
            }
        }

        txtfourth.Text = fourth_name;

        if (fourth_value != "")
        {
            Session["fourth_value"] = fourth_value.Remove(fourth_value.Length - 1);
        }
        else
        {
            Session["fourth_value"] = null;
        }
    }

    protected void chkfifth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string fifth_name = "";
        string fifth_value = "";

        for (int i = 0; i < chkfifth.Items.Count; i++)
        {
            if (chkfifth.Items[i].Selected)
            {
                fifth_name += chkfifth.Items[i].Text + ",";
                fifth_value += chkfifth.Items[i].Value + ",";
            }
        }

        txtfifth.Text = fifth_name;

        if (fifth_value != "")
        {
            Session["fifth_value"] = fifth_value.Remove(fifth_value.Length - 1);
        }
        else
        {
            Session["fifth_value"] = null;
        }
    }
    protected void Chksixth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string sixth_name = "";
        string sixth_value = "";

        for (int i = 0; i < Chksixth.Items.Count; i++)
        {
            if (Chksixth.Items[i].Selected)
            {
                sixth_name += Chksixth.Items[i].Text + ",";
                sixth_value += Chksixth.Items[i].Value + ",";
            }
        }

        txtsixth.Text = sixth_name;

        if (sixth_value != "")
        {
            Session["sixth_value"] = sixth_value.Remove(sixth_value.Length - 1);
        }
        else
        {
            Session["sixth_value"] = null;
        }
    }

    protected void chkseventh_SelectedIndexChanged(object sender, EventArgs e)
    {
        string seventh_name = "";
        string seventh_value = "";

        for (int i = 0; i < chkseventh.Items.Count; i++)
        {
            if (chkseventh.Items[i].Selected)
            {
                seventh_name += chkseventh.Items[i].Text + ",";
                seventh_value += chkseventh.Items[i].Value + ",";
            }
        }

        txtseventh.Text = seventh_name;

        if (seventh_value != "")
        {
            Session["seventh_value"] = seventh_value.Remove(seventh_value.Length - 1);
        }
        else
        {
            Session["seventh_value"] = null;
        }
    }

    protected void chkeighth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string eighth_name = "";
        string eighth_value = "";

        for (int i = 0; i < chkeighth.Items.Count; i++)
        {
            if (chkeighth.Items[i].Selected)
            {
                eighth_name += chkeighth.Items[i].Text + ",";
                eighth_value += chkeighth.Items[i].Value + ",";
            }
        }

      txteighth.Text = eighth_name;

      if (eighth_value != "")
      {
          Session["eighth_value"] = eighth_value.Remove(eighth_value.Length - 1);
      }
      else
      {
          Session["eighth_value"] = null;
      }
    }

    protected void chkninth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string ninth_name = "";
        string ninth_value = "";

        for (int i = 0; i < chkninth.Items.Count; i++)
        {
            if (chkninth.Items[i].Selected)
            {
                ninth_name += chkninth.Items[i].Text + ",";
                ninth_value += chkninth.Items[i].Value + ",";
            }
        }

        txtninth.Text= ninth_name;

        if (ninth_value != "")
        {
            Session["ninth_value"] = ninth_value.Remove(ninth_value.Length - 1);
        }
        else
        {
            Session["ninth_value"] = null;
        }
    }
    protected void chktenth_SelectedIndexChanged(object sender, EventArgs e)
    {
        string tenth_name = "";
        string tenth_value = "";

        for (int i = 0; i < chktenth.Items.Count; i++)
        {
            if (chktenth.Items[i].Selected)
            {
                tenth_name += chktenth.Items[i].Text + ",";
                tenth_value += chktenth.Items[i].Value + ",";
            }
        }

        txttenth.Text = tenth_name;

        if (tenth_value != "")
        {
            Session["tenth_value"] = tenth_value.Remove(tenth_value.Length - 1);
        }
    }

    private void Filldesg()
    {
        strQry = "select Designation_Code,Designation_Short_Name from Mas_SF_Designation where division_code='"+division_code+"' " +
                 " and type =2 and Designation_Active_Flag=0 order by Manager_SNo" ;

        dsDesignation = db_ER.Exec_DataSet(strQry);
        if (dsDesignation.Tables[0].Rows.Count > 0)
        {
            Chkfirst.DataSource = dsDesignation;
            Chkfirst.DataTextField = "Designation_Short_Name";
            Chkfirst.DataValueField = "Designation_Code";
            Chkfirst.DataBind();

            chksecond.DataSource = dsDesignation;
            chksecond.DataTextField = "Designation_Short_Name";
            chksecond.DataValueField = "Designation_Code";
            chksecond.DataBind();

            chkthird.DataSource = dsDesignation;
            chkthird.DataTextField = "Designation_Short_Name";
            chkthird.DataValueField = "Designation_Code";
            chkthird.DataBind();

            chkfourth.DataSource = dsDesignation;
            chkfourth.DataTextField = "Designation_Short_Name";
            chkfourth.DataValueField = "Designation_Code";
            chkfourth.DataBind();

            chkfifth.DataSource = dsDesignation;
            chkfifth.DataTextField = "Designation_Short_Name";
            chkfifth.DataValueField = "Designation_Code";
            chkfifth.DataBind();

            Chksixth.DataSource = dsDesignation;
            Chksixth.DataTextField = "Designation_Short_Name";
            Chksixth.DataValueField = "Designation_Code";
            Chksixth.DataBind();

            chkseventh.DataSource = dsDesignation;
            chkseventh.DataTextField = "Designation_Short_Name";
            chkseventh.DataValueField = "Designation_Code";
            chkseventh.DataBind();

            chkeighth.DataSource = dsDesignation;
            chkeighth.DataTextField = "Designation_Short_Name";
            chkeighth.DataValueField = "Designation_Code";
            chkeighth.DataBind();

            
            chkninth.DataSource = dsDesignation;
            chkninth.DataTextField = "Designation_Short_Name";
            chkninth.DataValueField = "Designation_Code";
            chkninth.DataBind();

            
            chktenth.DataSource = dsDesignation;
            chktenth.DataTextField = "Designation_Short_Name";
            chktenth.DataValueField = "Designation_Code";
            chktenth.DataBind();
        }
        
    }

   


    protected void btnSubmit_Click(object sender, EventArgs e)
    {

        string one = string.Empty;
        string two = string.Empty;
        string three = string.Empty;
        string four = string.Empty;
        string five = string.Empty;
        string six = string.Empty;
        string seven = string.Empty;
        string eight = string.Empty;
        string nine = string.Empty;
        string ten = string.Empty;

        if (Session["first_value"] == null)
        {
            one = "one";
        }
        else
        {
            one = Session["first_value"].ToString();
        }

        if (Session["second_value"] == null)
        {
            two = "two";
        }
        else
        {
            two = Session["second_value"].ToString();
        }

        if (Session["third_value"] == null)
        {
            three = "three";
        }
        else
        {
            three = Session["third_value"].ToString();
        }

        if (Session["fourth_value"] == null)
        {
            four = "four";
        }
        else
        {
            four = Session["fourth_value"].ToString();
        }
        if (Session["fifth_value"] == null)
        {
            five = "five";
        }
        else
        {
            five = Session["fifth_value"].ToString();
        }
        if (Session["sixth_value"] == null)
        {
            six = "six";
        }
        else
        {
            six = Session["sixth_value"].ToString();
        }

        if (Session["seventh_value"] == null)
        {
            seven = "seven";
        }
        else
        {
            seven = Session["seventh_value"].ToString();
        }

        if (Session["eighth_value"] == null)
        {
            eight = "eight";
        }
        else
        {
            eight = Session["eighth_value"].ToString();
        }


        if (Session["ninth_value"] == null)
        {
            nine = "nine";
        }
        else
        {
            nine = Session["ninth_value"].ToString();
        }

        if (Session["tenth_value"] == null)
        {
            ten = "ten";
        }
        else
        {
            ten = Session["tenth_value"].ToString();
        }


        string[] _input = new string[] {one,two,three,four,five,six,seven,eight,nine,ten};

        IGrouping<string, string> max =
          _input.GroupBy(n => n)
          .OrderByDescending(g => g.Count())
          .First();

        if (Convert.ToInt16(max.Count()) > 1)
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Not Allowed to Choose Same Designation for Differet level');</script>");
        }

        else
        {
            int iReturn = -1;

            strQry = "update mas_sf_designation set Report_Level='' where division_code='" + division_code + "'  ";
            iReturn = db_ER.ExecQry(strQry);

         
            if (Session["first_value"] != null)
            {
                string first_value = Session["first_value"].ToString();


                string[] first;

                first = first_value.Split(',');

                foreach (string frst in first)
                {
                    strQry = "update mas_sf_designation set Report_Level=1 where division_code='" + division_code + "' and Designation_Code='" + frst + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }

                }

            }

            if (Session["second_value"] != null)
            {
                string second_value = Session["second_value"].ToString();


                string[] second;

                second = second_value.Split(',');

                foreach (string sec in second)
                {
                    strQry = "update mas_sf_designation set Report_Level=2 where division_code='" + division_code + "' and Designation_Code='" + sec + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }

                }

            }

            if (Session["third_value"] != null)
            {
                string third_value = Session["third_value"].ToString();


                string[] third;

                third = third_value.Split(',');

                foreach (string thrd in third)
                {
                    strQry = "update mas_sf_designation set Report_Level=3 where division_code='" + division_code + "' and Designation_Code='" + thrd + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }

                }

            }

            if (Session["fourth_value"] != null)
            {
                string fourth_value = Session["fourth_value"].ToString();


                string[] fourth;

                fourth = fourth_value.Split(',');

                foreach (string frth in fourth)
                {
                    strQry = "update mas_sf_designation set Report_Level=4 where division_code='" + division_code + "' and Designation_Code='" + frth + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }

                }

            }

            if (Session["fifth_value"] != null)
            {
                string fifth_value = Session["fifth_value"].ToString();


                string[] fifth;

                fifth = fifth_value.Split(',');

                foreach (string fif in fifth)
                {
                    strQry = "update mas_sf_designation set Report_Level=5 where division_code='" + division_code + "' and Designation_Code='" + fif + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }

                }

            }

            if (Session["sixth_value"] != null)
            {
                string sixth_value = Session["sixth_value"].ToString();


                string[] sixth;

                sixth = sixth_value.Split(',');

                foreach (string sixx in sixth)
                {
                    strQry = "update mas_sf_designation set Report_Level=6 where division_code='" + division_code + "' and Designation_Code='" + sixx + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }

                }

            }

            if (Session["seventh_value"] != null)
            {
                string seventh_value = Session["seventh_value"].ToString();


                string[] seventh;

                seventh = seventh_value.Split(',');

                foreach (string sevenn in seventh)
                {
                    strQry = "update mas_sf_designation set Report_Level=7 where division_code='" + division_code + "' and Designation_Code='" + sevenn + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }
                }
            }

            if (Session["eighth_value"] != null)
            {
                string eighth_value = Session["eighth_value"].ToString();


                string[] eighth;

                eighth = eighth_value.Split(',');

                foreach (string eigh in eighth)
                {
                    strQry = "update mas_sf_designation set Report_Level=8 where division_code='" + division_code + "' and Designation_Code='" + eigh + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }
                }
            }

            if (Session["ninth_value"] != null)
            {
                string ninth_value = Session["ninth_value"].ToString();


                string[] ninth;

                ninth = ninth_value.Split(',');

                foreach (string nin in ninth)
                {
                    strQry = "update mas_sf_designation set Report_Level=9 where division_code='" + division_code + "' and Designation_Code='" + nin + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }
                }
            }

            if (Session["tenth_value"] != null)
            {
                string tenth_value = Session["tenth_value"].ToString();


                string[] tenth;

                tenth = tenth_value.Split(',');

                foreach (string tenn in tenth)
                {
                    strQry = "update mas_sf_designation set Report_Level=10 where division_code='" + division_code + "' and Designation_Code='" + tenn + "' ";
                    iReturn = db_ER.ExecQry(strQry);
                    if (iReturn > 0)
                    {
                        ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Saved Successfully');</script>");
                    }
                }
            }
        }
    }
    private void setValueToChkBoxList()
    {
        try
        {
            foreach (ListItem item in Chkfirst.Items)
            {
                item.Attributes.Add("cbValue", item.Value);
            }

            foreach (ListItem item in chksecond.Items)
            {
                item.Attributes.Add("cbValue2", item.Value);
            }

            foreach (ListItem item in chkthird.Items)
            {
                item.Attributes.Add("cbValue3", item.Value);
            }
            foreach (ListItem item in chkfourth.Items)
            {
                item.Attributes.Add("cbValue4", item.Value);
            }
            foreach (ListItem item in chkfifth.Items)
            {
                item.Attributes.Add("cbValue5", item.Value);
            }
            foreach (ListItem item in Chksixth.Items)
            {
                item.Attributes.Add("cbValue6", item.Value);
            }
            foreach (ListItem item in chkseventh.Items)
            {
                item.Attributes.Add("cbValue7", item.Value);
            }
            foreach (ListItem item in chkeighth.Items)
            {
                item.Attributes.Add("cbValue8", item.Value);
            }
            foreach (ListItem item in chkninth.Items)
            {
                item.Attributes.Add("cbValue9", item.Value);
            }
            foreach (ListItem item in chktenth.Items)
            {
                item.Attributes.Add("cbValue10", item.Value);
            }
        }
        catch (Exception)
        {
        }
    }


    protected void btnBack_Click(object sender, EventArgs e)
    {

        System.Threading.Thread.Sleep(time);
        Response.Redirect("Designation.aspx");
    }
}
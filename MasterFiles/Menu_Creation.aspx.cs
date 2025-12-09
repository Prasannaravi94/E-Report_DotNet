using Bus_EReport;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Web.Configuration;
using System.IO;
using System.Data.OleDb;
using System.Web.UI.HtmlControls;
using Newtonsoft.Json;



public partial class MasterFiles_Menu_Creation : System.Web.UI.Page
{
    string sf_code = string.Empty;

    string div_code = string.Empty;

    DataSet dsdiv = null;
    DataSet dsdiv1 = null;
    string PK_Id = string.Empty;
    string sCmd = string.Empty;
    string FK_PK_Id = string.Empty;
    DataSet ds;
    DataTable Dt;


    protected void Page_Load(object sender, EventArgs e)
    {
        div_code = Session["div_code"].ToString();


        // grdmenu.Visible = false;
        ddlOrderby.Visible = false;
        TextBox6.Visible = false;
        ddlmenupath.Visible = false;
        Label6.Visible = false;
        Label7.Visible = false;
        Label8.Visible = false;
        Button9.Visible = false;
        Label1.Visible = false;
        FileUpload1.Visible = false;
        GridView1.Visible = false;
        //  grdmenu.Visible = false;
        menuItemHeading.Visible = false;
        menuItemCreationHeading.Visible = false;
        // Button8.Visible = false;
        Button9.Visible = false;
        Button10.Visible = false;
        // menuTableHeading.Visible = false;
        //   menuTableBody.Visible = false;
        btnUpdate.Visible = false;
        menuTable.Visible = false;


        if (!Page.IsPostBack)
        {
            //  Menu1.Title = Page.Title;
            //  Menu1.FindControl("btnBack").Visible = false;  

            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                 
            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;
                
            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;
                
            }

        }
        else
        {
            if (Session["sf_type"].ToString() == "2")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                UserControl_MGR_Menu c1 =
               (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "1")
            {
                div_code = Session["div_code"].ToString();
                UserControl_MR_Menu c1 =
               (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
                Divid.Controls.Add(c1);
                c1.Title = Page.Title;
                c1.FindControl("btnBack").Visible = false;

            }
            else if (Session["sf_type"].ToString() == "" || Session["sf_type"].ToString() == "3")
            {
                div_code = Session["div_code"].ToString();
                sf_code = Session["sf_code"].ToString();
                if (div_code.Contains(','))
                {
                    div_code = div_code.Remove(div_code.Length - 1);
                }
                UserControl_MenuUserControl c1 =
               (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
                Divid.Controls.Add(c1);
                //c1.Title = Page.Title;
                //c1.FindControl("btnBack").Visible = false;

            }
        }



        //if (Session["sf_type"].ToString() == "1")
        //{
        //    sf_code = Session["sf_code"].ToString();
        //    UserControl_MR_Menu Usc_MR = (UserControl_MR_Menu)LoadControl("~/UserControl/MR_Menu.ascx");
        //    Divid.Controls.Add(Usc_MR);
        //    Divid.FindControl("btnBack").Visible = false;
        //    Usc_MR.Title = this.Page.Title;


        //    ddltype.Visible = true;
        //    menuid.Visible = true;

        //}
        //else if (Session["sf_type"].ToString() == "2")
        //{
        //    sf_code = Session["sf_code"].ToString();
        //    UserControl_MGR_Menu Usc_MGR = (UserControl_MGR_Menu)LoadControl("~/UserControl/MGR_Menu.ascx");
        //    Divid.Controls.Add(Usc_MGR);
        //    Divid.FindControl("btnBack").Visible = false;
        //    Usc_MGR.Title = this.Page.Title;


        //    ddltype.Visible = true;
        //    menuid.Visible = true;

        //}
        //else
        //{
        //    sf_code = Session["sf_code"].ToString();
        //    UserControl_MenuUserControl Admin = (UserControl_MenuUserControl)LoadControl("~/UserControl/MenuUserControl.ascx");
        //    Divid.Controls.Add(Admin);


        //    ddltype.Visible = true;
        //    menuid.Visible = true;

        //}





    }

    private void MenunameBindGridView(string menuType)
    {
        Division dv = new Division();
        DataTable dt = new DataTable();
        dt.Columns.Add("Menu_Type");
        dt.Columns.Add("Menu_Name");
        dt.Columns.Add("PK_Id");

        DataSet dsdiv = dv.getmenutype(div_code, menuType);

        if (dsdiv != null && dsdiv.Tables[0].Rows.Count > 0)
        {
            foreach (DataRow row in dsdiv.Tables[0].Rows)
            {
                DataRow newRow = dt.NewRow();
                newRow["Menu_Type"] = row["Menu_Type"];
                newRow["Menu_Name"] = row["Menu_Name"];
                newRow["PK_Id"] = row["PK_Id"];
                dt.Rows.Add(newRow);
            }

            GridView1.DataSource = dt;
            GridView1.DataBind();


        }
        else
        {
            GridView1.EmptyDataText = "No Records Found";
            GridView1.DataBind();

        }
    }

    protected void grdmenu_RowEditing1(object sender, GridViewEditEventArgs e)
    {

        GridView1.SelectedIndex = e.NewEditIndex;
        string selectedMenuType = ddltype.SelectedValue; // Get selected value from the dropdown
        MenunameBindGridView(selectedMenuType);
        GridView1.Visible = true;


    }

    protected void grdmenu_RowCommand(object sender, GridViewCommandEventArgs e)
    {
        if (e.CommandName == "Edit")
        {
            string PK_Id = e.CommandArgument.ToString();

            ListedDR LstDoc = new ListedDR();
            DataSet dsdiv = LstDoc.getmenucrea(Convert.ToInt32(PK_Id));

            if (dsdiv != null && dsdiv.Tables[0].Rows.Count > 0)
            {
                menuid.Text = dsdiv.Tables[0].Rows[0]["Menu_Name"].ToString();
                string menuPage = dsdiv.Tables[0].Rows[0]["Menu_Page"].ToString();


                if (menuPage == "MasterFiles/Dashboard_PreCall_Analysis.aspx")
                {
                    ddlpath.SelectedValue = "precallanalysis";
                }
                else if (menuPage == "MasterFiles/Order_booking.aspx")
                {
                    ddlpath.SelectedValue = "orderbooking";
                }
                else
                {
                    ddlpath.SelectedValue = "0";
                }

                ddltype.SelectedValue = dsdiv.Tables[0].Rows[0]["Menu_Type"].ToString();
                ViewState["PK_Id"] = PK_Id;
                ViewState["IsEditing"] = true;
                btnsave.Visible = false;
                btnUpdate.Visible = true;
                Button9.Visible = false;
                if (ddltype.SelectedValue == "M")
                {
                    menuItemHeading.Visible = true;
                    menuItemCreationHeading.Visible = true;
                    TextBox6.Visible = true;
                    ddlmenupath.Visible = true;
                    Label6.Visible = true;
                    Label7.Visible = true;
                    Label8.Visible = true;
                    ddlOrderby.Visible = true;
                    menuTable.Visible = true;
                    Button10.Visible = true;
                    Button9.Visible = false;

                    btnUpdate.Visible = false;
                    Label1.Visible = true;
                    FileUpload1.Visible = true;
                    DataSet menuDataSet = LstDoc.GetMenuItems(PK_Id);
                    menuTable.Rows.Clear();
                    HtmlTableRow headerRow = new HtmlTableRow();
                    HtmlTableCell headerCell1 = new HtmlTableCell();
                    headerCell1.InnerText = "S. No.";
                    headerCell1.Style["font-weight"] = "bold";
                    headerRow.Cells.Add(headerCell1);

                    HtmlTableCell headerCell2 = new HtmlTableCell();
                    headerCell2.InnerText = "Menu Name";
                    headerCell2.Style["font-weight"] = "bold";
                    headerRow.Cells.Add(headerCell2);

                    HtmlTableCell headerCell3 = new HtmlTableCell();
                    headerCell3.InnerText = "Edit";
                    headerCell3.Style["font-weight"] = "bold";
                    headerRow.Cells.Add(headerCell3);

                    HtmlTableCell headerCell4 = new HtmlTableCell();
                    headerCell4.InnerText = "Deactivate";
                    headerCell4.Style["font-weight"] = "bold";
                    headerRow.Cells.Add(headerCell4);

                    menuTable.Rows.Add(headerRow);
                    TextBox6.Text = "";
                    ddlmenupath.SelectedValue = "0";
                    ddlOrderby.SelectedValue = "0";


                    if (menuDataSet != null && menuDataSet.Tables[0].Rows.Count > 0)
                    {
                        int sno = 1;
                        foreach (DataRow row in menuDataSet.Tables[0].Rows)
                        {
                            HtmlTableRow newRow = new HtmlTableRow();
                            newRow.Attributes["data-menu-id"] = row["OptionMenu_Name"].ToString();
                            newRow.Attributes["data-path"] = row["OptionMenu_Page"].ToString();
                            newRow.Attributes["data-orderby"] = row["OptionMenu_Position"].ToString();
                            newRow.Attributes["data-fkpkid"] = row["FK_PK_Id"].ToString();
                            newRow.Attributes["data-optionmenuid"] = row["OptionMenu_Id"].ToString();

                            HtmlTableCell snoCell = new HtmlTableCell();
                            snoCell.InnerText = sno.ToString();
                            newRow.Cells.Add(snoCell);

                            HtmlTableCell cell1 = new HtmlTableCell();
                            string menuName = row["OptionMenu_Name"].ToString();
                            cell1.InnerText = menuName;
                            newRow.Cells.Add(cell1);

                            HtmlTableCell editCell = new HtmlTableCell();
                            HtmlAnchor editLink = new HtmlAnchor();
                            editLink.HRef = "#";
                            editLink.InnerText = "Edit";
                            editLink.Attributes["class"] = "link-btn";
                            editLink.Attributes["onclick"] = "EditRow(this.parentNode.parentNode); return false;";
                            editCell.Controls.Add(editLink);
                            newRow.Cells.Add(editCell);

                            HtmlTableCell deactivateCell = new HtmlTableCell();
                            HtmlAnchor deactivateLink = new HtmlAnchor();
                            deactivateLink.HRef = "#";
                            deactivateLink.InnerText = "Deactivate";
                            deactivateLink.Attributes["class"] = "link-btn";
                            deactivateLink.Attributes["onclick"] = "DeactivateRow(this.parentNode.parentNode); return false;";

                            deactivateCell.Controls.Add(deactivateLink);
                            newRow.Cells.Add(deactivateCell);

                            menuTable.Rows.Add(newRow);
                            sno++;
                        }
                    }
                    else
                    {
                        HtmlTableRow emptyRow = new HtmlTableRow();
                        HtmlTableCell emptyCell = new HtmlTableCell();
                        emptyCell.InnerText = "No menu items found.";
                        emptyCell.ColSpan = 4;
                        emptyRow.Cells.Add(emptyCell);
                        menuTable.Rows.Add(emptyRow);
                    }
                }
            }

        }
        else
        {

            string PK_Id = Convert.ToString(e.CommandArgument);

            if (!string.IsNullOrEmpty(PK_Id))
            {
                Division dv = new Division();
                int iReturn = dv.MenuDeActivate(PK_Id);

                if (iReturn > 0)
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script type='text/javascript'>alert('Deactivated Successfully');</script>");
                    menuTable.Visible = true;
                    string selectedMenuType = ddltype.SelectedValue;
                    MenunameBindGridView(selectedMenuType);
                    GridView1.Visible = true;
                    if (selectedMenuType == "M")
                    {
                        menuTable.Visible = true;
                        TextBox6.Visible = true;
                        ddlmenupath.Visible = true;
                        ddlOrderby.Visible = true;
                        FileUpload1.Visible = true;
                        menuItemCreationHeading.Visible = true;
                        menuItemHeading.Visible = true;
                        Label6.Visible = true;
                        Label7.Visible = true;
                        Label8.Visible = true;
                        Label8.Visible = true;
                        Label1.Visible = true;
                        Button9.Visible = true;


                    }
                    else
                    {

                        TextBox6.Visible = false;
                        ddlmenupath.Visible = false;
                        ddlOrderby.Visible = false;
                        FileUpload1.Visible = false;
                        menuTable.Visible = false;
                        menuItemCreationHeading.Visible = false;
                        menuItemHeading.Visible = false;
                        Label1.Visible = false;

                    }
                }
                else
                {

                    ClientScript.RegisterStartupScript(GetType(), "Message", "<script type='text/javascript'>alert('Unable to Deactivate');</script>");
                }
            }
            else
            {

                ClientScript.RegisterStartupScript(GetType(), "Message", "<script type='text/javascript'>alert('Invalid Menu ID');</script>");
            }
        }
    }
    protected void btnSave_Click(object sender, EventArgs e)
    {
        string menuName = menuid.Text;
        string menutype = ddltype.SelectedValue;
        string Menu_Icon = "";
        string page = ""; // Variable to store the page path

        // Check if the menu name already exists
        Holiday holi = new Holiday();
        bool isMenuNameExists = holi.CheckMenuNameExists(menuName);

        if (isMenuNameExists)
        {
            // Display message if the menu name already exists
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name Already Exists');</script>");
            menuid.Text = ""; // Clear the menu name field
            GridView1.Visible = true;
            return;
        }

        // Determine the page path based on ddlpath selection
        if (ddlpath.SelectedValue == "precallanalysis")
        {
            page = "MasterFiles/Dashboard_PreCall_Analysis.aspx";  // Set the page for Precall Analysis
        }
        else if (ddlpath.SelectedValue == "orderbooking")
        {
            page = "MasterFiles/Order_booking.aspx";  // Set the page for Order Booking
        }
        if (menutype == "0")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select a Menu Type.');</script>");
            GridView1.Visible = true;
            return;
        }
        if (string.IsNullOrEmpty(menuName))
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please enter a Menu Name.');</script>");
            GridView1.Visible = true;
            return;
        }



        // Validate Path
        string path = ddlpath.SelectedValue;
        if (path == "0")
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select a valid Path.');</script>");
            GridView1.Visible = true;
            return;
        }


        // Handle file upload for Menu Icon
        if (FlUploadcsv.HasFile)
        {
            string fileName = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
            string fileExtension = Path.GetExtension(fileName).ToLower();

            // Check if the uploaded file is a valid image
            string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
            if (!allowedExtensions.Contains(fileExtension))
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Only image files (.jpg, .jpeg, .png, .gif) are allowed');</script>");
                return;
            }

            string folderPath = Server.MapPath("~/MasterFiles/Menu_Icons/");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);  // Create directory if it doesn't exist
            }

            string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
            string filePath = folderPath + uniqueFileName;

            FlUploadcsv.SaveAs(filePath);  // Save the uploaded file

            Menu_Icon = "~/MasterFiles/Menu_Icons/" + uniqueFileName;  // Set the image path
        }
        else
        {
            // Default icon if no file is uploaded
            Menu_Icon = "MasterFiles/DynamicMenuIcon/Report/icon_default.png";
        }

        // Check if the menu name is empty
        if (string.IsNullOrEmpty(menuName))
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name is required');</script>");
            return;
        }

        // Call Add_Menu method to insert the menu data into the database
        int iReturn = holi.Add_Menu(PK_Id, menuName, div_code, Menu_Icon, menutype, page);

        // If menu is added successfully
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
            GridView1.Visible = true;

            // Show relevant fields based on menu type
            if (menutype == "M")
            {
                TextBox6.Visible = true;
                ddlmenupath.Visible = true;
                Label6.Visible = true;
                Label7.Visible = true;
                Label8.Visible = true;
                ddlOrderby.Visible = true;
                menuItemHeading.Visible = true;
                menuItemCreationHeading.Visible = true;
                GridView1.Visible = true;
            }

            // Rebind the GridView with updated menu data
            string selectedMenuType = ddltype.SelectedValue;
            MenunameBindGridView(selectedMenuType);

            // Clear the form fields for new entries
            menuid.Text = "";
            ddlpath.SelectedValue = "0";  // Reset the dropdown selection

            btnsave.Visible = true;  // Show the save button
            btnUpdate.Visible = false;  // Hide the update button
        }
        else
        {
            // Display error message if the menu creation failed
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error occurred while creating the menu');</script>");
        }
    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {
        string PK_Id = ViewState["PK_Id"] as string;


        if (string.IsNullOrEmpty(PK_Id))
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid record selected.');</script>");
            return;
        }

        string menuName = menuid.Text;
        string menutype = ddltype.SelectedValue;
        string Menu_Icon = "";
        string page = "";


        if (FlUploadcsv.HasFile)
        {
            try
            {
                string fileName = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                string folderPath = Server.MapPath("~/MasterFiles/Menu_Icons/");
                string fileExtension = Path.GetExtension(fileName).ToLower();


                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Only image files (.jpg, .jpeg, .png, .gif) are allowed');</script>");
                    return;
                }


                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }


                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = folderPath + uniqueFileName;


                FlUploadcsv.SaveAs(filePath);


                Menu_Icon = "~/MasterFiles/Menu_Icons/" + uniqueFileName;
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error uploading the file');</script>");
                return;
            }
        }
        else
        {

            Menu_Icon = "MasterFiles/DynamicMenuIcon/Report/icon_default.png";
        }


        if (ddlpath.SelectedValue == "precallanalysis")
        {
            page = "MasterFiles/Dashboard_PreCall_Analysis.aspx";
        }
        else if (ddlpath.SelectedValue == "orderbooking")
        {
            page = "MasterFiles/Order_booking.aspx";
        }
        else
        {

            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Please select a valid path');</script>");
            return;
        }


        if (string.IsNullOrEmpty(menuName))
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name is required');</script>");
            return;
        }


        Holiday holi = new Holiday();


        int iReturn = holi.Add_Menusave(PK_Id, menuName, div_code, Menu_Icon, menutype, page);


        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");


            string selectedMenuType = ddltype.SelectedValue;
            MenunameBindGridView(selectedMenuType);


            menuid.Text = "";
            ddlpath.SelectedValue = "0";


            btnsave.Visible = true;
            btnUpdate.Visible = false;
            GridView1.Visible = true;
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error occurred while updating the menu');</script>");
        }
    }

    protected void btnmenuupdate_Click(object sender, EventArgs e)
    {
        string PK_Id = ViewState["PK_Id"] as string;


        if (string.IsNullOrEmpty(PK_Id))
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Invalid record selected.');</script>");
            return;
        }

        string menuName = menuid.Text;
        string menutype = ddltype.SelectedValue;
        string Menu_Icon = "";
        string page = "";

        // Handle file upload (logo) if a file is selected
        if (FlUploadcsv.HasFile)
        {
            try
            {
                string fileName = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                string folderPath = Server.MapPath("~/MasterFiles/Menu_Icons/");
                string fileExtension = Path.GetExtension(fileName).ToLower();

                // Validate file extension (only image files allowed)
                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Only image files (.jpg, .jpeg, .png, .gif) are allowed');</script>");
                    return;
                }

                // Ensure the folder exists, if not create it
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                // Generate a unique file name for the uploaded file
                string uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
                string filePath = folderPath + uniqueFileName;

                // Save the file
                FlUploadcsv.SaveAs(filePath);

                // Update Menu_Icon and page with the path to the uploaded file
                Menu_Icon = "~/MasterFiles/Menu_Icons/" + uniqueFileName;
                page = Menu_Icon; // Assign page to the image path
            }
            catch (Exception ex)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error uploading the file');</script>");
                return;
            }
        }
        else
        {
            // If no file is uploaded, use the default icon
            Menu_Icon = "MasterFiles/DynamicMenuIcon/Report/icon_default.png";
            page = Menu_Icon; // Default icon for page as well
        }

        // Validate the menu name
        if (string.IsNullOrEmpty(menuName))
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name is required');</script>");
            return;
        }

        page = "";


        Holiday holi = new Holiday();

        // Call method to update the menu (including the page)
        int iReturn = holi.Add_Menuupdate(PK_Id, menuName, div_code, Menu_Icon, menutype, page);

        // Check if the update was successful
        if (iReturn > 0)
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Updated Successfully');</script>");

            // Refresh the grid view based on the selected menu type
            string selectedMenuType = ddltype.SelectedValue;
            MenunameBindGridView(selectedMenuType);

            // Fetch the updated menu data
            Division dv = new Division();
            DataSet dsdiv = dv.getmenu(div_code, menutype, menuName);
            DataSet dsdiv1 = dv.getmenuFkpkid(div_code, PK_Id);

            HtmlTable menuTable = (HtmlTable)FindControl("menuTable");

            // Get table data in JSON format (from hidden field or elsewhere)
            string tableDataJson = hfTableData.Value;

            if (dsdiv != null && dsdiv.Tables[0].Rows.Count > 0)
            {
                string pkId = dsdiv.Tables[0].Rows[0]["PK_Id"].ToString();

                var tableData = JsonConvert.DeserializeObject<List<TableRow>>(tableDataJson);

                if (dsdiv1.Tables[0].Rows.Count > 0)
                {
                    for (int i = 0; i < tableData.Count; i++)
                    {
                        string Menu_Name = tableData[i].menuName;
                        string path = tableData[i].path;
                        string order_by = tableData[i].orderby;

                        // Ensure valid data is present
                        if (!string.IsNullOrEmpty(Menu_Name) && !string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(order_by))
                        {
                            string optionmenuid = string.Empty;
                            string menuIcon = "MasterFiles/DynamicMenuIcon/Report/icon_default.png";

                            if (i < dsdiv1.Tables[0].Rows.Count)
                            {
                                optionmenuid = dsdiv1.Tables[0].Rows[i]["OptionMenu_Id"].ToString();

                                // Update option menu data if it exists
                                int irReturn = dv.GetOptionUpdateMenuData(div_code, pkId, optionmenuid, Menu_Name, path, order_by, menuIcon);

                                if (irReturn > 0)
                                {
                                    GridView1.Visible = true;
                                    Button9.Visible = true;
                                    Button10.Visible = false;
                                }
                            }
                            else
                            {
                                // Add new option menu if it doesn't exist
                                string newOptionMenuId = dv.GetNextOptionMenuId(pkId);
                                int irReturn = dv.GetOptionUpdateMenuData(div_code, pkId, newOptionMenuId, Menu_Name, path, order_by, menuIcon);

                                if (irReturn > 0)
                                {
                                    GridView1.Visible = true;
                                    Button9.Visible = true;
                                    Button10.Visible = false;
                                }
                            }
                        }
                    }

                    // Reset input fields
                    menuid.Text = "";
                    TextBox6.Text = "";
                    ddlmenupath.SelectedValue = "0";
                    ddlOrderby.SelectedValue = "0";

                    // Show/Hide relevant controls
                    TextBox6.Visible = true;
                    ddlmenupath.Visible = true;
                    Label6.Visible = true;
                    Label7.Visible = true;
                    Label8.Visible = true;
                    Label1.Visible = true;
                    FileUpload1.Visible = true;
                    ddlOrderby.Visible = true;
                    menuItemHeading.Visible = true;
                    menuItemCreationHeading.Visible = true;
                    menuTable.Visible = true;

                    Button9.Visible = true;
                    Button10.Visible = false;
                    GridView1.Visible = true;

                    // Toggle button text based on the editing status
                    if (ViewState["IsEditing"] != null && (bool)ViewState["IsEditing"])
                    {
                        Button9.Text = "Save";
                    }
                    else
                    {
                        Button9.Text = "Update";
                    }
                }
                else
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error occurred while updating the menu');</script>");
                }
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu update failed');</script>");
            }
        }
    }
    protected void btnclear_Click(object sender, EventArgs e)
    {

        Response.Redirect(Request.Url.AbsoluteUri);
    }

    private void BindMenuItemsToTable(string PK_Id)
    {
        ListedDR LstDoc = new ListedDR();
        DataSet menuDataSet = LstDoc.GetMenuItems(PK_Id);

        menuTable.Rows.Clear();


        HtmlTableRow headerRow = new HtmlTableRow();
        HtmlTableCell headerCell1 = new HtmlTableCell();
        headerCell1.InnerText = "S. No.";
        headerCell1.Style["font-weight"] = "bold";
        headerRow.Cells.Add(headerCell1);

        HtmlTableCell headerCell2 = new HtmlTableCell();
        headerCell2.InnerText = "Menu Name";
        headerCell2.Style["font-weight"] = "bold";
        headerRow.Cells.Add(headerCell2);

        HtmlTableCell headerCell3 = new HtmlTableCell();
        headerCell3.InnerText = "Edit";
        headerCell3.Style["font-weight"] = "bold";
        headerRow.Cells.Add(headerCell3);

        HtmlTableCell headerCell4 = new HtmlTableCell();
        headerCell4.InnerText = "Deactivate";
        headerCell4.Style["font-weight"] = "bold";
        headerRow.Cells.Add(headerCell4);

        menuTable.Rows.Add(headerRow);


        if (menuDataSet != null && menuDataSet.Tables[0].Rows.Count > 0)
        {
            int sno = 1;
            foreach (DataRow row in menuDataSet.Tables[0].Rows)
            {
                HtmlTableRow newRow = new HtmlTableRow();

                HtmlTableCell snoCell = new HtmlTableCell();
                snoCell.InnerText = sno.ToString();
                newRow.Cells.Add(snoCell);

                HtmlTableCell cell1 = new HtmlTableCell();
                cell1.InnerText = row["OptionMenu_Name"].ToString();
                newRow.Cells.Add(cell1);

                HtmlTableCell editCell = new HtmlTableCell();
                HtmlAnchor editLink = new HtmlAnchor();
                editLink.HRef = "#";
                editLink.InnerText = "Edit";
                editLink.Attributes["class"] = "link-btn";
                editLink.Attributes["onclick"] = "EditRow('" + row["OptionMenu_Name"].ToString() + "'); return false;";
                editCell.Controls.Add(editLink);
                newRow.Cells.Add(editCell);

                HtmlTableCell deactivateCell = new HtmlTableCell();
                HtmlAnchor deactivateLink = new HtmlAnchor();
                deactivateLink.HRef = "#";
                deactivateLink.InnerText = "Deactivate";
                deactivateLink.Attributes["class"] = "link-btn";
                deactivateLink.Attributes["onclick"] = "DeactivateRow('" + row["OptionMenu_Name"].ToString() + "'); return false;";
                deactivateCell.Controls.Add(deactivateLink);
                newRow.Cells.Add(deactivateCell);

                menuTable.Rows.Add(newRow);
                sno++;
            }
        }
    }


    protected void btnSubmit_Click(object sender, EventArgs e)
    {
        string menuName = menuid.Text;
        string menutype = ddltype.SelectedValue;
        string Menu_Icon = "";
        string page = "";
        Division dv = new Division();

        // Check if the menu name already exists
        bool isMenuNameExists = dv.CheckMenuNameExistsmenu(menuName);

        if (isMenuNameExists)
        {
            // If the menu exists but is inactive, allow creation
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name Already Exists');</script>");

            // Clear the menuid text field
            menuid.Text = "";

            // Show relevant controls
            GridView1.Visible = true;
            TextBox6.Visible = true;
            ddlmenupath.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            ddlOrderby.Visible = true;
            Button9.Visible = true;
            menuItemHeading.Visible = true;
            menuItemCreationHeading.Visible = true;
            Label1.Visible = true;
            FileUpload1.Visible = true;

            // Ensure the menuTable is shown
            HtmlTable menuTable1 = (HtmlTable)FindControl("menuTable");

            // Check if the menuTable is found
            if (menuTable1 != null)
            {
                menuTable1.Visible = true;  // Show the table
            }
            else
            {
                // If menuTable is not found, you can log or handle the error
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('menuTable not found!');</script>");
            }

            return;  // Exit the method, preventing further processing
        }

        // Get the page based on the dropdown value
        if (ddlpath.SelectedValue == "precallanalysis")
        {
            page = "MasterFiles/Dashboard_PreCall_Analysis.aspx";
        }
        else if (ddlpath.SelectedValue == "orderbooking")
        {
            page = "MasterFiles/Order_booking.aspx";
        }

        HtmlTable menuTable = (HtmlTable)FindControl("menuTable");
        string tableDataJson = hfTableData.Value;

        // Check if the menu data is empty or invalid
        if (string.IsNullOrEmpty(tableDataJson) || tableDataJson == "[]" || tableDataJson.Trim() == "[]")
        {
            // Alert when no valid menu items are created
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Enter the Menu_Item Creation');</script>");

            // Make sure all required controls are visible if this condition is met
            menuItemHeading.Visible = true;
            menuItemCreationHeading.Visible = true;
            TextBox6.Visible = true;
            ddlmenupath.Visible = true;
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            ddlOrderby.Visible = true;
            menuTable.Visible = true;
            Button9.Visible = true;
            GridView1.Visible = true;
            Label1.Visible = true;
            FileUpload1.Visible = true;

            return;  // Exit method, not proceeding with saving the menu
        }

        // Deserialize the table data
        var tableData = JsonConvert.DeserializeObject<List<TableRow>>(tableDataJson);

        if (tableData != null && tableData.Count > 0)
        {
            // Handle file upload for Menu Icon
            if (FlUploadcsv.HasFile)
            {
                string fileName = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                string folderPath = Server.MapPath("~/MasterFiles/Menu_Icons/");
                string filePath = folderPath + fileName;

                FlUploadcsv.SaveAs(filePath);
                Menu_Icon = "~/MasterFiles/Menu_Icons/" + fileName;
            }
            else
            {
                Menu_Icon = "MasterFiles/DynamicMenuIcon/Report/icon_default.png";
            }

            // Validate the file extension if uploaded
            if (FlUploadcsv.HasFile)
            {
                string fileName = Path.GetFileName(FlUploadcsv.PostedFile.FileName);
                string fileExtension = Path.GetExtension(fileName).ToLower();

                string[] allowedExtensions = { ".jpg", ".jpeg", ".png", ".gif" };
                if (!allowedExtensions.Contains(fileExtension))
                {
                    ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Only image files (.jpg, .jpeg, .png, .gif) are allowed');</script>");
                    return;
                }
            }

            // Ensure menu name is provided
            if (string.IsNullOrEmpty(menuName))
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name is required');</script>");
                return;
            }

            // Call the method to add the menu
            Holiday holi = new Holiday();
            int iReturn = holi.Add_Menu(PK_Id, menuName, div_code, Menu_Icon, menutype, page);

            if (iReturn > 0)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Created Successfully');</script>");
                menuid.Text = "";
                ddlpath.SelectedValue = "0";
                Button9.Visible = true;
                Button10.Visible = false;
            }
            else if (iReturn == -2)
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Menu Name Already Exists');</script>");
            }
            else
            {
                ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('Error occurred while creating the menu');</script>");
            }

            if (menutype == "M")
            {
                TextBox6.Visible = true;
                ddlmenupath.Visible = true;
                Label6.Visible = true;
                Label7.Visible = true;
                Label8.Visible = true;
                ddlOrderby.Visible = true;
                menuTable.Visible = true;
                Button9.Visible = true;
                menuItemHeading.Visible = true;
                menuItemCreationHeading.Visible = true;
                Label1.Visible = true;
                FileUpload1.Visible = true;
            }

            // Refresh grid view and menu data
            string selectedMenuType = ddltype.SelectedValue;
            MenunameBindGridView(selectedMenuType);

            Division dv1 = new Division();
            DataSet dsdiv = dv1.getmenu(div_code, menutype, menuName);

            string pkId = dsdiv.Tables[0].Rows[0]["PK_Id"].ToString();

            // Process table rows and save data
            foreach (var row in tableData)
            {
                string Menu_Name = row.menuName;
                string path = row.path;
                string order_by = row.orderby;
                string menuIcon = "~/MasterFiles/Menu_Icons/" + "icon_" + Menu_Name + ".png";

                if (!string.IsNullOrEmpty(Menu_Name) && !string.IsNullOrEmpty(path) && !string.IsNullOrEmpty(order_by))
                {
                    int irReturn = dv.GetOptionMenuData(div_code, pkId, Menu_Name, path, order_by, menuIcon);

                    if (irReturn > 0)
                    {
                        GridView1.Visible = true;
                        menuTable.Visible = true;
                        Button9.Visible = true;
                        Button10.Visible = false;
                    }
                }
            }
        }
        else
        {
            ClientScript.RegisterStartupScript(GetType(), "Message", "<SCRIPT LANGUAGE='javascript'>alert('No valid menu items to save');</script>");
        }
    }

    public class MenuItem
    {
        public string MenuName { get; set; }
        public string Path { get; set; }
        public int OrderBy { get; set; }
        public string MenuIcon { get; set; }
    }

    public class TableRow
    {
        public string menuName { get; set; }
        public string path { get; set; }
        public string orderby { get; set; }
    }

    protected void ddltype_SelectedIndexChanged(object sender, EventArgs e)
    {
        string selectedValue = ddltype.SelectedValue;

        // Disable ddltype to prevent further changes after selection
        ddltype.Enabled = false;

        // Handle the case when "Menu" (M) is selected
        if (selectedValue == "M")
        {
            // Ensure the Menu option is selected and visible
            ddltype.SelectedValue = "M";
            ddltype.Items[1].Text = "Menu";  // Optionally update text, although not necessary

            // Show elements related to Menu
            TextBox6.Visible = true;
            ddlmenupath.Visible = true;  // Show the menu path dropdown
            Label6.Visible = true;
            Label7.Visible = true;
            Label8.Visible = true;
            Label1.Visible = true;
            ddlOrderby.Visible = true;
            menuItemHeading.Visible = true;
            menuItemCreationHeading.Visible = true;
            btnsave.Visible = false;
            Button9.Visible = true;
            menuTable.Visible = true;
            GridView1.Visible = true;
            FileUpload1.Visible = true;


            ddlpath.Enabled = false;
            Label9.Enabled = false;
            ddlpath.Visible = false;
            Label9.Visible = false;

            // Retrieve filtered data for 'Menu' (M)
            DataSet filteredData = GetFilteredData(selectedValue);
            if (filteredData != null && filteredData.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = filteredData;
                GridView1.DataBind();
                Console.WriteLine("GridView1 data bound for 'M'.");
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Console.WriteLine("No data found for 'M', GridView1 bound to null.");
            }
        }

        // Handle the case when "Report" (R) is selected
        else if (selectedValue == "R")
        {
            ddltype.SelectedValue = "R";
            ddltype.Items[2].Text = "Report"; // Optionally update text, although not necessary

            // Show elements related to Report
            TextBox6.Visible = false;
            ddlmenupath.Visible = false;  // Hide the menu path dropdown
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            ddlOrderby.Visible = false;
            menuItemHeading.Visible = false;
            menuItemCreationHeading.Visible = false;
            btnsave.Visible = true;    // Show the save button
            Button9.Visible = false;  // Hide the update button
            menuTable.Visible = false;
            Label1.Visible = false;
            GridView1.Visible = true;

            // Enable and show ddlpath (Path dropdown) when "Report" is selected
            ddlpath.Enabled = true;
            ddlpath.Visible = true;
            Label9.Visible = true;
            Label9.Enabled = true;


            // Retrieve filtered data for 'Report' (R)
            DataSet filteredData = GetFilteredData(selectedValue);
            if (filteredData != null && filteredData.Tables[0].Rows.Count > 0)
            {
                GridView1.DataSource = filteredData;
                GridView1.DataBind();
                Console.WriteLine("GridView1 data bound for 'R'.");
            }
            else
            {
                GridView1.DataSource = null;
                GridView1.DataBind();
                Console.WriteLine("No data found for 'R', GridView1 bound to null.");
            }
        }

        // Handle the case when no selection or other options are selected
        else
        {
            // Hide all elements related to both Menu and Report
            TextBox6.Visible = false;
            ddlmenupath.Visible = false;
            Label6.Visible = false;
            Label7.Visible = false;
            Label8.Visible = false;
            ddlOrderby.Visible = false;
            menuItemHeading.Visible = false;
            menuItemCreationHeading.Visible = false;
            btnsave.Visible = false;
            Button9.Visible = false;
            GridView1.Visible = false;

            // Enable and show ddlpath (Path dropdown) when no valid menu type is selected
            ddlpath.Enabled = true;
            ddlpath.Visible = true;
        }

        // Log the selected value for debugging
        Console.WriteLine("Selected Value: " + selectedValue);

        // Rebind GridView with the filtered data based on the selected value
        MenunameBindGridView(selectedValue);
    }

    private DataSet GetFilteredData(string menuType)
    {
        ListedDR LstDoc = new ListedDR();
        return LstDoc.GetFilteredMenuData(menuType, div_code);

    }


    protected void grdmenu_RowEditing(object sender, GridViewCancelEditEventArgs e)
    {

    }
    protected void grdmenu_RowCommand1(object sender, GridViewCancelEditEventArgs e)
    {
        // Add your RowCancelingEdit logic here
    }
    protected void grdmenu_PageIndexChanging1(object sender, GridViewCancelEditEventArgs e)
    {
        // Add your RowCancelingEdit logic here
    }



    protected void grdmenu_RowCancelingEdit(object sender, GridViewCancelEditEventArgs e)
    {
        // Add your RowCancelingEdit logic here
    }

    protected void grdmenu_RowDataBound(object sender, GridViewRowEventArgs e)
    {
        // Add your RowDataBound logic here
    }


    protected void grdmenu_PageIndexChanging(object sender, GridViewPageEventArgs e)
    {
        /// grdmenu.PageIndex = e.NewPageIndex;
        // BindGridView(); // Rebind the GridView
    }


    [System.Web.Services.WebMethod]
    public static string DeactivateRow(string FK_PK_Id, string OptionMenu_Id)
    {
        string connectionString = WebConfigurationManager.ConnectionStrings["Ereportcon"].ConnectionString;

        using (SqlConnection con = new SqlConnection(connectionString))
        {
            try
            {
                con.Open();

               
                string query = "UPDATE Tbl_DynamicOptionMenuCreation " +
                               "SET Is_Active = 1 " +  
                               "WHERE FK_PK_Id = '" + FK_PK_Id + "' AND OptionMenu_Id = '" + OptionMenu_Id + "'";

             
                SqlCommand cmd = new SqlCommand(query, con);

              
                cmd.ExecuteNonQuery();

                return "{\"status\":\"success\", \"message\":\" Deactivated Successfully\"}";
            }
            catch (Exception ex)
            {
             
                return "{\"status\":\"error\", \"message\": \"" + ex.Message + "\"}";
            }
        }
    }

}
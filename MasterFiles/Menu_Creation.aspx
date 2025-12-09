<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Menu_Creation.aspx.cs" Inherits="MasterFiles_Menu_Creation" EnableViewState="true" %>
<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MGR_Menu.ascx" TagName="Menu1" TagPrefix="ucl1" %>
<%@ Register Src="~/UserControl/MR_Menu.ascx" TagName="Menu1" TagPrefix="ucl2" %>

<!DOCTYPE html>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Menu Creation</title>
     <style type="text/css">
        .padding {
            padding: 3px;
        }

        .chkboxLocation label {
            padding-left: 5px;
        }

        td.stylespc {
            padding-bottom: 5px;
            padding-right: 5px;
        }
        /*body {
            background-color: #e8ebec !important;
        }*/
    </style>
    <%--<link type="text/css" rel="stylesheet" href="../css/style.css" />--%>
    <style type="text/css">
        .resize-input {
            width: 40% !important;
            padding: 20px;
            font-size: 14px;
        }

        .file-upload {
            width: 200px;
            font-size: 12px;
        }


        .savebutton {
            padding: 20px 20px;
            background-color: #007bff;
            color: white;
            border: none;
            cursor: pointer;
            border-radius: 5px;
            text-align: center;
            width: 280px;
        }

            .savebutton:hover {
                background-color: #0056b3;
            }




        .form-container {
            display: flex;
            gap: 30px;
            justify-content: flex-start;
        }

        .form-content {
            width: 40%;
        }

        .grid-content {
            width: 55%;
        }


        .table {
            width: 100%;
            text-align: center;
            font-family: Arial;
            font-size: 10pt;
        }

        .single-des {
            display: flex;
            justify-content: space-between;
            margin-bottom: 15px;
            gap: 10px;
        }

        .input {
            width: 50% !important;
            padding: 8px;
            font-size: 14px;
            margin-right: 10px;
        }

        .label {
            font-weight: bold;
            margin-right: 5px;
        }

        .center {
            text-align: left;
            width: 100%;
            display: flex;
            justify-content: flex-start;
            align-items: center;
            margin-left: 0;
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
        }

        th {
            background-color: #f4f4f4;
            padding: 10px;
            text-align: center;
            border: 1px solid #ddd;
            font-size: 14px;
            vertical-align: middle;
            padding-left: 55px;
        }


        td {
            padding: 8px;
            text-align: center;
            border: 1px solid #ddd;
            font-weight: normal;
            font-size: 14px;
        }

        .delete-btn {
            background-color: #dc3545;
        }

            .delete-btn:hover {
                background-color: #c82333;
            }


        table, th, td {
            border: 1px solid #ddd;
            border-radius: 5px;
        }

        .file-upload {
            padding-left: 20px; /* Adjust the value as needed */
        }
    </style>
</head>
<script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
<script type="text/javascript">
    var editingRow = null;
   function AddOrUpdateMenuItem() {
    var menuName = document.getElementById('TextBox6').value.trim();
    var path = document.getElementById('<%= ddlmenupath.ClientID %>').value.trim();
    var orderBy = document.getElementById('<%= ddlOrderby.ClientID %>').value.trim();

    // Check if fields are empty and show alerts if needed
    if (menuName === "") {
        alert("Menu Name is required!");
        return;
    }

    if (path === "" || path === "0") {
        alert("Menu Path is required!");
        return;
    }

    if (orderBy === "" || orderBy === "0") {
        alert("Order By is required!");
        return;
    }

    // Perform other logic like adding/updating rows...
    if (editingRow) {
        UpdateRow(editingRow, menuName, path, orderBy);
    } else {
        AddNewRow(menuName, path, orderBy);
    }
 

    // Reset inputs after the action is performed
    document.getElementById('TextBox6').value = "";
    var ddlmenupath = document.getElementById('<%= ddlmenupath.ClientID %>');
    ddlmenupath.value = "0";  // Reset the dropdown
    var ddlOrderby = document.getElementById('<%= ddlOrderby.ClientID %>');
    ddlOrderby.value = "0";  // Reset the dropdown

    // Reinitialize the niceSelect dropdowns if needed
    var niceSelectWrapperPath = $(ddlmenupath).next('.nice-select');
    if (niceSelectWrapperPath.length > 0) {
        niceSelectWrapperPath.remove();
    }
    $(ddlmenupath).niceSelect();
    
    var niceSelectWrapperOrderby = $(ddlOrderby).next('.nice-select');
    if (niceSelectWrapperOrderby.length > 0) {
        niceSelectWrapperOrderby.remove();
    }
    $(ddlOrderby).niceSelect();

    // Reset save button text to "Add"
    document.getElementById('saveBtn').textContent = "Add";

    // Reset editingRow to null
    editingRow = null;
}
    function AddNewRow(menuName, path, orderBy, FK_PK_Id, OptionMenu_Id) {
    var table = document.getElementById('menuTable');
    var tableBody = table.getElementsByTagName('tbody')[0];
    var rows = tableBody.getElementsByTagName('tr');

   
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        var existingMenuName = row.cells[1].textContent.trim();
        if (existingMenuName === menuName) {
            alert('Menu name already exists!');
            return;
        }
    }

    var ddlmenupath = document.getElementById('<%= ddlmenupath.ClientID %>');
    var selectedPath = ddlmenupath.value.trim();

   
    if (selectedPath === "precallanalysis") {
        path = "MasterFiles/Dashboard_PreCall_Analysis.aspx";
    } else if (selectedPath === "orderbooking") {
        path = "MasterFiles/Order_booking.aspx";
    } else {
        path = "";  
    }


    var newRow = document.createElement('tr');
    
   
    var serialCell = document.createElement('td');
    serialCell.textContent = rows.length;
    newRow.appendChild(serialCell);

   
    var menuNameCell = document.createElement('td');
    menuNameCell.textContent = menuName;
    newRow.appendChild(menuNameCell);

   
    newRow.setAttribute('data-fkpkid', FK_PK_Id);
    newRow.setAttribute('data-optionmenuid', OptionMenu_Id);
    newRow.setAttribute('data-menuName', menuName);
    newRow.setAttribute('data-path', path);
    newRow.setAttribute('data-orderby', orderBy);

    
    var editCell = document.createElement('td');
    var editLink = document.createElement('a');
    editLink.href = "#";
    editLink.textContent = "Edit";
    editLink.classList.add("link-btn");
    editLink.onclick = function () { EditRow(newRow); };
    editCell.appendChild(editLink);
    newRow.appendChild(editCell);

   
    var deactivateCell = document.createElement('td');
    var deactivateLink = document.createElement('a');
    deactivateLink.href = "#";
    deactivateLink.textContent = "Deactivate";
    deactivateLink.classList.add("link-btn");

    deactivateLink.addEventListener("click", function (e) {
        e.preventDefault();
        DeactivateRow(newRow);
    });

    deactivateCell.appendChild(deactivateLink);
    newRow.appendChild(deactivateCell);

    
    tableBody.appendChild(newRow);

   
    updateSerialNumbers();
    updateHiddenField();
}

   function updateSerialNumbers() {
    console.log('Updating serial numbers...');  
    var table = document.getElementById('menuTable');
    var tableBody = table.getElementsByTagName('tbody')[0];
    var rows = tableBody.getElementsByTagName('tr');
    for (var i = 0; i < rows.length; i++) {
        var row = rows[i];
        if (row.cells.length > 0) {
            row.cells[0].textContent = i + 1; 
        }
    }
}
    function EditRow(optionMenuName) {

        alert("Edit item: " + optionMenuName);
    }

    function DeactivateRow(optionMenuName) {

        alert("Deactivate item: " + optionMenuName);
    }

    function updateHiddenField() {
        var tableData = [];
        var rows = document.getElementById('menuTable').getElementsByTagName('tr');
        for (var i = 1; i < rows.length; i++) {
            var row = rows[i];
            var cells = row.getElementsByTagName('td');
            var path = row.getAttribute('data-path');
            var orderby = row.getAttribute('data-orderby');
            var rowData = {
                menuName: cells[1].textContent,
                path: path,
                orderby: orderby
            };

            tableData.push(rowData);
        }

        document.getElementById('<%= hfTableData.ClientID %>').value = JSON.stringify(tableData);
    }
    function EditRow(optionMenuName) {

        alert("Edit item: " + optionMenuName);
    }

    function DeactivateRow(optionMenuName) {

        alert("Deactivate item: " + optionMenuName);
    }
    function EditRow(row) {
    var menuName = row.getElementsByTagName('td')[1].textContent.trim();
    var path = row.getAttribute('data-path');
    var orderBy = row.getAttribute('data-orderby');
    document.getElementById('TextBox6').value = menuName;

    var ddlmenupath = document.getElementById('<%= ddlmenupath.ClientID %>');
    
   
    if (path === "MasterFiles/Dashboard_PreCall_Analysis.aspx") {
        ddlmenupath.value = "precallanalysis";
    } else if (path === "MasterFiles/Order_booking.aspx") {
        ddlmenupath.value = "orderbooking";
    } else {
        ddlmenupath.value = "0";  
    }

  
    var niceSelectWrapper = $(ddlmenupath).next('.nice-select');
    if (niceSelectWrapper.length > 0) {
        niceSelectWrapper.remove();  
    }
    $(ddlmenupath).niceSelect(); 

  
    var ddlOrderby = document.getElementById('<%= ddlOrderby.ClientID %>');
    ddlOrderby.value = orderBy;

 
    niceSelectWrapper = $(ddlOrderby).next('.nice-select');
    if (niceSelectWrapper.length > 0) {
        niceSelectWrapper.remove();
    }
    $(ddlOrderby).niceSelect();  

   
    

 
        editingRow = row;
       
}

    function UpdateRow(row, updatedMenuName, selectedPath, updatedOrderBy) {
    // Determine the path based on the selected path dropdown value
    var updatedPath = "";
    if (selectedPath === "precallanalysis") {
        updatedPath = "MasterFiles/Dashboard_PreCall_Analysis.aspx";
    } else if (selectedPath === "orderbooking") {
        updatedPath = "MasterFiles/Order_booking.aspx";
    } else {
        updatedPath = ""; // Fallback if the path doesn't match
    }

    // Update the table cell with the updated menu name
    row.getElementsByTagName('td')[1].textContent = updatedMenuName;

    // Update the row's data attributes with the new values
    row.setAttribute('data-menuName', updatedMenuName);
    row.setAttribute('data-path', updatedPath);
    row.setAttribute('data-orderby', updatedOrderBy);

    // Re-index the rows (e.g., for order or numbering)
    var tableBody = row.parentNode;
    for (var i = 0; i < tableBody.rows.length; i++) {
        tableBody.rows[i].cells[0].textContent = i + 1;
    }

    // Optionally, you can update any hidden fields if required
    updateHiddenField();
}
    function serializeMenuTable() {
        var table = document.getElementById('menuTable');
        var rows = table.getElementsByTagName('tr');
        var tableData = [];

        for (var i = 1; i < rows.length; i++) {
            var row = rows[i];
            var cells = row.getElementsByTagName('td');

            if (cells.length >= 3) {
                var menuName = cells[1].textContent;
                var path = cells[2].textContent;
                var orderBy = cells[3].textContent;

                tableData.push({
                    menuName: menuName,
                    path: path,
                    orderby: orderBy
                });
            }
        }
        document.getElementById('hfTableData').value = JSON.stringify(tableData);
    }
    function DeactivateRow(row) {
        var FK_PK_Id = row.getAttribute('data-fkpkid');
        var OptionMenu_Id = row.getAttribute('data-optionmenuid');

        $.ajax({
            type: 'POST',
            url: "Menu_Creation.aspx/DeactivateRow",
            contentType: "application/json; charset=utf-8",
            dataType: "json",
            data: JSON.stringify({
                FK_PK_Id: FK_PK_Id,
                OptionMenu_Id: OptionMenu_Id
            }),
            success: function (response) {
                console.log("Response from server:", response);
                var respond = response.d ? JSON.parse(response.d) : response;

                console.log(respond);
                if (respond.status === 'success') {
                    alert('Row deactivated successfully!');


                    row.parentNode.removeChild(row);
                } else {
                    alert('Failed to deactivate the row. Error: ' + respond[0].message);
                }
            },
            error: function (xhr, ErrorText, thrownError) {
                console.error('AJAX Error:', ErrorText);
            }
        });
    }

</script>

<script type="text/javascript">
    window.onload = function () {
        var saveButton = document.getElementById('saveBtn');
        var dropdown = document.getElementById('<%= ddltype.ClientID %>');

        saveButton.style.display = 'none';

        toggleSaveButton(dropdown.value);
    };

    function toggleSaveButton(value) {
        var saveButton = document.getElementById('saveBtn');
        if (value === 'R') {
            saveButton.style.display = 'none';
        } else if (value === 'M') {
            saveButton.style.display = 'inline-block';
        } else {
            saveButton.style.display = 'inline-block';
        }
    }
</script>
<script type="text/javascript">

    function showMenuTable() {
        document.getElementById("menuTable").style.display = "table";
    }
    function hideMenuTable() {
        document.getElementById("menuTable").style.display = "none";
    }
</script>
<script type="text/javascript">
    function handleDropdownChange() {
        var dropdown = document.getElementById("ddltype");
        var saveBtn = document.getElementById("saveBtn");

        if (dropdown.value === "M") {
            saveBtn.style.display = "block";
        } else if (dropdown.value === "R") {
            saveBtn.style.display = "none";
        }
    }
    window.onload = function () {
        document.getElementById("saveBtn").style.display = "none";
        document.getElementById("ddltype").addEventListener("change", handleDropdownChange);
        handleDropdownChange();
    };
    </script>


<body>
    <form id="form1" runat="server">
        <div id="Divid" runat="server"></div>
        <h2 class="text-center heading" id="h2" runat="server">Menu Creation</h2>

        <div class="container home-section-main-body position-relative clearfix">

            <div class="form-container">

                <div class="single-des clearfix">
                    <asp:Label ID="menutype" runat="server" CssClass="label">
        Menu Type<span style="color: Red; padding-left: 10px;">*</span>
                    </asp:Label>
                    <asp:DropDownList
                        ID="ddltype"
                        runat="server"
                        SkinID="ddlRequired"
                        CssClass="input resize-input"
                        OnSelectedIndexChanged="ddltype_SelectedIndexChanged"
                        AutoPostBack="true"
                        onchange="toggleSaveButton(this.value)">
                        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                        <asp:ListItem Value="R" Text="Report"></asp:ListItem>
                        <asp:ListItem Value="M" Text="Menu"></asp:ListItem>
                    </asp:DropDownList>


                    <asp:Label ID="Label5" runat="server" CssClass="label">
        Menu Name<span style="color: Red; padding-left: 5px;">*</span>
                    </asp:Label>
                    <asp:TextBox ID="menuid" runat="server" CssClass="input" Width="150%" onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="6"></asp:TextBox>

                    <asp:Label ID="Label9" runat="server" CssClass="label">
        Path<span style="color: Red; padding-left: 10px;">*</span>
    </asp:Label>
    <asp:DropDownList
        ID="ddlpath"
        runat="server"
        SkinID="ddlRequired"
        CssClass="input resize-input">
       
        <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
        <asp:ListItem Value="precallanalysis" Text="Precall Analysis"></asp:ListItem>
        <asp:ListItem Value="orderbooking" Text="Order Booking"></asp:ListItem>
    </asp:DropDownList>

                     <tr>
    <td align="center">
        <asp:Label ID="lblExcel" runat="server" CssClass="label">
            Logo<span style="color: Red; padding-left:1px;">*</span>
        </asp:Label>
        <asp:FileUpload ID="FlUploadcsv" runat="server" CssClass="file-upload" Enabled="false" />
    </td>
</tr>

                    <asp:Button ID="btnsave" runat="server" Width="100px" Text="Save" CssClass="savebutton" OnClick="btnSave_Click" />
                      <asp:Button ID="clearbutton" runat="server" Width="100px" Text="Clear" CssClass="savebutton" OnClick="btnclear_Click" />
                    <asp:Button ID="btnUpdate" runat="server" Width="100px" Text="Update" CssClass="savebutton" OnClick="btnUpdate_Click" Visible="false" />
                </div>

            </div>



            <div class="form-container">
                <div class="grid-content" style="flex: 1;">

                    <h1 id="menuItemHeading" runat="server" style="font-size: 20px;">Menu Item</h1>


                    <table id="menuTable" runat="server">
                        <thead>
                            <tr>
                                <th>S.No</th>
                                <th>Menu Name</th>
                                <th>Edit</th>
                                <th>Deactivate</th>
                            </tr>
                        </thead>

                        <tbody id="menuTableBody">
                            <!-- Rows will be added here dynamically using JavaScript -->
                        </tbody>

                    </table>
                    <asp:HiddenField ID="hfTableData" runat="server" />
                </div>

                <div class="form-content" style="flex: 1;">
                    <h1 id="menuItemCreationHeading" runat="server" style="font-size: 20px;">Menu_Item Creation</h1>

                    <div class="single-des clearfix">
                        <asp:Label ID="Label6" runat="server" CssClass="label">
            Name<span style="color: Red; padding-left: 5px;">*</span>
                        </asp:Label>
                        <asp:TextBox ID="TextBox6" runat="server" CssClass="input" Width="100%" onkeypress="AlphaNumeric_NoSpecialChars(event);" TabIndex="6"></asp:TextBox>
                    </div>

                     <div class="single-des clearfix">
    <asp:Label ID="Label7" runat="server" CssClass="label">
        Path<span style="color: Red; padding-left: 10px;">*</span>
    </asp:Label>
    <asp:DropDownList
        ID="ddlmenupath"
        runat="server"
        SkinID="ddlRequired"
        CssClass="input resize-input">
        <asp:ListItem Value="0" Text="--Select--" />
        <asp:ListItem Value="precallanalysis" Text="Precall Analysis" />
        <asp:ListItem Value="orderbooking" Text="Order Booking" />
        
    </asp:DropDownList>
</div>

                    <div class="single-des clearfix">
                        <asp:Label ID="Label8" runat="server" CssClass="label">
        Order By<span style="color: Red; padding-left: 5px;">*</span>
                        </asp:Label>
                        <asp:DropDownList ID="ddlOrderby" TabIndex="4" runat="server" SkinID="ddlRequired" CssClass="input resize-input">
                            <asp:ListItem Value="0" Text="--Select--"></asp:ListItem>
                            <asp:ListItem Value="1" Text="1"></asp:ListItem>
                            <asp:ListItem Value="2" Text="2"></asp:ListItem>
                            <asp:ListItem Value="3" Text="3"></asp:ListItem>
                            <asp:ListItem Value="4" Text="4"></asp:ListItem>
                            <asp:ListItem Value="5" Text="5"></asp:ListItem>
                            <asp:ListItem Value="6" Text="6"></asp:ListItem>
                            <asp:ListItem Value="7" Text="7"></asp:ListItem>
                            <asp:ListItem Value="8" Text="8"></asp:ListItem>
                            <asp:ListItem Value="9" Text="9"></asp:ListItem>
                            <asp:ListItem Value="10" Text="10"></asp:ListItem>
                            <asp:ListItem Value="11" Text="11"></asp:ListItem>
                            <asp:ListItem Value="12" Text="12"></asp:ListItem>
                            <asp:ListItem Value="13" Text="13"></asp:ListItem>
                            <asp:ListItem Value="14" Text="14"></asp:ListItem>
                            <asp:ListItem Value="15" Text="15"></asp:ListItem>
                            <asp:ListItem Value="16" Text="16"></asp:ListItem>
                            <asp:ListItem Value="17" Text="17"></asp:ListItem>
                            <asp:ListItem Value="18" Text="18"></asp:ListItem>
                            <asp:ListItem Value="19" Text="19"></asp:ListItem>
                            <asp:ListItem Value="20" Text="20"></asp:ListItem>
                            <asp:ListItem Value="21" Text="21"></asp:ListItem>
                            <asp:ListItem Value="22" Text="22"></asp:ListItem>
                            <asp:ListItem Value="23" Text="23"></asp:ListItem>
                            <asp:ListItem Value="24" Text="24"></asp:ListItem>
                            <asp:ListItem Value="25" Text="25"></asp:ListItem>
                        </asp:DropDownList>
                    </div>




                    <div class="single-des clearfix move-right">
                        <asp:Label ID="Label1" runat="server" CssClass="label">
        Logo<span style="color: Red; padding-left:1px;">*</span>
                        </asp:Label>
                        <asp:FileUpload ID="FileUpload1" runat="server" CssClass="file-upload" Style="margin-left: 20px;" Enabled="false"/>
                        <%--<asp:Button ID="Button8" runat="server" Width="70px" Text="Add" CssClass="savebutton" OnClientClick="AddMenuItem(); return false;" />--%>
                        <!-- Use a button with type="button" to avoid form submission -->
                        <button type="button" id="saveBtn" class="savebutton" style="width: 70px;" onclick="AddOrUpdateMenuItem()">Add</button>



                    </div>

                    <!-- Add button -->

                </div>


            </div>
            <div style="display: flex; justify-content: center; align-items: center; padding-top: 20px;">
                <!-- Save Button -->
                <asp:Button ID="Button9" runat="server" Width="60px" Text="Save" CssClass="savebutton" OnClick="btnSubmit_Click" />

                <!-- Update Button -->
                <asp:Button ID="Button10" runat="server" Width="60px" Text="Update" CssClass="savebutton" OnClick="btnmenuupdate_Click"/>
            </div>

            <asp:GridView ID="GridView1" runat="server" Width="100%" HorizontalAlign="Center"
                EmptyDataText="No Records Found" AutoGenerateColumns="false" AllowPaging="True"
                PageSize="10" DataKeyNames="PK_Id"
                OnRowEditing="grdmenu_RowEditing1"
                OnRowCancelingEdit="grdmenu_RowCancelingEdit"
                OnRowDataBound="grdmenu_RowDataBound"
                OnRowCommand="grdmenu_RowCommand"
                OnPageIndexChanging="grdmenu_PageIndexChanging"
                GridLines="Both" CssClass="table" PagerStyle-CssClass="gridview1"
                AlternatingRowStyle-CssClass="alt" AllowSorting="True" Visible="true">
                <Columns>
                    <asp:TemplateField HeaderText="S.No" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lblSNo" runat="server" Text='<%# ((GridViewRow)Container).RowIndex + 1 %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False">
                        <ItemTemplate>
                            <asp:HiddenField ID="hfId1" runat="server" Value='<%# Eval("PK_Id") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Menu Type" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lbl_Type" runat="server" Text='<%# Bind("Menu_Type") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Menu Name" ItemStyle-HorizontalAlign="Center" ItemStyle-Width="30%">
                        <ItemTemplate>
                            <asp:Label ID="lbl_MenuName" runat="server" Text='<%# Bind("Menu_Name") %>'></asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Edit">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkEdit" runat="server" CommandName="Edit" CommandArgument='<%# Eval("PK_Id") %>'>Edit</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>

                    <asp:TemplateField HeaderText="Deactivate" ItemStyle-HorizontalAlign="Center">
                        <ItemTemplate>
                            <asp:LinkButton ID="lnkbutDeactivate" runat="server" CommandArgument='<%# Eval("PK_Id") %>'
                                CommandName="Deactivate">Deactivate</asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>

    </form>
</body>
</html>

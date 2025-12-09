<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Listeddr_BulkUpload_Dynamic_BK.aspx.cs" Inherits="MasterFiles_Options_Listeddr_BulkUpload_Dynamic_Bk" %>

<%@ Register Src="~/UserControl/MenuUserControl.ascx" TagName="Menu" TagPrefix="ucl" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
       
    <title>Listed Doctor Upload Tool</title>
    <style type="text/css">
        .modal {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
            
        }
.curser{
     cursor: not-allowed;
    color: gray !important;
    background: transparent !important;
    text-decoration: none !important;

}
        .loading {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }

        #divdr {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 130px;
        }

        #detail {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 130px;
            padding: 2px;
        }

        #divcat {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 90px;
        }

        #detailcat {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 90px;
            padding: 2px;
        }

        #divTerr {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }

        #detailTerr {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }

        #divcls {
            font-family: Verdana;
            font-size: small;
            font-weight: bold;
            padding: 2px;
            border: solid 1px;
            width: 120px;
        }

        #detailCls {
            font-family: Verdana;
            font-size: Smaller;
            border: solid 1px;
            width: 120px;
            padding: 2px;
        }
          #CblDoctorCode tr td .border {
            border: 0px solid #dee2e6 !important;
        }
           .border1 {
            text-decoration: line-through Red;
        }
           
#CblDoctorCode [type="checkbox"]:disabled + label {
    color: #6c757d;
    cursor: not-allowed;
}


[type="checkbox"]:not(:checked) + label, [type="checkbox"]:checked + label {
    position: relative;
    padding-left: 2.15em;
    cursor: pointer;
    vertical-align: top;
    line-height: 20px;
    margin: 2px 0;
    display: block;
}
#CblDoctorCode [type="checkbox"]:disabled:checked + label:after {
    color: #de350a;
    cursor: not-allowed;
}


#CblDoctorCode [type="checkbox"]:not(:checked) + label:after, [type="checkbox"]:checked + label:after {
    content: '';
    position: absolute;
    left: 0;
    top: 0;
    width: 1.4em;
    height: 1.4em;
    border: 4px solid #ccc;
    background: #de350a;
    border-radius: 4px;
    box-shadow: inset 0 1px 3px rgb(0 0 0 / 10%);
    margin-left: 3px;
    /* color: #09ad7e; */
}
#CblDoctorCode [type="checkbox"]:disabled:not(:checked) + label:before {
    box-shadow: none;
    border-color: #bbb;

}
#CblDoctorCode  [type="checkbox"]:disabled:checked + label:after {
    background: #de350a;
    color: #de350a;

}
#btnGen.savebutton.disabled {
  opacity: 0.65; 
  cursor: not-allowed;
}
input[type="submit"]:disabled {
    cursor: not-allowed;
    color:black;
    background:lightgray;
}
input[type="file"]:disabled {
    cursor: not-allowed;
    color:black;
    background:lightgray;
}
a.disabled {
  text-decoration: line-through Red;

}
.lnkDelete[disabled="disabled"]
{
text-decoration: line-through Red; 
}
    </style>
    <script src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.4/jquery.min.js"></script>
    <script type="text/javascript">
    $(document).ready(function () {

        $("#btnGen").submit(function (e) {

            e.preventDefault();          
            $("#btnGen").attr("disabled", true);           
            $("#CblDoctorCode").attr("disabled", true);

            return true;

        });
    });
</script>
    <style type="text/css">
    [data-title]:hover:after {
    opacity: 1;
    transition: all 0.1s ease 0.5s;
    visibility: visible;
}
/* box for title text */
[data-title]:after {
    content: attr(data-title);
  	/* position for title view box */
  	position: absolute;
  	
    left: 100%;
    z-index: 99999;
    visibility: hidden;
    /* optional */
    white-space: nowrap;
  	/* stilization */
    background-color: lightgoldenrodyellow;
    color: #111;
    font-size: 80%;
    padding: 2px 2px 2px 2px;
    box-shadow: 1px 1px 3px #222222;
    opacity: 0;
    border: 1px solid #111111;
}
[data-title] {
    position: relative;
}
        </style>
      <script type="text/javascript" language="javascript">
    var isReload = false;
    function DownloadFiles()
    {
        if(isReload == true)
        {
            isReload = false;
            window.location.reload();     
        }
        else
        {
            isReload = true;
        }
        window.setTimeout("DownloadFiles()", 1000);
    }
    </script>
    <script type="text/javascript" src="../JsFiles/CommonValidation.js"></script>
    <script type="text/javascript" src="../JsFiles/jquery-1.10.1.js"></script>
    <script type="text/javascript">

        $(document).ready(function () {
            document.getElementById('<%= GrdFixation.ClientID %>').style.display = 'none';
        });
        </script>
</head>
<body>
    <form id="form1" runat="server">
        <ucl:Menu ID="menu1" runat="server" />
        <br />
        <div class="container home-section-main-body position-relative clearfix">
            <div class="row justify-content-center">
                <div class="col-lg-12">
                   <table>
                    <h2 class="text-center" id="hHeading" runat="server"></h2>
                    <tr>
                          <td><a href="Speciality_Category_Name.aspx" target="_new" > <font color="red" size="3" >Speciality / Category </font></a></td>
                            <td width="800"></td>
                           <td> <a href="Video_Help.aspx" target="_new" > <font color="red" size="3" >? Video Help </font></a> </td>
                    </tr>
                    </table>
                    <div class="row justify-content-center">
                        <div class="col-lg-12">
                            <div class="designation-area clearfix">
                                 <div class="row justify-content-center">
                                    <asp:Label ID="lblTitle" runat="server" Text="Select the Parameter to Upload" ForeColor="#696d6e" Font-Bold="true"
                                        TabIndex="6">
                                    </asp:Label>
                                </div>
                                <br />
                                 <center>
                                  
                                        <%--<div class="row justify-content-center" style="overflow-x: auto; padding-bottom: 20px; margin-left: -45px; margin-right: -45px;">--%>
                                        <asp:CheckBoxList ID="CblDoctorCode" runat="server" CellPadding="5" 
                                            RepeatColumns="6" RepeatDirection="Horizontal">
                                              <asp:ListItem Value="Sl_No" Selected="True"  Enabled="false">Sl No</asp:ListItem>
                                            <asp:ListItem Value="Sf_Code" Selected="True" Enabled="false">User Name</asp:ListItem>
                                            <asp:ListItem Value="ListedDr_Name" Selected="True" Enabled="false">Listed Doctor Name</asp:ListItem>
                                            <asp:ListItem Value="Territory_Code" Selected="True" Enabled="false">Territory/Cluster(For DCR)</asp:ListItem>
                                            <asp:ListItem Value="Cluster_Name" Selected="True" Enabled="false">City Name(For Expense)</asp:ListItem>
                                                 <asp:ListItem Value="Doc_Special_Code" Selected="True" Enabled="false">Speciality</asp:ListItem>
                                                 <asp:ListItem Value="Doc_Cat_Code" Selected="True" Enabled="false">Category</asp:ListItem>
                                            <asp:ListItem Value="Doc_QuaCode">Qualification</asp:ListItem>
                                             <asp:ListItem Value="Doc_ClsCode">Class</asp:ListItem>
                                               <asp:ListItem Value="Territory_Cat">Territory Type</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_Address1">Address</asp:ListItem>
                                              <asp:ListItem Value="ListedDr_Hospital">Hospital Name</asp:ListItem>
                                              <asp:ListItem Value="Hospital_Address">Hospital Address</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_DOB">DOB(DD/MM/YY)</asp:ListItem>
                                            <asp:ListItem Value="ListedDR_DOW">DOW(DD/MM/YY)</asp:ListItem>
                                               <asp:ListItem Value="ListedDR_EMail">EMail ID</asp:ListItem>
                                               <asp:ListItem Value="ListedDR_Phone">Phone No</asp:ListItem>
                                             <asp:ListItem Value="ListedDR_Mobile">Mobile No</asp:ListItem>
                                             <asp:ListItem Value="ListedDr_Sex">Gender</asp:ListItem>
                                            <asp:ListItem Value="No_of_Visit">No of Visit</asp:ListItem>
                                            <asp:ListItem Value="State_Code">State</asp:ListItem>
                                         <asp:ListItem Value="ListedDr_Fax">Fax</asp:ListItem>
                                          <asp:ListItem Value="ListedDr_website">Website</asp:ListItem>
                                           
                                            <asp:ListItem Value="ListedDr_PinCode">Pin Code</asp:ListItem>
                                              <asp:ListItem Value="Dr_Business_Value">Doctor Business Value</asp:ListItem>
                                             <asp:ListItem Value="Dr_Potential">Expected Business Value</asp:ListItem>
                                            <asp:ListItem Value="visit_Session">Product Code(p1/p2/p3)</asp:ListItem>
                                            <asp:ListItem Value="Hospital_Country">Country</asp:ListItem>
                                               <asp:ListItem Value="Hospital_State">Hospital State</asp:ListItem>
                                            <asp:ListItem Value="Day_1">DAY1</asp:ListItem>
                                              <asp:ListItem Value="Day_2">DAY2</asp:ListItem>
                                              <asp:ListItem Value="Day_3">DAY3</asp:ListItem>
                                            
                                            <asp:ListItem Value="Geo_Tag_Count">Geo Tag Count</asp:ListItem>
                                            <asp:ListItem Value="Unique_Dr_Code">Unique Code</asp:ListItem>
                                            <asp:ListItem Value="ListedDr_RegNo">Reg No</asp:ListItem>
                                            <asp:ListItem Value="ListedDr_Avg_Patients">Avg. Patient/day</asp:ListItem>
                                            <asp:ListItem Value="ListedDr_Visit_Days">Visiting Days(Sun/Mon/)</asp:ListItem>
                                             <asp:ListItem Value="Other1">Others 1</asp:ListItem>
                                             <asp:ListItem Value="Other2">Others 2</asp:ListItem>
                                               <asp:ListItem Value="Other3">Others 3</asp:ListItem>
                                        </asp:CheckBoxList>
                                    
                                </center>
                                </div>
                              </div>
                         <div class="w-100 designation-submit-button text-center clearfix">
                                    <br />
                                    <asp:Button ID="btnGen" runat="server" OnClientClick="DownloadFiles();"  CssClass="savebutton" Width="120px" Text="Generate Excel" OnClick="btnGen_Click"  />

                             &nbsp;&nbsp;
                               <asp:LinkButton ID="lnkDelete" runat="server" Text="Delete and Generate New Excel" OnClick="lnkDelete_Click" > 
                                    </asp:LinkButton>
                           
                                </div>
                        
                                <div class="single-des clearfix " >
                                    <br />
                     
                                    <asp:Label ID="lblExcel" runat="server" CssClass="label">Excel file</asp:Label>
                                    <asp:FileUpload ID="FlUploadcsv" runat="server" CssClass="input" Enabled="False" />
                                        <asp:CheckBox ID="chkDeact" runat="server" ForeColor="Red" Font-Size="12px" Font-Names="Verdana"
                                        Text="Deactivate Existing Doctor List ( if Yes then Check this Option )"
                                        OnCheckedChanged="chkDeact_CheckedChanged" />
                                </div>
                        <br />
                              
                                <div class="w-100 designation-submit-button text-center clearfix">
                                  
                                    <asp:Button ID="btnUpload" runat="server" CssClass="savebutton" Text="Upload" OnClick="btnUpload_Click" />
                                </div>
                               
                                <div class="single-des clearfix">
                                    <asp:Label ID="lblExc" runat="server" CssClass="label">Excel Format File</asp:Label>
                                    <asp:LinkButton ID="lnkDownload" runat="server" Text="Download Here" OnClick="lnkDownload_Click"> 
                                    </asp:LinkButton>
                                </div>
                            
                    <br />
                      
                     
                    </div>
        
                    
                                    <asp:Panel ID="pnlDr" runat="server" Visible="false">
                                        <center>
                                            <img src="../../Images/arrowdown1.gif" height="80px" alt="" />
                                            <h2 style="color: Red; font-weight: bold; font-size: x-large">Not Uploaded List</h2>
                                             <div class="single-des clearfix">
                                                                                             <asp:GridView ID="GrdFixation" runat="server"  Width="100%" OnRowDataBound="GrdFixation_RowDataBound"
                                                CssClass="table" HeaderStyle-ForeColor="White" HeaderStyle-BackColor="Teal" PagerStyle-CssClass="gridview1" AlternatingRowStyle-CssClass="alt">

                                            <Columns>
                                            </Columns>
                                            <EmptyDataRowStyle CssClass="no-result-area" />
                                        </asp:GridView>

                                <asp:Label ID="Label2" runat="server" Text="Download Not Uploaded List" CssClass="label"></asp:Label>
                                <asp:LinkButton ID="lnlnot" runat="server"
                                    Text="Download Here" OnClick="lnlnot_Click"> 
                                </asp:LinkButton>
                            </div>

                                            </center>
                                        </asp:Panel>


                </div>
            </div>
    
        <div class="loading" align="center">
            Loading. Please wait.<br />
            <br />
            <img src="../../Images/loader.gif" alt="" />
        </div>
    </form>
</body>
</html>

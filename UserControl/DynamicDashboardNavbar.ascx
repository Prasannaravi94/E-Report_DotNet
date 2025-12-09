<%@ Control Language="C#" AutoEventWireup="true" CodeFile="DynamicDashboardNavbar.ascx.cs" Inherits="UserControl_DynamicDashboardNavbar" %>
<nav id="dashboard-navbar" class="navbar fixed-top navbar-expand-lg navbar-dark bg-dark">
    <div class="container">
        <a class="navbar-brand" href="<%= Page.ResolveClientUrl("~/MasterFiles/DynamicDashboard/Dashboard.aspx") %>">Dashboard</a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse"
            data-bs-target="#navbarSupportedContent" aria-controls="navbarSupportedContent" aria-expanded="false"
            aria-label="Toggle navigation">
            <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="navbarSupportedContent">
            <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                <li class="nav-item">
                    <ul class="navbar-nav">
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle dashbaord-module-link" data-name="master_kpi" href="#" role="button"
                                data-bs-toggle="dropdown" aria-expanded="false">Master KPI
                            </a>
                            <ul class="dropdown-menu dropdown-menu-dark kpi-nav-dropdown">
                                <asp:Repeater ID="MasterKpiLinks" runat="server">
                                    <ItemTemplate>
                                        <li><a class="dropdown-item dashbaord-link" data-id="<%# Eval("LinkId") %>" href='<%# Eval("LinkUrl") %>'><%# Eval("LinkText") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle dashbaord-module-link" data-name="marketing_kpi" href="#" role="button"
                                data-bs-toggle="dropdown" aria-expanded="false">Marketing KPI
                            </a>
                            <ul class="dropdown-menu dropdown-menu-dark kpi-nav-dropdown">
                                <asp:Repeater ID="MarketingKpiLinks" runat="server">
                                    <ItemTemplate>
                                        <li><a class="dropdown-item dashbaord-link" data-id="<%# Eval("LinkId") %>" href='<%# Eval("LinkUrl") %>'><%# Eval("LinkText") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                        <li class="nav-item dropdown">
                            <a class="nav-link dropdown-toggle dashbaord-module-link" data-name="sales_kpi" href="#" role="button"
                                data-bs-toggle="dropdown" aria-expanded="false">Sales KPI
                            </a>
                            <ul class="dropdown-menu dropdown-menu-dark kpi-nav-dropdown">
                                <asp:Repeater ID="SalesKpiLinks" runat="server">
                                    <ItemTemplate>
                                        <li><a class="dropdown-item dashbaord-link" data-id="<%# Eval("LinkId") %>" href='<%# Eval("LinkUrl") %>'><%# Eval("LinkText") %></a></li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </li>
                    </ul>
                </li>
            </ul>
            <div class="d-flex">
                <button type="button" class="btn btn-primary" data-bs-toggle="modal" data-id="0" data-bs-target="#dashboardFormModal">
                    <i class="fa-solid fa-plus"></i>&nbsp;New Dashboard
                </button>
                |
                <div class="dropdown ms-3 d-flex align-items-center">
                  <i class="text-light fa-solid fa-circle-user fa-xl"></i>
                  <a class="ps-1 nav-link text-light dropdown-toggle dropdown-toggle-no-arrow " type="button" id="dropdownMenuButton1" data-bs-toggle="dropdown" aria-expanded="false">
                    <%= Session["sf_name"] %>
                  </a>
                  <ul class="dropdown-menu dropdown-menu-dark " aria-labelledby="dropdownMenuButton1">
                    <li><a class="dropdown-item" href="<%= Page.ResolveClientUrl("~/index.aspx") %>"><i class="text-danger fa-solid fa-power-off"></i> Logout</a></li>
                  </ul>
                </div>
            </div>
        </div>
    </div>
</nav>

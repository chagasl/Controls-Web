<%@ Page Title="" Language="C#" MasterPageFile="../../Web.Master" AutoEventWireup="true" CodeBehind="LimitsChart.aspx.cs" Inherits="PlantControl.Views.LimitsChart" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div runat="server" class="LimitsConfigpage">
        <div class="btnGroup" style="padding-left: 10px">
            <asp:DropDownList ID="serverSelect" class="ServerSelect" OnSelectedIndexChanged="serverSelect_SelectedIndexChanged" AutoPostBack="true" runat="server">
                <asp:ListItem Text="" Value="" />
                <asp:ListItem Text="Front Axle" Value="Front_Axle" />
                <asp:ListItem Text="MAN 3rdM" Value="MAN_3rdM" />
                <asp:ListItem Text="MAN Final" Value="MAN_Final" />
                <asp:ListItem Text="MAN WheelHub" Value="MAN WHUB_ASSY" />
                <asp:ListItem Text="RPU 3rdM" Value="RPU_3rdM" />
                <asp:ListItem Text="RPU Final" Value="RPU_Final" />
                <asp:ListItem Text="RPU AxleShaft" Value="RPU_AxleShaft" />
                <asp:ListItem Text="Banjo Welding" Value="Banjo_Welding" />
            </asp:DropDownList>
            <asp:DropDownList ID="stationSelect" class="ServerSelect" AutoPostBack="true" runat="server">
                <asp:ListItem Text="Station" Value="" />
            </asp:DropDownList>
            <button class="button" id="btnShowCharIDAvailable" type="button" onserverclick="btnShowCharIDAvailable_ServerClick" runat="server">AVAILABLE PARAM ID</button>
            <button class="button" id="btnRegisterCharID" type="button" onserverclick="btnRegisterCharID_ServerClick" runat="server">REGISTER PARAM ID</button>
            <div class="lblGroup">
                <label id="lblResults" runat="server"></label>
            </div>
        </div>
    </div>

    <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
    <script src="../../../Scripts/highchart/highcharts.js"></script>
    <script src="../../../Scripts/highchart/highcharts-3d.js"></script>
    <script src="../../../Scripts/highchart/modules/exporting.js"></script>
    <script src="../../../Scripts/highchart/modules/export-data.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

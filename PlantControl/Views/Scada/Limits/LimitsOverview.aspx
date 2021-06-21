<%@ Page Title="" Language="C#" MasterPageFile="../../Web.Master" AutoEventWireup="true" CodeBehind="LimitsOverview.aspx.cs" Inherits="PlantControl.Views.LimitsOverview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <asp:Literal ID="ltrChart" runat="server"></asp:Literal>
    
    <script src="../../../Scripts/highchart/highcharts.js"></script>
    <script src="../../../Scripts/highchart/modules/exporting.js"></script>
    <script src="../../../Scripts/highchart/modules/export-data.js"></script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

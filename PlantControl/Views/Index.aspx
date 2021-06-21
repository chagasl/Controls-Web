<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Web.Master" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="PlantControl.Views.Index" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <style type="text/css">
        /*style for AAM image*/
        .BackImage {
            width: 80%;
            opacity: 1;
            display: block;
            margin-left: auto;
            margin-right: auto;
            margin-top: 35px;
        }
    </style>
    <img src="/Images/DeliveringPower.jpg" alt="Icone" class="BackImage" />

    <div class="footer">
        <p id="footerID" runat="server"></p>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

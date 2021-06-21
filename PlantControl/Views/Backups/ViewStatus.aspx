<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="ViewStatus.aspx.cs" Inherits="PlantControl.Views.ViewStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <%--<meta http-equiv="refresh" content="400">--%>
    <!-- Refresh every 400 seconds -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div class="backups">
        <div class="machiningPercentage">
            <div class="Backupcenter">
                <asp:Label ID="machining" runat="server" />
            </div>
        </div>
        <div class="spacer"></div>
        <div class="assemblyPercentage">
            <div class="Backupcenter">
                <asp:Label ID="assembly1" runat="server" />
            </div>
        </div>
    </div>

    <div style="height:3px"></div>

    <div class="backups">
        <div class="btnMachine">
            <div class="Backupcenter">
                <button class="BtnMaq" id="btnMachining" type="button" runat="server" onserverclick="btnMachining_ServerClick">CHECK USINAGEM</button>
            </div>
        </div>
         <div class="spacer"></div>
        <div class="btnMachine">
            <div class="Backupcenter">
                <button class="BtnMaq" id="btnAssembly" type="button" runat="server" onserverclick="btnAssembly_ServerClick">CHECK MONTAGEM</button>
            </div>
        </div>
    </div>

    <script src="../../../Scripts/highchart/highcharts.js"></script>
    <script src="../../Scripts/highchart/modules/solid-gauge.js"></script>
    <script src="../../Scripts/highchart/highcharts-more.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

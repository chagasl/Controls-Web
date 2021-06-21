<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="MachMachines.aspx.cs" Inherits="PlantControl.Views.MachMachines" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <%--<meta http-equiv="refresh" content="400">--%>
    <!-- Refresh every 400 seconds -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div runat="server" class="AssyMachinepage">
        <div class="btnGroup" style="padding-left: 10px">
            <asp:DropDownList ID="SelectionCell" CssClass="ServerSelect" runat="server" Style="width: 200px;" OnSelectedIndexChanged="SelectionCell_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <button class="button" id="btnBack" type="button" onserverclick="btnBack_ServerClick" runat="server">GO BACK</button>
            <asp:Label CssClass="AssyMachinepagelbl" Text="Machine Status" runat="server" />
            <button class="button" id="btnSaveStatus" type="button" onserverclick="btnSaveStatus_ServerClick" runat="server">SAVE STATUS</button>
        </div>
    </div>

    <div>
        <asp:GridView CssClass="AssyMachinepageGridView" Style="margin-bottom: 60px;"
            HeaderStyle-CssClass="AssyMachinepageGridHeader" RowStyle-CssClass="AssyMachinepageGridRow"
            ID="MachIDResults" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" PageSize="300">
            <Columns>
                <asp:BoundField DataField="BT" HeaderText="BT" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="MACHINE_NAME" HeaderText="MACHINE DESCRIPTION" ItemStyle-Wrap="true"></asp:BoundField>
                <asp:BoundField DataField="BT_STATUS" HeaderText="STATUS %" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="LAST_DATE" HeaderText="LAST DATE" ItemStyle-Wrap="false"></asp:BoundField>

                <asp:TemplateField HeaderText="HD">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxHD" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" PLC ">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxPLC" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" IHM ">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxIHM" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText=" CNC ">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxCNC" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="GAGE">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxGAGE" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="PRESS">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxPRESS" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="DRIVE">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxDRIVE" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ROBOT">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxROBOT" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CAMERA">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxCAMERA" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NUTRUNNER">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxNUTRUNNER" runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="40" Position="TopAndBottom" />
            <PagerStyle Font-Bold="True" Font-Size="18px" />
        </asp:GridView>
    </div>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

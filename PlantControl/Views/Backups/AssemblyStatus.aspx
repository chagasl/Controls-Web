<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="AssemblyStatus.aspx.cs" Inherits="PlantControl.Views.AssemblyStatus" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <%--<meta http-equiv="refresh" content="400">--%>
    <!-- Refresh every 400 seconds -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div>
        <asp:GridView CssClass="LinesBackupsStatusGridView" Style="margin-bottom: 60px;"
            HeaderStyle-CssClass="LinesBackupsStatusGridViewGridHeader" RowStyle-CssClass="LinesBackupsStatusGridViewGridRow"
            ID="AssemblyResults" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" PageSize="300">
            <Columns>
                <asp:BoundField DataField="CELL" HeaderText="ASSEMBLY LINE" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="MACH_STATUS" HeaderText="STATUS %" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton class="Linkbtn" ID="btnLink" Text="Select" runat="server" CommandArgument='<%# Eval("CELL") %>' OnClick="btnLink_Click" />
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

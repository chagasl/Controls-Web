<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="ViewRequests.aspx.cs" Inherits="PlantControl.Views.ViewRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
    <%--<meta http-equiv="refresh" content="400">--%>
    <!-- Refresh every 400 seconds -->

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div runat="server" class="viewrequestpage">
        <div class="btnGroup" style="padding-left: 4px">
            <button class="buttonComplete" id="btnCompleted" type="button" runat="server" onserverclick="btnCompleted_ServerClick">Completed</button>
            <button class="buttonOpened" id="btnOpened" type="button" runat="server" onserverclick="btnOpened_ServerClick">Opened</button>
            <button class="buttonOngoing" id="btnOnGoing" type="button" runat="server" onserverclick="btnOnGoing_ServerClick">Ongoing</button>
            <button class="buttonAllOrders" id="btnAllOrders" type="button" runat="server" onserverclick="btnAllOrders_ServerClick">All Orders</button>
            <button class="buttonFind" id="btnFind" type="button" runat="server" onserverclick="btnFind_ServerClick">Find</button>
            <input class="textBoxFilter" id="ControlsName" type="text" runat="server" />
            <input class="textBoxFilter" id="RequesterName" type="text" runat="server" />
            <input class="textBoxFilter" id="OSNum" type="number" runat="server" />
            <input class="textBoxFilter" id="Bt" type="number" runat="server" />
            <input class="textBoxQt" id="txtBox" type="number" value="1000" runat="server" />
        </div>
        <div class="lblGroup">
            <label style="margin-right: 40px; margin-left: 15px">Controls Name</label>
            <label style="margin-right: 47px">Requester Name</label>
            <label style="margin-right: 57px">OS Number</label>
            <label style="margin-right: 35px">Machine BT</label>
            <label>Quantity</label>
        </div>
    </div>
    <div>
        <asp:GridView CssClass="ViewGridView" HeaderStyle-CssClass="ViewGridViewHeader" RowStyle-CssClass="ViewGridViewRow" 
            ID="Viewresults" runat="server" AutoGenerateColumns="false" OnRowDataBound="Viewresults_RowDataBound" 
            AllowSorting="true" OnSorting="Viewresults_Sorting" >
            <Columns>
                <asp:BoundField DataField="OS_STATUS" HeaderText="Status" ItemStyle-Wrap="false" SortExpression="OS_STATUS" />
                <asp:BoundField DataField="PRIORITY" HeaderText="Priority" ItemStyle-Wrap="false" SortExpression ="PRIORITY" />
                <asp:BoundField DataField="REQ_NAME" HeaderText="Req. Name" ItemStyle-Wrap="true" SortExpression="REQ_NAME" />
                <asp:BoundField DataField="REQ_DEPARTMENT" HeaderText="Department" ItemStyle-Wrap="true" SortExpression="REQ_DEPARTMENT" />
                <asp:BoundField DataField="SUMMARY" HeaderText="Summary" ItemStyle-Wrap="true" />
                <asp:BoundField DataField="CELL" HeaderText="Cell" ItemStyle-Wrap="true" SortExpression="CELL" />
                <asp:BoundField DataField="MACHINE_NAME" HeaderText="Machine" ItemStyle-Wrap="true" SortExpression="MACHINE" />
                <asp:BoundField DataField="BT" HeaderText="BT" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="ID" HeaderText="O.S Number" ItemStyle-Wrap="false" SortExpression="ID" />
                <asp:BoundField DataField="OS_DATE" HeaderText="Created Date" DataFormatString="{0:dd-MM-yyyy HH:mm:ss}" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="PLANNED_DATE" HeaderText="Planning Date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Wrap="false" SortExpression="PLANNED_DATE" />
                <asp:BoundField DataField="END_DATE" HeaderText="End Date" DataFormatString="{0:dd-MM-yyyy}" ItemStyle-Wrap="false" SortExpression="END_DATE" />
                <asp:BoundField DataField="CONTROLS_NAME" HeaderText="Controls Name" ItemStyle-Wrap="false" SortExpression="CONTROLS_NAME" />
                <asp:TemplateField  HeaderText="Select">
                    <ItemTemplate>
                        <asp:LinkButton class="Linkbtn" ID="btnLink" Text="Select" runat="server" CommandArgument='<%# Eval("ID") %>' OnClick="btnLink_Click" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

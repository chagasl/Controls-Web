<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="FindRequest.aspx.cs" Inherits="PlantControl.Views.FindRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div class="findrequestpage" runat="server">
        <div class="lines">
            <label>OS number</label>
            <input type="number" id="OSnumber" style="width: 140px; text-align: center;" runat="server" />
            <label style="padding-left: 15px">Requester name</label>
            <input type="text" id="RequestName" style="width: 230px;" runat="server" />
            <label style="padding-left: 15px">Machine BT</label>
            <input type="text" id="BT" style="width: 220px;" runat="server" />
            <button id="btSend" type="button" runat="server" onserverclick="btSend_ServerClick">Find</button>
        </div>
    </div>
    <div>
        <asp:GridView CssClass="FindGridView" HeaderStyle-CssClass="FindGridViewHeader" RowStyle-CssClass="FindGridViewRow" ID="Findresults" runat="server" AutoGenerateColumns="false">
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="OS Number" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="REQ_NAME" HeaderText="Name" ItemStyle-Wrap="true" />
                <asp:BoundField DataField="REQ_DEPARTMENT" HeaderText="Department" ItemStyle-Wrap="true" />
                <asp:BoundField DataField="OS_DATE" HeaderText="Created Date" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="CELL" HeaderText="Cell" ItemStyle-Wrap="true" />
                <asp:BoundField DataField="MACHINE_NAME" HeaderText="Machine" ItemStyle-Wrap="true" />
                <asp:BoundField DataField="BT" HeaderText="BT" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="SUMMARY" HeaderText="Summary" ItemStyle-Wrap="true" />
                <asp:BoundField DataField="OS_STATUS" HeaderText="Status" ItemStyle-Wrap="false"/>
                <asp:BoundField DataField="END_DATE" HeaderText="Planning Date" ItemStyle-Wrap="false" />
            </Columns>
        </asp:GridView>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

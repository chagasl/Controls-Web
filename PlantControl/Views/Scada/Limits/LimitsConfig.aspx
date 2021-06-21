<%@ Page Title="" Language="C#" MasterPageFile="../../Web.Master" AutoEventWireup="true" CodeBehind="LimitsConfig.aspx.cs" Inherits="PlantControl.Views.LimitsConfig" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    
    <div runat="server" class="LimitsConfigpage">
        <div class="btnGroup" style="padding-left: 10px">
            <asp:DropDownList ID="serverSelect" class="ServerSelect" OnSelectedIndexChanged="serverSelect_SelectedIndexChanged" AutoPostBack="true" runat="server">
                <asp:ListItem Text="" Value="" />
                <asp:ListItem Text="FRONT AXLE" Value="FRONT AXLE" />
                <asp:ListItem Text="MAN 3RDM" Value="MAN 3RDM" />
                <asp:ListItem Text="MAN FINAL" Value="MAN FINAL" />
                <asp:ListItem Text="MAN WHUB_ASSY" Value="MAN WHUB" />
                <asp:ListItem Text="RPU_GM 3RDM" Value="RPU_GM 3RDM" />
                <asp:ListItem Text="RPU_GM FINAL" Value="RPU_GM FINAL" />
                <asp:ListItem Text="RPU_AXLE_SHAFT" Value="RPU_AXLE_SHAFT" />
                <asp:ListItem Text="BANJO WELD" Value="BANJO WELD" />
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
    <div>
        <asp:GridView CssClass="ConfigPageGridView" Style="margin-bottom: 60px;"
            HeaderStyle-CssClass="ConfigPageGridHeader" RowStyle-CssClass="ConfigPageGridRow"
            ID="charIDResults" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" PageSize="300">
            <Columns>
                <asp:BoundField DataField="CHAR_ID" HeaderText="CHAR ID" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="CHAR_DESC" HeaderText="CHAR DESCRIPTION" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:TemplateField HeaderText="ADD CHAR ID">
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxAdd" runat="server" />
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

<%@ Page Title="" Language="C#" MasterPageFile="../../Web.Master" AutoEventWireup="true" CodeBehind="LimitsReview.aspx.cs" Inherits="PlantControl.Views.LimitsReview" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div class="LimitsReviewPage" runat="server">
        <div class="btnGroup" style="padding-left: 10px">
            <select id="serverSelected" class="ServerSelect" runat="server">
                <option value="" style="display: none">Line Name</option>
                <option value=""></option>
                <option value="FRONT AXLE">Front Axle</option>
                <option value="MAN 3RDM">MAN 3rdM</option>
                <option value="MAN FINAL">MAN Final</option>
                <option value="MAN WHUB_ASSY">MAN WheelHub</option>
                <option value="MAN PROPSHAFT">MAN PropShaft</option>
                <option value="RPU_GM 3RDM">RPU 3rdM</option>
                <option value="RPU_GM FINAL">RPU Final</option>
                <option value="RPU_GM_SHAFT">RPU AxleShaft</option>
                <option value="BANJO WELD">Banjo Welding</option>
                <option value="CVJ ASSY">CV Joints</option>
            </select>
            <button class="button" id="BtnViewLimits" type="button" runat="server" onserverclick="BtnViewLimits_ServerClick">LIMITS</button>
            <button id="btnSaveStatus" style="margin-right: 10px;" class="button" type="button" onserverclick="btnSaveStatus_ServerClick" runat="server">SAVE</button>
            <label id="lblResults" style="font-size: 19px; font-weight: bold; color: blue; margin-left: 240px; padding-top: 7px" runat="server"></label>
        </div>
    </div>
    <div style="height: 5px"></div>
    <div id="limitsReviewFilter" class="LimitsReviewFilter" runat="server" visible="false">
        <asp:DropDownList ID="SelReviewed" CssClass="ServerSelect" runat="server" Style="width: 70px;" OnSelectedIndexChanged="SelReviewed_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelPartID" CssClass="ServerSelect" runat="server" Style="width: 70px;" OnSelectedIndexChanged="SelPartID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelParamID" CssClass="ServerSelect" runat="server" Style="width: 85px;" OnSelectedIndexChanged="SelParamID_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelParamDesc" CssClass="ServerSelect" runat="server" Style="width: 105px;" OnSelectedIndexChanged="SelParamDesc_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelLimType" CssClass="ServerSelect" runat="server" Style="width: 85px;" OnSelectedIndexChanged="SelLimType_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelHiLim" CssClass="ServerSelect" runat="server" Style="width: 68px;" OnSelectedIndexChanged="SelHiLim_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelLoLim" CssClass="ServerSelect" runat="server" Style="width: 68px;" OnSelectedIndexChanged="SelLoLim_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelTarg" CssClass="ServerSelect" runat="server" Style="width: 68px;" OnSelectedIndexChanged="SelTarg_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelTol" CssClass="ServerSelect" runat="server" Style="width: 68px;" OnSelectedIndexChanged="SelTol_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelText" CssClass="ServerSelect" runat="server" Style="width: 68px;" OnSelectedIndexChanged="SelText_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelStation" CssClass="ServerSelect" runat="server" Style="width: 90px;" OnSelectedIndexChanged="SelStation_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <asp:DropDownList ID="SelPartNum" CssClass="ServerSelect" runat="server" Style="width: 90px;" OnSelectedIndexChanged="SelPartNum_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
        <button id="btnRstFilter" class="button" type="button" onserverclick="btnRstFilter_ServerClick" runat="server">RESET FILTER</button>
    </div>
    <div>
        <asp:GridView CssClass="LimitsReviewPageGridView" Style="margin-bottom: 60px;"
            HeaderStyle-CssClass="LimitsReviewPageGridHeader" RowStyle-CssClass="LimitsReviewPageGridRow"
            ID="LimitsResults" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" OnPageIndexChanging="LimitsResults_PageIndexChanging" OnRowDataBound="LimitsResults_RowDataBound" PageSize="300">
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="chkHeader" Text="SELECT ALL" runat="server" OnCheckedChanged="chkHeader_CheckedChanged" AutoPostBack="true" />
                    </HeaderTemplate>
                    <ItemTemplate>
                        <%--<asp:CheckBox ID="checkBoxReview" runat="server" Checked='<%# Eval("REVIEWED") %>' BackColor='<%# ((Eval("REVIEWED").ToString().Equals("True"))?System.Drawing.Color.Green:System.Drawing.Color.Red) %>' />--%>
                        <asp:CheckBox ID="checkBoxReview" runat="server" Checked='<%# Eval("REVIEWED") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:BoundField DataField="PART_ID" HeaderText="PART ID" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PARAM_ID" HeaderText="PARAM ID" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PARAM_DESC" HeaderText="PARAM DESC." ItemStyle-Wrap="true"></asp:BoundField>
                <asp:BoundField DataField="LIMIT_TYPE" HeaderText="LIMIT TYPE" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="HI_LIM" HeaderText="HI_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="LO_LIM" HeaderText="LO_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="TARG" HeaderText="TARG" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="TOL" HeaderText="TOL" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="TEXT" HeaderText="TEXT" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="STN_NAME" HeaderText="STATION" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_NUM" HeaderText="PART NUM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_DESC" HeaderText="PART DESC." ItemStyle-Wrap="false"></asp:BoundField>


            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="40" Position="TopAndBottom" />
            <PagerStyle Font-Bold="True" Font-Size="18px" />
        </asp:GridView>

    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

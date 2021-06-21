<%@ Page Title="" Language="C#" MasterPageFile="../../Web.Master" AutoEventWireup="true" CodeBehind="LimitsCompare.aspx.cs" Inherits="PlantControl.Views.LimitsCompare" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    
    <div class="Limitspage" runat="server">
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
            <button class="button" id="btnUpdate" type="button" runat="server" onserverclick="btnUpdate_ServerClick">UPDATE DB</button>
            <button class="button" id="btnCompare" type="button" runat="server" onserverclick="btnCompare_ServerClick">COMPARE</button>
            <button class="button" id="btnCompareAll" type="button" runat="server" onserverclick="btnCompareAll_ServerClick">COMPARE ALL LINES</button>
            <button class="button" id="BtnViewLimits" type="button" runat="server" onserverclick="BtnViewLimits_ServerClick">LIMITS</button>
            <div class="lblGroup">
                <label id="lblResults" runat="server"></label>
            </div>
        </div>

    </div>
    <div>
        <asp:GridView CssClass="ComparepageGridView" Style="margin-bottom: 60px;"
            HeaderStyle-CssClass="ComparepageGridHeader" RowStyle-CssClass="ComparepageGridRow"
            ID="CompareResults" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" OnPageIndexChanging="CompareResults_PageIndexChanging" OnRowDataBound="CompareResults_RowDataBound" PageSize="300">
            <Columns>
                <asp:BoundField DataField="STATION" HeaderText="STATION" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_ID" HeaderText="PART ID" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_NUM" HeaderText="PART NUM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_DESC" HeaderText="PART DESC." ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PARAM_DESC" HeaderText="PARAM DESC." ItemStyle-Wrap="true"></asp:BoundField>
                <asp:BoundField DataField="TYPE" HeaderText="LIMIT TYPE" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="OLD_HI" HeaderText="OLD HI_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="OLD_LOW" HeaderText="OLD LO_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="OLD_TARG" HeaderText="OLD TARG" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="OLD_TOL" HeaderText="OLD TOL" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="OLD_TEXT" HeaderText="OLD TEXT" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="NEW_HI" HeaderText="NEW HI_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="NEW_LOW" HeaderText="NEW LO_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="NEW_TARG" HeaderText="NEW TARG" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="NEW_TOL" HeaderText="NEW TOL" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="NEW_TEXT" HeaderText="NEW TEXT" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PARAM_ID" HeaderText="PARAM ID" ItemStyle-Wrap="false"></asp:BoundField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="40" Position="TopAndBottom" />
            <PagerStyle Font-Bold="True" Font-Size="18px" />
        </asp:GridView>

        <asp:GridView CssClass="ComparepageGridView" Style="margin-bottom: 60px;"
            HeaderStyle-CssClass="ComparepageGridHeader" RowStyle-CssClass="ComparepageGridRow"
            ID="LimitsResults" runat="server" AutoGenerateColumns="false" AllowPaging="true"
            AllowSorting="true" OnPageIndexChanging="LimitsResults_PageIndexChanging" PageSize="300">
            <Columns>
                <asp:BoundField DataField="STN_NAME" HeaderText="STATION" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_ID" HeaderText="PART ID" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_NUM" HeaderText="PART NUM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PART_DESC" HeaderText="PART DESC." ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PARAM_DESC" HeaderText="PARAM DESC." ItemStyle-Wrap="true"></asp:BoundField>
                <asp:BoundField DataField="LIMIT_TYPE" HeaderText="LIMIT TYPE" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="HI_LIM" HeaderText="HI_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="LO_LIM" HeaderText="LO_LIM" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="TARG" HeaderText="TARG" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="TOL" HeaderText="TOL" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="TEXT" HeaderText="TEXT" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="PARAM_ID" HeaderText="PARAM ID" ItemStyle-Wrap="false"></asp:BoundField>
            </Columns>
            <PagerSettings FirstPageText="First" LastPageText="Last" Mode="NumericFirstLast" PageButtonCount="40" Position="TopAndBottom" />
            <PagerStyle Font-Bold="True" Font-Size="18px" />
        </asp:GridView>

        <asp:GridView CssClass="ComparepageGridView" Style="margin-bottom: 60px;"
            HeaderStyle-CssClass="ComparepageGridHeader" RowStyle-CssClass="ComparepageGridRow"
            ID="CompareResultsAll" runat="server" AutoGenerateColumns="false" AllowSorting="true" PageSize="300">
            <Columns>
                <asp:BoundField DataField="ASSY_LINE" HeaderText="ASSEMBLY LINE" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:BoundField DataField="DIFFERENCES" HeaderText="DIFFERENCES FOUND ON DATA BASE" ItemStyle-Wrap="false"></asp:BoundField>
                <asp:TemplateField>
                    <ItemTemplate>
                        <asp:LinkButton class="Linkbtn" ID="btnLink" Text="Select" runat="server" CommandArgument='<%# Eval("ASSY_LINE") %>' OnClick="btnLink_Click" />
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

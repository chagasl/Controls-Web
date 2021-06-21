<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="NewRequest.aspx.cs" Inherits="PlantControl.Views.NewRequest" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div runat="server" class="workrequestpage">
        <div class="lines">
            <label>OS number</label>
            <input type="text" id="txtOSNum" style="width: 120px; height: 19px; text-align: center; left: 0px" readonly runat="server" />
            <label style="padding-left: 38px">Your name</label>
            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtReqName" Style="color: red; font-size: 19px" runat="server" />
            <input type="text" id="txtReqName" style="width: 270px; height: 19px" placeholder="name" runat="server" />
            <label style="padding-left: 81px">Your department</label>
            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="SelReqDep" Style="color: red; font-size: 19px;" runat="server" />
            <select id="SelReqDep" style="width: 260px; font-size: 18px; font-weight: normal; float: right" runat="server">
                <option value=""></option>
                <option value="Engenharia">Engenharia</option>
                <option value="Facilities">Facilities</option>
                <option value="Manutenção">Manutenção</option>
                <option value="Produção">Produção</option>
                <option value="Logística">Logística</option>
                <option value="Qualidade">Qualidade</option>
                <option value="Laboratório Met.">Laboratório Met.</option>
                <option value="T.I">T.I</option>
                <option value="Afiação">Afiação</option>
                <option value="Outro">Outro</option>
            </select>
        </div>
        <div class="lines">
            <label>Cell</label>
            <asp:DropDownList ID="SelectionCell" runat="server" Style="width: 200px; height: 23px" OnSelectedIndexChanged="SelectionCell_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <label style="padding-left: 35px">Machine BT</label>
            <asp:DropDownList ID="SelectionBT" runat="server" Style="width: 160px; height: 23px" OnSelectedIndexChanged="SelectionBT_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <label style="padding-left: 35px">Machine</label>
            <input type="text" id="txtMachine" runat="server" style="width: 470px; height: 19px; float: right;" placeholder="machine" readonly />
        </div>
        <div class="lines">
            <label>Current date</label>
            <input type="text" id="txtDateOS" style="width: 185px; text-align: center;" readonly runat="server" />
            <label style="padding-left: 90px">Work summary</label>
            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtSummary" Style="color: red; font-size: 19px" runat="server" />
            <input type="text" maxlength="80" id="txtSummary" style="width: 590px; height: 19px; float: right;" placeholder="work summary" runat="server" />
        </div>
        <div class="lines">
            <label>Description:</label>
            <asp:RequiredFieldValidator ErrorMessage="*" ControlToValidate="txtDesc" Style="color: red; font-size: 19px" runat="server" />
        </div>
        <div class="lines">
            <textarea maxlength="600" id="txtDesc" style="width: 100%; height: 140px; vertical-align: top; font-size: 17px; resize: none" runat="server"></textarea>
        </div>
        <div class="lines">
            <button id="btnNew" type="button" causesvalidation="false"  runat="server" onserverclick="btnNew_ServerClick">New</button>
            <button id="btSend" type="button" runat="server" style="float: right; margin-right: -5px" onserverclick="btSend_ServerClick">Send</button>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

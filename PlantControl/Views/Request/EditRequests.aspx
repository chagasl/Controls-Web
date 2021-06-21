<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="EditRequests.aspx.cs" Inherits="PlantControl.Views.EditRequests" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
    <div runat="server" id="BackPanel" class="editrequestpage">
        <div class="lines">
            <label>OS number</label>
            <input type="text" id="txtOSNum" style="width: 120px; text-align: center; left: 0px" readonly runat="server" />
            <label style="padding-left: 15px">Name</label>
            <input type="text" id="txtReqName" style="width: 370px;" readonly runat="server" />
            <label style="padding-left: 15px">Department</label>
            <input type="text" id="txtReqDep" style="width: 310px; float: right" readonly runat="server" />
        </div>
        <div class="lines">
            <label>Machine</label>
            <input type="text" id="txtMachine" readonly runat="server" style="width: 350px;" />
            <label style="padding-left: 45px">Cell</label>
            <input type="text" id="txtCell" readonly runat="server" style="width: 215px;" placeholder="cell" />
            <label style="padding-left: 55px">Machine BT</label>
            <input type="number" id="txtBT" style="width: 215px; float: right;" runat="server" readonly />
        </div>
        <div class="lines">
            <label>Created date</label>
            <input type="text" id="txtDateOS" style="width: 185px; text-align: center;" readonly runat="server" />
            <label style="padding-left: 15px">Work summary</label>
            <input type="text" maxlength="60" id="txtSummary" style="width: 650px; float: right;" readonly runat="server" />
        </div>
        <div class="lines">
            <label>Controls Name</label>
            <select id="SelectControlsName" style="width: 150px; font-size: 17px; text-align: center;" runat="server">
                <option value=""></option>
                <option value="Leandro">Leandro</option>
                <option value="Karsten">Karsten</option>
                <option value="Iago">Iago</option>
            </select>
            <label style="padding-left: 15px">Status</label>
            <asp:TextBox ID="txtStatus" Style="width: 110px; text-align: center;" runat="server" OnTextChanged="status_TextChanged" AutoPostBack="true" />
            <label style="padding-left: 15px">I.T</label>
            <input type="checkbox" id="chboxIT" style="width: 18px; height: 18px; vertical-align: bottom" runat="server" />
            <label style="padding-left: 15px">Maintenance</label>
            <input type="checkbox" id="chboxManut" style="width: 18px; height: 18px; vertical-align: bottom" runat="server" />
            <label style="padding-left: 15px">Production</label>
            <input type="checkbox" id="chboxProd" style="width: 18px; height: 18px; vertical-align: bottom" runat="server" />
            <label style="padding-left: 15px">Project</label>
            <input type="checkbox" id="chboxProj" style="width: 18px; height: 18px; vertical-align: bottom" runat="server" />
            <label style="padding-left: 15px">Eng.</label>
            <input type="checkbox" id="chboxEng" style="width: 18px; height: 18px; vertical-align: bottom" runat="server" />
            <label style="padding-left: 25px; color: red">Priority</label>
            <input type="number" id="priorityNum" style="width: 50px; height: 18px; float: right; text-align: center" runat="server" />
        </div>
        <div class="lines">
            <label>Planning Date</label>
            <input type="date" id="txtPlanningDateOS" style="width: 138px; text-align: center;" runat="server" />
            <label style="padding-left: 63px">Start Date</label>
            <input type="date" id="txtStartDateOS" style="width: 138px; text-align: center;" runat="server" />
            <input type="time" id="txtStartTimeOS" style="width: 138px; text-align: center;" runat="server" />
            <label style="padding-left: 64px">End Date</label>
            <input type="date" id="txtEndDate" style="width: 138px; text-align: center;" runat="server" />
            <input type="time" id="txtEndTime" style="width: 138px; text-align: center; float: right" runat="server" />

        </div>
        <div class="lines">
            <label>Description:</label>
        </div>
        <div class="lines">
            <textarea id="txtDesc" style="width: 100%; height: 80px; vertical-align: top; font-size: 17px; resize: none" readonly runat="server"></textarea>
        </div>
        <div class="lines">
            <label>Work:</label>
        </div>
        <div class="lines">
            <textarea maxlength="600" id="txtWorkDone" style="width: 100%; height: 90px; vertical-align: top; font-size: 17px; resize: none" runat="server" onchange="txtWorkDone_change"></textarea>
        </div>
        <div class="lines">
            <button id="btSave" type="button" runat="server" onserverclick="btSave_ServerClick">Save</button>
            <asp:Label class="statusLbl" ID="statusLbl" runat="server" AutoPostBack="true" />
            <button id="btnBack" type="button" style="float:right; margin-right:-5px" runat="server" onserverclick="btnBack_ServerClick">Back</button>
        </div>
    </div>
    <script>
        $(function () {
            $('#datetimepicker1').datetimepicker();
        });
    </script>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

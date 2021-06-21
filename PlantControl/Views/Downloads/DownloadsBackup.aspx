<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="DownloadsBackup.aspx.cs" Inherits="PlantControl.Views.DownloadsBackup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div runat="server" class="DownloadPage">
        <div class="btnGroup" style="padding-left: 15px">
            <asp:DropDownList ID="SelectionCellType" CssClass="ServerSelect" runat="server" Style="width: 120px;" OnSelectedIndexChanged="SelectionCellType_SelectedIndexChanged" AutoPostBack="true">
                <asp:ListItem Text="SELECT" Value="" />
                <asp:ListItem Text="ASSEMBLY" Value="ASSEMBLY" />
                <asp:ListItem Text="MACHINING" Value="MACHINING" />
            </asp:DropDownList>
            <asp:DropDownList ID="SelectionCell" CssClass="ServerSelect" runat="server" Style="width: 200px;" OnSelectedIndexChanged="SelectionCell_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:DropDownList ID="SelectionBTDesc" CssClass="ServerSelect" runat="server" Style="width: 320px;" OnSelectedIndexChanged="SelectionBTDesc_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
            <asp:TextBox ID="txtBT" CssClass="textBox" runat="server" Style="width: 90px; height: 31px" ReadOnly="true" />
            <button id="btnBack" style="margin-left: 15px" class="button" type="button" onserverclick="btnBack_ServerClick" runat="server">BACK</button>
            <button id="btnSaveFiles" style="float: right; margin-right: 10px;" class="button" type="button" onserverclick="btnSaveFiles_ServerClick" runat="server">DOWNLOAD</button>
        </div>
    </div>

    <asp:GridView CssClass="DownloadPageGridView" Style="margin-bottom: 60px;"
        HeaderStyle-CssClass="DownloadPageGridHeader" RowStyle-CssClass="DownloadPageGridRow"
        ID="AssemblyFiles" runat="server" AutoGenerateColumns="false">
        <Columns>
            <asp:TemplateField HeaderText="SELECT">
                <ItemTemplate>
                    <asp:CheckBox ID="chkSelect" Visible='<%# Eval("chkVisible") %>' runat="server" />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="NAME">
                <ItemTemplate>
                    <asp:Label ID="lblFileName" runat="server" Text='<%# Eval("FileName") %>' />
                    <asp:Label ID="lblFilePath" Visible="false" runat="server" Text='<%# Eval("FilePath") %>' />
                </ItemTemplate>
            </asp:TemplateField>

               <asp:TemplateField HeaderText="SIZE">
                <ItemTemplate>
                    <asp:Label ID="lblFileSize" runat="server" Text='<%# Eval("FileSize") %>' />
                </ItemTemplate>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="TYPE">
                <ItemTemplate>
                    <asp:Label ID="lblFileFolder" runat="server" Text='<%# Convert.ToBoolean(Eval("FileFolder")) == true ? "Folder" : Convert.ToBoolean(Eval("FileFolder")) == false ? "File" : "Unknown"  %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="OPEN">
                <ItemTemplate>
                    <asp:LinkButton class="Linkbtn" ID="btnLink" Text="OPEN FOLDER" Visible='<%# Eval("LinkbtnVisible") %>' runat="server" CommandArgument='<%# Eval("FilePath") %>' OnClick="btnLink_Click" />
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

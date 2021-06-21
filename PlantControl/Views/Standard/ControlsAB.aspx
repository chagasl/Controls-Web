<%@ Page Title="" Language="C#" MasterPageFile="../Web.Master" AutoEventWireup="true" CodeBehind="ControlsAB.aspx.cs" Inherits="PlantControl.Views.ControlsAB" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">
  
    <div class="Standardlines">
        <label class="Stdlabel">American Axle Standard for - ESM type A/B</label>
        <button id="btnESM" class="Stdbtn" type="button" style="cursor: pointer" onserverclick="btnESM_ServerClick" runat="server">ESM</button>
    </div>
   
    <div id="id05" class="StandardPopUp" runat="server">
        <div method="post" class="content" action="#" runat="server">
            <div class="divLabel">
                <label id="labelType" runat="server"><b></b></label>
            </div>
            <div id="PLCContent2" class="Gridcontainer">
                <asp:GridView CssClass="GridView" Style="margin-bottom: 5px;"
                    HeaderStyle-CssClass="GridHeader" RowStyle-CssClass="GridRow"
                    ID="StandardFiles" runat="server" AutoGenerateColumns="false">
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
                    </Columns>
                </asp:GridView>

                <div class="divSave">
                    <button id="btnSave" class="btnSave" type="button" onserverclick="btnSave_ServerClick" runat="server">Download</button>
                </div>
            </div>

            <div class="divCancel">
                <button id="btnCancel" class="btnCancel" type="button" onserverclick="btnCancel_ServerClick" runat="server">Cancel</button>
            </div>
        </div>
    </div>
    <script type="text/javascript">
        // Get the modal
        var modal5 = document.getElementById('id05');

        // When the user clicks anywhere outside of the modal, close it
        window.onclick = function (event) {
            if (event.target == modal5) {
                modal5.style.display = "none";
            }
        }
    </script>

</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

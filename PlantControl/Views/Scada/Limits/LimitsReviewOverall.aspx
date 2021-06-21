<%@ Page Title="" Language="C#" MasterPageFile="../../Web.Master" AutoEventWireup="true" CodeBehind="LimitsReviewOverall.aspx.cs" Inherits="PlantControl.Views.LimitsReviewOverall" %>

<asp:Content ID="Content1" ContentPlaceHolderID="Header" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="Main" runat="server">

    <div class="limitsReviewMainDiv">

        <div class="OverAllDiv">
            <div style="margin-left: 0px; margin-right: 0px; width: 290px">
                <asp:Label ID="AllChart" runat="server" />
            </div>
        </div>

        <div class="SecondAllDiv">

            <div class="FirstLineDiv">
                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart1" runat="server" />
                    </div>
                </div>

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart2" runat="server" />
                    </div>
                </div>

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart3" runat="server" />
                    </div>
                </div>

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart4" runat="server" />
                    </div>
                </div>

            </div>

            <hr class="solid" />

            <div class="SecondLineDiv">
                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart5" runat="server" />
                    </div>
                </div>

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart6" runat="server" />
                    </div>
                </div>

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart7" runat="server" />
                    </div>
                </div>

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart8" runat="server" />
                    </div>
                </div>
            </div>

            <hr class="solid" />

            <div class="ThirdLineDiv">

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart9" runat="server" />
                    </div>
                </div>

                <div class="oneLine">
                    <div style="margin-left: 0px; margin-right: 0px; width: 235px">
                        <asp:Label ID="OneChart10" runat="server" />
                    </div>
                </div>
            </div>

        </div>
    </div>




    <script src="../../../Scripts/highchart/highcharts.js"></script>
    <script src="../../../Scripts/highchart/modules/solid-gauge.js"></script>
    <script src="../../../Scripts/highchart/highcharts-more.js"></script>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="Footer" runat="server">
</asp:Content>

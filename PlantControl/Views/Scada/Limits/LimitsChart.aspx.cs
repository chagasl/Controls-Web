using System;
using System.Drawing;
using System.Web.UI;
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;

namespace PlantControl.Views
{
    public partial class LimitsChart : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "LimitsChart");
            if (authorizated)
            {
                Highcharts chart = new Highcharts("chart")
                .InitChart(new Chart { Type = ChartTypes.Scatter, ZoomType = ZoomTypes.Xy })
                .SetTitle(new Title { Text = "Line Selected" })
                .SetSubtitle(new Subtitle { Text = "Assembly Line" })
                .SetYAxis(new YAxis { Title = new YAxisTitle { Text = "Results" } })
                .SetXAxis(new XAxis
                {
                    Title = new XAxisTitle { Text = "Timeline" },
                    StartOnTick = true,
                    EndOnTick = true,
                    ShowLastLabel = true
                })
                .SetTooltip(new Tooltip { Formatter = "function() {return ''+ this.x +' cm, '+ this.y +' kg'; }" })
                .SetLegend(new Legend
                {
                    Layout = Layouts.Vertical,
                    Align = HorizontalAligns.Left,
                    VerticalAlign = VerticalAligns.Top,
                    X = 100,
                    Y = 50,
                    Floating = true,
                    BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFF")),
                    BorderWidth = 1
                })
                .SetPlotOptions(new PlotOptions
                {
                    Scatter = new PlotOptionsScatter
                    {
                        Marker = new PlotOptionsScatterMarker
                        {
                            Radius = 5,
                            States = new PlotOptionsScatterMarkerStates
                            {
                                Hover = new PlotOptionsScatterMarkerStatesHover
                                {
                                    Enabled = true,
                                    LineColor = ColorTranslator.FromHtml("#646464")
                                }
                            }
                        },
                        States = new PlotOptionsScatterStates { Hover = new PlotOptionsScatterStatesHover { Enabled = false } }
                    }
                })
                .SetSeries(new[] { ChartsData.Female, ChartsData.Male });

                ltrChart.Text = chart.ToHtmlString();
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }             
        }      
        protected void serverSelect_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblResults.InnerText = "";
            SQLQuery sQLQuery = new SQLQuery();

            stationSelect.DataSource = sQLQuery.GetStationName(serverSelect.Text);
            stationSelect.DataTextField = "STN_NAME";
            stationSelect.DataValueField = "STN_NAME";
            stationSelect.DataBind();
        }
            
    }
}
using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Drawing;
using Point = DotNet.Highcharts.Options.Point;

namespace PlantControl.Views
{
    public partial class LimitsPizza : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "LimitsPizza");
            if (authorizated)
            {
                Highcharts chart = new Highcharts("chart")
                                .InitChart(new Chart
                                {
                                    Type = ChartTypes.Pie,
                                    MarginTop = 0,
                                    MarginRight = 0,
                                    MarginBottom = 0,
                                    MarginLeft = 0,
                                    Height = 530,
                                    Options3d = new ChartOptions3d
                                    {
                                        Enabled = true,
                                        Alpha = 45,
                                        Beta = 0
                                    }
                                })
                                .SetTitle(new Title { Text = "SCADA LIMITS CHARACTERISTICS" })
                                .SetTooltip(new Tooltip { PointFormat = "<b>{point.percentage:.1f} %</b>" })
                                .SetPlotOptions(new PlotOptions
                                {
                                    Pie = new PlotOptionsPie
                                    {
                                        Size = new PercentageOrPixel(100, true),
                                        AllowPointSelect = true,
                                        Cursor = Cursors.Pointer,
                                        Depth = 70,
                                        DataLabels = new PlotOptionsPieDataLabels
                                        {
                                            Color = ColorTranslator.FromHtml("#000000"),
                                            ConnectorColor = ColorTranslator.FromHtml("#000000"),
                                            Format = "<b>{point.name}</b>: {point.percentage:.1f} %",
                                        }
                                    },
                                    Series = new PlotOptionsSeries
                                    {
                                        Point = new PlotOptionsSeriesPoint
                                        {
                                            Events = new PlotOptionsSeriesPointEvents
                                            {
                                                Click = @"function() {location.href = this.id;}"
                                            }
                                        },
                                    }
                                })
                                .SetSeries(new Series
                                {
                                    Type = ChartTypes.Pie,
                                    Name = "Scada Limits",
                                    Data = new Data(new object[]
                                    {
                        new Point{Id="GM_3rdM.aspx", Name = "3rdM GM", Y = 2649, Sliced = false,Selected = false },
                        new Point{Id="GM_Final.aspx", Name = "Final RPU/GM", Y = 12498, Sliced = false,Selected = false },
                        new Point{Id="GM_Shaft.aspx", Name = "Shaft RPU/GM", Y = 671, Sliced = false,Selected = false},
                        new Point{Id="Banjo.aspx", Name = "Banjo Welding", Y = 98, Sliced = false,Selected = false},
                        new Point{Id="FrontAxle.aspx", Name = "Front Axle", Y = 2359, Sliced = false,Selected = false},
                        new Point{Id="MAN_3rdM.aspx", Name = "3rdM MAN", Y = 2158, Sliced = false,Selected = false},
                        new Point{Id="MAN_Final.aspx", Name = "Final MAN", Y = 364, Sliced = false,Selected = false},
                        new Point{Id="MAN_HUB.aspx", Name = "Wheel Hub MAN", Y = 65, Sliced = false,Selected = false},
                        new Point{Id="MAN_PropShaft.aspx",Name = "PropShaft MAN", Y = 2045, Sliced = false,Selected = false},
                                    })
                                });

                ltrChart.Text = chart.ToHtmlString();
            }
            else
            {
                Response.Redirect("/Views/Index.aspx");
            }                
        }
    }
}
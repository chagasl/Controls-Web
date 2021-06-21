using DotNet.Highcharts;
using DotNet.Highcharts.Enums;
using DotNet.Highcharts.Helpers;
using DotNet.Highcharts.Options;
using System;
using System.Drawing;

namespace PlantControl.Views
{
    public partial class LimitsOverview : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Encryption userAuthorization = new Encryption();

            Web masterPage = (Web)Page.Master;
            int role = userAuthorization.ReadCookieRole();
            bool authorizated = masterPage.DefineUserRights(role, "LimitsOverview");
            if (authorizated)
            {
                Highcharts chart = new Highcharts("chart")
                             .InitChart(new Chart { Type = ChartTypes.Column })
                             .SetTitle(new Title { Text = "Limits Overall" })
                             .SetXAxis(new XAxis
                             {
                                 Title = new XAxisTitle
                                 {
                                     Text = "Assembly Line",
                                     Style = "fontWeight:'regular', fontSize: '15px'"
                                 },
                                 Categories = new[] { "3rdM GM", "Final GM", "Shaft GM", "Front Axle", "Banjo", "3rdM MAN", "Final MAN", "Wheel Hub MAN", "PropShaft MAN"
                                 }
                             })
                             .SetYAxis(new YAxis
                             {
                                 Min = 0,
                                 Title = new YAxisTitle
                                 {
                                     Text = "Total Characteristics",
                                     Style = "fontWeight:'regular', fontSize: '15px'"
                                 },
                                 StackLabels = new YAxisStackLabels
                                 {
                                     Enabled = true,
                                     Style = "fontWeight:'bold', color: (Highcharts.theme && Highcharts.theme.textColor) || 'gray'"
                                 }
                             })
                             .SetLegend(new Legend
                             {
                                 Layout = Layouts.Vertical,
                                 Align = HorizontalAligns.Left,
                                 VerticalAlign = VerticalAligns.Bottom,
                                //X = 100,
                                //Y = 0,
                                Floating = true,
                                 BackgroundColor = new BackColorOrGradient(ColorTranslator.FromHtml("#FFFFFF")),
                                 Shadow = true
                             })
                             .SetTooltip(new Tooltip { Formatter = @"function() { return ''+ this.x +': '+ this.y +' mm'; }" })
                             .SetPlotOptions(new PlotOptions
                             {
                                 Column = new PlotOptionsColumn
                                 {
                                     PointPadding = 0.2,
                                     BorderWidth = 5,
                                     DataLabels = new PlotOptionsColumnDataLabels
                                     {
                                         Enabled = true,
                                         Color = Color.Black
                                     }
                                 },
                                 Series = new PlotOptionsSeries
                                 {
                                     Point = new PlotOptionsSeriesPoint
                                     {
                                         Events = new PlotOptionsSeriesPointEvents
                                         {
                                            //Click = @"function() {
                                            //location.href = this.id;}"
                                        }
                                     },
                                 },
                             })
                             .SetSeries(new[]
                             {
                                // "3rdM GM", "Final GM", "Shaft GM", "Front Axle", "Banjo", "3rdM MAN", "Final MAN", "Wheel Hub MAN", "PropShaft MAN"
                                new Series { Name = "Total Char", Data = new Data(new object[] {2649, 12498, 671, 2359, 98, 2158, 364, 65, 2045})},
                                new Series { Name = "To correct", Data = new Data(new object[] {90, 9, 0, 0, 0, 0, 3, 0, 0 })},
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